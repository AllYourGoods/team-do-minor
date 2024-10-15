namespace AllYourGoods.Api.Models
{
    public class Tag : BaseEntity
    {
        public Guid Name { get; set; }

        public virtual ICollection<ProductHasTag> ProductTags { get; set; } = new List<ProductHasTag>();
        public virtual ICollection<RestaurantHasTags> RestaurantTags { get; set; } = new List<RestaurantHasTags>();
    }
}