using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models
{
    public class Order : Entity
    {
        [ForeignKey(nameof(Buyer))]
        public int CretedById { get; set; } 
        public User Buyer { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PromocodeId { get; set; }
        public Promocode Promocode { get; set; }
        public int Quantity { get; set; }

        [MaxLength(100)]
        public string ShippingAdress { get; set; }
        public int Status { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal TotalPrice { get; set; }
        public List<ProductOrder> ProductOrders { get; } = [];
    }
}
