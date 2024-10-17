using System.ComponentModel.DataAnnotations;
using System.Net;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderDto
    {
        public  CreateOrderDto(CreateAddress address, List<CreateOrderHasProduct> orderHasProduct)
        {
            Address = address;
            OrderHasProduct = orderHasProduct;
            
        }
        [Required]
        public Guid? RestaurantId { get; set; }
        [Required]
        public Guid? CustomerId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "TotalPrice must be greater than 0.")]
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
        [Required]
        public CreateAddress Address { get; set; }
        [Required]
        public List<CreateOrderHasProduct> OrderHasProduct { get; set; }
        
        public Guid DeliveryPersonId { get; set; }
       
        public double ETA { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }


    }

}
