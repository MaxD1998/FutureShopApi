using Product.Domain.Entities;
using Shared.Infrastructure.Bases;

namespace Product.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>
{
}

public class ProductBaseRepository(ProductPostgreSqlContext context) : BaseRepository<ProductPostgreSqlContext, ProductBaseEntity>(context), IProductBaseRepository
{
}