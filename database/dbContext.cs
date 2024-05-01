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
        modelBuilder.Entity<Employee>().ToTable("Employee");
        modelBuilder.Entity<Ticket>().ToTable("Ticket");
        modelBuilder.Entity<TimeSlot>().ToTable("TimeSlot");
        modelBuilder.Entity<Part>().ToTable("Part");

        modelBuilder.Entity<Employee>().HasKey(e => e.Id);
        modelBuilder.Entity<Ticket>().HasKey(t => t.Id);
        modelBuilder.Entity<TimeSlot>().HasKey(ts => ts.Id);
        modelBuilder.Entity<Part>().HasKey(p => p.Id);

        modelBuilder.Entity<Employee>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Ticket>().Property(t => t.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<TimeSlot>().Property(ts => ts.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Part>().Property(p => p.Id).ValueGeneratedOnAdd();

    }


}