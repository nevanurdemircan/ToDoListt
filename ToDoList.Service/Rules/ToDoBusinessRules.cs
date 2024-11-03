using Core.Exceptions;
using ToDoList.Models.Entities;

namespace ToDoList.Service.Rules;

public class ToDoBusinessRules
{
    public void ToDoIsNullCheck(ToDo toDo)
    {
        if(toDo is null)
        {
            throw new NotFoundException("İlgili todo bulunamadı.");
        }
    }
}