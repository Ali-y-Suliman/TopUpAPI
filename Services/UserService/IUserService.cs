using TopUpAPI.Dto;
using TopUpAPI.Models;

namespace TopUpAPI.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseModel<GetUserDto>> AddUserAsync(AddUserDto user);
        Task<ResponseModel<User>> GetUserAsync(int id);
        Task<ResponseModel<User>> GetUserByEmailAsync(string email);
    }
}