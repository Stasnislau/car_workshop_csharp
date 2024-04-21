public class Employee
{
    public int id { get; set; }
    public required string name { get; set; }
    public int hourlyRate { get; set; }

    public Employee(int id, string name, int hourlyRate)
    {
        this.id = id;
        this.name = name;
        this.hourlyRate = hourlyRate;
    }
}

public class Ticket 
{
    public int id { get; set; }

    public required string brand { get; set; }

    public required string model { get; set; }

    public required string registrationId { get; set; }
    public required string problemDescription { get; set; }
    public int employeeId { get; set; }

    public DateTime? createdAt { get; set; }

    public string estimateDescription { get; set; }

    public string estimateCost { get; set; }

    public bool estimateAccepted { get; set; }

    public float pricePaid { get; set; }

    public Ticket(int id, string brand, string model, string registrationId, string problemDescription, int employeeId, DateTime? createdAt, string estimateDescription, string estimateCost, bool estimateAccepted, float pricePaid)
    {
        this.id = id;
        this.brand = brand;
        this.model = model;
        this.registrationId = registrationId;
        this.problemDescription = problemDescription;
        this.employeeId = employeeId;
        this.createdAt = createdAt;
        this.estimateDescription = estimateDescription;
        this.estimateCost = estimateCost;
        this.estimateAccepted = estimateAccepted;
        this.pricePaid = pricePaid;
    }
}

public class TimeSlot
{
    public int id { get; set; }
    public DateTime startTime { get; set; }
    public DateTime endTime { get; set; }
    public int employeeId { get; set; }

    public int ticketId { get; set; }

    public TimeSlot(int id, DateTime startTime, DateTime endTime, int employeeId, int ticketId)
    {
        this.id = id;
        this.startTime = startTime;
        this.endTime = endTime;
        this.employeeId = employeeId;
        this.ticketId = ticketId;
    }
}

public class Part 
{
    public int id { get; set; }
    public string name { get; set; }

    public float amount { get; set; }

    public float unitPrice { get; set; }

    public float totalPrice { get; set; }

    public int ticketId { get; set; }

    public Part(int id, string name, float amount, float unitPrice, float totalPrice, int ticketId)
    {
        this.id = id;
        this.name = name;
        this.amount = amount;
        this.unitPrice = unitPrice;
        this.totalPrice = totalPrice;
        this.ticketId = ticketId;
    }
}


