public class Employee
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int HourlyRate { get; set; }
}

public class Ticket 
{
    public int Id { get; set; }

    public required string Brand { get; set; }

    public required string Model { get; set; }

    public required string RegistrationId { get; set; }
    public required string ProblemDescription { get; set; }
    public int EmployeeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string EstimateDescription { get; set; }

    public string EstimateCost { get; set; }

    public bool EstimateAccepted { get; set; }

    public float PricePaid { get; set; }


}

public class TimeSlot
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }

    public int TicketId { get; set; }

}

public class Part 
{
    public int Id { get; set; }
    public string Name { get; set; }

    public float Amount { get; set; }

    public float UnitPrice { get; set; }

    public float TotalPrice { get; set; }

    public int TicketId { get; set; }

}


