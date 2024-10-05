namespace AllYourGoods.Api.Models.Dtos
{
    public class AddressDto
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string HouseNumber { get; set; } = null!; 
        public string ZipCode { get; set; } = null!;     
        public string City { get; set; } = null!;        
        public string StreetName { get; set; } = null!;  
    }
}
