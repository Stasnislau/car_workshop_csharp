using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]

public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<OkObjectResult> GetEmployees()
    {
        try 
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }
        catch (Exception e)
        {
            return (OkObjectResult)StatusCode(500, e.Message);
        }
    }

}
