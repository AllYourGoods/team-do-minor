using System.Net;
using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models;
using Microsoft.Extensions.DependencyInjection;

namespace AllYourGoods.Api.IntegrationTests.Controllers;

[TestFixture]
public class MenuController
{
    [SetUp]
    public async Task Setup()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        await LoadRestaurantMenusIntoDatabaseAsync();
    }

    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;
    private List<Menu> _databaseMenus;

    [Test]
    public async Task GetMenu_InvalidRestaurantId_ReturnsNotFound()
    {
        // Arrange: Use a random GUID that does not exist in the database
        var nonExistentRestaurantId = Guid.NewGuid();

        // Act: Make a GET request to the /api/restaurant/{id}/menu endpoint
        var response = await _client.GetAsync($"/api/restaurant/{nonExistentRestaurantId}/menu");

        // Assert: Check that the status code is 404 Not Found
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound),
            "Expected response code is 404 Not Found.");

        // Check the error message
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Is.EqualTo($"Menu with RestaurantID = {nonExistentRestaurantId} not found."),
            "Expected specific error message for not found restaurant.");
    }
    //
    // [Test]
    // public async Task GetRestaurantMenu_ValidId_ReturnsOkResponse()
    // {
    //     // Arrange: Use a random GUID that does not exist in the database
    //     var restaurantId = _databaseMenus.First().RestaurantId;
    //
    //     // Act: Make a GET request to the /api/restaurant/{id}/menu endpoint
    //     var response = await _client.GetAsync($"/api/restaurant/{restaurantId}/menu");
    //
    //     // Assert: Check that the status code is 404 Not Found
    //     Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
    //         "Expected response code is 200 Ok");
    // }

    private async Task LoadRestaurantMenusIntoDatabaseAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        _databaseMenus =
        [
            new Menu { Name = "Menu 1", Active = true, RestaurantId = Guid.NewGuid() },
            new Menu { Name = "Menu 2", Active = true, RestaurantId = Guid.NewGuid() }
        ];

        await context.Menus.AddRangeAsync(_databaseMenus);
        await context.SaveChangesAsync();
    }
}