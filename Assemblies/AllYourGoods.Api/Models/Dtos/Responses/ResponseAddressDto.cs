namespace AllYourGoods.Api.Models.Dtos.Responses;

public class ResponseAddressDto
{
    public ResponseAddressDto(string houseNumber, string zipCode, string city, string streetName)
    {
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        City = city;
        StreetName = streetName;
    }

    public Guid Id { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string StreetName { get; set; }
}
