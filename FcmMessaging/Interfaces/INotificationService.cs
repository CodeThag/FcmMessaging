using FcmMessaging.Models;
using FcmMessaging.Models.Dto;
using Utilities.Models;

namespace FcmMessaging.Interfaces;

public interface INotificationService
{
    Task<ResponseResult<List<MessageDto>>> GetAllCampaignMessages();
    Task<ResponseResult<MessageDto>> GetCampaignMessage(Guid id);
    Task<ResponseResult<List<MessageDto>>> GetUserMessages(Guid id);
    Task<ResponseResult<MessageDto>> GetUserMessage(Guid id, Guid messageId);
    /// <summary>
    /// Send Campaign message to all users
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResponseResult<MessageDto>>  SendCampaignMessageAsync(CampaignMessageRequest request);

    /// <summary>
    /// Send message to an individual
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResponseResult<MessageDto>> SendTargetMessageAsync(TargetMessageRequest request);
}