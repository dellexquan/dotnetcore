using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculator calculator;

    public CalculatorController(ICalculator calculator)
    {
        this.calculator = calculator;
    }

    [HttpGet]
    public int Add(int i1, int i2)
    {
        return calculator.Add(i1, i2);
    }
}