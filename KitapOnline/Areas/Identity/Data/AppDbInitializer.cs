using Azure.Core;
using System.Security.Policy;
using System.Text;
using System.Text.Encodings.Web;
using KitapOnline;
using KitapOnline.Areas.Identity;
using KitapOnline.Migrations;
using KitapOnline.Models.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;


namespace KitapOnline
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                ////Cinema
                //if (!context.Cinemas.Any())
                //{
                //    context.Cinemas.AddRange(new List<Cinema>()
                //    {
                //        new Cinema()
                //        {
                //            Name = "Cinema 1",
                //            Logo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                //            Description = "This is the description of the first cinema"
                //        },
                //        new Cinema()
                //        {
                //            Name = "Cinema 2",
                //            Logo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                //            Description = "This is the description of the first cinema"
                //        },
                //        new Cinema()
                //        {
                //            Name = "Cinema 3",
                //            Logo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                //            Description = "This is the description of the first cinema"
                //        },
                //        new Cinema()
                //        {
                //            Name = "Cinema 4",
                //            Logo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                //            Description = "This is the description of the first cinema"
                //        },
                //        new Cinema()
                //        {
                //            Name = "Cinema 5",
                //            Logo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                //            Description = "This is the description of the first cinema"
                //        },
                //    });
                //    context.SaveChanges();
                //}
             
            }

        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(ApplicationRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Admin));
                if (!await roleManager.RoleExistsAsync(ApplicationRoles.Normal))
                    await roleManager.CreateAsync(new IdentityRole(ApplicationRoles.Normal));

                //Users
                var _userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string adminUserEmail = "admin@kitaponline.com";
                var adminUser = await _userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var user = new IdentityUser { EmailConfirmed = true, Email = adminUserEmail, UserName = adminUserEmail };

                    var result = await _userManager.CreateAsync(user, "Admin123_");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }

                }
                string appUserEmail = "user@kitaponline.com";


                var normalUser = await _userManager.FindByEmailAsync(appUserEmail);
                if (normalUser == null)
                {
                    var user = new IdentityUser { EmailConfirmed = true, Email = appUserEmail, UserName = appUserEmail };

                    var result = await _userManager.CreateAsync(user, "User123_");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Normal);
                    }

                }
            }
        }
    }
}