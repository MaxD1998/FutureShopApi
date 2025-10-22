using Microsoft.Extensions.DependencyInjection;
using Shared.Core.Dtos;
using Shared.Shared.Interfaces;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Product.Price;
using Shop.Core.Interfaces;
using Shop.Core.Logics.ProductLogics;
using Shop.Core.Logics.PromotionLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Factories;

public interface ILogicFactory : ILogicFactory<ILogicFactory>
{
    ILogic<SetPromotionForProductsRequestModel<T>, List<T>> SetPromotionForProductsLogic<T>() where T : IProductPrice;

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic();

    ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic();

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic();
}

internal class LogicFactory(IServiceProvider serviceProvider) : ILogicFactory
{
    public Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken)
    {
        var instace = factorySelector(this);

        return instace.ExecuteAsync(request, cancellationToken);
    }

    public ILogic<SetPromotionForProductsRequestModel<T>, List<T>> SetPromotionForProductsLogic<T>() where T : IProductPrice
    {
        var promotionRepository = serviceProvider.GetRequiredService<IPromotionRepository>();
        return new SetPromotionForProductsLogic<T>(promotionRepository);
    }

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic()
    {
        var productRepository = serviceProvider.GetRequiredService<IProductRepository>();

        return new SimulateAddPriceLogic(productRepository);
    }

    public ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic()
    {
        var productRepository = serviceProvider.GetRequiredService<IProductRepository>();

        return new SimulateRemovePriceLogic(productRepository);
    }

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic()
    {
        var productRepository = serviceProvider.GetRequiredService<IProductRepository>();

        return new SimulateUpdatePriceLogic(productRepository);
    }
}