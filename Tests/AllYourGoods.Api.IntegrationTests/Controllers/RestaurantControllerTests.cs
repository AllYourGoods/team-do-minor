using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Updates;
using AllYourGoods.Api.Models.Dtos.Views;
using AllYourGoods.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace AllYourGoods.Api.IntegrationTests.Controllers;

[TestFixture]
public class RestaurantControllerTests
{
    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;
    private List<Restaurant> _databaseRestaurants;

    [SetUp]
    public async Task Setup()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _client = _factory.CreateClient();
        await LoadRestaurantsIntoDatabaseAsync();
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
            new CreateLogo("TestLogoUrl", "TestLogoMimeType", 1212))
        {
            AboutUs = "This is a text about us",
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
        Assert.That(restaurantInDb.Address.StreetName, Is.EqualTo(createDto.Address.StreetName), "Stored Address street does not match.");

        // Assert Banner values in database
        Assert.That(restaurantInDb.Banner.Url, Is.EqualTo(createDto.Banner.Url), "Stored Banner URL does not match.");
        Assert.That(restaurantInDb.Banner.MimeType, Is.EqualTo(createDto.Banner.MimeType), "Stored Banner MimeType does not match.");
        Assert.That(restaurantInDb.Banner.FileSize, Is.EqualTo(createDto.Banner.FileSize), "Stored Banner Size does not match.");

        // Assert Logo values in database
        Assert.That(restaurantInDb.Logo.Url, Is.EqualTo(createDto.Logo.Url), "Stored Logo URL does not match.");
        Assert.That(restaurantInDb.Logo.MimeType, Is.EqualTo(createDto.Logo.MimeType), "Stored Logo MimeType does not match.");
        Assert.That(restaurantInDb.Logo.FileSize, Is.EqualTo(createDto.Logo.FileSize), "Stored Logo Size does not match.");
    }

    [Test]
    public async Task CreateRestaurant_InvalidZipCode_ReturnsBadRequestResponse()
    {
        // Arrange
        var createDto = new CreateRestaurantDto(
            "Test Restaurant",
            "123456773",
            new CreateAddress("12", "2312RF123", "Rotterdam", "TestStreet"),  // Invalid ZipCode
            new CreateBanner("TestBannerUrl", "TestBannerMimetype", 2323),
            new CreateLogo("TestLogoUrl", "TestLogoMimeType", 1212))
        {
            AboutUs = "This is a text about us",
            Radius = 34
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/restaurant", createDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Expected response code is 400 Bad Request.");
    }

    [Test]
    public async Task GetRestaurants_ValidRequest_ReturnsOkResponseWithPaginatedList()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 2;

        // Act
        var response = await _client.GetAsync($"/api/restaurant/paginated?pageNumber={pageNumber}&pageSize={pageSize}");

        // Assert that the response status is 200 OK
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected response code is 200 OK.");

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var paginatedList = JsonConvert.DeserializeObject<PaginatedList<ResponseRestaurantDto>>(jsonResponse);

        // Assert that the deserialized object is not null
        Assert.That(paginatedList, Is.Not.Null, "Expected the response body to be a non-null PaginatedList.");
        Assert.That(paginatedList.Items.Count, Is.EqualTo(pageSize), $"Expected {pageSize} items in the paginated list.");

        //TODO Add more tests to validate response
    }

    [Test]
    public async Task GetRestaurants_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange: Set invalid page number and page size values
        var invalidPageNumber = -1; 
        var invalidPageSize = 0;  

        // Act: Make a GET request with invalid query parameters
        var response = await _client.GetAsync($"/api/restaurant/paginated?pageNumber={invalidPageNumber}&pageSize={invalidPageSize}");

        // Assert: Check that the status code is 400 Bad Request
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Expected response code is 400 Bad Request.");

        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Is.EqualTo("Page number and page size must be greater than zero."), "Expected specific error message for invalid parameters.");
    }

    [Test]
    public async Task GetRestaurant_ValidId_ReturnsOkResponse()
    {
        // Arrange: Use the ID of an existing restaurant (e.g., "McDonald's")
        var existingRestaurantId = _databaseRestaurants.First().Id;

        // Act: Make a GET request to the /api/restaurant/{id} endpoint
        var response = await _client.GetAsync($"/api/restaurant/{existingRestaurantId}");

        // Assert: Check that the status code is 200 OK
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected response code is 200 OK.");

        // Deserialize the response
        var returnedDto = JsonConvert.DeserializeObject<ResponseRestaurantDto>(await response.Content.ReadAsStringAsync());

        // Assert: Check that the returned restaurant details match the existing restaurant
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");
        Assert.That(returnedDto.Id, Is.EqualTo(existingRestaurantId), "Returned restaurant ID does not match the requested ID.");
        Assert.That(returnedDto.Name, Is.EqualTo(_databaseRestaurants.First().Name), "Returned restaurant name does not match the expected value.");
        //TODO Add more validations
    }

    [Test]
    public async Task GetRestaurant_InvalidId_ReturnsNotFoundResponse()
    {
        // Arrange: Use a random GUID that does not exist in the database
        var nonExistentRestaurantId = Guid.NewGuid();

        // Act: Make a GET request to the /api/restaurant/{id} endpoint
        var response = await _client.GetAsync($"/api/restaurant/{nonExistentRestaurantId}");

        // Assert: Check that the status code is 404 Not Found
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected response code is 404 Not Found.");

        // Check the error message
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Is.EqualTo($"Restaurant with ID = {nonExistentRestaurantId} not found."), "Expected specific error message for not found restaurant.");
    }

    [Test]
    public async Task UpdateRestaurant_ValidData_ReturnsOkResponseAndUpdatesInDatabase()
    {
        // Arrange: Use the ID of an existing restaurant (e.g., "McDonald's")
        var existingRestaurantId = _databaseRestaurants.First().Id;

        // Create an UpdateRestaurantDto with new data
        var updateDto = new UpdateRestaurantDto
        {
            Name = "Updated McDonald's",
            PhoneNumber = "987654321",
            AboutUs = "Updated About Us Text",
            Radius = 10,
        };

        // Act: Make a PUT request to the /api/restaurant/{id} endpoint with the updateDto
        var response = await _client.PutAsJsonAsync($"/api/restaurant/{existingRestaurantId}", updateDto);

        // Assert: Check that the status code is 200 OK
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected response code is 200 OK.");

        // Deserialize the response
        var returnedDto = JsonConvert.DeserializeObject<ResponseRestaurantDto>(await response.Content.ReadAsStringAsync());

        // Assert that the returned restaurant has the updated details
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");
        Assert.That(returnedDto.Id, Is.EqualTo(existingRestaurantId), "Returned restaurant ID does not match.");
        Assert.That(returnedDto.Name, Is.EqualTo(updateDto.Name), "Returned Name does not match the updated value.");
        Assert.That(returnedDto.PhoneNumber, Is.EqualTo(updateDto.PhoneNumber), "Returned PhoneNumber does not match the updated value.");
        Assert.That(returnedDto.AboutUs, Is.EqualTo(updateDto.AboutUs), "Returned AboutUs does not match the updated value.");
        Assert.That(returnedDto.Radius, Is.EqualTo(updateDto.Radius), "Returned Radius does not match the updated value.");

        // Optional: Verify the update in the database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var restaurantInDb = await context.Restaurants.Include(r => r.Address).Include(r => r.Logo).Include(r => r.Banner)
            .FirstOrDefaultAsync(r => r.Id == existingRestaurantId);

        Assert.That(restaurantInDb, Is.Not.Null, "Restaurant should exist in the database after update.");
        Assert.That(restaurantInDb.Name, Is.EqualTo(updateDto.Name), "Database Name does not match the updated value.");
    }

    [Test]
    public async Task UpdateRestaurant_InvalidId_ReturnsNotFoundResponse()
    {
        // Arrange: Use a random GUID that does not exist in the database
        var nonExistentRestaurantId = Guid.NewGuid();

        // Create a valid UpdateRestaurantDto
        var updateDto = new UpdateRestaurantDto
        {
            Name = "Nonexistent Restaurant",
            PhoneNumber = "987654321",
            AboutUs = "Nonexistent About Us Text",
            Radius = 10,
        };

        // Act: Make a PUT request to the /api/restaurant/{id} endpoint
        var response = await _client.PutAsJsonAsync($"/api/restaurant/{nonExistentRestaurantId}", updateDto);

        // Assert: Check that the status code is 404 Not Found
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected response code is 404 Not Found.");

        // Check the error message
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Is.EqualTo($"Restaurant with ID = {nonExistentRestaurantId} not found."), "Expected specific error message for not found restaurant.");
    }

    [Test]
    public async Task UpdateRestaurant_InvalidData_ReturnsBadRequest()
    {
        // Arrange: Use the ID of an existing restaurant (e.g., "McDonald's")
        var existingRestaurantId = _databaseRestaurants.First().Id;

        // Create an invalid UpdateRestaurantDto with empty or null fields
        var invalidUpdateDto = new UpdateRestaurantDto
        {
            // Missing Name
            PhoneNumber = "invalid-phone-number-123456678900",
            AboutUs = "About Us Text",
            Radius = -5,
        };

        // Act: Make a PUT request to the /api/restaurant/{id} endpoint
        var response = await _client.PutAsJsonAsync($"/api/restaurant/{existingRestaurantId}", invalidUpdateDto);

        // Assert: Check that the status code is 400 Bad Request
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest), "Expected response code is 400 Bad Request.");

        // Check for validation errors in the response
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Does.Contain("One or more validation errors occurred."), "Expected error message for missing Name.");
    }


    private async Task LoadRestaurantsIntoDatabaseAsync()
    {
        // Retrieve ApplicationContext from DI container
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var restaurants = new List<Restaurant>
     {
            new()
            {
                Id = Guid.NewGuid(),
                Name = "McDonald's",
                PhoneNumber = "123456789",
                AboutUs = "The best fast food in the world",
                Radius = 5,
                Logo = new ImageFile
                {
                    Url = "https://www.mcdonalds.com/content/dam/sites/usa/nfl/icons/arches-logo_108x108.jpg",
                    AltText = "McDonald's logo",
                    MimeType = "image/jpeg",
                    FileSize = 0.1
                },
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    City = "Rotterdam",
                    HouseNumber = "12",
                    StreetName = "Coolsingel",
                    ZipCode = "3011AD",
                    Latitude = 51.9225,
                    Longitude = 4.47917
                },
                Banner = new ImageFile
                {
                    Id = Guid.NewGuid(),
                    Url = "https://banner2.cleanpng.com/20180714/buk/kisspng-india-medplus-business-retail-pharmacy-logo-mcdonald-5b4a5d7662d712.2249596115316002464049.jpg",
                    AltText = "McDonald's banner",
                    MimeType = "image/jpeg",
                    FileSize = 0.1
                },
                Owner = new User
                {
                    UserName = "John Doe",
                    Email = "OwnerEmail@email.com",
                },
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Monday
                    },
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Tuesday
                    },
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Wednesday
                    },
                    new ()
                    {
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(22, 30),
                        Day = Day.Thursday
                    },
                    new ()
                    {
                        Id = Guid.NewGuid(),
                        Opening = new TimeOnly(8, 30),
                        Closing = new TimeOnly(21, 30),
                        Day = Day.Friday
                    },
                }

            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "Burger King",
                PhoneNumber = "987654321",
                AboutUs = "Home of the Whopper",
                Radius = 5,
                Logo = new ImageFile
                {
                    Id = Guid.NewGuid(),
                    Url = "https://upload.wikimedia.org/wikipedia/commons/c/cc/Burger_King_2020.svg",
                    AltText = "Burger King logo",
                    MimeType = "image/png",
                    FileSize = 0.15
                },
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    City = "Amsterdam",
                    HouseNumber = "45",
                    StreetName = "Damrak",
                    ZipCode = "1012LP",
                    Latitude = 52.3738,
                    Longitude = 4.8910
                },
                Banner = new ImageFile
                {
                    Id = Guid.NewGuid(),
                    Url = "https://upload.wikimedia.org/wikipedia/commons/c/cc/Burger_King_2020.svg",
                    AltText = "Burger King banner",
                    MimeType = "image/jpeg",
                    FileSize = 0.2
                },
                Owner = new User
                {
                    UserName = "Jane Smith",
                    Email = "OwnerBK@email.com",
                },
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Monday },
                    new () { Id = Guid.NewGuid(),Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Tuesday },
                    new () { Id = Guid.NewGuid(),Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Wednesday },
                    new () { Id = Guid.NewGuid(),Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Thursday },
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(9, 00), Closing = new TimeOnly(23, 00), Day = Day.Friday },
                }
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = "KFC",
                PhoneNumber = "456789123",
                AboutUs = "Finger Lickin' Good",
                Radius = 5,
                Logo = new ImageFile
                {
                    Id = Guid.NewGuid(),
                    Url = "https://commons.wikimedia.org/wiki/File:Kentucky_Fried_Chicken_201x_logo.svg",
                    AltText = "KFC logo",
                    MimeType = "image/svg+xml",
                    FileSize = 0.05
                },
                Address = new Address
                {
                    Id = Guid.NewGuid(),
                    City = "The Hague",
                    HouseNumber = "88",
                    StreetName = "Grote Markt",
                    ZipCode = "2511BJ",
                    Latitude = 52.0800,
                    Longitude = 4.3100
                },
                Banner = new ImageFile
                {
                    Id = Guid.NewGuid(),
                    Url = "https://commons.wikimedia.org/wiki/File:Kentucky_Fried_Chicken_201x_logo.svg",
                    AltText = "KFC banner",
                    MimeType = "image/jpeg",
                    FileSize = 0.3
                },
                Owner = new User
                {
                    UserName = "Bob Brown",
                    Email = "OwnerKFC@email.com",
                },
                OpeningsTimes = new List<OpeningsTime>()
                {
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Monday },
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Tuesday },
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Wednesday },
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Thursday },
                    new () { Id = Guid.NewGuid(), Opening = new TimeOnly(10, 00), Closing = new TimeOnly(22, 00), Day = Day.Friday },
                }
            }
     };

        context.Restaurants.AddRange(restaurants);
        await context.SaveChangesAsync();

        _databaseRestaurants = restaurants;
    }
}
