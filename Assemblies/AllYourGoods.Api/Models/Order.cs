namespace AllYourGoods.Api.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime? ExpiredOnUTC { get; set; }
        public Guid AddressId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime? ETA { get; set; }
        public Guid DeliveryPersonId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual User Customer { get; set; }
        public virtual Address Address { get; set; }
        public virtual User DeliveryPerson { get; set; }

        public virtual ICollection<OrderHasProduct> OrderProducts { get; set; }
    }

}
