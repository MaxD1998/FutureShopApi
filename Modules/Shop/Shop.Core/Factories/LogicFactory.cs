using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Core.Logics.Baskets;
using Shop.Core.Logics.ProductLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Factories;

internal interface ILogicFactory
{
    Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken);

    ILogic<GetBasketDtoByIdRequestModel, ResultDto<BasketDto>> GetBasketDtoByIdLogic(IBasketRepository basketRepository, IProductRepository productRepository);

    ILogic<GetBasketDtoByUserIdRequestModel, ResultDto<BasketDto>> GetBasketDtoByUserIdLogic(IBasketRepository basketRepository, IProductRepository productRepository);

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic(IProductRepository productRepository);

    ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic(IProductRepository productRepository);

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic(IProductRepository productRepository);
}

internal class LogicFactory : ILogicFactory
{
    public Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken)
    {
        var instace = factorySelector(this);

        return instace.ExecuteAsync(request, cancellationToken);
    }

    public ILogic<GetBasketDtoByIdRequestModel, ResultDto<BasketDto>> GetBasketDtoByIdLogic(IBasketRepository basketRepository, IProductRepository productRepository)
        => new GetBasketDtoByIdLogic(basketRepository, productRepository);

    public ILogic<GetBasketDtoByUserIdRequestModel, ResultDto<BasketDto>> GetBasketDtoByUserIdLogic(IBasketRepository basketRepository, IProductRepository productRepository)
        => new GetBasketDtoByUserIdLogic(basketRepository, productRepository);

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic(IProductRepository productRepository)
        => new SimulateAddPriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic(IProductRepository productRepository)
        => new SimulateRemovePriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic(IProductRepository productRepository)
        => new SimulateUpdatePriceLogic(new GetProductWasActiveLogic(productRepository));
}