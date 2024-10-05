using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Mappings;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using AllYourGoods.Api.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        _mockRepository.Setup(x => x.GetRestaurants(null)).ReturnsAsync(restaurants);

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

    [Test]
    public async Task GetRestaurants_ShouldReturnFilteredRestaurants_WhenMultipleFiltersAreApplied()
    {
        // Arrange
        var restaurantId1 = Guid.NewGuid();
        var restaurantId2 = Guid.NewGuid();
        var restaurantId3 = Guid.NewGuid();

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
            },
            new ()
            {
                Id = restaurantId3,
                Name = "Restaurant 3",
                OpeningTime = new TimeOnly(9, 0),  
                ClosingTime = new TimeOnly(22, 0), 
                StreetName = "Third St",
                HouseNumber = "789",
                Description = "An amazing place",
                Radius = 4.0,  
                ImageLink = "http://example.com/image3.jpg",
                Tags = new List<Tag>
                {
                    new () { Id = Guid.NewGuid(), Name = "Mexican" }
                }
            }
        };

 
        var filter = new FilterRestaurantDto
        {
            Name = "Restaurant",
            Radius = 4.5, // Match restaurants with Radius <= 4.5
            Tags = new List<string> { "Mexican" }, // Match only restaurants with "Mexican" tag
            OpeningTime = new TimeOnly(9, 0), // Match only restaurants that open at 9:00 or later
            ClosingTime = new TimeOnly(22, 0) // Match only restaurants that close at 22:00 or earlier
        };

        // Set up mock repository to return filtered restaurants
        _mockRepository.Setup(x => x.GetRestaurants(filter))
                       .ReturnsAsync(restaurants.Where(r => 
                           r.Name.Contains(filter.Name) &&
                           r.Radius <= filter.Radius &&
                           r.Tags.Any(t => filter.Tags.Contains(t.Name)) &&
                           r.OpeningTime >= filter.OpeningTime &&
                           r.ClosingTime <= filter.ClosingTime
                       ).ToList());

        // Act
        var result = (await _restaurantService.GetRestaurants(filter)).ToList();

        // Assert that only one restaurant matches all filter criteria
        Assert.That(result.Count, Is.EqualTo(1));

        // Asserting the filtered restaurant's properties
        var filteredRestaurant = result.FirstOrDefault();
        Assert.That(filteredRestaurant, Is.Not.Null);
        Assert.That(filteredRestaurant.Id, Is.EqualTo(restaurantId3)); // Only "Restaurant 3" should match
        Assert.That(filteredRestaurant.Name, Is.EqualTo("Restaurant 3"));
        Assert.That(filteredRestaurant.Radius, Is.EqualTo(4.0));
        Assert.That(filteredRestaurant.Tags, Is.EqualTo(new List<string> { "Mexican" }));
        Assert.That(filteredRestaurant.OpeningTime, Is.EqualTo(new TimeOnly(9, 0)));
        Assert.That(filteredRestaurant.ClosingTime, Is.EqualTo(new TimeOnly(22, 0)));

        // Verify that the repository was called with the correct filter
        _mockRepository.Verify(x => x.GetRestaurants(filter), Times.Once);
    }


    [Test]
    public async Task DeleteRestaurant_ShouldDeleteRestaurant_WhenRestaurantExists()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        var restaurant = new Restaurant
        {
            Id = restaurantId,
            Name = "Test Restaurant"
        };

        // Set up mock repository to return the restaurant for deletion
        _mockRepository.Setup(x => x.GetRestaurant(restaurantId)).ReturnsAsync(restaurant);

        // Act
        await _restaurantService.DeleteRestaurant(restaurantId);

        // Assert
        // Verify that the DeleteRestaurant method was called once
        _mockRepository.Verify(x => x.DeleteRestaurant(restaurantId), Times.Once);
    }
    [Test]
    public async Task CreateRestaurant_ShouldCreateRestaurant_WithAllPropertiesMappedCorrectly()
    {
        // Arrange
        var restaurantId = Guid.NewGuid();
        
        // DTO for creating a restaurant
        var createRestaurantDto = new CreateRestaurantDto
        {
            Name = "New Restaurant",
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 0),
            StreetName = "Main St",
            HouseNumber = "123",
            Description = "A brand new place",
            Radius = 5.0,
            ImageLink = "http://example.com/image.jpg"
        };

        // The restaurant object that should be created based on the DTO
        var createdRestaurant = new Restaurant
        {
            Id = restaurantId,
            Name = createRestaurantDto.Name,
            OpeningTime = createRestaurantDto.OpeningTime,
            ClosingTime = createRestaurantDto.ClosingTime,
            StreetName = createRestaurantDto.StreetName,
            HouseNumber = createRestaurantDto.HouseNumber,
            Description = createRestaurantDto.Description,
            Radius = createRestaurantDto.Radius,
            ImageLink = createRestaurantDto.ImageLink,
            Tags = new List<Tag>()
        };

        // Set up the repository mock to simulate the CreateRestaurant call
        _mockRepository.Setup(x => x.CreateRestaurant(It.IsAny<CreateRestaurantDto>()))
                       .ReturnsAsync(createdRestaurant); // Return the created restaurant

        // Act
        await _restaurantService.CreateRestaurant(createRestaurantDto);

        // Assert
        _mockRepository.Verify(x => x.CreateRestaurant(It.Is<CreateRestaurantDto>(r =>
            r.Name == createRestaurantDto.Name &&
            r.OpeningTime == createRestaurantDto.OpeningTime &&
            r.ClosingTime == createRestaurantDto.ClosingTime &&
            r.StreetName == createRestaurantDto.StreetName &&
            r.HouseNumber == createRestaurantDto.HouseNumber &&
            r.Description == createRestaurantDto.Description &&
            r.Radius == createRestaurantDto.Radius &&
            r.ImageLink == createRestaurantDto.ImageLink
        )), Times.Once);
    }

}
