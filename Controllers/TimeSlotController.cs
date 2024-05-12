using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using car_workshop_csharp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;


namespace car_workshop_csharp.Controllers
{
    [Authorize]
    public class TimeSlotController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TimeSlotController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Add(int ticketId)
        {
            ViewData["TicketId"] = ticketId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(int id, [Bind("StartTime, EndTime")] TimeSlotDTO slots)
        {

            if (ModelState.IsValid)
            {
                var ticket = _context.Tickets.FirstOrDefault(t => t.Id == id);
                if (ticket == null)
                {
                    return NotFound();
                }
                var timeSlot = new TimeSlot
                {
                    StartTime = slots.StartTime.ToUniversalTime(),
                    EndTime = slots.EndTime.ToUniversalTime(),
                    TicketId = id,
                    EmployeeId = ticket.EmployeeId
                };
                _context.TimeSlots.Add(timeSlot);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Ticket");
            }
            return View(slots);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var timeSlot = _context.TimeSlots.FirstOrDefault(t => t.Id == id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            return View(timeSlot);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeSlot = _context.TimeSlots.FirstOrDefault(t => t.Id == id);
            if (timeSlot == null)
            {
                return NotFound();
            }
            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ticket");
        }
    }
}
