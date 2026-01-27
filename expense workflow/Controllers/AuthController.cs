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

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        await _authService.RegisterAsync(
            dto.FullName,
            dto.Email,
            dto.Password,
            dto.Role);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await _authService.LoginAsync(dto.Email, dto.Password);
        return Ok(user);
    }
}
