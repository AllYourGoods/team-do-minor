using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Creates;

public class CreateRestaurantDto
{
    public CreateRestaurantDto(string name, string phoneNumber, CreateAddress address, CreateBanner banner, CreateLogo logo)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Address = address;
        Banner = banner;
        Logo = logo;
    }

    [Required]
    [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
    public string Name { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "PhoneNumber cannot exceed 255 characters.")]
    public string PhoneNumber { get; set; }
    
    [StringLength(255, ErrorMessage = "AboutUs cannot exceed 255 characters.")]
    public string? AboutUs { get; set; }
    
    public double? Radius { get; set; }
    public CreateAddress Address { get; set; }
    public CreateBanner Banner { get; set; }
    public CreateLogo Logo { get; set; }
    public Guid? OwnerId { get; set; }
}
