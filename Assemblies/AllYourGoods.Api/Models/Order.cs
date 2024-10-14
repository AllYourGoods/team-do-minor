using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class Order : BaseEntity
{
 
    public  Guid RestaurantId { get; set; }
    public Guid CustomerId { get; set; }
    public double TotalPrice { get; set; }
    public string? Note { get; set; }
    public Guid? AddressId {  get; set; }
    public Guid OrderHasProductId { get; set; }
    public Guid DeliveryPersonId { get; set; }
    public double ETA { get; set; }

    // Navigation properties
    public virtual List<OrderHasProduct>?OrderHasProduct { get; set; } 
    public virtual PaymentMethod PaymentMethod { get; set; }
    public virtual OrderStatus Status { get; set; }
    public virtual Address Address { get; set; } = null!;
    public virtual Restaurant Restaurant { get; set; } = null!;

}


