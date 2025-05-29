using AutoMapper;
using AutoMapper.QueryableExtensions;
using FcmMessaging.Infrastructure.Persistence.Sql;
using FcmMessaging.Interfaces;
using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Models;

namespace FcmMessaging.Services;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(ILogger<UserService> logger, ApplicationDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseResult<List<UserDto>>> GetUsers()
    {
        var list = await _context.Users.ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync();
        
        return ResponseResult<List<UserDto>>.Success(list);
    }

    public async Task<ResponseResult<UserDto>> GetUser(Guid id)
    {
        var user = await _context.Users.Include(x => x.Devices).ProjectTo<UserDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(x => x.Id == id);
        if(null == user)
            return ResponseResult<UserDto>.Failure("User not found");
        
        return ResponseResult<UserDto>.Success(user);
    }

    public async Task<ResponseResult<UserDto>> Login(LoginRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseResult<UserDto>> RegisterUser(UserRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (null != user)
        {
            return ResponseResult<UserDto>.Failure("User is already registered");
        }
        
        user = request.ToUser();
        user.Devices.Add(request.GetDevice());
        
        await _context.Users.AddAsync(user);
        
        await _context.SaveChangesAsync();
        
        var dto = _mapper.Map<UserDto>(user);
        
        return ResponseResult<UserDto>.Success("Account created", dto);
    }
}