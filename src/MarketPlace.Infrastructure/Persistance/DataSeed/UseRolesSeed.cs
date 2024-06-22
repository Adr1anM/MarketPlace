using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.DataSeed
{
    public class UseRolesSeed
    {
        public static async Task Seed(RoleManager<Role> roleManager)
        {
            var roles = new List<string> { "Admin", "User", "Author" }; 

            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var role = new Role { Name = roleName };
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
