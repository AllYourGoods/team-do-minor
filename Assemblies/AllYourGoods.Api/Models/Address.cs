using System.ComponentModel.DataAnnotations;

namespace AllYourGoods.Api.Models
{
    public class Address : BaseEntity
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        [StringLength(6)]
        public string HouseNumber { get; set; } = null!;

        [StringLength(10)]
        public string ZipCode { get; set; } = null!;

        [StringLength(255)]
        public string City { get; set; } = null!;

        [StringLength(255)]
        public string StreetName { get; set; } = null!;

        public Address(string houseNumber, string zipCode, string city, string streetName)
        {
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            City = city;
            StreetName = streetName;
        }

        public Address() { }
    }
}