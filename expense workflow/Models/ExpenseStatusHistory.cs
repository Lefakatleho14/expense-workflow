using System.ComponentModel.DataAnnotations;

namespace ExpenseSystem.Api.Models;

public class ExpenseStatusHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid ExpenseId { get; set; }

    public string? OldStatus { get; set; }

    public string NewStatus { get; set; } = null!;

    public Guid ChangedByUserId { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}
