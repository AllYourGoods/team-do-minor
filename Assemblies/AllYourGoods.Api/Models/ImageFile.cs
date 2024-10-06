namespace AllYourGoods.Api.Models;

public class ImageFile : BaseEntity
{
    public string? Url { get; set; }
    public string? AltText { get; set; }
    public string? MimeType { get; set; }
    public double? fileSize { get; set; }
}
