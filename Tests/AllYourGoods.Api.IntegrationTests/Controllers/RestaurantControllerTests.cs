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

    [Test]
    public async Task GetRestaurants_ReturnsOkResult()
    {
        //  Arrange
        await AddTestRestaurants();

        // Act
        var response = await _client.GetAsync("/api/Restaurant");
        response.EnsureSuccessStatusCode();
        var restaurants = await response.Content.ReadFromJsonAsync<List<ViewRestaurantDto>>();

        // Assert
        Assert.That(restaurants, Does.Not.EqualTo(null));
        Assert.That(restaurants.Count > 0);
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
                OpeningTime = new TimeOnly(8, 0),
                ClosingTime = new TimeOnly(22, 0),
                StreetName = "Kerkstraat",
                HouseNumber = "1",
                Name = "De Klok",
                Description = "Gezellig café",
                Tags = "Café",
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
                Tags = "Café",
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
                Tags = "Café",
                Radius = 1,
                ImageLink = "https://www.google.com"
            }
        };

        context.Restaurants.AddRange(restaurants);
        await context.SaveChangesAsync();
    }
}
