using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models
{
    public class Order : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public virtual User? Customer { get; set; }

        public Guid DeliveryPersonId { get; set; }
        public virtual DeliveryPerson? DeliveryPerson { get; set; }

        public Guid RestaurantId { get; set; }
        public virtual Restaurant? Restaurant { get; set; }

        public Guid AddressId { get; set; }
        public virtual Address? Address { get; set; }

        public decimal TotalPrice { get; set; } = 0; 
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
        public DateTime? DeliveredAt { get; set; } 

        public virtual ICollection<OrderHasProduct> OrderProducts { get; set; } = new List<OrderHasProduct>();
    }
}