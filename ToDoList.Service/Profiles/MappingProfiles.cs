using AutoMapper;
using ToDoList.Models.Dtos.Categories.Requests;
using ToDoList.Models.Dtos.Categories.Responses;
using ToDoList.Models.Dtos.ToDos.Requests;
using ToDoList.Models.Dtos.ToDos.Responses;
using ToDoList.Models.Dtos.Users.Requests;
using ToDoList.Models.Dtos.Users.Responses;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CategoryCreateRequestDto, Category>();
        CreateMap<ToDoCreateRequestDto, ToDo>();
        CreateMap<ToDoUpdateRequestDto, ToDo>();
        CreateMap<UserUpdateRequestDto, User>();

        CreateMap<Category, CategoryResponseDto>();
        CreateMap<ToDo, ToDoResponseDto>();
        CreateMap<User, UserResponseDto>();



    }
}