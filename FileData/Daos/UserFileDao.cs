using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.Daos;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        User? user = _context.Users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
        );

        return Task.FromResult(user);
    }

    // public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    // {
    //     IEnumerable<User> users = _context.Users.AsEnumerable();
    //
    //     if (searchParameters.UsernameContains != null)
    //         users = _context.Users.Where(u =>
    //             u.Username.Contains(searchParameters.UsernameContains, StringComparison.OrdinalIgnoreCase)
    //         );
    //
    //     return Task.FromResult(users);
    // }
}