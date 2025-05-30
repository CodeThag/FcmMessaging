using FcmMessaging.Interfaces;
using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Utilities.Helpers;
using Utilities.Models;

namespace FcmMessaging.Api.Controllers;

public class MessageController : BaseController
{
    private readonly ILogger<MessageController> _logger;
    private readonly INotificationService _notificationService;

    public MessageController(ILogger<MessageController> logger, INotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }
    
    
    [HttpPost("SendTargetMessageByToken")]
    public async Task<ActionResult<ResponseResult<MessageDto>>> SendTargetMessageByToken(ExpoTargetMessageRequest request)
    {
        var response = await _notificationService.SendTargetExpoMessageAsync(request);

        return ActionResultHelper.MapResponseByStatusCode(response);
    }

    [HttpPost("SendTargetMessage")]
    public async Task<ActionResult<ResponseResult<MessageDto>>> SendTargetMessage(TargetMessageRequest request)
    {
        var response = await _notificationService.SendTargetMessageAsync(request);

        return ActionResultHelper.MapResponseByStatusCode(response);
    }

    [HttpPost("SendCampaignMessage")]
    public async Task<ActionResult<ResponseResult<MessageDto>>> SendCampaignMessage(CampaignMessageRequest request)
    {
        var response = await _notificationService.SendCampaignMessageAsync(request);

        return ActionResultHelper.MapResponseByStatusCode(response);
    }

    [HttpGet("GetAllCampaignMessages")]
    public async Task<ActionResult<ResponseResult<List<MessageDto>>>> GetAllCampaignMessages()
    {
        var response = await _notificationService.GetAllCampaignMessages();
        
        return ActionResultHelper.MapResponseByStatusCode(response);
    }
    
    [HttpGet("GetCampaignMessage")]
    public async Task<ActionResult<ResponseResult<MessageDto>>> GetCampaignMessage(Guid id)
    {
        var response = await _notificationService.GetCampaignMessage(id);
        
        return ActionResultHelper.MapResponseByStatusCode(response);
    }
    
    [HttpGet("GetUserMessages")]
    public async Task<ActionResult<ResponseResult<List<MessageDto>>>> GetUserMessages(Guid id)
    {
        var response = await _notificationService.GetUserMessages(id);
        
        return ActionResultHelper.MapResponseByStatusCode(response);
    }

    
    [HttpGet("GetUserMessage")]
    public async Task<ActionResult<ResponseResult<MessageDto>>> GetUserMessage(Guid id, Guid messageId)
    {
        var response = await _notificationService.GetUserMessage(id, messageId);
        
        return ActionResultHelper.MapResponseByStatusCode(response);
    }
}