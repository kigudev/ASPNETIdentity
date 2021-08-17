using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETIdentity.Data
{
    /// <summary>
    /// El seed va a llamarse cada vez que el sistema vaya a correr
    /// </summary>
    public class ApplicationContextSeed
    {
        public static async Task SeedIdentityAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // definir los roles que queremos que existan en el sistema
            //var roles = new List<string> {"Administrator", "Customer", "Supervisor" };
            var roles = Enum.GetNames<Role>();
            // si los roles (o alguno de ellos) no existen
            foreach(var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = role });
                    Console.WriteLine($"El rol {role} se agregó");
                }
            }

            // definir usuario que queramos que sea el administrador
            var adminUserName = "admin@gmail.com";
            var adminEmail = "admin@gmail.com";
            var adminPassword = "qweqwe";
            // si el usuario no existe
            var user = await userManager.FindByNameAsync(adminUserName);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail
                };
                // agregar
                await userManager.CreateAsync(user, adminPassword);
                // agregar rol
                await userManager.AddToRoleAsync(user, Role.Administrator.ToString());
            }

        }
    }
}
