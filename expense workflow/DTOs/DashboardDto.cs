public record EmployeeDashboardDto(
    decimal TotalSubmitted,
    decimal TotalApproved,
    decimal TotalRejected,
    IEnumerable<ExpenseSummaryDto> RecentExpenses
);

public record ManagerDashboardDto(
    int PendingApprovalCount,
    IEnumerable<ExpenseSummaryDto> PendingExpenses,
    IEnumerable<EmployeeMonthlyTotalDto> MonthlyTotals
);

public record FinanceDashboardDto(
    decimal TotalApprovedUnpaid,
    decimal TotalPaidThisMonth,
    IEnumerable<CategoryTotalDto> TotalsByCategory
);

public record ExpenseSummaryDto(
    Guid Id,
    decimal Amount,
    string Category,
    string Status,
    DateTime ExpenseDate
);

public record EmployeeMonthlyTotalDto(
    Guid UserId,
    decimal TotalAmount
);

public record CategoryTotalDto(
    string Category,
    decimal TotalAmount
);
