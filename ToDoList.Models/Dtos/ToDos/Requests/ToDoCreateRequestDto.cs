using ToDoList.Models.Entities;

namespace ToDoList.Models.Dtos.ToDos.Requests;

public sealed record ToDoCreateRequestDto(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Priority Priority,
    int CategoryId
    );