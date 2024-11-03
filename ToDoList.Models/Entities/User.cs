using Microsoft.AspNetCore.Identity;

namespace ToDoList.Models.Entities;

public sealed class User : IdentityUser<string>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
    public List<ToDo> ToDo { get; set; }
}