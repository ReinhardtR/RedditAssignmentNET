namespace Domain.Models;

public class Post
{
    public Post(User author, string title, string content)
    {
        Author = author;
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
    }

    // Is generated in PostFileDao
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}