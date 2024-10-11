using AllYourGoods.Api.Models.Enums;

namespace AllYourGoods.Api.Models;

public class OpeningsTime : BaseEntity
{
    public TimeOnly? Opening { get; set; }
    public TimeOnly? Closing { get; set; }
    public Day? Day { get; set; }

}
