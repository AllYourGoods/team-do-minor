using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Creates;
using AllYourGoods.Api.Models.Dtos.Responses;
using AllYourGoods.Api.Models.Enums;
using AllYourGoods.Api.Services;
using AutoMapper;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace AllYourGoods.Api.UnitTests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _orderService = new OrderService(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateOrderAsync_ValidData_ReturnsResponseOrderDto()
        {
            // Arrange
            var Address = new CreateAddress("12", "2312RF", "Rotterdam", "TestStreet");
            var orderHasProductList = new List<CreateOrderHasProduct> {
            new CreateOrderHasProduct { ProductId = Guid.NewGuid(), Amount = 3 }
            };
            var orderDto = new CreateOrderDto(Address, orderHasProductList)
            {
                RestaurantId = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                Note = "Deliver by 7 PM",
                DeliveryPersonId = Guid.NewGuid(),
                ETA = 30.5,
                PaymentMethod = PaymentMethod.CreditCard,
                Status = OrderStatus.Confirmed
            };
            var restaurantLogo = new ImageFile
            {
                Url = "https://testlogo.com",
                AltText = "Restaurant Logo",
                FileSize = 12345
            };

            var restaurant = new Restaurant
            {
                Name = "Test Restaurant",
                Logo = restaurantLogo 
            };

            var order = new Order
            {
                Id = Guid.NewGuid(),
                RestaurantId = (Guid)orderDto.RestaurantId,
                CustomerId = (Guid)orderDto.CustomerId,
                Note = orderDto.Note,
                Address = new Address
                {
                    HouseNumber = orderDto.Address.HouseNumber,
                    ZipCode = orderDto.Address.ZipCode,
                    City = orderDto.Address.City,
                    StreetName = orderDto.Address.StreetName
                },
                OrderHasProduct = orderDto.OrderHasProduct.Select(productDto => new OrderHasProduct
                {
                    Amount = productDto.Amount,
                    ProductId = Guid.NewGuid() 
                }).ToList(),
                DeliveryPersonId = orderDto.DeliveryPersonId,
                ETA = orderDto.ETA,
                PaymentMethod = orderDto.PaymentMethod,
                Status = orderDto.Status,
                Restaurant = restaurant
            };

            var responseDto = new ResponseOrderDto
            {
                Id = order.Id,
                CreatedOnUTC = order.CreatedOnUTC,
                TotalPrice = order.TotalPrice,
                StreetName = order.Address.StreetName,
                HouseNumber = order.Address.HouseNumber,
                RestaurantName = order.Restaurant.Name,  
                Logo = new ResponseLogoDto(
                Guid.NewGuid(),
                order.Restaurant.Logo.Url,  
                order.Restaurant.Logo.AltText,
                order.Restaurant.Logo.FileSize
        )
            };

            _mockMapper.Setup(m => m.Map<Order>(orderDto)).Returns(order);
            _mockUnitOfWork.Setup(u => u.Repository<Order>().Add(order));
            _mockUnitOfWork.Setup(u => u.SaveAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
            _mockMapper.Setup(m => m.Map<ResponseOrderDto>(order)).Returns(responseDto);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalPrice, Is.EqualTo(orderDto.TotalPrice));
            Assert.That(result.StreetName, Is.EqualTo(orderDto.Address.StreetName));

            _mockUnitOfWork.Verify(u => u.Repository<Order>().Add(order), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public void GetOrderByIdAsync_NonExistentOrderId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            _mockUnitOfWork.Setup(u => u.Repository<Order>().GetByIdAsync(
                orderId,
                It.IsAny<Expression<Func<Order, object>>[]>())).ReturnsAsync((Order)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.GetOrderByIdAsync(orderId));
            _mockUnitOfWork.Verify(u => u.Repository<Order>().GetByIdAsync(orderId, It.IsAny<Expression<Func<Order, object>>[]>()), Times.Once);
        }

        [Test]
        public async Task GetAllAsync_OrdersExist_ReturnsListOfResponseOrderDto()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), TotalPrice = 100.00 },
                new Order { Id = Guid.NewGuid(), TotalPrice = 200.00 }
            };
            var responseDtos = new List<ResponseOrderDto>
            {
                new ResponseOrderDto { Id = orders[0].Id, TotalPrice = orders[0].TotalPrice },
                new ResponseOrderDto { Id = orders[1].Id, TotalPrice = orders[1].TotalPrice }
            };

            _mockUnitOfWork.Setup(u => u.Repository<Order>().GetAllAsync(It.IsAny<Expression<Func<Order, bool>>?>(), null, null, null, It.IsAny<Expression<Func<Order, object>>[]>())).ReturnsAsync(orders);
            _mockMapper.Setup(m => m.Map<List<ResponseOrderDto>>(orders)).Returns(responseDtos);

            // Act
            var result = await _orderService.GetAllAsync();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].Id, Is.EqualTo(orders[0].Id));
            Assert.That(result[1].TotalPrice, Is.EqualTo(orders[1].TotalPrice));
            _mockUnitOfWork.Verify(u => u.Repository<Order>().GetAllAsync(It.IsAny<Expression<Func<Order, bool>>?>(), null, null, null, It.IsAny<Expression<Func<Order, object>>[]>()), Times.Once);
        }

        [Test]
        public void GetAllAsync_NoOrdersExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(u => u.Repository<Order>().GetAllAsync(It.IsAny<Expression<Func<Order, bool>>?>(), null, null, null, It.IsAny<Expression<Func<Order, object>>[]>())).ReturnsAsync(new List<Order>());

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _orderService.GetAllAsync());
            _mockUnitOfWork.Verify(u => u.Repository<Order>().GetAllAsync(It.IsAny<Expression<Func<Order, bool>>?>(), null, null, null, It.IsAny<Expression<Func<Order, object>>[]>()), Times.Once);
        }
        
    }
}