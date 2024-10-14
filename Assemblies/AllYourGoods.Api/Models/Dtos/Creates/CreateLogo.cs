using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Creates;

public class CreateLogo
{
    public CreateLogo(string url, string mimeType, double fileSize)
    {
        Url = url;
        MimeType = mimeType;
        FileSize = fileSize;
    }

    [Required]
    [StringLength(255, ErrorMessage = "Url cannot exceed 255 characters.")]
    public string Url { get; set; }

    [StringLength(255, ErrorMessage = "AltText cannot exceed 255 characters.")]
    public string? AltText { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "MimeType cannot exceed 255 characters.")]
    public string MimeType { get; set; }

    [Required]
    public double FileSize { get; set; }
}
