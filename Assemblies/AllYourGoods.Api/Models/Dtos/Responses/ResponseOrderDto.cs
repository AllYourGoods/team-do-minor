using AllYourGoods.Api.Models.Enums;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;

namespace AllYourGoods.Api.Models.Dtos.Responses;

    public class ResponseOrderDto
    {
    public OrderStatus StatusCode { get; set; }
    public string? StatusMessage { get; set; }
    public Guid Id { get; set; }
    public TimeOnly? CreatedOnUTC { get; set; }
    public decimal TotalPrice { get; set; }
    public string? StreetName { get; set; }
    public string? HouseNumber { get; set; }
    public string RestaurantName { get; set; } = null!;
    public ResponseLogoDto Logo { get; set; } = null!;

    public string? Note { get; set; }


}

