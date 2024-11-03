using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Service.Abstracts;

namespace ToDoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController(IUserService _userService): ControllerBase
{
    
    [HttpGet("email")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var result = await _userService.GetByEmailAsync(email);
        return Ok(result);
    }
}