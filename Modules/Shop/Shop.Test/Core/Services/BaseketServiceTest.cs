using Moq;
using Shared.Core.Interfaces.Services;
using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.Basket.BasketItem;
using Shop.Core.Factories;
using Shop.Core.Interfaces.Repositories;
using Shop.Core.Interfaces.Services;
using Shop.Core.Services;
using Shop.Domain.Entities.Baskets;
using System.Linq.Expressions;

namespace Shop.Test.Core.Services;

public class BaseketServiceTest
{
    private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
    private readonly IBasketService _basketService;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
    private readonly Mock<IHeaderService> _headerService = new();
    private readonly Mock<ILogicFactory> _logicFactory = new();
    private readonly Mock<IPromotionRepository> _promotionRepositoryMock = new();
    private readonly Mock<IPurchaseListRepository> _purchaseListRepositoryMock = new();

    public BaseketServiceTest()
    {
        _basketService = new BasketService(
            _basketRepositoryMock.Object,
            _currentUserServiceMock.Object,
            _headerService.Object,
            _logicFactory.Object,
            _promotionRepositoryMock.Object,
            _purchaseListRepositoryMock.Object
        );
    }

    [Theory]
    [InlineData("67bbea48-8951-48fc-b2f5-f42c98530080")]
    [InlineData(null)]
    public async Task CreateAsync_CreateBasket_ReturnBasket(string guidString)
    {
        //Arrange
        var userId = guidString == null ? null : (Guid?)Guid.Parse(guidString);
        var basketDto = new BasketRequestFormDto()
        {
            BasketItems = new List<BasketItemFormDto>(),
        };

        var basketInputEntity = basketDto.ToEntity(userId);
        var basketOutputEntity = basketInputEntity.Clone();
        basketOutputEntity.Id = Guid.NewGuid();

        _currentUserServiceMock.Setup(x => x.GetUserId()).Returns(userId);
        _basketRepositoryMock
            .Setup(x =>
                x.CreateAsync(It.IsAny<BasketEntity>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(basketOutputEntity);

        _basketRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<BasketEntity, BasketResponseFormDto>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BasketResponseFormDto
            {
                Id = basketOutputEntity.Id,
                BasketItems = new List<BasketItemFormDto>()
            });
        //Act
        var result = await _basketService.CreateAsync(basketDto, default);
        //Assert
        _basketRepositoryMock
            .Verify(x =>
                x.CreateAsync(It.Is<BasketEntity>(x => x.UserId == userId),
                It.IsAny<CancellationToken>()));

        _basketRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(It.Is<Guid>(x => x == basketOutputEntity.Id),
                It.IsAny<Expression<Func<BasketEntity, BasketResponseFormDto>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BasketResponseFormDto
            {
                Id = basketOutputEntity.Id,
                BasketItems = new List<BasketItemFormDto>()
            });

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Result);
        Assert.Equal(basketOutputEntity.Id, result.Result.Id);
    }
}