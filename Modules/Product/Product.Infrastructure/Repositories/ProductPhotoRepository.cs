using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Shared.Infrastructure.Bases;
using Product.Core.Interfaces.Repositories;

namespace Product.Infrastructure.Repositories;

internal class ProductPhotoRepository(ProductContext context) : BaseRepository<ProductContext, ProductPhotoEntity>(context), IProductPhotoRepository
{
    public async Task<List<string>> GetMissingFileIdsAsync(List<string> fileIds, CancellationToken cancellationToken)
    {
        var productPhotoFileIds = await _context.Set<ProductPhotoEntity>().Where(x => fileIds.Contains(x.FileId)).Select(x => x.FileId).ToListAsync(cancellationToken);
        var results = fileIds.Except(productPhotoFileIds).ToList();

        return results;
    }
}