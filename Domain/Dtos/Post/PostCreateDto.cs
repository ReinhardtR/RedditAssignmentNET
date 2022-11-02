namespace Domain.Dtos;

public class PostCreateDto
{
    public PostCreateDto(string authorUsername, string title, string content)
    {
        AuthorUsername = authorUsername;
        Title = title;
        Content = content;
    }

    public string AuthorUsername { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}