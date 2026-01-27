using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem.Api.DTOs;

public class RegisterDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Role { get; set; } = "Employee";
}
