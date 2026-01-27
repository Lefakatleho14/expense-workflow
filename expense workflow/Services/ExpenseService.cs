using ExpenseSystem.Api.Data;
using ExpenseSystem.Api.DTOs;
using ExpenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace ExpenseSystem.Api.Services;

public class ExpenseService
{
    private readonly AppDbContext _db;

    public ExpenseService(AppDbContext db)
    {
        _db = db;
    }

    // -----------------------------
    // CREATE EXPENSE (EMPLOYEE)
    // -----------------------------
    public async Task CreateExpenseAsync(
        CreateExpenseDto dto,
        Guid userId)
    {
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = dto.Amount,
            Category = dto.Category,
            Description = dto.Description,
            ExpenseDate = dto.ExpenseDate,
            Status = "Submitted",
            CreatedAt = DateTime.UtcNow
        };

        _db.Expenses.Add(expense);

        await LogStatusChange(
            expense.Id,
            null,
            "Submitted",
            userId);

        await _db.SaveChangesAsync();
    }

    // -----------------------------
    // CHANGE STATUS (MANAGER / FINANCE)
    // -----------------------------
    public async Task ChangeStatusAsync(
        Guid expenseId,
        string newStatus,
        Guid actingUserId,
        string role)
    {
        var expense = await _db.Expenses
            .FirstOrDefaultAsync(e => e.Id == expenseId)
            ?? throw new Exception("Expense not found");

        ValidateTransition(
            expense.Status,
            newStatus,
            role);

        var oldStatus = expense.Status;
        expense.Status = newStatus;

        await LogStatusChange(
            expense.Id,
            oldStatus,
            newStatus,
            actingUserId);

        await _db.SaveChangesAsync();
    }

    // -----------------------------
    // DOMAIN RULES (STATE MACHINE)
    // -----------------------------
    private static void ValidateTransition(
        string currentStatus,
        string newStatus,
        string role)
    {
        if (role == "Employee")
            throw new Exception("Employees cannot change expense status");

        if (role == "Manager" && currentStatus != "Submitted")
            throw new Exception("Managers can only approve submitted expenses");

        if (role == "Finance" && currentStatus != "ManagerApproved")
            throw new Exception("Finance can only process manager-approved expenses");

        var validTransitions = new Dictionary<string, string[]>
        {
            ["Submitted"] = new[] { "ManagerApproved", "Rejected" },
            ["ManagerApproved"] = new[] { "FinanceApproved" },
            ["FinanceApproved"] = new[] { "Paid" }
        };

        if (!validTransitions.ContainsKey(currentStatus) ||
            !validTransitions[currentStatus].Contains(newStatus))
        {
            throw new Exception("Invalid expense status transition");
        }
    }

    // -----------------------------
    // AUDIT LOGGING
    // -----------------------------
    private async Task LogStatusChange(
        Guid expenseId,
        string? oldStatus,
        string newStatus,
        Guid changedByUserId)
    {
        var history = new ExpenseStatusHistory
        {
            Id = Guid.NewGuid(),
            ExpenseId = expenseId,
            OldStatus = oldStatus,
            NewStatus = newStatus,
            ChangedByUserId = changedByUserId,
            ChangedAt = DateTime.UtcNow
        };

        _db.ExpenseStatusHistory.Add(history);
        await Task.CompletedTask;
    }
}
