using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.App.Orders.Responses
{
    public class OrderDto
    {
        public int Id { get; set; } 
        public int CretedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public int PromocodeId { get; set; }
        public int Quantity { get; set; }
        public string ShippingAdress { get; set; }
        public int StatusId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
