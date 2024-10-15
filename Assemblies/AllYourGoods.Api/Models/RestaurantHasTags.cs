namespace AllYourGoods.Api.Models
{
    public class RestaurantHasTags
    {
        public Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }

        public Guid TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

}
