using ToDoList.Models.Entities;

namespace ToDoList.Models.Dtos.ToDos.Requests;

public sealed record ToDoUpdateRequestDto(
   Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Priority Priority,
    int CategoryId,
    string UserId);