using Application.LogicInterfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserBasicDto>>> GetAll()
    {
        IEnumerable<UserBasicDto> users = await _userLogic.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<UserBasicDto>> GetByUsername(string username)
    {
        UserBasicDto? user = await _userLogic.GetByUsernameAsync(username);

        if (user == null) return NotFound($"User with username {username} not found");

        return Ok(user);
    }
}