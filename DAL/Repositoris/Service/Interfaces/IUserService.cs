using DAL.DTO.Req;
using DAL.DTO.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositoris.Service.Interfaces
{
    public interface IUserService
    {
        Task<string> Register(ReqRegisterUserDTO register);
        Task<List<ResUserDto>> GetAllUsers();

        Task<ResUserDto> GetUserById(string id);

        Task<RestLoginDto> Login(ReqLoginDto reqLogin);

        Task DeleteUserById(string id);
        Task<ResUserDto> UpdateUserById(string id, ReqEditDto user);
    }
}
