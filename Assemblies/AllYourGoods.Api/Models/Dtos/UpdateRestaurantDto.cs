namespace AllYourGoods.Api.Models.Dtos
{
    public class UpdateRestaurantDto
    {
        public string Name { get; set; } = null!;          
        public string PhoneNumber { get; set; } = null!;   
        public string AboutUs { get; set; } = null!;       
        public float Radius { get; set; }                  
        public AddressDto Address { get; set; } = null!;   
        public BannerDto Banner { get; set; } = null!;     
        public LogoDto Logo { get; set; } = null!;         
        public Guid OwnerID { get; set; }                  
    }
}
