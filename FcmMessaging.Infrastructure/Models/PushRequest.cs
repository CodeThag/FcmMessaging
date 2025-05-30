namespace FcmMessaging.Infrastructure.Models;

public class PushRequest
{
    public string Token { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}

public class ExpoPushRequest
{
    public List<string> Tokens { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
}

public class PushResponse
{
    public string Response { get; set; }
}