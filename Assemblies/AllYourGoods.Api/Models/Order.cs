using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class Order : BaseEntity
{
 
    public  int RestaurantId { get; set; }
    public int CustomerId { get; set; }
    public string? CreatedOnUtc { get; set; }
    public int AddressId { get; set; }
    public int DeliveryPersonId { get; set; }
    public double TotalPrice { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Note { get; set; }

    // Navigation properties
    public ICollection<OrderHasProduct>? OrderHasProduct { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }

    public  Address Address { get; set; } = null!;
    public DeliveryPerson DeliveryPerson { get; set; } = null!;





}


