namespace FcmMessaging.Infrastructure.Persistence.Entities;
public class User : AuditableEntity
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string RegistrationToken { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<Device> Devices { get; set; } = new HashSet<Device>();
}

public class AuditableEntity
{
}
