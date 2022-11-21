using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class User
{
    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    private User()
    {
    }

    [Key] public string Username { get; set; } // unique

    public string Password { get; set; }
}