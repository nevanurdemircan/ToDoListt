using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.DataAccess.Abstracts;
using ToDoList.DataAccess.Concretes;
using ToDoList.DataAccess.Contexts;

namespace ToDoList.DataAccess;

public static class DataAccessRepositoryDependencies
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IToDoRepository, EfToDoRepository>();
        services.AddScoped<ICategoryRepository, EfCategoryRepository>();

        services.AddDbContext<BaseDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("SqlConnection")));
        return services;
    }
}