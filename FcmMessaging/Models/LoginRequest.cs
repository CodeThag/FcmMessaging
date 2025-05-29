using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FcmMessaging.Models;

public class LoginRequest
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required, MinLength(8), MaxLength(8)]
    public string Password { get; set; } = null!;
}