using System.Text;
using Expo.Server.Client;
using Expo.Server.Models;
using FcmMessaging.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FcmMessaging.Infrastructure.Service;

public class ExpoService : IExpoSerivce
{
    public async Task<string> SendToken(ExpoPushRequest request)
    {
        var expoSDKClient = new PushApiClient();
        var pushTicketReq = new PushTicketRequest() {
            PushTo = request.Tokens,
            PushTitle = request.Title,
            PushBody = request.Body,
        };
        var result = await expoSDKClient.PushSendAsync(pushTicketReq);

        var hasError = false;
        StringBuilder builder = new StringBuilder();
        foreach (var p in result.PushTicketStatuses)
        {
            if (p.TicketStatus == "error")
            {
                hasError = true;
                builder.AppendLine(p.TicketMessage);
            }
        }
        
        return hasError ? builder.ToString() : "Success";
    }
}

public interface IExpoSerivce
{
    Task<string> SendToken(ExpoPushRequest request);
}