using DAL.DTO.Req;
using DAL.DTO.Rest;
using DAL.Models;
using DAL.Repositoris.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoris.Service
{
    public class UserService : IUserService
    {

        public readonly CustomerContext _context;

        public UserService(CustomerContext context)
        {
            _context = context; 
        }

        public async Task<List<ResUserDto>> GetAllUser()
        {
            return await _context.MstUser2
                .Where(user => user.Role != "admin")
                .Select(user => new ResUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    Role = user.Role,
                    Balance = user.Balance,
                }).ToListAsync();
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

    }
}
