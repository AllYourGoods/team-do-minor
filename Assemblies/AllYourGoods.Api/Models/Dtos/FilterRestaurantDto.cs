namespace AllYourGoods.Api.Models.Dtos;

public class FilterRestaurantDto
{
    public TimeOnly? OpeningTime { get; set; } 
    public TimeOnly? ClosingTime { get; set; } 
    public string? Name { get; set; } 
    public double? Radius { get; set; } 
    public List<string>? Tags { get; set; } 
    public int CurrentPage { get; set; } = 1; 
    public int PageSize { get; set; } = 10; 
    public string SortBy { get; set; } = "Name"; 
    public string SortDirection { get; set; } = "asc"; 
}
