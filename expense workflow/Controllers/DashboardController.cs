using ExpenseSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseSystem.Api.Controllers;

[ApiController]
[Route("api/dashboard")]
[Authorize]
public class DashboardController : ControllerBase
{
    private readonly ReportingService _reportingService;

    public DashboardController(ReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var now = DateTime.UtcNow;

        var monthlyTotal = await _reportingService
            .GetMonthlyTotalAsync(userId, now.Year, now.Month);

        return Ok(new
        {
            Month = now.Month,
            Total = monthlyTotal
        });
    }
}
