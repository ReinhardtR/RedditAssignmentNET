using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using Domain.Dtos;
using HttpClients.Interfaces;

namespace HttpClients.Clients;

public class JwtAuthService : IAuthService
{
    private readonly HttpClient _client = new();

    public static string? Jwt { get; private set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;

    public async Task LoginAsync(UserLoginDto userLoginDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync(
            "https://localhost:7212/auth/login",
            userLoginDto
        );

        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode) throw new Exception(responseContent);

        Jwt = responseContent;

        ClaimsPrincipal principal = CreateClaimsPrincipal();
        OnAuthStateChanged(principal);
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }

    public async Task CreateAsync(UserCreateDto userCreateDto)
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("https://localhost:7212/auth", userCreateDto);

        if (!response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            throw new Exception(responseContent);
        }
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        return Task.FromResult(CreateClaimsPrincipal());
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt)) return new ClaimsPrincipal();

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);

        ClaimsIdentity identity = new(claims, "jwt");

        return new ClaimsPrincipal(identity);
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}