using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    public async Task<PostBasicDto> CreateAsync(PostCreateDto dto)
    {
        if (dto.Title.Length < 5) throw new ArgumentException("Title must be at least 5 characters long.");
        if (dto.Title.Length > 128) throw new ArgumentException("Title must be at most 128 characters long.");

        User? author = await _userDao.GetByUsernameAsync(dto.AuthorUsername);

        if (author == null) throw new ArgumentException($"User with username '{dto.AuthorUsername}' does not exist.");

        Post postToCreate = new(author, dto.Title, dto.Content);

        Post post = await _postDao.CreateAsync(postToCreate);

        return new PostBasicDto(post.Id, post.CreatedAt, post.Author.Username, post.Title);
    }

    public async Task<PostFullDto> GetFullByIdAsync(string id)
    {
        Post? post = await _postDao.GetFullByIdAsync(id);

        if (post == null) throw new Exception($"Post with id '{id}' not found");

        return new PostFullDto(post.Id, post.CreatedAt, post.Author.Username, post.Title, post.Content);
    }

    public async Task<IEnumerable<PostBasicDto>> GetAllAsync()
    {
        IEnumerable<Post> posts = await _postDao.GetAllAsync();

        return posts.Select(post => new PostBasicDto(post.Id, post.CreatedAt, post.Author.Username, post.Title));
    }
}