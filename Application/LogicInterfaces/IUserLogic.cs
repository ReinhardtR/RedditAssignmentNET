using Domain.Dtos;

namespace Application.LogicInterfaces;

public interface IUserLogic
{
    Task<UserBasicDto> CreateAsync(UserCreateDto userToCreate);
    Task<UserBasicDto?> GetByUsernameAsync(string username);
    Task<UserBasicDto> GetByUsernameAndPasswordAsync(string username, string password);
    Task<IEnumerable<UserBasicDto>> GetAllAsync();
}