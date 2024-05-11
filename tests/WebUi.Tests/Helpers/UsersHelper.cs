using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers  
{
    public static class UsersHelper
    {
        public static string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?";

            var random = new Random();

            var password = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }


        public static void SeedData(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();


            if (!userManager.Users.Any())
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }
          
        }

        private static void SeedRoles(RoleManager<Role> roleManager)
        {
            var roles = new[] { "Admin", "User" };

            foreach (var roleName in roles)
            {      
                roleManager.CreateAsync(new Role { Name = roleName}).Wait();
            }
        }

        private static void SeedUsers(UserManager<User> userManager)
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = 2,
                    UserName = "user1@example.com",
                    Email = "user1@example.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                },
            };

            foreach (var user in users)
            {
                userManager.CreateAsync(user, GenerateRandomPassword()).Wait();
                userManager.AddToRoleAsync(user, "User").Wait(); 
              
            }
        }

        public static async Task ClearUsers(UserManager<User> userManager)
        {
            var users = await userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                await userManager.DeleteAsync(user);
            }
        }
    }
}
