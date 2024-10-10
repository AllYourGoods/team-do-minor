namespace AllYourGoods.Api.Models.Dtos.Updates
{
    public class UpdateRestaurantDto
    {
        //TODO Add max string length with error message see restaurantDto
        public string Name { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string AboutUs { get; set; } = null!;
        public float Radius { get; set; }

    }
}
