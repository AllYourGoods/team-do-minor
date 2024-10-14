using AllYourGoods.Api.Models.Enums;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;

namespace AllYourGoods.Api.Models.Dtos.Responses;

    public class ResponseOrderDto
    {
    public Guid Id { get; set; }
    public OrderStatus StatusCode { get; set; }
    public string? StatusMessage { get; set; }
    public TimeOnly? CreatedOnUTC { get; set; }
    public double TotalPrice { get; set; }
    public ResponseAddressDto Address { get; set; } = null!;
    public ResponseRestaurantDto? RestaurantName { get; set; } = null!;
    public List<ResponseLogoDto> Logo { get; set; } = null!;
    public List<OrderHasProduct>? OrderHasProduct { get; set; } = null!;




}

