using System.ComponentModel.DataAnnotations;
using FcmMessaging.Infrastructure.Persistence.Entities;
using Swashbuckle.AspNetCore.Annotations;
using Utilities.Attributes;

namespace FcmMessaging.Models;

public class UserRequest
{
    [Required]
    [SwaggerSchema(Description = "User's firstname")]
    [SwaggerSchemaExample("Newton")]
    public string Firstname { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "User's lastname")]
    [SwaggerSchemaExample("Kyari")]
    public string Lastname { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "User's email")]
    [SwaggerSchemaExample("ntkyari@gmail.com")]
    public string Email { get; set; } = null!;
    
    [SwaggerSchema(Description = "User's password")]
    [SwaggerSchemaExample("123456")]
    public string Password { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "User's phone")]
    [SwaggerSchemaExample("08065565986")]
    public string Phone { get; set; } = null!;
    
    [Required]
    [SwaggerSchema(Description = "User's Token")]
    [SwaggerSchemaExample("1234567890")]
    public string RegistrationToken { get; set; } = null!;

    [Required]
    [SwaggerSchema(Description = "User's phone type")]
    [SwaggerSchemaExample("android")]
    public string Platform { get; set; } = null!;

    public Device GetDevice()
    {
        return new Device()
        {
            Platform = Platform,
            Token = RegistrationToken,
            LastActive = DateTimeOffset.Now
        };
    }

    public User ToUser()
    {
        return new User()
        {
            Email = Email,
            Firstname = Firstname,
            Lastname = Lastname,
            Phone = Phone,
            RegistrationToken = RegistrationToken,
            Password = Password,
        };
    }
}