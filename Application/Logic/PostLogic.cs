using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.Dtos;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;

    public PostLogic(IPostDao postDao)
    {
        _postDao = postDao;
    }

    public async Task<PostBasicDto> CreateAsync(PostCreateDto dto)
    {
        Post postToCreate = new(dto.Author, dto.Title, dto.Content);

        Post post = await _postDao.CreateAsync(postToCreate);

        return new PostBasicDto(post.Id, post.CreatedAt, post.Author.Username, post.Title);
    }

    public async Task<PostFullDto?> GetFullByIdAsync(string id)
    {
        Post? post = await _postDao.GetFullByIdAsync(id);

        if (post == null) throw new Exception("Post not found");

        return new PostFullDto(post.Id, post.CreatedAt, post.Author.Username, post.Title, post.Content);
    }

    public async Task<IEnumerable<PostBasicDto>> GetAllAsync()
    {
        IEnumerable<Post> posts = await _postDao.GetAllAsync();

        return posts.Select(post => new PostBasicDto(post.Id, post.CreatedAt, post.Author.Username, post.Title));
    }
}