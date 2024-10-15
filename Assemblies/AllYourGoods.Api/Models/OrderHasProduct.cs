namespace AllYourGoods.Api.Models
{
    public class OrderHasProduct : BaseEntity
    {
        public Guid OrderId { get; set; }
        public required virtual Order Order { get; set; }

        public Guid ProductId { get; set; }
        public required virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }

}
