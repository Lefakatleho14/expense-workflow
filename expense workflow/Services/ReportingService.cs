using ExpenseSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseSystem.Api.Services;

public class ReportingService
{
    private readonly AppDbContext _db;

    public ReportingService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<decimal> GetMonthlyTotalAsync(
        Guid userId,
        int year,
        int month)
    {
        return await _db.Expenses
            .Where(e =>
                e.UserId == userId &&
                e.ExpenseDate.Year == year &&
                e.ExpenseDate.Month == month &&
                e.Status == "Paid")
            .SumAsync(e => e.Amount);
    }

    public async Task<object> GetCategoryBreakdownAsync(Guid userId)
    {
        return await _db.Expenses
            .Where(e => e.UserId == userId && e.Status == "Paid")
            .GroupBy(e => e.Category)
            .Select(g => new
            {
                Category = g.Key,
                Total = g.Sum(x => x.Amount)
            })
            .ToListAsync();
    }
}
