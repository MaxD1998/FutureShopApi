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
    ILogic<GetBasketDtoByIdRequestModel, ResultDto<BasketDto>> CreateGetBasketDtoByIdLogic(IBasketRepository basketRepository, IProductRepository productRepository);

    ILogic<GetBasketDtoByUserIdRequestModel, ResultDto<BasketDto>> CreateGetBasketDtoByUserIdLogic(IBasketRepository basketRepository, IProductRepository productRepository);

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateAddPriceLogic(IProductRepository productRepository);

    ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateRemovePriceLogic(IProductRepository productRepository);

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateUpdatePriceLogic(IProductRepository productRepository);

    Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken);
}

internal class LogicFactory : ILogicFactory
{
    public ILogic<GetBasketDtoByIdRequestModel, ResultDto<BasketDto>> CreateGetBasketDtoByIdLogic(IBasketRepository basketRepository, IProductRepository productRepository)
        => new GetBasketDtoByIdLogic(basketRepository, productRepository);

    public ILogic<GetBasketDtoByUserIdRequestModel, ResultDto<BasketDto>> CreateGetBasketDtoByUserIdLogic(IBasketRepository basketRepository, IProductRepository productRepository)
        => new GetBasketDtoByUserIdLogic(basketRepository, productRepository);

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateAddPriceLogic(IProductRepository productRepository)
        => new SimulateAddPriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateRemovePriceLogic(IProductRepository productRepository)
        => new SimulateRemovePriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> CreateSimulateUpdatePriceLogic(IProductRepository productRepository)
        => new SimulateUpdatePriceLogic(new GetProductWasActiveLogic(productRepository));

    public Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken)
    {
        var instace = factorySelector(this);

        return instace.ExecuteAsync(request, cancellationToken);
    }
}