using Microsoft.AspNetCore.Identity;

namespace  car_workshop_csharp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }


    public class ApplicationUser : IdentityUser
    {
        public Employee Employee { get; set; }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<TimeSlot> TimeSlots { get; set; }
    }

    public class Ticket
    {
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
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}