using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using Utilities.Attributes;

namespace FcmMessaging.Models;

public class CampaignMessageRequest
{
    [Required]
    [SwaggerSchema(Description = "Message Title")]
    [SwaggerSchemaExample("Testing Push Notification")]
    public string Title { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "Description of Message")]
    [SwaggerSchemaExample("Description of Message")]
    public string Description { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "Message Body")]
    [SwaggerSchemaExample("Testing Push Notification Message using Firebase Admin v3.2.0")]
    public string Body { get; set; } = null!;
    
    [SwaggerSchema(Description = "Message Image Url")]
    [SwaggerSchemaExample("https://foo.bar/pizza-monster.png")]
    public string ImageUrl { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "Is the message instant or scheduled?")]
    [SwaggerSchemaExample("true")]
    public bool IsInstant { get; set; } 
    
    [SwaggerSchema(Description = "Time message should be sent out")]
    [SwaggerSchemaExample("2025-05-29 15:00:00 PM")]
    [DataType(DataType.DateTime)]
    public DateTimeOffset SendAt { get; set; }
}