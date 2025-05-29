using AutoMapper;
using AutoMapper.QueryableExtensions;
using FcmMessaging.Infrastructure.Models;
using FcmMessaging.Infrastructure.Persistence.Sql;
using FcmMessaging.Infrastructure.Service;
using FcmMessaging.Interfaces;
using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Utilities.Models;

namespace FcmMessaging.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<INotificationService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IFcmMessageService _fcmMessageService;
    private readonly IMapper _mapper;

    public NotificationService(ILogger<INotificationService> logger, ApplicationDbContext context,
        IFcmMessageService fcmMessageService, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _fcmMessageService = fcmMessageService;
        _mapper = mapper;
    }


    public async Task<ResponseResult<List<MessageDto>>> GetAllCampaignMessages()
    {
        var messages = await _context.Messages.Where(x => x.UserId != null)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();

        return ResponseResult<List<MessageDto>>.Success(messages);
    }

    public async Task<ResponseResult<MessageDto>> GetCampaignMessage(Guid id)
    {
        var message = await _context.Messages.Where(x => x.Id == id)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        
        if(message == null)
            ResponseResult<MessageDto>.Failure("Message not found");
        
        return ResponseResult<MessageDto>.Success(message);
    }

    public async Task<ResponseResult<List<MessageDto>>> GetUserMessages(Guid id)
    {
        var messages = await _context.Messages.Where(x => x.UserId == id)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider).ToListAsync();

        return ResponseResult<List<MessageDto>>.Success(messages);
    }

    public async Task<ResponseResult<MessageDto>> GetUserMessage(Guid id, Guid messageId)
    {
        var message = await _context.Messages.Where(x => x.Id == id && x.UserId == messageId)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        
        if(message == null)
            ResponseResult<MessageDto>.Failure("Message not found");
        
        return ResponseResult<MessageDto>.Success(message);
    }

    public async Task<ResponseResult<MessageDto>> SendCampaignMessageAsync(CampaignMessageRequest request)
    {
        var users = await _context.Users.Include(x => x.Devices).ToListAsync();
        foreach (var user in users)
        {
            var devices = user.Devices.ToList();
            foreach (var device in devices)
            {
                var response = await _fcmMessageService.SendPushNotification(new PushRequest()
                {
                    Token = device.Token,
                    Title = request.Title,
                    Body = request.Body,
                    ImageUrl = request.ImageUrl
                });
            }
        }
        throw new NotImplementedException();
    }

    public async Task<ResponseResult<MessageDto>> SendTargetMessageAsync(TargetMessageRequest request)
    {
        var user = await _context.Users.Include(x => x.Devices).FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
            return ResponseResult<MessageDto>.Failure("User not found");

        if (!user.Devices.Any())
            return ResponseResult<MessageDto>.Failure("No devices found");


        var response = await _fcmMessageService.SendPushNotification(new PushRequest()
        {
            Token = user.Devices.FirstOrDefault()
                .Token, //TODO: Change this implementation to capture all users token as a list. Incase he has multiple devices active
            Title = request.Title,
            Body = request.Body,
        });

        return ResponseResult<MessageDto>.Success("message sent");
    }
}