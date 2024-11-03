namespace ToDoList.Models.Dtos.Users.Responses;

public sealed record UserResponseDto(
    string UserId,
    string UserName
);