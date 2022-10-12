namespace Domain.Dtos;

public class UserCreateDto
{
    public UserCreateDto(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public string Username { get; }
    public string Password { get; }
}