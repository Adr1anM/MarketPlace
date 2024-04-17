/*using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Sculptures.Responses
{
    public class SculptureDTO
    {
        public int Id { get; set; } 
        public required string Artist { get; set; } 
        public required decimal Price { get; set; }  
        
        public static SculptureDTO FromSculpture(Sculpture scupture)
        {
            return new SculptureDTO
            {
                Id = scupture.Id,
                Artist = scupture.Artist,
                Price = scupture.Price, 
            };

        }

    }
}
*/