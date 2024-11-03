using Core.Repositories;
using ToDoList.DataAccess.Abstracts;
using ToDoList.DataAccess.Contexts;
using ToDoList.Models.Entities;

namespace ToDoList.DataAccess.Concretes;

public class EfToDoRepository:EfRepositoryBase<BaseDbContext, ToDo, Guid>,IToDoRepository
{
    public EfToDoRepository(BaseDbContext context) : base(context)
    {
    }
}