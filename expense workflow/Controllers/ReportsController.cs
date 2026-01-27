using ExpenseSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseSystem.Api.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly ReportingService _reportingService;

    public ReportsController(ReportingService reportingService)
    {
        _reportingService = reportingService;
    }

    [HttpGet("monthly")]
    public async Task<IActionResult> Monthly(int year, int month)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var total = await _reportingService
            .GetMonthlyTotalAsync(userId, year, month);

        return Ok(new { total });
    }

    [HttpGet("categories")]
    public async Task<IActionResult> Categories()
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var data = await _reportingService
            .GetCategoryBreakdownAsync(userId);

        return Ok(data);
    }
}
