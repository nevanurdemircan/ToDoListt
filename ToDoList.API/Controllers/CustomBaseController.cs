using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.API.Controllers;


public class CustomBaseController : ControllerBase
{
    public string GetUserId()
    {
        return HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
    }

   
}