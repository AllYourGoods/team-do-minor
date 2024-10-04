namespace AllYourGoods.Api.Models.Dtos
{
    public class UpdateRestaurantDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AboutUs { get; set; }
        public float Radius { get; set; } // in km
        public AddressDto Address { get; set; }
        public BannerDto Banner { get; set; }
        public LogoDto Logo { get; set; }
        public Guid OwnerID { get; set; }
    }

    public class AddressDto
    {
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
    }

    public class BannerDto
    {
        public string URL { get; set; }
        public string AltText { get; set; }
        public string MimeType { get; set; }
        public float FileSize { get; set; } // in bytes
    }

    public class LogoDto
    {
        public string URL { get; set; }
        public string AltText { get; set; }
        public string MimeType { get; set; }
        public float FileSize { get; set; } // in bytes
    }

}
