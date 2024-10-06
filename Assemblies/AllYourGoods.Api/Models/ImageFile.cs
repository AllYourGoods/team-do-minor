namespace AllYourGoods.Api.Models;

public class ImageFile : BaseEntity
{
    public string Url { get; set; } = null!;
    public string? AltText { get; set; }
    public string MimeType { get; set; } = null!;
    public double FileSize { get; set; }
}
