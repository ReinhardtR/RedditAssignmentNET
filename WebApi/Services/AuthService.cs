using System.ComponentModel.DataAnnotations;
using Domain.Models;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IList<User> users = new List<User>
    {
        new()
        {
            Username = "Reinhardt",
            Password = "123"
        },
        new()
        {
            Username = "Dva",
            Password = "321"
        },
        new()
        {
            Username = "Winston",
            Password = "456"
        },
        new()
        {
            Username = "Zarya",
            Password = "654"
        }
    };

    public Task<User> GetUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (existingUser == null) throw new Exception("User with this username does not exist");

        if (!existingUser.Password.Equals(password)) throw new Exception("Invalid password");

        return Task.FromResult(existingUser);
    }

    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.Username)) throw new ValidationException("Username is required");

        if (string.IsNullOrEmpty(user.Password)) throw new ValidationException("Password is required");

        // Persistence
        users.Add(user);

        return Task.CompletedTask;
    }
}