using AutoMapper;
using AutoMapper.QueryableExtensions;
using FcmMessaging.Infrastructure.Models;
using FcmMessaging.Infrastructure.Persistence.Entities;
using FcmMessaging.Infrastructure.Persistence.Sql;
using FcmMessaging.Infrastructure.Service;
using FcmMessaging.Interfaces;
using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Microsoft.AspNetCore.Http;
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
    private readonly IExpoSerivce _expoService;

    public NotificationService(ILogger<INotificationService> logger, ApplicationDbContext context,
        IFcmMessageService fcmMessageService, IMapper mapper, IExpoSerivce expoService)
    {
        _logger = logger;
        _context = context;
        _fcmMessageService = fcmMessageService;
        _mapper = mapper;
        _expoService = expoService;
    }


    public async Task<ResponseResult<List<MessageDto>>> GetAllCampaignMessages()
    {
        var messages = await _context.Messages
            .ToListAsync();

        var dto = _mapper.Map<List<MessageDto>>(messages);

        return ResponseResult<List<MessageDto>>.Success(dto);
    }

    public async Task<ResponseResult<MessageDto>> GetCampaignMessage(Guid id)
    {
        var message = await _context.Messages.Where(x => x.Id == id)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (message == null)
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

        if (message == null)
            ResponseResult<MessageDto>.Failure("Message not found");

        return ResponseResult<MessageDto>.Success(message);
    }

    public async Task<ResponseResult<MessageDto>> SendCampaignMessageAsync(CampaignMessageRequest request)
    {
        var users = await _context.Users.Include(x => x.Devices).ToListAsync();
        var tokens = new List<string>();
        foreach (var user in users)
            tokens = user.Devices.Select(x => x.Token).ToList();

        var response = await _expoService.SendToken(new ExpoPushRequest()
        {
            Tokens = tokens,
            Body = request.Body,
            Title = request.Title,
            ImageUrl = request.ImageUrl
        });

        var message = _mapper.Map<Message>(request);

        await _context.Messages.AddAsync(message);

        var result = await _context.SaveChangesAsync();

        var dto = _mapper.Map<MessageDto>(message);

        return ResponseResult<MessageDto>.Success(response, dto, statusCode: StatusCodes.Status201Created);
    }

    public async Task<ResponseResult<MessageDto>> SendTargetMessageAsync(TargetMessageRequest request)
    {
        var user = await _context.Users.Include(x => x.Devices).FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
            return ResponseResult<MessageDto>.Failure("User not found");

        if (!user.Devices.Any())
            return ResponseResult<MessageDto>.Failure("No devices found");

        var tokens = user.Devices.Select(x => x.Token).ToList();

        var response = await _expoService.SendToken(new ExpoPushRequest()
        {
            Tokens = tokens,
            Body = request.Body,
            Title = request.Title,
            ImageUrl = request.ImageUrl
        });

        return ResponseResult<MessageDto>.Success(response);
    }

    public async Task<ResponseResult<MessageDto>> SendTargetExpoMessageAsync(ExpoTargetMessageRequest request)
    {
        var response = await _expoService.SendToken(new ExpoPushRequest()
        {
            Tokens = new List<string>() { request.Token },
            Body = request.Body,
            Title = request.Title,
            ImageUrl = request.ImageUrl
        });

        return ResponseResult<MessageDto>.Success(response);
    }
}