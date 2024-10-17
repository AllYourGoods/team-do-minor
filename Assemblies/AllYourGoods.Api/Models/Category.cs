namespace AllYourGoods.Api.Models
{
    public class Category: BaseEntity
    { 
        public double Weight { get; set; }
        public string Name { get; set; }
        public Guid MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<CategoryHasProduct> CategoryProducts { get; set; }
    }

}
