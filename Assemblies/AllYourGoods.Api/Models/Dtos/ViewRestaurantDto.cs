namespace AllYourGoods.Api.Models.Dtos;

public class ViewRestaurantDto
{
    public Guid Id { get; set; }
    public TimeOnly? OpeningTime { get; set; }
    public TimeOnly? ClosingTime { get; set; }
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public List<string>? Tags { get; set; }
    public double? Radius { get; set; }
    public string? ImageLink { get; set; }
}
