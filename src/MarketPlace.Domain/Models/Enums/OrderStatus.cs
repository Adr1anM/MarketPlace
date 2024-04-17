using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Domain.Models.Enums
{
    public class OrderStatus
    {
        public int Id { get; }
        public string Status { get; }

        private OrderStatus(int id, string status)
        {
            Id = id;
            Status = status;
        }

        public static readonly OrderStatus Processing = new OrderStatus(1, "Processing");
        public static readonly OrderStatus Shipped = new OrderStatus(2, "Shipped");
        public static readonly OrderStatus Delivered = new OrderStatus(3, "Delivered");
        public static readonly OrderStatus Canceled = new OrderStatus(4, "Canceled");
    }

}
