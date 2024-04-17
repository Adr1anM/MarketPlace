using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class AuthorCategory : Entity
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public List<AuthorAuthorCategory> AuthorAuthorCategories { get; } = [];   

    }
}
