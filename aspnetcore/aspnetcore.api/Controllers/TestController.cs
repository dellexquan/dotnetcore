using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public Person GetPerson()
    {
        return new Person("Dellex", 18);
    }
    [HttpPost]
    public string[] SaveNote(SaveNoteRequest req)
    {
        System.IO.File.WriteAllText(req.Title + ".txt", req.Content);
        return new string[] { "ok", req.Title };
    }
}