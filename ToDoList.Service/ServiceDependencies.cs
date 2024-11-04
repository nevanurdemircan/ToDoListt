using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Models.Dtos.Validator;
using ToDoList.Models.Entities;
using ToDoList.Service.Abstracts;
using ToDoList.Service.Concretes;
using ToDoList.Service.Profiles;
using ToDoList.Service.Rules;

namespace ToDoList.Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddScoped<ToDoBusinessRules>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IToDoService, ToDoService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddValidatorsFromAssemblyContaining<ToDoCreateRequestValidator>(); 

        return services;
    }
}