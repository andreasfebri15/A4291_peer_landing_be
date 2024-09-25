using DAL.DTO.Req;
using DAL.DTO.Rest;
using DAL.Models;
using DAL.Repositoris.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoris.Service
{

    public class UserService : IUserService
    {

        private readonly CustomerContext _context;
        private readonly IConfiguration _configuration;

        public UserService(CustomerContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<List<ResUserDto>> GetAllUsers()
        {
            return await _context.MstUser2.Select(
                user => new ResUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Balance = user.Balance
                }
            ).Where(ResBaseDto => ResBaseDto.Role != "admin").ToListAsync();
        }


        public async Task<ResUserDto> GetUserById(string id)
        {
            var user = await _context.MstUser2.FindAsync(id);
            if (user == null) return null;

            return new ResUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Balance = user.Balance
            };
        }

        public async Task<RestLoginDto> Login(ReqLoginDto reqLogin )
        {
            var user = await _context.MstUser2.SingleOrDefaultAsync(e => e.Email == reqLogin.Email);
            if(user == null)
            {
                throw new Exception("Invalid email or Password");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(reqLogin.Password, user.Password);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid email or Password");
            }

            var token = GenerateJwtToken(user);

            var loginResponse = new RestLoginDto
            {
                Token = token
            };

            return loginResponse;
        }

        public string GenerateJwtToken(MstUser2 user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey =jwtSettings["SecreteKey"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
{
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Validissuer"],
                audience: jwtSettings["ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }





        public async Task<string> Register(ReqRegisterUserDTO register)
        {
            var isAnyEmail = await _context.MstUser2.SingleOrDefaultAsync(e => e.Email == register.Email);
            if (isAnyEmail != null)
            {
                throw new Exception("Email already used");
            }

            var newUser = new MstUser2
            {
                Name = register.Name,
                Email = register.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(register.Password),
                Role = register.Role,
                Balance = register.Balance,
            };

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUser.Name;

        }

        public async Task DeleteUserById(string id)
        {
            await _context.MstUser2.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<ResUserDto> UpdateUserById(string id, ReqEditDto dto)
        {
            var user = await _context.MstUser2.SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("Not Found");
            }
            user.Name = dto.Name;
            user.Role = dto.Role;
            user.Balance = dto.Balance ?? user.Balance;
            _context.MstUser2.Update(user);
            await _context.SaveChangesAsync();
            return new ResUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Balance = user.Balance
            };
        }

    }
}
