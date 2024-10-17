namespace AllYourGoods.Api.Models
{
    public class Menu : BaseEntity
    { 
        public string Name { get; set; }
        public bool Active { get; set; }
        public Guid RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }

}
