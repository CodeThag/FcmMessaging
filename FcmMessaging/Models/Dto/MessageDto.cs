using FcmMessaging.Commons.Mappings;
using FcmMessaging.Infrastructure.Persistence.Entities;

namespace FcmMessaging.Models.Dto;

public class MessageDto: IMapFrom<Message>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public bool IsInstant { get; set; } 
    public DateTimeOffset SendAt { get; set; }
}