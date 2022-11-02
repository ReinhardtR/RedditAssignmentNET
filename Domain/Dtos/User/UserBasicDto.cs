namespace Domain.Dtos;

public class UserBasicDto
{
    public UserBasicDto(string username)
    {
        Username = username;
    }

    public string Username { get; }
}