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
    public class PartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
        public async Task<IActionResult> Add(int id, [Bind("Name,UnitPrice, Amount")] PartDTO part)
        {

            if (ModelState.IsValid)
            {
                var ticket = await _context.Tickets
                    .Include(t => t.Parts)
                    .FirstOrDefaultAsync(t => t.Id == id);
                if (ticket == null)
                {
                    Console.WriteLine("Ticket not found" + id);
                    return NotFound();
                }

                var newPart = new Part
                {
                    Name = part.Name,
                    UnitPrice = part.UnitPrice,
                    Amount = part.Amount,
                    TotalPrice = part.UnitPrice * part.Amount
                };

                ticket.Parts.Add(newPart);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Ticket");
            }
            return View(part);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            if (part == null)
            {
                return NotFound();
            }
            return View(part);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == id);
            if (part == null)
            {
                return NotFound();
            }
            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Ticket");
        }
    }
}
