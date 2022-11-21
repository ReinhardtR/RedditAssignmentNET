using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcData.Daos;

public class PostDao : IPostDao
{
    private readonly DataContext _context;

    public PostDao(DataContext context)
    {
        _context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> postAdded = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return postAdded.Entity;
    }

    public async Task<Post?> GetFullByIdAsync(string id)
    {
        Post? post = await _context.Posts
            .Include(p => p.Author)
            .SingleOrDefaultAsync(p => p.Id.Equals(id));

        return post;
    }

    public Task<ICollection<Post>> GetAllAsync()
    {
        ICollection<Post> posts = _context.Posts
            .Include(p => p.Author)
            .ToList();

        return Task.FromResult(posts);
    }
}