using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class AuthorAuthorCategory : Entity
    {
        public int? AuthorId { get; set; }   
        public Author? Author { get; set; }
        public int? AuthorCategoryId { get; set; }  
        public AuthorCategory? AuthorCategory { get; set;}  
    }
}
