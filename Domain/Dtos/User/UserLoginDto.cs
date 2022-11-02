namespace Domain.Dtos;

public class UserLoginDto
{
    public UserLoginDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}