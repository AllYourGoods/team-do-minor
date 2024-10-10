using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models.Dtos.Creates;

public class CreateAddress
{
    public CreateAddress(string houseNumber, string zipCode, string city, string streetName)
    {
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        City = city;
        StreetName = streetName;
    }

    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    [Required]
    [StringLength(5, ErrorMessage = "HouseNumber cannot exceed 5 characters.")]
    public string HouseNumber { get; set; }

    [Required]
    [StringLength(6, ErrorMessage = "Zipcode cannot exceed 6 characters.")]
    public string ZipCode { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "City cannot exceed 255 characters.")]
    public string City { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "StreetName cannot exceed 255 characters.")]
    public string StreetName { get; set; }
}
