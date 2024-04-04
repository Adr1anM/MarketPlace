using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Paints.Responses
{
    public class PaintDTO
    {
        public int  Id { get; set; }
        public string Title { get; set; } 
        public required string Artist { get; set; }
        public required decimal Price { get; set; } 

        public static PaintDTO FromPaint(Paint paint)
        {
            return new PaintDTO
            { 
                Id = paint.Id,
                Title = paint.Title,
                Artist = paint.Artist,
                Price = paint.Price,
            };


        }
    }
}
