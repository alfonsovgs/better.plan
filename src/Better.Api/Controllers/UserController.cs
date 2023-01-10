using Better.Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace Better.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserQueryService _userQuery;

    public UserController(IUserQueryService userQuery)
    {
        _userQuery = userQuery;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _userQuery.GetUser(id);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
