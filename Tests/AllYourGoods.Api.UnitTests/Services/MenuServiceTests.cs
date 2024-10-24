using AllYourGoods.Api.Interfaces.Repositories;
using AllYourGoods.Api.Services;
using AutoMapper;
using Moq;

namespace AllYourGoods.Api.UnitTests.Services;

[TestFixture]
public class MenuServiceTests
{
    // protected readonly MapperConfiguration MapperConfiguration = new(cfg =>
    // {
    //     cfg.AddProfile<MenuMappingProfile>();
    // });

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _menuService = new MenuService(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IMapper> _mockMapper;
    private MenuService _menuService;


    // [Test]
    // public void AssertMapperConfiguration()
    // {
    //     MapperConfiguration.AssertConfigurationIsValid();
    // }

    [Test]
    public async Task Return_ResponseMenuDto() // Wat moet de naming convention zijn voor een test
    {
        // Arrange


        // Act

        // Assert
    }

    // [Test]
    // public void GetMenuByIdAsync_NonExistentMenuId_ThrowsKeyNotFoundException()
    // {
    //     // Arrange
    //     var restaurantId = Guid.NewGuid();
    //
    //     _mockUnitOfWork.Setup(u => u.Repository<Menu>().GetByIdAsync(
    //         restaurantId,
    //         It.IsAny<Expression<Func<Menu, object>>[]>())).ReturnsAsync((Menu)null);
    //
    //     // Act & Assert
    //     Assert.ThrowsAsync<KeyNotFoundException>(() => _menuService.GetMenuByIdAsync(restaurantId));
    // }
}