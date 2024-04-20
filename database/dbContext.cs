using Microsoft.EntityFrameworkCore;


public class ApplicationDbContext : DbContext
{

    private IConfiguration Configuration { get; }

    public ApplicationDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
    }
}