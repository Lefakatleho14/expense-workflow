using ExpenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseSystem.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<ExpenseStatusHistory> ExpenseStatusHistory
        => Set<ExpenseStatusHistory>();
}
