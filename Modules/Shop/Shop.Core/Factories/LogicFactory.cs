using Shared.Core.Dtos;
using Shared.Shared.Interfaces;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Logics.ProductLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Factories;

internal interface ILogicFactory : ILogicFactory<ILogicFactory>
{
    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic(IProductRepository productRepository);

    ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic(IProductRepository productRepository);

    ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic(IProductRepository productRepository);
}

internal class LogicFactory() : ILogicFactory
{
    public Task<TResult> ExecuteAsync<TRequest, TResult>(TRequest request, Func<ILogicFactory, ILogic<TRequest, TResult>> factorySelector, CancellationToken cancellationToken)
    {
        var instace = factorySelector(this);

        return instace.ExecuteAsync(request, cancellationToken);
    }

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateAddPriceLogic(IProductRepository productRepository)
        => new SimulateAddPriceLogic(productRepository);

    public ILogic<SimulateRemovePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateRemovePriceLogic(IProductRepository productRepository)
        => new SimulateRemovePriceLogic(productRepository);

    public ILogic<SimulatePriceRequestDto, ResultDto<List<SimulatePriceFormDto>>> SimulateUpdatePriceLogic(IProductRepository productRepository)
        => new SimulateUpdatePriceLogic(productRepository);
}