using AllYourGoods.Api.Models.Enums;
using AllYourGoods.Api.Models.Dtos.Responses;

namespace AllYourGoods.Api.Models.Dtos.Responses;

    public class ResponseOrderDto
    { 
        public Guid Id { get; set; }
        public int RestaurantId { get; set; }
        public int DeliveryPersonId { get; set; }
        public string? CreatedOnUtc { get; set; }
        public int CustomerId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public ResponseAddressDto Address { get; set; } = null!;
        public double? TotalPrice { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public List<ResponseOrderHasProductDto>? OrderHasProduct { get; set; }


    

}

