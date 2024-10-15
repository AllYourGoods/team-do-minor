namespace AllYourGoods.Api.Models
{
    public class RestaurantHasTags : BaseEntity
    {
        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; } = null!;
        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; } = null!;
    }
}