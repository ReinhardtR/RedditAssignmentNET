using System.Security.Claims;
using Domain.Models;

namespace HttpClients.Interfaces;

public interface IAuthService
{
    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task CreateAsync(User user);
    public Task<ClaimsPrincipal> GetAuthAsync();
}