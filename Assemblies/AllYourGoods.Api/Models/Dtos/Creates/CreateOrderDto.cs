using System.ComponentModel.DataAnnotations;
using System.Net;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderDto
    {

        public CreateOrderDto(CreateAddress  address , string phoneNumber , string email, int restaurantId 
            ,int deliveryPersonId, int customerId , List<OrderHasProduct> orderHasProduct) {

            Address = address;
            PhoneNumber = phoneNumber;
            Email = email;
            RestaurantId = restaurantId;
            DeliveryPersonId = deliveryPersonId;
            CustomerId = customerId;
            OrderHasProduct = orderHasProduct;

        }
        [Required]
        public int RestaurantId { get; set; }
        [Required]
        public int DeliveryPersonId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        
        public PaymentMethod? PaymentMethod { get; set; }

        public CreateAddress Address { get; set; } = null!;
        public double? TotalPrice { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Note { get; set; }
        public List<OrderHasProduct> OrderHasProduct { get; set; }
    }
}
