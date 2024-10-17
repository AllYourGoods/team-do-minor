namespace AllYourGoods.Api.Models
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool NotAvailable { get; set; }
        public Guid ImageFileId { get; set; }

        public virtual ImageFile ImageFile { get; set; }
        public virtual ICollection<CategoryHasProduct> CategoryProducts { get; set; }
        public virtual ICollection<ProductHasTag> ProductTags { get; set; }
    }

}
