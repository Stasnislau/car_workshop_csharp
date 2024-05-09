using System.Linq;
using System.Threading.Tasks;
using car_workshop_csharp;
using car_workshop_csharp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

public class DbInitializer
{
    public static async Task InitializeAsync(IServiceScope serviceScope)
    {
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!context.Roles.Any())
        {
            await SeedRoles(roleManager);
        }

        if (!context.Users.Any())
        {
            await SeedUsers(userManager, roleManager);
        }
    }

    public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[]
        {
            new IdentityRole("Admin"),
            new IdentityRole("Worker"),
        };

        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
    }

    public static async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var users = new[]
        {
            new ApplicationUser
            {
                UserName = "admin",
                Employee = new Employee
                {
                    Name = "Admin",
                    HourlyRate = 0
                }
            },
            new ApplicationUser
            {
                UserName = "rab1",
                Employee = new Employee
                {
                    Name = "Akim",
                    HourlyRate = 50
                }
            },
            new ApplicationUser
            {
                UserName = "rab2",
                Employee = new Employee
                {
                    Name = "Fiodar",
                    HourlyRate = 30
                }
            }
        };

        foreach (var user in users)
        {
            var existingUser = await userManager.FindByNameAsync(user.UserName);
            if (existingUser == null)
            {
                var createUserResult = await userManager.CreateAsync(user, "123ZaZ!");
                if (createUserResult.Succeeded)
                {
                    if (user.UserName == "admin")
                    {
                        await AssignAdminRole(userManager, roleManager);
                    }
                }
            }
        }
    }

    private static async Task AssignAdminRole(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync("admin");
        var adminUser = await userManager.FindByNameAsync("admin");
       

        if (adminUser != null && adminRole != null && !await userManager.IsInRoleAsync(adminUser, adminRole.Name))
        {
            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
        }
    }
}
