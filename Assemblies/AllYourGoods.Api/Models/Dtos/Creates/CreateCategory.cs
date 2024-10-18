using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateCategoryDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Category name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required]
        [Range(0, long.MaxValue, ErrorMessage = "Weight must be a non-negative number.")]
        public long Weight { get; set; }

        [Required(ErrorMessage = "MenuId is required.")]
        public Guid MenuId { get; set; }
    }
}
