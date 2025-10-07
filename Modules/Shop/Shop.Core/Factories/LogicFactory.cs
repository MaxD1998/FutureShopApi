using Shared.Core.Dtos;
using Shared.Shared.Interfaces;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Interfaces;
using Shop.Core.Logics.ProductLogics;
using Shop.Core.Logics.PromotionLogics;
using Shop.Core.Logics.PromotionLogics.Models;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Factories;

public interface ILogicFactory : ILogicFactory<ILogicFactory>
{
    ILogic<SetPromotionForProductsRequestModel<T>, List<T>> SetPercentPromotionForProductsLogic<T>() where T : IProductPrice;

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic();

    ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic();

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic();
}

internal class LogicFactory(IProductRepository productRepository) : ILogicFactory
{
    private readonly IProductRepository _productRepository = productRepository;

    public Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken)
    {
        var instace = factorySelector(this);

        return instace.ExecuteAsync(request, cancellationToken);
    }

    public ILogic<SetPromotionForProductsRequestModel<T>, List<T>> SetPercentPromotionForProductsLogic<T>() where T : IProductPrice
        => new SetPercentPromotionForProductsLogic<T>();

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic()
        => new SimulateAddPriceLogic(_productRepository);

    public ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic()
        => new SimulateRemovePriceLogic(_productRepository);

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic()
        => new SimulateUpdatePriceLogic(_productRepository);
}