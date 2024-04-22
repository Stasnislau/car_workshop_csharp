using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using car_workshop_csharp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace car_workshop_csharp.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public IActionResult Index()
    {
        return View();
    }
}

