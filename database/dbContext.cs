using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{

    private IConfiguration Configuration { get; }

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Ticket> Tickets { get; set; }

    public DbSet<TimeSlot> TimeSlots { get; set; }

    public DbSet<Part> Parts { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Employee>().HasMany(e => e.Tickets).WithOne(t => t.Employee).HasForeignKey(t => t.EmployeeId);
        modelBuilder.Entity<Ticket>().HasMany(t => t.TimeSlots).WithOne(ts => ts.Ticket).HasForeignKey(ts => ts.TicketId);
        modelBuilder.Entity<Ticket>().HasMany(t => t.Parts).WithOne(p => p.Ticket).HasForeignKey(p => p.TicketId);

        modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        modelBuilder.Entity<Ticket>().HasKey(t => t.Id);
        modelBuilder.Entity<TimeSlot>().HasKey(ts => ts.Id);
        modelBuilder.Entity<Part>().HasKey(p => p.Id);

        modelBuilder.Entity<Employee>().Property(e => e.Name).IsRequired();
        modelBuilder.Entity<Employee>().Property(e => e.HourlyRate).IsRequired();

        

        

    }


}