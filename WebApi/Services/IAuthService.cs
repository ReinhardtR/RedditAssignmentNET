using Domain.Dtos;

namespace WebApi.Services;

public interface IAuthService
{
    Task<UserBasicDto> GetUser(string username, string password);
    Task<UserBasicDto> CreateUser(UserCreateDto userCreateDto);
}