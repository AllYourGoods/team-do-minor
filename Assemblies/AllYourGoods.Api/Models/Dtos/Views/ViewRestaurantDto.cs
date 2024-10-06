namespace AllYourGoods.Api.Models.Dtos.Views;

public class ViewRestaurantDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public ViewAddressDto? Address { get; set; }
    public ViewBannerDto? Banner { get; set; }
    public ViewLogoDto? Logo { get; set; }
    public string? AboutUs { get; set; }
    public string? PhoneNumber { get; set; }
    public List<OpeningsTime> OpeningsTimes { get; set; } = null!;
    public double? Radius { get; set; }
    public int? StatusCode { get; set; }
    public string? StatusMessage { get; set; }
}
