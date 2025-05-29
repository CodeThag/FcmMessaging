using FcmMessaging.Models;

namespace FcmMessaging.Interfaces;

public interface IInAppNotificationService
{
    /// <summary>
    /// Send Campaign message to all users
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task SendCampaignMessageAsync(CampaignMessageRequest request);

    /// <summary>
    /// Send message to an individual
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task SendTargetMessageAsync(TargetMessageRequest request);
}