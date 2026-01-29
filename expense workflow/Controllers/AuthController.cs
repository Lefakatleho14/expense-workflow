using ExpenseSystem.Api.DTOs;
using ExpenseSystem.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseSystem.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        try
        {
            var token = _authService.Login(dto, out var role);

            return Ok(new
            {
                token,
                role
            });
        }
        catch
        {
            return Unauthorized();
        }
    }
}
