namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderHasProduct
    {
        public CreateOrderHasProduct()
        {
        }

        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
    }
}