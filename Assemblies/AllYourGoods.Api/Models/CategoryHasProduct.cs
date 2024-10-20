namespace AllYourGoods.Api.Models
{
    public class CategoryHasProduct : BaseEntity
    {
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}