using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Creates
{
    public class CreateTagDto
    {
        [Required]
        [StringLength(255, ErrorMessage = "Tag name cannot exceed 255 characters.")]
        public string Name { get; set; }
    }
}
