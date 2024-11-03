using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Service.Abstracts;

namespace ToDoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ToDosController(IToDoService _toDoService) : ControllerBase
{
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _toDoService.GetAll();
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] ToDoCreateRequestDto dto)
    {
        var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        var result = _toDoService.Add(dto, userId);
        return Ok(result);
    }

    [HttpGet("get/{id}")]
    public IActionResult GetById(Guid id)
    {
        var result = _toDoService.GetById(id);
        return Ok(result);
    }

    [HttpPut("update/{id}")]
    public IActionResult Update(Guid id, [FromBody] ToDoUpdateRequestDto dto)
    {
        var result = _toDoService.Update(dto);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Remove(Guid id)
    {
        var result = _toDoService.Remove(id);
        return Ok(result);
    }

    [HttpGet("my-todos")]
    public IActionResult GetUserTodos()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var result = _toDoService.GetUserTodos(userId);
        return Ok(result);
    }

    [HttpPost("my-todos/filter")]
    public IActionResult GetFilteredUserTodos([FromBody] ToDoFilterDto filter)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Unauthorized();
        }

        var result = _toDoService.GetFilteredUserTodos(userId, filter);
        return Ok(result);
    }
}