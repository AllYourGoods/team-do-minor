namespace AllYourGoods.Api.Models
{
    public class Order : BaseEntity
    {
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime? ExpiredOnUTC { get; set; }
        public Guid AddressId { get; set; }
        public required string PaymentMethod { get; set; } 
        public DateTime? ETA { get; set; }
        public Guid DeliveryPersonId { get; set; }

        public required virtual Restaurant Restaurant { get; set; }
        public required virtual User Customer { get; set; }
        public required virtual Address Address { get; set; }
        public required virtual User DeliveryPerson { get; set; }

        public virtual ICollection<OrderHasProduct> OrderProducts { get; set; } = new List<OrderHasProduct>();

        public Order(Guid restaurantId, Guid customerId, string paymentMethod, Guid addressId, Guid deliveryPersonId)
        {
            RestaurantId = restaurantId;
            CustomerId = customerId;
            PaymentMethod = paymentMethod;
            AddressId = addressId;
            DeliveryPersonId = deliveryPersonId;

            CreatedOnUTC = DateTime.UtcNow;
            OrderProducts = new List<OrderHasProduct>();
        }
    }
}