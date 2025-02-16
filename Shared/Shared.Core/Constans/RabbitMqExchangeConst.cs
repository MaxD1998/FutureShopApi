namespace Shared.Core.Constans;

public static class RabbitMqExchangeConst
{
    public const string ProductModuleCategory = $"{_productModule}-Category";
    public const string ProductModuleProduct = $"{_productModule}-Product";
    public const string ProductModuleProductBase = $"{_productModule}-ProductBase";
    private const string _productModule = "ProductModule";
}