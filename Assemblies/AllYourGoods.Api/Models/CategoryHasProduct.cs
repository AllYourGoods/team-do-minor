namespace AllYourGoods.Api.Models
{
    public class CategoryHasProduct
    {
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }

}
