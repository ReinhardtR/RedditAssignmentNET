using Application.LogicInterfaces;
using Domain.Dtos;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private readonly IUserLogic _userLogic;

    public AuthService(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    public async Task<UserBasicDto> GetUser(string username, string password)
    {
        UserBasicDto existingUser = await _userLogic.GetByUsernameAndPasswordAsync(username, password);
        return new UserBasicDto(existingUser.Username);
    }

    public async Task<UserBasicDto> CreateUser(UserCreateDto userCreateDto)
    {
        return await _userLogic.CreateAsync(userCreateDto);
    }
}