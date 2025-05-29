using FcmMessaging.Commons.Mappings;
using FcmMessaging.Infrastructure.Persistence.Entities;

namespace FcmMessaging.Models.Dto;

public class UserDto : IMapFrom<User>
{
    public Guid Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string RegistrationToken { get; set; } = null!;
    public ICollection<DeviceDto> Devices { get; set; } = new HashSet<DeviceDto>();
}

public class DeviceDto : IMapFrom<Device>
{
    public string Token { get; set; } = null!;
    public string Platform { get; set; } = null!; // -- 'ios', 'android'
    public DateTimeOffset LastActive { get; set; } 
}