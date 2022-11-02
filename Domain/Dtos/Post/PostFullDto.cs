namespace Domain.Dtos;

public class PostFullDto
{
    public PostFullDto(string id, DateTime createdAt, string authorUsername, string title, string content)
    {
        Id = id;
        CreatedAt = createdAt;
        AuthorUsername = authorUsername;
        Title = title;
        Content = content;
    }

    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string AuthorUsername { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}