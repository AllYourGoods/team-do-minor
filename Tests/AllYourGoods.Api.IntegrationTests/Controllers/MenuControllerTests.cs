using System.Net;

namespace AllYourGoods.Api.IntegrationTests.Controllers;

[TestFixture]
public class MenuController
{
    [SetUp]
    public async Task Setup()
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

    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;

    [Test]
    public async Task GetRestaurantMenu_InvalidId_ReturnsNotFound()
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
}