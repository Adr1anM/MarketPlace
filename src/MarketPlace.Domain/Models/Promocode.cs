using System.ComponentModel.DataAnnotations;


namespace MarketPlace.Domain.Models
{
    public class Promocode : Entity
    {
        [MaxLength(100)]
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public Order Order { get; set; }
    }
}
