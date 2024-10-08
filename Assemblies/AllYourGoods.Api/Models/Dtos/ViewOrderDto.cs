namespace AllYourGoods.Api.Models;

public class ViewOrderDto
{
    public Guid OrderId { get; set; }
    public int RestaurantId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public double? TotalPrice { get; set; }
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; } 
    public string? ZipCode { get; set; } 
    public string? Email { get; set; } 
    public string? PhoneNumber { get; set; } 
    public string? Note { get; set; }
    public List<string>? MenuItems { get; set; }

}
