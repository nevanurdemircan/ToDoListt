using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Dtos.Users.Responses;
using ToDoList.Models.Entities;

namespace ToDoList.Models.Dtos.ToDos.Responses;

public sealed record ToDoResponseDto(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    Priority Priority,
    bool Completed,
    CategoryResponseDto Category,
    UserResponseDto User
);