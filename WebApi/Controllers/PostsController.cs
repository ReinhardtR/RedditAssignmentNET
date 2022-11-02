using Application.LogicInterfaces;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostLogic _postLogic;

    public PostsController(IPostLogic postLogic)
    {
        _postLogic = postLogic;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PostBasicDto>>> GetAll()
    {
        IEnumerable<PostBasicDto> posts = await _postLogic.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PostFullDto>> GetFullById(string id)
    {
        try
        {
            PostFullDto post = await _postLogic.GetFullByIdAsync(id);
            return Ok(post);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<PostBasicDto>> Create([FromBody] PostCreateDto postCreateDto)
    {
        if (!postCreateDto.AuthorUsername.Equals(User.Identity?.Name))
            return BadRequest("You can't create posts for other users");

        try
        {
            PostBasicDto post = await _postLogic.CreateAsync(postCreateDto);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}