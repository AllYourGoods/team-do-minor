namespace AllYourGoods.Api.Models
{
    public class Product : BaseEntity
    {


        public ICollection<OrderHasProduct>? OrderHasProduct { get; set; }
    }
}
