using FcmMessaging.Infrastructure.Persistence.Entities;
using FcmMessaging.Interfaces;
using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;
using Utilities.Models;

namespace FcmMessaging.Api.Controllers;

public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<ResponseResult<List<UserDto>>>> GetAllUsers()
    {
        var response = await _userService.GetUsers();

        return ActionResultHelper.MapResponseByStatusCode(response);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseResult<UserDto>>> GetAllUser(Guid id)
    {
        var response = await _userService.GetUser(id);

        return ActionResultHelper.MapResponseByStatusCode(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseResult<UserDto>>> RegisterUser(UserRequest request)
    {
        var response = await _userService.RegisterUser(request);

        return ActionResultHelper.MapResponseByStatusCode(response);
    }
}