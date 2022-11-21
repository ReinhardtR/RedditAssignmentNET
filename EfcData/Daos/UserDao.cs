using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.Daos;

public class UserDao : IUserDao
{
    private readonly DataContext _context;

    public UserDao(DataContext context)
    {
        _context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        Console.WriteLine("user: " + user);
        EntityEntry<User> newUser = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        Console.WriteLine("done");
        return newUser.Entity;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Username == username);
        return Task.FromResult(user);
    }

    public Task<ICollection<User>> GetAllAsync()
    {
        ICollection<User> users = _context.Users.ToList();
        return Task.FromResult(users);
    }
}