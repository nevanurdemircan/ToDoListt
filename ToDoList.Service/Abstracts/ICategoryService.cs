using Core.Responses;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;

namespace ToDoList.Service.Abstracts;

public interface ICategoryService
{
    ReturnModel<List<CategoryResponseDto>> GetAll();
    ReturnModel<CategoryResponseDto> Add(CategoryCreateRequestDto create);
    ReturnModel<CategoryResponseDto> Remove(int id);
}