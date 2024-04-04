using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Photographies.Responses
{
    public class PhotographyDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Artist { get; set; }
        public required decimal Price { get; set; }

        public static PhotographyDTO FromPhotography(Photography photo)
        {
            return new PhotographyDTO
            { 
                Id = photo.Id,  
                Title = photo.Title,
                Artist = photo.Artist,
                Price = photo.Price
            };

        }
    }
}
