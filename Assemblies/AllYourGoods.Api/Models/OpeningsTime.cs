using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Enums;

public class OpeningsTime : BaseEntity
{
    public TimeOnly? Opening { get; set; }
    public TimeOnly? Closing { get; set; }
    public Day? Day { get; set; }
    public Guid RestaurantId { get; set; }
    public virtual Restaurant Restaurant { get; set; }

    public OpeningsTime(Guid restaurantId)
    {
        RestaurantId = restaurantId;
    }

    protected OpeningsTime() { }
}