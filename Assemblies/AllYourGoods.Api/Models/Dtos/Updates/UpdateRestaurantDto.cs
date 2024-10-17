using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Updates
{
    public class UpdateRestaurantDto
    {
        [Required(ErrorMessage = "Restaurant name is required.")]
        [StringLength(255, ErrorMessage = "Restaurant name cannot exceed 255 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters.")]
        public string PhoneNumber { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "About Us description cannot exceed 1000 characters.")]
        public string? AboutUs { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Radius must be a positive number.")]
        public double Radius { get; set; }
    }
}