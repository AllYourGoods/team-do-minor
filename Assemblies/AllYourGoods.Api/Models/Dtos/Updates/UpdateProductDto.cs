using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Updates
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        public bool NotAvailable { get; set; }

        [Required(ErrorMessage = "ImageFileId is required.")]
        public Guid ImageFileId { get; set; }

        public ICollection<Guid> CategoryIds { get; set; } = new List<Guid>();
        public ICollection<Guid> TagIds { get; set; } = new List<Guid>();
    }
}
