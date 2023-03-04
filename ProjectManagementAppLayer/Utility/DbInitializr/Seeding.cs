using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using System.Linq;

namespace ProjectManagementAppLayer.Utility.DbInitializr
{
    public static class Seeding
    {
        public static void InitializeAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
               var userManager = serviceScope.ServiceProvider.GetService<UserManager<Person>>();
               var userRole = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
               var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate();
                }
                if (!userRole.RoleExistsAsync(WebRoles.Admin).GetAwaiter().GetResult())
                {
                    userRole.CreateAsync(new IdentityRole(WebRoles.Admin))
                        .GetAwaiter().GetResult();
                    userRole.CreateAsync(new IdentityRole(WebRoles.ProjectManager))
                        .GetAwaiter().GetResult();
                    userRole.CreateAsync(new IdentityRole(WebRoles.ProjectDirector))
                        .GetAwaiter().GetResult();
                }
                var admin = new Admin()
                {
                    Email = "Admin@email.com",
                    FullName = "Admin",
                    ImageUrl = "userPics.png",
                    IsLoggedIn = false,
                    UserName = "Admin@email.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0797654310",
                };
                 userManager.CreateAsync(admin, "Admin1234*").GetAwaiter().GetResult();
                 var user = userManager.FindByEmailAsync(admin.Email);
                 userManager.AddToRoleAsync(admin, WebRoles.Admin).GetAwaiter().GetResult();
            }
        }
    }
}
