namespace AllYourGoods.Api.Models
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public bool NotAvailable { get; set; }
        public Guid ImageFileId { get; set; }

        public virtual ImageFile? ImageFile { get; set; }

        public virtual ICollection<CategoryHasProduct> CategoryProducts { get; set; } = new List<CategoryHasProduct>();
        public virtual ICollection<ProductHasTag> ProductTags { get; set; } = new List<ProductHasTag>();
    }
}