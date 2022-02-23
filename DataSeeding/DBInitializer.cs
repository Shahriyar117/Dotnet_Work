using Employee_Directory.DBContext;
using Microsoft.AspNetCore.Identity;

namespace Employee_Directory.DataSeeding
{
    public class DBInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {

            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                var _userManager =
                         serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var _roleManager =
                         serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                if (!context.Users.Any(usr => usr.UserName == "admin@gmail.com"))
                {
                    var user = new IdentityUser()
                    {
                        UserName = "admin@gmail.com",
                        Email = "admin@gmail.com",
                        EmailConfirmed = true,
                    };

                    var userResult = _userManager.CreateAsync(user, "Admin*123").Result;
                }

                if (!_roleManager.RoleExistsAsync("Admin").Result)
                {
                    var role = _roleManager.CreateAsync
                               (new IdentityRole { Name = "Admin" }).Result;
                }

                var adminUser = _userManager.FindByNameAsync("admin@gmail.com").Result;
                var userRole = _userManager.AddToRolesAsync
                               (adminUser, new string[] { "Admin" }).Result;

                context.SaveChanges();
            }
        }

    }
}
