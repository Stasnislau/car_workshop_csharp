using car_workshop_csharp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace car_workshop_csharp
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.UserId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Tickets)
                .WithOne(t => t.Employee)
                .HasForeignKey(t => t.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.TimeSlots)
                .WithOne(ts => ts.Employee)
                .HasForeignKey(ts => ts.EmployeeId);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.TimeSlots)
                .WithOne(ts => ts.Ticket)
                .HasForeignKey(ts => ts.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Parts)
                .WithOne(p => p.Ticket)
                .HasForeignKey(p => p.TicketId);

            modelBuilder.Entity<TimeSlot>()
                .HasOne(ts => ts.Ticket)
                .WithMany(t => t.TimeSlots)
                .HasForeignKey(ts => ts.TicketId);

            modelBuilder.Entity<TimeSlot>()
                .HasOne(ts => ts.Employee)
                .WithMany(e => e.TimeSlots)
                .HasForeignKey(ts => ts.EmployeeId);

            modelBuilder.Entity<Part>()
                .HasOne(p => p.Ticket)
                .WithMany(t => t.Parts)
                .HasForeignKey(p => p.TicketId);
        }
    }


}