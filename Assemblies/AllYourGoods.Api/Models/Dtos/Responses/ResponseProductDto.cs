namespace AllYourGoods.Api.Models.Dtos.Responses;
public class ResponseProductDto
{
    public ResponseProductDto(Guid id, string name, string description, double price, bool notAvailable, Guid imageFileId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        NotAvailable = notAvailable;
        ImageFileId = imageFileId;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool NotAvailable { get; set; }
    public Guid ImageFileId { get; set; }
    public int? StatusCode { get; set; }
    public string? StatusMessage { get; set; }

    // Relationships can be displayed by fetching them in the controller and mapping them into DTOs
    public ICollection<string> CategoryNames { get; set; } = new List<string>();
    public ICollection<string> TagNames { get; set; } = new List<string>();
}
