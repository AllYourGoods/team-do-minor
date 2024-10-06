using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net;
using Microsoft.EntityFrameworkCore;

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
    public async Task CreateRestaurant_ValidData_ReturnsCreatedResponseAndStoresInDatabase()
    {
        // Arrange
        var createDto = new CreateRestaurantDto(
            "Test Restaurant", 
            "123456773", 
            new CreateAddress("12", "2312RF", "Rotterdam", "TestStreet"), 
            new CreateBanner("TestBannerUrl", "TestBannerMimetype", 2323),
            new CreateLogo("TestLogoUrl", "TestLogoMimeType", 1212 ))
        {
          AboutUs  = "This is a text about us",
          Radius = 34
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/restaurant", createDto);

        // Assert
        // 1. Check for 201 Created status
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected response code is 201 Created.");

        // 2. Check that the response contains a valid ResponseRestaurantDto
        var returnedDto = await response.Content.ReadFromJsonAsync<ResponseRestaurantDto>();
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");

        // 3. Assert returned DTO values match the input values
        Assert.That(returnedDto.Name, Is.EqualTo(createDto.Name), "Name does not match.");
        Assert.That(returnedDto.PhoneNumber, Is.EqualTo(createDto.PhoneNumber), "Phone number does not match.");
        Assert.That(returnedDto.AboutUs, Is.EqualTo(createDto.AboutUs), "AboutUs does not match.");
        Assert.That(returnedDto.Radius, Is.EqualTo(createDto.Radius), "Radius does not match.");

        // 4. Assert Address values
        Assert.That(returnedDto.Address.HouseNumber, Is.EqualTo(createDto.Address.HouseNumber), "Address house number does not match.");
        Assert.That(returnedDto.Address.ZipCode, Is.EqualTo(createDto.Address.ZipCode), "Address postal code does not match.");
        Assert.That(returnedDto.Address.City, Is.EqualTo(createDto.Address.City), "Address city does not match.");
        Assert.That(returnedDto.Address.StreetName, Is.EqualTo(createDto.Address.StreetName), "Address street does not match.");

        // 5. Assert Banner values
        Assert.That(returnedDto.Banner.Url, Is.EqualTo(createDto.Banner.Url), "Banner URL does not match.");
        Assert.That(returnedDto.Banner.MimeType, Is.EqualTo(createDto.Banner.MimeType), "Banner MimeType does not match.");
        Assert.That(returnedDto.Banner.FileSize, Is.EqualTo(createDto.Banner.FileSize), "Banner Size does not match.");

        // 6. Assert Logo values
        Assert.That(returnedDto.Logo.Url, Is.EqualTo(createDto.Logo.Url), "Logo URL does not match.");
        Assert.That(returnedDto.Logo.MimeType, Is.EqualTo(createDto.Logo.MimeType), "Logo MimeType does not match.");
        Assert.That(returnedDto.Logo.FileSize, Is.EqualTo(createDto.Logo.FileSize), "Logo Size does not match.");

        // 7. Verify that the restaurant is stored correctly in the database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var restaurantInDb = context.Restaurants
            .Include(r => r.Address)
            .Include(r => r.Logo)
            .Include(r => r.Banner)
            .FirstOrDefault(r => r.Id == returnedDto.Id);
        
        Assert.That(restaurantInDb, Is.Not.Null, "Restaurant should exist in the database.");

        // Assert basic properties
        Assert.That(restaurantInDb.Name, Is.EqualTo(createDto.Name), "Stored Name does not match.");
        Assert.That(restaurantInDb.PhoneNumber, Is.EqualTo(createDto.PhoneNumber), "Stored Phone number does not match.");
        Assert.That(restaurantInDb.AboutUs, Is.EqualTo(createDto.AboutUs), "Stored AboutUs does not match.");
        Assert.That(restaurantInDb.Radius, Is.EqualTo(createDto.Radius), "Stored Radius does not match.");

        // Assert Address values in database
        Assert.That(restaurantInDb.Address.HouseNumber, Is.EqualTo(createDto.Address.HouseNumber), "Stored Address house number does not match.");
        Assert.That(restaurantInDb.Address.ZipCode, Is.EqualTo(createDto.Address.ZipCode), "Stored Address postal code does not match.");
        Assert.That(restaurantInDb.Address.City, Is.EqualTo(createDto.Address.City), "Stored Address city does not match.");
        Assert.That(restaurantInDb.Address.StreetName,Is.EqualTo(createDto.Address.StreetName), "Stored Address street does not match.");

        // Assert Banner values in database
        Assert.That(restaurantInDb.Banner.Url, Is.EqualTo(createDto.Banner.Url), "Stored Banner URL does not match.");
        Assert.That(restaurantInDb.Banner.MimeType, Is.EqualTo(createDto.Banner.MimeType), "Stored Banner MimeType does not match.");
        Assert.That(restaurantInDb.Banner.FileSize, Is.EqualTo(createDto.Banner.FileSize), "Stored Banner Size does not match.");

        // Assert Logo values in database
        Assert.That(restaurantInDb.Logo.Url, Is.EqualTo(createDto.Logo.Url), "Stored Logo URL does not match.");
        Assert.That(restaurantInDb.Logo.MimeType, Is.EqualTo(createDto.Logo.MimeType), "Stored Logo MimeType does not match.");
        Assert.That(restaurantInDb.Logo.FileSize, Is.EqualTo(createDto.Logo.FileSize), "Stored Logo Size does not match.");
    }
}
