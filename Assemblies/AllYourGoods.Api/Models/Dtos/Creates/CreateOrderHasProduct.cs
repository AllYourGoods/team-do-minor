namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderHasProduct
    {
        public  CreateOrderHasProduct( )
        {
          
      
        }

        public Guid Orderid { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        

    }
}
