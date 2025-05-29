using System.Diagnostics;
using FcmMessaging.Infrastructure.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;

namespace FcmMessaging.Infrastructure.Service;

public class FcmMessageService : IFcmMessageService
{
    private readonly ILogger<FcmMessageService> _logger;
    private readonly FirebaseMessaging _firebaseMessaging;

    public FcmMessageService(ILogger<FcmMessageService> logger, FirebaseMessaging firebaseMessaging)
    {
        _logger = logger;
        _firebaseMessaging = firebaseMessaging;
    }

    public async Task<PushResponse> SendPushNotification(PushRequest request)
    {
        try
        {
            var message = new FirebaseAdmin.Messaging.Message()
            {
                Token = request.Token,
                Data = new Dictionary<string, string>(),
                Notification = new FirebaseAdmin.Messaging.Notification
                {
                    Title = request.Title,
                    Body = request.Body,
                },
                Android = new AndroidConfig
                {
                    Priority = Priority.High,
                    Notification = new AndroidNotification
                    {
                        ChannelId = "high_priority",
                        // New in 3.x: Image URL support
                        ImageUrl = "https://example.com/image.png"
                    }
                },
                Apns = new ApnsConfig
                {
                    Aps = new Aps
                    {
                        // New: Custom sound support
                        Sound = "default"
                    }
                }
            };

            var response = await _firebaseMessaging.SendAsync(message);

            return new PushResponse() { Response = response };
        }
        catch (Exception ex)
        {
            Activity.Current?.AddEvent(new(name: "Firebase_Exception", tags: new ActivityTagsCollection()
            {
                { "Message", ex.ToString() },
                { "Time", DateTimeOffset.Now.ToString() }
            }));

            return new PushResponse() { Response = ex.Message };
        }
    }
}

public interface IFcmMessageService
{
    public Task<PushResponse> SendPushNotification(PushRequest request);
}