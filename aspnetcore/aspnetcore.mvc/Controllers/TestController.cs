using aspnetcore.mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.mvc.Controllers;

public class TestController : Controller
{
    public IActionResult Demo1()
    {
        var model = new Person("Dellex", true, new DateTime(1999, 9, 9));
        return View(model);
    }
}