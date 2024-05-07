using Microsoft.AspNetCore.Mvc;

namespace car_workshop_csharp.Controllers;
using car_workshop_csharp.Models;


[Route("api/[controller]")]
public class EmployeeController : Controller
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var listOfEmployees = await _employeeService.GetEmployees();
        return View(listOfEmployees);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create(
        AddEmployeeViewModel employee)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    // [HttpGet]
    // public async Task<IActionResult> Edit([BindRequired] int id)
    // {
    //     var employee = await _employeeService.GetEmployee(id);
    //     if (employee == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(employee);
    // }

    // [HttpPost]
    // public async Task<IActionResult> Edit(Employee employee)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         await _employeeService.UpdateEmployee(employee);
    //         return RedirectToAction("Index");
    //     }
    //     return View(employee);
    // }

    // [HttpGet]
    // public async Task<IActionResult> Delete(int id)
    // {
    //     var employee = await _employeeService.GetEmployee(id);
    //     if (employee == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(employee);
    // }

}

