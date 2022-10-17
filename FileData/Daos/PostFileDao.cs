using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.Daos;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        // Short unique id
        string uid = Guid.NewGuid().ToString().Split("-")[0];

        post.Id = post.Title + "-" + uid;

        _context.Posts.Add(post);
        _context.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<Post?> GetFullByIdAsync(string id)
    {
        Post? thread = _context.Posts.FirstOrDefault(t => t.Id.Equals(id));

        return Task.FromResult(thread);
    }

    public Task<IEnumerable<Post>> GetAllAsync()
    {
        return Task.FromResult(_context.Posts.AsEnumerable());
    }
}