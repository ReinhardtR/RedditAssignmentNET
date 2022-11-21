using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Post
{
    public Post(User author, string title, string content)
    {
        string uid = Guid.NewGuid().ToString().Split("-")[0];
        Id = title + "-" + uid;

        Author = author;
        Title = title;
        Content = content;
        CreatedAt = DateTime.Now;
    }

    private Post()
    {
    }

    [Key] public string Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}