using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using car_workshop_csharp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace car_workshop_csharp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var employees = _context.Employees.Include(e => e.User).ToList().Where(e => e.User.UserName != "admin").ToList();
            return View(employees);
        }

        [Authorize()]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var employee = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);
            var Tickets = _context.Tickets.Where(t => t.EmployeeId == employee.Id).ToList();
            var TimeSlots = _context.TimeSlots.Where(ts => ts.EmployeeId == employee.Id).ToList();

            var employeeViewModel = new EmployeeViewModel
            {
                Name = employee.Name,
                HourlyRate = employee.HourlyRate,
                Tickets = Tickets,
                TimeSlots = TimeSlots
            };
            return View(employeeViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var employee = _context.Employees.Include(e => e.User).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Name,HourlyRate")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.Name = employee.Name;
            existingEmployee.HourlyRate = employee.HourlyRate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var employee = _context.Employees.Include(e => e.User).FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            _context.Users.Remove(employee.User);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }


}
