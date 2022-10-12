namespace Domain.Models;

public class Thread
{
    public Thread(User author, string title, string content)
    {
        Author = author;
        Title = title;
        Content = content;
        Date = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime Date { get; set; }

    public User Author { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}