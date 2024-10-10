using AllYourGoods.Api.Models.Dtos.Responses;

namespace AllYourGoods.Api.Models.Dtos.Views;

public class ResponseRestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ResponseAddressDto Address { get; set; } = null!;
    public ResponseBannerDto Banner { get; set; } = null!;
    public ResponseLogoDto Logo { get; set; } = null!;
    public string? AboutUs { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public List<OpeningsTime>? OpeningsTimes { get; set; }
    public double Radius { get; set; }
    public int? StatusCode { get; set; }
    public string? StatusMessage { get; set; }
}
