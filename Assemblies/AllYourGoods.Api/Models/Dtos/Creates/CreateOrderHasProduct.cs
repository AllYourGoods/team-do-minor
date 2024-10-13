namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateOrderHasProduct
    {
        public  CreateOrderHasProduct(int amount)
        {
            Amount = amount;
            
        }

        
        public int Amount { get; set; }
        
    }
}
