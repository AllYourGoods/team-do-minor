using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class OpeningsTime : BaseEntity
{
    public DateOnly? Opening { get; set; }
    public DateOnly? Closing { get; set; }
    public Day? Day { get; set; }
    public Guid RestaurantId { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;

}
