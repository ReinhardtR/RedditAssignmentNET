using System.Security.Claims;
using Domain.Dtos;

namespace HttpClients.Interfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(UserLoginDto userLoginDto);
    public Task LogoutAsync();
    public Task CreateAsync(UserCreateDto userCreateDto);
    public Task<ClaimsPrincipal> GetAuthAsync();
}