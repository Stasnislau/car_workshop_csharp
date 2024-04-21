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
}