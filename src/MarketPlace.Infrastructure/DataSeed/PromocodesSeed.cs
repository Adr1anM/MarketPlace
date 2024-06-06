using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.DataSeed
{
    public class PromocodesSeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if(context.Promocodes.Any())
            {
                var promoCodes = new List<Promocode>
                {
                    
                };
                context.Promocodes.AddRange(promoCodes);    
                await context.SaveChangesAsync();   
            }
        }
    }
}
