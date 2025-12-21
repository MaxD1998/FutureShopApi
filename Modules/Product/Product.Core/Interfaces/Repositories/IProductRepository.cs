using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Interfaces.Repositories;

public interface IProductRepository : IBaseRepository<ProductEntity>, IUpdateRepository<ProductEntity>
{
}