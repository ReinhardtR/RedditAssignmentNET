using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<Post?> GetFullByIdAsync(string id);
    Task<ICollection<Post>> GetAllAsync();
}