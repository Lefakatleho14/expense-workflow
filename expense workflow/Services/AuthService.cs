using ExpenseSystem.Api.Data;
using ExpenseSystem.Api.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace ExpenseSystem.Api.Services;

public class AuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db)
    {
        _db = db;
    }

    public async Task RegisterAsync(
        string fullName,
        string email,
        string password,
        string role)
    {
        if (await _db.Users.AnyAsync(u => u.Email == email))
            throw new Exception("User already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            FullName = fullName,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }



    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new Exception("Invalid credentials");

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new Exception("Invalid credentials");

        return user;
    }


}
