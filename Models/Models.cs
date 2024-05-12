using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace car_workshop_csharp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }

        public List<TimeSlot> TimeSlots { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
    public class TicketsOverviewViewModel
    {
        public List<TicketViewModel> Tickets { get; set; }
    }
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationId { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }

        public List<Part> Parts { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }


    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }

    }
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }

        public string UserId { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }

        public ApplicationUser User { get; set; }

    }

    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationId { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public List<TimeSlot> TimeSlots { get; set; }
        public List<Part> Parts { get; set; }
    }

    public class TimeSlot
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }


    public class Part
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
    public class TicketDTO
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationId { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }
    }

    public class ExtendedTicketDTO : Ticket
    {
        public decimal TotalPrice { get; set; }

        public ExtendedTicketDTO(Ticket ticket)
        {
            Id = ticket.Id;
            Brand = ticket.Brand;
            Model = ticket.Model;
            RegistrationId = ticket.RegistrationId;
            ProblemDescription = ticket.ProblemDescription;
            Status = ticket.Status;
            EmployeeId = ticket.EmployeeId;
            TimeSlots = ticket.TimeSlots;
            Parts = ticket.Parts;
            Employee = ticket.Employee;
            TotalPrice = ticket.Parts.Sum(p => p.TotalPrice) + ticket.TimeSlots.Sum(ts => (ts.EndTime - ts.StartTime).Hours * ticket.Employee.HourlyRate);
        }
    }

    public class EdtiTicketViewModel
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegistrationId { get; set; }
        public string ProblemDescription { get; set; }
        public string Status { get; set; }

        public List<Part> Parts { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

    public class PartDTO
    {
        
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
