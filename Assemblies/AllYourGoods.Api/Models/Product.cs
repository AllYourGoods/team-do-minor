namespace AllYourGoods.Api.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool NotAvailable { get; set; } = false;
        public Guid ImageFileId { get; set; }
        public decimal Price { get; set; }

        public virtual ImageFile? ImageFile { get; set; }  
        public virtual ICollection<CategoryHasProduct> CategoryProducts { get; set; } = new List<CategoryHasProduct>();
        public virtual ICollection<ProductHasTag> ProductTags { get; set; } = new List<ProductHasTag>();
    }
}