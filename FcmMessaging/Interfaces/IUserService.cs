using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Utilities.Models;

namespace FcmMessaging.Interfaces;

public interface IUserService
{
    Task<ResponseResult<List<UserDto>>> GetUsers();
    Task<ResponseResult<UserDto>> GetUser(Guid id);
    Task<ResponseResult<UserDto>> Login(LoginRequest request);
    Task<ResponseResult<UserDto>> RegisterUser(UserRequest request);
}