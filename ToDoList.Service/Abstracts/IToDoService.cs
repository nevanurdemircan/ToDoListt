using Core.Responses;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;

namespace ToDoList.Service.Abstracts;

public interface IToDoService
{
    ReturnModel<List<ToDoResponseDto>> GetAll();
    
    ReturnModel<ToDoResponseDto?> GetById(Guid id);
    ReturnModel<ToDoResponseDto> Add(ToDoCreateRequestDto create, string userId);

    ReturnModel<ToDoResponseDto> Update(ToDoUpdateRequestDto update);

    ReturnModel<ToDoResponseDto> Remove(Guid id);
    ReturnModel<List<ToDoResponseDto>> GetUserTodos(string UserId);
    ReturnModel<List<ToDoResponseDto>> GetFilteredUserTodos(string userId, ToDoFilterDto filter);

}