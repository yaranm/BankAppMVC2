using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BankAppMVC2.Data
{
    public class DataInitializer
    {
        public static void SeedData(BankAppDataContext context, UserManager<IdentityUser> userManager)
        {
            context.Database.Migrate();
            AddRoleIfNotExists(context, "Admin");
            AddRoleIfNotExists(context, "Cashier");
            AddUserIfNotExists(userManager, "stefan.holmberg@systementor.se", "Hejsan123#", new string[] { "Admin" });
            AddUserIfNotExists(userManager, "stefan.holmberg@nackademin.se", "Hejsan123#", new string[] { "Cashier" });

            
        }

        private static void AddRoleIfNotExists(BankAppDataContext context, string role)
        {
            if (context.Roles.Any(r => r.Name == role)) return;
            context.Roles.Add(new IdentityRole { Name = role, NormalizedName = role });
            context.SaveChanges();
        }

        private static void AddUserIfNotExists(UserManager<IdentityUser> userManager,
            string userName, string password, string[] roles)
        {
            if (userManager.FindByEmailAsync(userName).Result != null) return;

            var user = new IdentityUser
            {
                UserName = userName,
                Email = userName,
                EmailConfirmed = true
            };
            var result = userManager.CreateAsync(user, password).Result;
            var r = userManager.AddToRolesAsync(user, roles).Result;
        }

       


    }
}