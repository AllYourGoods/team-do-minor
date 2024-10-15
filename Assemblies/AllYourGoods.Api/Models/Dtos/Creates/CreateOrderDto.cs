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
        public Guid? RestaurantId { get; set; }
        public Guid? CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public string? Note { get; set; }
        public CreateAddress Address { get; set; }
        public List<CreateOrderHasProduct> OrderHasProduct { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public double ETA { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderStatus Status { get; set; }
    
}

}
