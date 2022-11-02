using Domain.Dtos;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<PostBasicDto> CreateAsync(PostCreateDto postCreateDto);
    Task<PostFullDto> GetFullByIdAsync(string id);
    Task<IEnumerable<PostBasicDto>> GetAllAsync();
}