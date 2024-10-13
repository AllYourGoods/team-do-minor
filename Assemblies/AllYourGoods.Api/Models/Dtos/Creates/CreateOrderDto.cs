using System.ComponentModel.DataAnnotations;
using System.Net;
using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderDto
    {

        
        [Required]
        public double? TotalPrice { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Note cannot exceed 255 characters.")]
        public string? Note { get; set; }
        public CreateAddress AddressId { get; set; } = null!;
        public CreateOrderHasProduct OrderHasProductId { get; set; } = null!;
        public double ETA { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
        
    }

}
