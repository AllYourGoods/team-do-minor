namespace AllYourGoods.Api.Models.Dtos.Responses;

public class ResponseMenuDto
{
    public string id { get; set; }
    public string name { get; set; }
    public bool active { get; set; }
    public List<CategoryDto> categories { get; set; }
}

public class CategoryDto
{
    public string id { get; set; }
    public string name { get; set; }
    public List<ProductDto> products { get; set; }
}

public class ProductDto
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public string imageUrl { get; set; }
}