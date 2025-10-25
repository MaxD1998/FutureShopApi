namespace Shared.Core.Constans;

public static class RabbitMqExchangeConst
{
    public const string AuthorizationModuleUser = $"{_authorizationModule}-User";
    public const string FileModduleToDelete = $"{_fileModule}-FilesToDelete";
    public const string ProductModuleCategory = $"{_productModule}-Category";
    public const string ProductModuleFilesToDelete = $"{_productModule}-FilesToDelete";
    public const string ProductModuleProduct = $"{_productModule}-Product";
    public const string ProductModuleProductBase = $"{_productModule}-ProductBase";
    private const string _authorizationModule = "AuthorizationModule";
    private const string _fileModule = "FileModule";
    private const string _productModule = "ProductModule";
}