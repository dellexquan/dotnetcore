using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.api.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    public ActionResult<LoginResult> Login(LoginRequest loginReq)
    {
        if (loginReq.UserName == "admin" && loginReq.Password == "123456")
        {
            return new LoginResult
            {
                IsLogin = true,
                Processes = Process.GetProcesses().Select(p => new ProcessInfo
                {
                    Id = p.Id,
                    ProcessName = p.ProcessName,
                    WorkingSet64 = p.WorkingSet64
                }).ToArray()
            };
        }
        else
            return new LoginResult
            {
                IsLogin = false
            };
    }

    public class LoginRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginResult
    {
        public bool IsLogin { get; set; }
        public ProcessInfo[]? Processes { get; set; }
    }

    public class ProcessInfo
    {
        public int Id { get; set; }
        public string ProcessName { get; set; } = null!;
        public long WorkingSet64 { get; set; }
    }
}