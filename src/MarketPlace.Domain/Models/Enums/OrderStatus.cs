
namespace MarketPlace.Domain.Models.Enums
{
    public class OrderStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }

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
