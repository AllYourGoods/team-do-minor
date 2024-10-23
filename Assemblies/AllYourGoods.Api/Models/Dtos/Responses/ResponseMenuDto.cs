namespace AllYourGoods.Api.Models.Dtos.Responses;

public class ResponseMenuDto
{
    public ResponseMenuDto(string id, string name, bool active, List<CategoryDto> categories)
    {
        Id = id;
        Name = name;
        Active = active;
        Categories = categories;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public bool Active { get; set; }
    public List<CategoryDto> Categories { get; set; }
}

public class CategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<ProductDto> Products { get; set; }
}

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public string imageUrl { get; set; }
}