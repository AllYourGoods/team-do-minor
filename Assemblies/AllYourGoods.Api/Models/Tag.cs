namespace AllYourGoods.Api.Models
{
    public class Tag: BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<ProductHasTag> ProductTags { get; set; }
        public virtual ICollection<RestaurantHasTags> RestaurantTags { get; set; }
    }

}
