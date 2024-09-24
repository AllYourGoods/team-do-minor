using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Mappings;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Services;
using AutoMapper;
using Moq;

namespace AllYourGoods.Api.UnitTests.Services;

public class RestaurantServiceTests
{
    private readonly Mock<IRestaurantRepository> _mockRepository;
    private readonly RestaurantService _restaurantService;

    public RestaurantServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantMappingProfile>();
        });

        var mapper= config.CreateMapper();
        _mockRepository = new Mock<IRestaurantRepository>();
        _restaurantService = new RestaurantService(_mockRepository.Object, mapper);
    }

    [Test]

    public async Task GetRestaurants_ShouldReturnRestaurants()
    {
        // Arrange
        var restaurants = new List<Restaurant>
        {
            new () { Id = Guid.NewGuid(), Name = "Restaurant 1" },
            new () { Id = Guid.NewGuid(), Name = "Restaurant 2" }
        };

        _mockRepository.Setup(x => x.GetRestaurants()).ReturnsAsync(restaurants);

        // Act
        var result = (await _restaurantService.GetRestaurants()).ToList();
        Assert.That(result.Count, Is.EqualTo(2));

        var restaurant1 = result.FirstOrDefault(r => r.Name == "Restaurant 1");
        Assert.That(restaurant1, Does.Not.EqualTo(null));
        Assert.That(restaurant1.Id, Is.EqualTo(restaurants[0].Id));

        var restaurant2 = result.FirstOrDefault(r => r.Name == "Restaurant 2");
        Assert.That(restaurant2, Does.Not.EqualTo(null));
        Assert.That(restaurant2.Id, Is.EqualTo(restaurants[1].Id));
    }

}
