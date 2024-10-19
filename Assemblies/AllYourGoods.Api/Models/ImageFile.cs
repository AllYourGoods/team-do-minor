using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class ImageFile : BaseEntity
    {
        [Required]
        public string Url { get; set; } = null!;

        public string? AltText { get; set; }

        [Required]
        public string MimeType { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public double FileSize { get; set; }
    }
}