using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Interfaces.Repositories;

public interface IProductPhotoRepository : IBaseRepository<ProductPhotoEntity>
{
    Task<List<string>> GetMissingFileIdsAsync(List<string> fileIds, CancellationToken cancellationToken);
}