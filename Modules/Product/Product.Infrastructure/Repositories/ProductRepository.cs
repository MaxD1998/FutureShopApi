using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Repositories;

public interface IProductRepository : IBaseRepository<ProductEntity>
{
}

public class ProductRepository(ProductContext context) : BaseRepository<ProductContext, ProductEntity>(context), IProductRepository
{
}