namespace AllYourGoods.Api.Models
{
    public class FrequentlyAskedQuestion: BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }

}
