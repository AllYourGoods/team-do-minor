namespace AllYourGoods.Api.Models
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }    
        public double Price { get; set; }

        public ICollection<OrderHasProduct>? OrderHasProduct { get; set; }
    }
}
