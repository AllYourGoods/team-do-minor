namespace AllYourGoods.Api.Models
{
    public class OrderHasProduct : BaseEntity
    {
        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; } 

        public int Amount { get; set; }

    }
}