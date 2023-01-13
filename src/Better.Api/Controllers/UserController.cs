using Better.Application.Queries;
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

    [HttpGet("{id}/summary")]
    public async Task<IActionResult> GetSummaryByUserId(int id)
    {
        var result = await _userQuery.GetSummaryByUserId(id);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet("{id}/goals")]
    public async Task<IActionResult> GetGoalsByUserId(int id, int pageNumber = 1, int pageSize = 10)
    {
        var user = await _userQuery.GetGoalsByUserId(id, pageNumber, pageSize);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }
}
