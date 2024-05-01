using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using car_workshop_csharp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Workshop.Models;

namespace car_workshop_csharp.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddEmployeeViewModel employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeService.AddEmployee(employee);
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employeeService.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeService.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }
        return View(employee);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var employee = await _employeeService.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

}

