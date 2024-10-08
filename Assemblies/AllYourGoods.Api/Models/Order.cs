namespace AllYourGoods.Api.Models;

public class Order
{
    public Guid OrderId { get; set; }
    public  int RestaurantId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public double TotalPrice { get; set; }
    public string StreetName { get; set; } = null!;
    public string HouseNumber { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Note { get; set; }
    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();


}


public enum PaymentMethod
{
    CreditCard,
    PayPal,
    Cash,
    BankTransfer
}