using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>
{
}

public class ProductBaseRepository(ProductContext context) : BaseRepository<ProductContext, ProductBaseEntity>(context), IProductBaseRepository
{
}