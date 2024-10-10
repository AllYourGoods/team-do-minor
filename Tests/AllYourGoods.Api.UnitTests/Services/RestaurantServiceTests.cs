using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Models;
using AllYourGoods.Api.Models.Dtos.Views;
using AllYourGoods.Api.Services;
using AutoMapper;
using Moq;
using System.Linq.Expressions;
using AllYourGoods.Api.Mappings;

namespace AllYourGoods.Api.UnitTests.Services
{
    [TestFixture]
    public class RestaurantServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private RestaurantService _restaurantService;
        protected readonly MapperConfiguration MapperConfiguration = new(cfg =>
        {
            cfg.AddProfile<RestaurantMappingProfile>();
        });

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _restaurantService = new RestaurantService(_mockUnitOfWork.Object, _mockMapper.Object);
        }
        

        [Test]
        public void AssertMapperConfiguration()
        {
            MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public async Task CreateRestaurantAsync_ValidData_ReturnsResponseRestaurantDto()
        {
            //TODO to be implemented
        }

        [Test]
        public async Task GetRestaurantByIdAsync_ExistingRestaurantId_ReturnsResponseRestaurantDto()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();
            var restaurant = new Restaurant { Id = restaurantId, Name = "Test Restaurant" };
            var responseDto = new ResponseRestaurantDto { Id = restaurantId, Name = "Test Restaurant" };

            _mockUnitOfWork.Setup(u => u.Repository<Restaurant>().GetByIdAsync(
                restaurantId,
                It.IsAny<Expression<Func<Restaurant, object>>[]>())).ReturnsAsync(restaurant);

            _mockMapper.Setup(m => m.Map<ResponseRestaurantDto>(restaurant)).Returns(responseDto);

            // Act
            var result = await _restaurantService.GetRestaurantByIdAsync(restaurantId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(restaurantId));
            Assert.That(result.Name, Is.EqualTo("Test Restaurant"));
            _mockUnitOfWork.Verify(u => u.Repository<Restaurant>().GetByIdAsync(restaurantId, It.IsAny<Expression<Func<Restaurant, object>>[]>()), Times.Once);
        }

        [Test]
        public void GetRestaurantByIdAsync_NonExistentRestaurantId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var restaurantId = Guid.NewGuid();

            _mockUnitOfWork.Setup(u => u.Repository<Restaurant>().GetByIdAsync(
                restaurantId,
                It.IsAny<Expression<Func<Restaurant, object>>[]>())).ReturnsAsync((Restaurant)null);

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(() => _restaurantService.GetRestaurantByIdAsync(restaurantId));
        }

        [Test]
        public async Task DeleteRestaurantAsync_ValidRestaurantId_DeletesRestaurantAndRelatedEntities()
        {
          //TODO to be implemented
        }

        [Test]
        public async Task UpdateRestaurantAsync_ExistingRestaurantId_ReturnsUpdatedResponseRestaurantDto()
        {
          //TODO to be implemented
        }
    }
}