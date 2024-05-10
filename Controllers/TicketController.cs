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
    public class TicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Employee)
                .Include(t => t.Parts)
                .Include(t => t.TimeSlots)
                .ToListAsync();
            var model = new TicketsOverviewViewModel
            {
                Tickets = tickets.Select(t => new TicketViewModel
                {
                    Id = t.Id,
                    Brand = t.Brand,
                    Model = t.Model,
                    RegistrationId = t.RegistrationId,
                    ProblemDescription = t.ProblemDescription,
                    Status = t.Status,
                    Parts = t.Parts,
                    TimeSlots = t.TimeSlots
                }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        [Authorize()]
        public IActionResult Create()
        {
            var employees = _context.Employees.ToList();
            ViewData["Employees"] = employees;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> Create([Bind("Brand,Model,RegistrationId,ProblemDescription,Status")] TicketDTO ticket)
        {


            ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(e => Console.WriteLine(e.ErrorMessage));


            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var employee = _context.Employees.FirstOrDefault(e => e.UserId == user.Id);

                _context.Tickets.Add(new Ticket
                {
                    Brand = ticket.Brand,
                    Model = ticket.Model,
                    RegistrationId = ticket.RegistrationId,
                    ProblemDescription = ticket.ProblemDescription,
                    Status = ticket.Status,
                    EmployeeId = employee.Id
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var employees = _context.Employees.ToList();
            ViewData["Employees"] = employees;

            return View(ticket);
        }

        [HttpGet]
        [Authorize()]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Employee)
                .Include(t => t.Parts)
                .Include(t => t.TimeSlots) // Include TimeSlots to show them in the view
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var ticketViewModel = new TicketViewModel
            {
                Id = ticket.Id,
                Brand = ticket.Brand,
                Model = ticket.Model,
                RegistrationId = ticket.RegistrationId,
                ProblemDescription = ticket.ProblemDescription,
                Status = ticket.Status,
                Parts = ticket.Parts,
                TimeSlots = ticket.TimeSlots
            };

            var employees = _context.Employees.ToList();
            ViewData["Employees"] = employees;

            return View(ticketViewModel);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> Edit(int id, TicketDTO ticketDTO)
        {
            
            ModelState.Values.SelectMany(v => v.Errors).ToList().ForEach(e => Console.WriteLine(e.ErrorMessage));
            if (ModelState.IsValid)
            {
                try
                {
                    var ticket = await _context.Tickets.Include(t => t.Parts).Include(t => t.TimeSlots).FirstOrDefaultAsync(m => m.Id == id);
                    if (ticket != null)
                    {
                        ticket.Brand = ticketDTO.Brand;
                        ticket.Model = ticketDTO.Model;
                        ticket.RegistrationId = ticketDTO.RegistrationId;
                        ticket.ProblemDescription = ticketDTO.ProblemDescription;
                        ticket.Status = ticketDTO.Status;

                        // Update the associated TimeSlots and Parts if necessary

                        _context.Update(ticket);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(id))
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

            var employees = _context.Employees.ToList();
            ViewData["Employees"] = employees;

            return View(ticketDTO);
        }


        // GET: Tickets/Delete/5
        [HttpGet]
        [Authorize()]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Employee)
                .Include(t => t.Parts)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var timeSlots = await _context.TimeSlots.Where(ts => ts.TicketId == ticket.Id).ToListAsync();
            ticket.TimeSlots = timeSlots;

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize()]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.Include(t => t.Parts).Include(t => t.TimeSlots).FirstOrDefaultAsync(m => m.Id == id);
            if (ticket != null)
            {
                // Delete the associated TimeSlots
                _context.TimeSlots.RemoveRange(ticket.TimeSlots);

                // Delete the associated Parts
                _context.Parts.RemoveRange(ticket.Parts);

                // Delete the ticket
                _context.Tickets.Remove(ticket);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
