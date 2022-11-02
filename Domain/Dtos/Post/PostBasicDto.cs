namespace Domain.Dtos;

public class PostBasicDto
{
    public PostBasicDto(string id, DateTime createdAt, string authorUsername, string title)
    {
        Id = id;
        CreatedAt = createdAt;
        AuthorUsername = authorUsername;
        Title = title;
    }

    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorUsername { get; set; }
    public string Title { get; set; }
}