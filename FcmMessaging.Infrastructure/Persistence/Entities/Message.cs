namespace FcmMessaging.Infrastructure.Persistence.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public bool IsInstant { get; set; } 
    public DateTimeOffset SendAt { get; set; }
    public Guid? UserId { get; set; }
    public User User { get; set; } = null!;
}
