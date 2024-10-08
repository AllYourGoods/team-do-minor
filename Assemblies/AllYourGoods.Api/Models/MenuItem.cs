namespace AllYourGoods.Api.Models
{
    public class MenuItem
    { 
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductID { get; set; }
        public long Amount { get; set; }
    }
}
 