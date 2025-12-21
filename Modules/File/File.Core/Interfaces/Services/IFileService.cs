using Microsoft.AspNetCore.Http;
using File.Domain.Documents;
using Shared.Core.Dtos;
using File.Core.Dtos.ProductPhoto;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace File.Core.Interfaces.Services;

public interface IFileService
{
    Task<ResultDto<List<string>>> CreateListAsync(IFormFileCollection files, CancellationToken cancellationToken);

    Task DeleteManyAsync(List<string> ids, CancellationToken cancellationToken);

    Task<ResultDto<FileDocument>> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductPhotoInfoDto>>> GetListInfoByIdsAsync(List<string> ids, CancellationToken cancellationToken);
}
