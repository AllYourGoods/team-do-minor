using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models;

public class Address: BaseEntity
{
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    [StringLength(6)]
    public string? HouseNumber { get; set; }

    [StringLength(5)]
    public string? ZipCode { get; set; }

    [StringLength(255)]
    public string? City { get; set; }

    [StringLength(255)]
    public string? StreetName { get; set; }


}
