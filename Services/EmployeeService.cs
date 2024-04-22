using Microsoft.EntityFrameworkCore;

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
        return await _context.Employees.FirstOrDefaultAsync(employee => employee.id == id);
    }
}