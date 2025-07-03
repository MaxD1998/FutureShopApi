using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Price;
using Shop.Core.Interfaces;
using Shop.Core.Logics.ProductLogics;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Factories;

internal interface ILogicFactory
{
    ILogic<SimulatePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateAddPriceLogic(IProductRepository productRepository);

    ILogic<SimulateRemovePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateRemovePriceLogic(IProductRepository productRepository);

    ILogic<SimulatePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateUpdatePriceLogic(IProductRepository productRepository);
}

internal class LogicFactory() : ILogicFactory
{
    public ILogic<SimulatePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateAddPriceLogic(IProductRepository productRepository)
        => new SimulateAddPriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulateRemovePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateRemovePriceLogic(IProductRepository productRepository)
        => new SimulateRemovePriceLogic(new GetProductWasActiveLogic(productRepository));

    public ILogic<SimulatePriceRequest, ResultDto<List<PriceFormDto>>> CreateSimulateUpdatePriceLogic(IProductRepository productRepository)
        => new SimulateUpdatePriceLogic(new GetProductWasActiveLogic(productRepository));
}