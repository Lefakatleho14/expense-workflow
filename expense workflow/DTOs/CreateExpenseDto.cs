namespace ExpenseSystem.Api.DTOs;

public record CreateExpenseDto(
    decimal Amount,
    string Category,
    string? Description,
    DateTime ExpenseDate
);
