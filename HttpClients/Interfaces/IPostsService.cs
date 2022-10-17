using Domain.Dtos;

namespace HttpClients.Interfaces;

public interface IPostsService
{
    Task<IEnumerable<PostBasicDto>> GetAllPostsAsync();
    Task<PostFullDto> GetPostByIdAsync(string id);
    Task<PostBasicDto> CreatePostAsync(PostCreateDto dto);
}