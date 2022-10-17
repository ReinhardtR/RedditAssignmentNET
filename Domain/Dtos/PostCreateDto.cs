using Domain.Models;

namespace Domain.Dtos;

public class PostCreateDto
{
    public PostCreateDto(User author, string title, string content)
    {
        Author = author;
        Title = title;
        Content = content;
    }

    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}