using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config, IAuthService authService)
    {
        _config = config;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            UserBasicDto user = await _authService.GetUser(userLoginDto.Username, userLoginDto.Password);
            string token = GenerateJwt(user);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] UserCreateDto userCreateDto)
    {
        try
        {
            UserBasicDto userBasicDto = await _authService.CreateUser(userCreateDto);
            return Ok(userBasicDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private IEnumerable<Claim> GenerateClaims(UserBasicDto userBasicDto)
    {
        return new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new(ClaimTypes.Name, userBasicDto.Username)
        };
    }

    private string GenerateJwt(UserBasicDto userBasicDto)
    {
        IEnumerable<Claim> claims = GenerateClaims(userBasicDto);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512);

        JwtHeader header = new(credentials);
        JwtPayload payload = new(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            null,
            DateTime.UtcNow.AddMinutes(60)
        );

        JwtSecurityToken token = new(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}