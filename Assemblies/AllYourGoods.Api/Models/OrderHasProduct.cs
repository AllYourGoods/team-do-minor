namespace AllYourGoods.Api.Models;

    public class OrderHasProduct : BaseEntity
    { 
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }

       // Navigation properties
       public Product product { get; set; } = null!;
       public Order Order { get; set; } = null!;
}



