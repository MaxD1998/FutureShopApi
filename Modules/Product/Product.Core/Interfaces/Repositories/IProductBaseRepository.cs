using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Interfaces.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>, IUpdateRepository<ProductBaseEntity>
{
}