using System.Net.Http.Json;
using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace AllYourGoods.Api.IntegrationTests.Controllers;

[TestFixture]
public class RestaurantControllerTests
{
    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;

    [SetUp]
    public void Setup()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _client = _factory.CreateClient();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task GetRestaurants_WithFilters_ShouldReturnFilteredRestaurants()
    {
        //  Arrange
        await AddTestRestaurants(); // Adding test restaurants to the in-memory database

        // Define filter criteria to test
        var filter = new FilterRestaurantDto
        {
            Name = "De", // Match restaurants containing "De" in their name
            Radius = 1.5, // Match restaurants with Radius <= 1.5
            Tags = new List<string> { "Cafe" }, // Match only restaurants with the "Cafe" tag
            OpeningTime = new TimeOnly(9, 0), // Match only restaurants that open at 9:00 or later
            ClosingTime = new TimeOnly(22, 0) // Match only restaurants that close at or before 22:00
        };

        // Build query string parameters based on the filter
        var queryString = $"?Name={filter.Name}&Radius={filter.Radius}&Tags={string.Join(",", filter.Tags)}&OpeningTime={filter.OpeningTime.Value}&ClosingTime={filter.ClosingTime.Value}";

        // Act: Make the GET request with query string filters
        var response = await _client.GetAsync($"/api/Restaurant{queryString}");
        response.EnsureSuccessStatusCode();
        var filteredRestaurants = await response.Content.ReadFromJsonAsync<List<ViewRestaurantDto>>();

        // Assert that the list of filtered restaurants is not null and has the correct count
        Assert.That(filteredRestaurants, Is.Not.Null);
        Assert.That(filteredRestaurants.Count, Is.EqualTo(2)); // Expecting two restaurants to match

        // Assert that the returned restaurants match the filter criteria
        var restaurant1 = filteredRestaurants.FirstOrDefault(r => r.Name == "De Klok");
        Assert.That(restaurant1, Is.Not.Null);
        Assert.That(restaurant1.Name, Is.EqualTo("De Klok"));
        Assert.That(restaurant1.Radius, Is.LessThanOrEqualTo(1.5));
        Assert.That(restaurant1.Tags, Does.Contain("Cafe"));
        Assert.That(restaurant1.OpeningTime, Is.GreaterThanOrEqualTo(new TimeOnly(9, 0)));
        Assert.That(restaurant1.ClosingTime, Is.LessThanOrEqualTo(new TimeOnly(22, 0)));

        var restaurant2 = filteredRestaurants.FirstOrDefault(r => r.Name == "De Kroeg");
        Assert.That(restaurant2, Is.Not.Null);
        Assert.That(restaurant2.Name, Is.EqualTo("De Kroeg"));
        Assert.That(restaurant2.Radius, Is.LessThanOrEqualTo(1.5));
        Assert.That(restaurant2.Tags, Does.Contain("Cafe"));
        Assert.That(restaurant2.OpeningTime, Is.GreaterThanOrEqualTo(new TimeOnly(9, 0)));
        Assert.That(restaurant2.ClosingTime, Is.LessThanOrEqualTo(new TimeOnly(23, 0)));
    }
    [Test]
    public async Task CreateRestaurant_ShouldReturnCreatedResult_WithValidData()
    {
        // Arrange
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

        // Act
        var response = await _client.PostAsJsonAsync("/api/Restaurant", createRestaurantDto);
        response.EnsureSuccessStatusCode();

        // Assert the response status code is Created (201)
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));

        // Perform a GET request to verify that the restaurant was created correctly
        var getResponse = await _client.GetAsync("/api/Restaurant");
        getResponse.EnsureSuccessStatusCode();
        var restaurants = await getResponse.Content.ReadFromJsonAsync<List<ViewRestaurantDto>>();

        // Assert that the new restaurant is present in the list
        var createdRestaurant = restaurants.FirstOrDefault(r => r.Name == createRestaurantDto.Name);
        Assert.That(createdRestaurant, Is.Not.Null);
        Assert.That(createdRestaurant.Name, Is.EqualTo(createRestaurantDto.Name));
        Assert.That(createdRestaurant.OpeningTime, Is.EqualTo(createRestaurantDto.OpeningTime));
        Assert.That(createdRestaurant.ClosingTime, Is.EqualTo(createRestaurantDto.ClosingTime));
        Assert.That(createdRestaurant.StreetName, Is.EqualTo(createRestaurantDto.StreetName));
        Assert.That(createdRestaurant.HouseNumber, Is.EqualTo(createRestaurantDto.HouseNumber));
        Assert.That(createdRestaurant.Description, Is.EqualTo(createRestaurantDto.Description));
        Assert.That(createdRestaurant.Radius, Is.EqualTo(createRestaurantDto.Radius));
        Assert.That(createdRestaurant.ImageLink, Is.EqualTo(createRestaurantDto.ImageLink));
    }

    private async Task AddTestRestaurants()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var restaurants = new Restaurant[]
        {
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(9, 0),
                ClosingTime = new TimeOnly(22, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "1",
                Name = "De Klok",
                Description = "Gezellig café",
                Tags = new List<Tag> { new Tag("Cafe"), new Tag("Lokaal") },
                Radius = 1,
                ImageLink = "https://www.google.com"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(9, 0),
                ClosingTime = new TimeOnly(23, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "2",
                Name = "De Kroeg",
                Description = "Gezellig café",
                Tags = new List<Tag> { new Tag("Cafe") },
                Radius = 1,
                ImageLink = "https://www.google.com"
            },
            new ()
            {
                Id = Guid.NewGuid(),
                OpeningTime = new TimeOnly(10, 0),
                ClosingTime = new TimeOnly(0, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "3",
                Name = "De Pub",
                Description = "Gezellig café",
                Tags = new List<Tag> { new Tag("Cafe") },
                Radius = 1,
                ImageLink = "https://www.google.com"
            }
        };

        context.Restaurants.AddRange(restaurants);
        await context.SaveChangesAsync();
    }
}
