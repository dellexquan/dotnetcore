using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.filter.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class DemoController : ControllerBase
{
    [HttpGet]
    public string Test1()
    {
        return System.IO.File.ReadAllText("ccc.txt");
    }
}
