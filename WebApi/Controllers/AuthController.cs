using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserLogic _userLogic;

    public AuthController(IConfiguration config, IUserLogic userLogic)
    {
        _config = config;
        _userLogic = userLogic;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            UserBasicDto user =
                await _userLogic.GetByUsernameAndPasswordAsync(userLoginDto.Username, userLoginDto.Password);
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
            UserBasicDto userBasicDto = await _userLogic.CreateAsync(userCreateDto);
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