using System.ComponentModel.DataAnnotations;

public class Expense
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    [Required]
    public string Category { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime ExpenseDate { get; set; }

    [Required]
    public string Status { get; set; } = "Submitted";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
