using AllYourGoods.Api.Data;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Dtos.Views;
using AllYourGoods.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http.Json;

namespace AllYourGoods.Api.IntegrationTests.Controllers;

[TestFixture]
public class OrderControllerTests
{
    private HttpClient _client;
    private CustomWebApplicationFactory<Program> _factory;
    private List<Order> _databaseOrders;

    [SetUp]
    public async Task Setup()
    {
        _factory = new CustomWebApplicationFactory<Program>();
        _client = _factory.CreateClient();

        // Reset the database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        await LoadOrdersIntoDatabaseAsync();
    }


    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }

    [Test]
    public async Task CreateOrder_ValidData_ReturnsCreatedResponseAndStoresInDatabase()
    {
        // Arrange
        var createAddress = new CreateAddress("12", "2312RF", "Rotterdam", "TestStreet");
        var orderHasProductList = new List<CreateOrderHasProduct> {
            new CreateOrderHasProduct { ProductId = Guid.NewGuid(), Amount = 3 }
            };
        var createDto = new CreateOrderDto(createAddress , orderHasProductList)
        {
            RestaurantId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            TotalPrice = 120.50,
            Note = "Please deliver by 7 PM",
            Address = createAddress,
            DeliveryPersonId = Guid.NewGuid(),
            ETA = 30.5,
            PaymentMethod = PaymentMethod.CreditCard,
            Status = OrderStatus.Confirmed
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/order", createDto);

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Expected response code is 201 Created.");
        var returnedDto = await response.Content.ReadFromJsonAsync<ResponseOrderDto>();
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");

        // Validate response properties
        Assert.That(returnedDto.TotalPrice, Is.EqualTo(createDto.TotalPrice), "TotalPrice does not match.");
        Assert.That(returnedDto.StreetName, Is.EqualTo(createDto.Address.StreetName), "Address StreetName does not match.");

        // Validate that the order is stored in the database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var orderInDb = context.Orders.Include(o => o.OrderHasProduct).FirstOrDefault(o => o.Id == returnedDto.Id);
        Assert.That(orderInDb, Is.Not.Null, "Order should exist in the database.");
        Assert.That(orderInDb.TotalPrice, Is.EqualTo(createDto.TotalPrice), "Stored TotalPrice does not match.");
    }

    [Test]
    public async Task CreateOrder_StreetNameAsTEST_ReturnsCreatedResponseAndStoresInDatabase()
    {
        // Arrange
        var createAddress = new CreateAddress("12", "2312RF", "Rotterdam", "TEST");
        var orderHasProductList = new List<CreateOrderHasProduct>
    {
        new CreateOrderHasProduct { ProductId = Guid.NewGuid(), Amount = 5 }
    };

        var createDto = new CreateOrderDto(createAddress, orderHasProductList)
        {
            RestaurantId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid(),
            
            TotalPrice = 150.75,
            Note = "Deliver at backdoor",
            DeliveryPersonId = Guid.NewGuid(),
            ETA = 25.0,
            PaymentMethod = PaymentMethod.Cash,
            Status = OrderStatus.Confirmed
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/order", createDto);

        // Log response content for debugging if the test fails
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Response Content: {responseContent}");

        // Assert the status code
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created),
            $"Expected response code is 201 Created but got {response.StatusCode}. Response Content: {responseContent}");

        // Deserialize the response
        var returnedDto = await response.Content.ReadFromJsonAsync<ResponseOrderDto>();
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");

        // Validate response properties
        Assert.That(returnedDto.StreetName, Is.EqualTo(createDto.Address.StreetName), "StreetName does not match.");

        // Validate that the order is stored in the database
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        // Retrieve the order by ID and include the Address navigation property
        var orderInDb = await context.Orders.Include(o => o.Address).FirstOrDefaultAsync(o => o.Id == returnedDto.Id);
        Assert.That(orderInDb, Is.Not.Null, "Order should exist in the database.");
        Assert.That(orderInDb.Address.StreetName, Is.EqualTo(createDto.Address.StreetName), "Stored StreetName does not match.");
    }

    [Test]
    public async Task CreateOrder_InvalidData_ReturnsBadRequest()
    {
        // Arrange
        
         var orderHasProductList = new List<CreateOrderHasProduct>
{
        new CreateOrderHasProduct { ProductId = Guid.NewGuid(), Amount = 5 }

};
        var createDto = new CreateOrderDto(new CreateAddress("12", "2312RF", "Rotterdam", "TestStreet") , orderHasProductList)
        {
            RestaurantId = Guid.Empty, 
            CustomerId = Guid.Empty,   
            TotalPrice = -1,          
            Note = null,              
            DeliveryPersonId = Guid.Empty,   
            ETA = -10,                
            PaymentMethod = PaymentMethod.CreditCard,
            Status = OrderStatus.Confirmed
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/order", createDto);

        // Assert
        if (response.StatusCode != HttpStatusCode.BadRequest)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Fail($"Expected response code is 400 Bad Request but received {(int)response.StatusCode} {response.StatusCode}. Response Content: {responseContent}");
        }
    }




    [Test]
    public async Task GetOrder_ValidId_ReturnsOkResponse()
    {
        // Arrange
        var existingOrderId = _databaseOrders.First().Id;

        // Act
        var response = await _client.GetAsync($"/api/order/{existingOrderId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Expected response code is 200 OK.");
        var returnedDto = JsonConvert.DeserializeObject<ResponseOrderDto>(await response.Content.ReadAsStringAsync());
        Assert.That(returnedDto, Is.Not.Null, "Response body should not be null.");
        Assert.That(returnedDto.Id, Is.EqualTo(existingOrderId), "Returned order ID does not match the requested ID.");
    }

    [Test]
    public async Task GetOrder_InvalidId_ReturnsNotFoundResponse()
    {
        // Arrange
        var nonExistentOrderId = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/order/{nonExistentOrderId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "Expected response code is 404 Not Found.");
        var errorMessage = await response.Content.ReadAsStringAsync();
        Assert.That(errorMessage, Is.EqualTo($"Order with ID = {nonExistentOrderId} not found."), "Expected specific error message for not found order.");
    }

    private async Task LoadOrdersIntoDatabaseAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        var orders = new List<Order>
        {
            new()
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                TotalPrice = 120.50,
                Note = "Deliver by 7 PM",
                OrderHasProductId = Guid.NewGuid(),
                Address = new Address
                {
                    HouseNumber = "12",
                    ZipCode = "2312RF",
                    City = "Rotterdam",
                    StreetName = "TestStreet",
                    Longitude = 4.47917,
                    Latitude = 51.9225
                },
                DeliveryPersonId = Guid.NewGuid(),
                ETA = 30.5,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = OrderStatus.Confirmed
            },
            new()
            {
                Id = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                TotalPrice = 200.00,
                Note = "Leave at front door",
                OrderHasProductId = Guid.NewGuid(),
                Address = new Address
                {
                    HouseNumber = "45",
                    ZipCode = "1012LP",
                    City = "Amsterdam",
                    StreetName = "Damrak",
                    Longitude = 4.8910,
                    Latitude = 52.3738
                },
                DeliveryPersonId = Guid.NewGuid(),
                ETA = 45.0,
                PaymentMethod = PaymentMethod.PayPal,
                Status = OrderStatus.Success
            }
        };

        context.Orders.AddRange(orders);
        await context.SaveChangesAsync();

        _databaseOrders = orders;
    }
}