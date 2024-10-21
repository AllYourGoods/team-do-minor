using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models.Dtos.Responses
{
    public class ResponseOrderDto
    {
        public Guid Id { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus StatusCode { get; set; }
        public string? Note { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public DateTime ExpiredOnUTC { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal ETA { get; set; }
        public Guid? ShiftId { get; set; }  // Foreign key to Shift table
    }
}