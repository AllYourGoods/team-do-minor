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
    public async Task GetRestaurants_ShouldReturnRestaurants_WithAllPropertiesMappedCorrectly()
    {
        // Arrange
        var restaurantId1 = Guid.NewGuid();
        var restaurantId2 = Guid.NewGuid();

        // Creating mock data for Restaurant entities
        var restaurants = new List<Restaurant>
    {
        new ()
        {
            Id = restaurantId1,
            Name = "Restaurant 1",
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 0),
            StreetName = "Main St",
            HouseNumber = "123",
            Description = "A nice place",
            Radius = 5.0,
            ImageLink = "http://example.com/image1.jpg",
            Tags = new List<Tag>
            {
                new () { Id = Guid.NewGuid(), Name = "Italian" },
                new () { Id = Guid.NewGuid(), Name = "Pizza" }
            }
        },
        new ()
        {
            Id = restaurantId2,
            Name = "Restaurant 2",
            OpeningTime = new TimeOnly(11, 0),
            ClosingTime = new TimeOnly(23, 0),
            StreetName = "Second St",
            HouseNumber = "456",
            Description = "Another great place",
            Radius = 3.5,
            ImageLink = "http://example.com/image2.jpg",
            Tags = new List<Tag>
            {
                new () { Id = Guid.NewGuid(), Name = "Mexican" }
            }
        }
    };

        // Setting up mock repository to return the list of restaurants
        _mockRepository.Setup(x => x.GetRestaurants()).ReturnsAsync(restaurants);

        // Act
        var result = (await _restaurantService.GetRestaurants()).ToList();

        // Assert that the total number of restaurants returned is correct
        Assert.That(result.Count, Is.EqualTo(2));

        // Asserting the first restaurant's properties
        var restaurant1Dto = result.FirstOrDefault(r => r.Name == "Restaurant 1");
        Assert.That(restaurant1Dto, Is.Not.Null);
        Assert.That(restaurant1Dto.Id, Is.EqualTo(restaurantId1));
        Assert.That(restaurant1Dto.Name, Is.EqualTo("Restaurant 1"));
        Assert.That(restaurant1Dto.OpeningTime, Is.EqualTo(new TimeOnly(10, 0)));
        Assert.That(restaurant1Dto.ClosingTime, Is.EqualTo(new TimeOnly(22, 0)));
        Assert.That(restaurant1Dto.StreetName, Is.EqualTo("Main St"));
        Assert.That(restaurant1Dto.HouseNumber, Is.EqualTo("123"));
        Assert.That(restaurant1Dto.Description, Is.EqualTo("A nice place"));
        Assert.That(restaurant1Dto.Radius, Is.EqualTo(5.0));
        Assert.That(restaurant1Dto.ImageLink, Is.EqualTo("http://example.com/image1.jpg"));
        Assert.That(restaurant1Dto.Tags, Is.Not.Null);
        Assert.That(restaurant1Dto.Tags, Is.EqualTo(new List<string> { "Italian", "Pizza" }));

        // Asserting the second restaurant's properties
        var restaurant2Dto = result.FirstOrDefault(r => r.Name == "Restaurant 2");
        Assert.That(restaurant2Dto, Is.Not.Null);
        Assert.That(restaurant2Dto.Id, Is.EqualTo(restaurantId2));
        Assert.That(restaurant2Dto.Name, Is.EqualTo("Restaurant 2"));
        Assert.That(restaurant2Dto.OpeningTime, Is.EqualTo(new TimeOnly(11, 0)));
        Assert.That(restaurant2Dto.ClosingTime, Is.EqualTo(new TimeOnly(23, 0)));
        Assert.That(restaurant2Dto.StreetName, Is.EqualTo("Second St"));
        Assert.That(restaurant2Dto.HouseNumber, Is.EqualTo("456"));
        Assert.That(restaurant2Dto.Description, Is.EqualTo("Another great place"));
        Assert.That(restaurant2Dto.Radius, Is.EqualTo(3.5));
        Assert.That(restaurant2Dto.ImageLink, Is.EqualTo("http://example.com/image2.jpg"));
        Assert.That(restaurant2Dto.Tags, Is.Not.Null);
        Assert.That(restaurant2Dto.Tags, Is.EqualTo(new List<string> { "Mexican" }));
    }

}
