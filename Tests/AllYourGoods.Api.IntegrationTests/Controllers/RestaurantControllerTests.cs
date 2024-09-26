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
    public async Task GetRestaurants_ReturnsOkResult_WithAllProperties()
    {
        //  Arrange
        await AddTestRestaurants();

        // Act
        var response = await _client.GetAsync("/api/Restaurant");
        response.EnsureSuccessStatusCode();
        var restaurants = await response.Content.ReadFromJsonAsync<List<ViewRestaurantDto>>();

        // Assert that the list of restaurants is not null and has the correct count
        Assert.That(restaurants, Is.Not.Null);
        Assert.That(restaurants.Count, Is.EqualTo(3));

        // Assert the first restaurant's properties
        var restaurant1 = restaurants.FirstOrDefault(r => r.Name == "De Klok");
        Assert.That(restaurant1, Is.Not.Null);
        Assert.That(restaurant1.Name, Is.EqualTo("De Klok"));
        Assert.That(restaurant1.OpeningTime, Is.EqualTo(new TimeOnly(8, 0)));
        Assert.That(restaurant1.ClosingTime, Is.EqualTo(new TimeOnly(22, 0)));
        Assert.That(restaurant1.StreetName, Is.EqualTo("Kerkstraat"));
        Assert.That(restaurant1.HouseNumber, Is.EqualTo("1"));
        Assert.That(restaurant1.Description, Is.EqualTo("Gezellig café"));
        Assert.That(restaurant1.Radius, Is.EqualTo(1));
        Assert.That(restaurant1.ImageLink, Is.EqualTo("https://www.google.com"));
        Assert.That(restaurant1.Tags, Is.Not.Null);
        Assert.That(restaurant1.Tags, Has.Count.EqualTo(2));
        Assert.That(restaurant1.Tags, Does.Contain("Cafe"));
        Assert.That(restaurant1.Tags, Does.Contain("Lokaal"));

        // Assert the second restaurant's properties
        var restaurant2 = restaurants.FirstOrDefault(r => r.Name == "De Kroeg");
        Assert.That(restaurant2, Is.Not.Null);
        Assert.That(restaurant2.Name, Is.EqualTo("De Kroeg"));
        Assert.That(restaurant2.OpeningTime, Is.EqualTo(new TimeOnly(9, 0)));
        Assert.That(restaurant2.ClosingTime, Is.EqualTo(new TimeOnly(23, 0)));
        Assert.That(restaurant2.StreetName, Is.EqualTo("Kerkstraat"));
        Assert.That(restaurant2.HouseNumber, Is.EqualTo("2"));
        Assert.That(restaurant2.Description, Is.EqualTo("Gezellig café"));
        Assert.That(restaurant2.Radius, Is.EqualTo(1));
        Assert.That(restaurant2.ImageLink, Is.EqualTo("https://www.google.com"));
        Assert.That(restaurant2.Tags, Is.Not.Null);
        Assert.That(restaurant2.Tags, Has.Count.EqualTo(1));
        Assert.That(restaurant2.Tags, Does.Contain("Cafe"));

        // Assert the third restaurant's properties
        var restaurant3 = restaurants.FirstOrDefault(r => r.Name == "De Pub");
        Assert.That(restaurant3, Is.Not.Null);
        Assert.That(restaurant3.Name, Is.EqualTo("De Pub"));
        Assert.That(restaurant3.OpeningTime, Is.EqualTo(new TimeOnly(10, 0)));
        Assert.That(restaurant3.ClosingTime, Is.EqualTo(new TimeOnly(0, 0))); // Midnight
        Assert.That(restaurant3.StreetName, Is.EqualTo("Kerkstraat"));
        Assert.That(restaurant3.HouseNumber, Is.EqualTo("3"));
        Assert.That(restaurant3.Description, Is.EqualTo("Gezellig café"));
        Assert.That(restaurant3.Radius, Is.EqualTo(1));
        Assert.That(restaurant3.ImageLink, Is.EqualTo("https://www.google.com"));
        Assert.That(restaurant3.Tags, Is.Not.Null);
        Assert.That(restaurant3.Tags, Has.Count.EqualTo(1));
        Assert.That(restaurant3.Tags, Does.Contain("Cafe"));
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
