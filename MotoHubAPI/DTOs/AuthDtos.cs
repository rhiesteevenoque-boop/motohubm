using System.ComponentModel.DataAnnotations;
namespace MotoHubAPI.DTOs;
public record RegisterRequest([Required,MaxLength(120)] string FullName,[Required,EmailAddress] string Email,[Required,MinLength(8)] string Password);
public record LoginRequest([Required,EmailAddress] string Email,[Required] string Password);
