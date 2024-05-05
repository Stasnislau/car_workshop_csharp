using Microsoft.EntityFrameworkCore;

using car_workshop_csharp.Models;


public class EmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployee(int id)
    {
        return await _context.Employees.FirstOrDefaultAsync(employee => employee.Id == id);
    }

    public async Task AddEmployee(AddEmployeeViewModel employee)
    {
        _context.Employees.Add(new Employee
        {
            Name = employee.Name,
            HourlyRate = employee.HourlyRate
        });
        await _context.SaveChangesAsync();
    }

    public async Task UpdateEmployee(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }
}