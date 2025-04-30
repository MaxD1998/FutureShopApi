using Product.Core.Dtos.ProductBase;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Services;

public interface IProductBaseService
{
}

public class ProductBaseService : BaseService, IProductBaseService
{
    public async Task<ResultDto<ProductBaseFormDto>> CreateAsync()
    {
    }
}