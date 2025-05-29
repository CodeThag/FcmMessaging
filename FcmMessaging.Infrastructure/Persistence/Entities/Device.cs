namespace FcmMessaging.Infrastructure.Persistence.Entities;

public class Device
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } 
    public User User { get; set; }
    public string Token { get; set; } = null!;
    public string Platform { get; set; } = null!; // -- 'ios', 'android'
    public DateTimeOffset LastActive { get; set; } 
}
