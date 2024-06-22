using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace MarketPlace.Infrastructure.Persistance.Extentions
{
    public static class UserOptionsExtensions
    {
        public static async Task<bool> CheckIfDuplicateUserName(this UserOptions options, string firstName, string lastName, IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
                return await context.Users.AnyAsync(u => u.FirstName == firstName && u.LastName == lastName);
            }
        }
    }
}
