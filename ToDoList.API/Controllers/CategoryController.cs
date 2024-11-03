using Microsoft.AspNetCore.Mvc;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Service.Abstracts;

namespace ToDoList.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(ICategoryService _categoryService): ControllerBase
{
    [HttpGet("getall")]
    public IActionResult GetAll()
    {
        var result = _categoryService.GetAll();
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add([FromBody] CategoryCreateRequestDto dto)
    {
        var result = _categoryService.Add(dto);
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Remove(int id)
    {
        var result = _categoryService.Remove(id);
        return Ok(result);
    }
}
