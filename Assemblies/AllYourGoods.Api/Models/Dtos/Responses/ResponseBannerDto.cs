namespace AllYourGoods.Api.Models.Dtos.Responses;

public class ResponseBannerDto
{
    public ResponseBannerDto(Guid id, string url, string mimeType, double fileSize)
    {
        Id = id;
        Url = url;
        MimeType = mimeType;
        FileSize = fileSize;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
    public string? AltText { get; set; }
    public string MimeType { get; set; }
    public double FileSize { get; set; }
}
