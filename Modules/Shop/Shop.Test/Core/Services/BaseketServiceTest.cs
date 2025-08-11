using Moq;
using Shared.Core.Services;
using Shop.Core.Dtos.Basket;
using Shop.Core.Services;
using Shop.Domain.Aggregates.Baskets;
using Shop.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace Shop.Test.Core.Services;

public class BaseketServiceTest
{
    private readonly Mock<IBasketRepository> _basketRepositoryMock = new();
    private readonly IBasketSerivce _basketSerivce;
    private readonly Mock<ICurrentUserService> _currentUserServiceMock = new();
    private readonly Mock<IProductRepository> _productRepositoryMock = new();
    private readonly Mock<IPurchaseListRepository> _purchaseListRepositoryMock = new();

    public BaseketServiceTest()
    {
        _basketSerivce = new BasketService(_basketRepositoryMock.Object, _currentUserServiceMock.Object, _productRepositoryMock.Object, _purchaseListRepositoryMock.Object);
    }

    [Theory]
    [InlineData("67bbea48-8951-48fc-b2f5-f42c98530080")]
    [InlineData(null)]
    public async Task CreateAsync_CreateBasket_ReturnBasket(string guidString)
    {
        //Arrange
        var userId = guidString == null ? null : (Guid?)Guid.Parse(guidString);
        var basketDto = new BasketFormRequestDto()
        {
            BasketItems = [],
        };

        var basketInputEntity = basketDto.ToEntity(userId);
        var basketOutputEntity = basketInputEntity.Clone();
        basketOutputEntity.Id = Guid.NewGuid();

        _currentUserServiceMock.Setup(x => x.GetUserId()).Returns(userId);
        _basketRepositoryMock
            .Setup(x =>
                x.CreateAsync(It.IsAny<BasketAggregate>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(basketOutputEntity);

        _basketRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(It.IsAny<Guid>(),
                It.IsAny<Expression<Func<BasketAggregate, BasketFormResponseDto>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BasketFormResponseDto
            {
                Id = basketOutputEntity.Id,
                BasketItems = []
            });
        //Act
        var result = await _basketSerivce.CreateAsync(basketDto, default);
        //Assert
        _basketRepositoryMock
            .Verify(x =>
                x.CreateAsync(It.Is<BasketAggregate>(x => x.UserId == userId),
                It.IsAny<CancellationToken>()));

        _basketRepositoryMock
            .Setup(x =>
                x.GetByIdAsync(It.Is<Guid>(x => x == basketOutputEntity.Id),
                It.IsAny<Expression<Func<BasketAggregate, BasketFormRequestDto>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new BasketFormResponseDto
            {
                Id = basketOutputEntity.Id,
                BasketItems = []
            });

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Result);
        Assert.Equal(basketOutputEntity.Id, result.Result.Id);
    }
}