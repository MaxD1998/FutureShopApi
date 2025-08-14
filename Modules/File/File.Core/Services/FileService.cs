using File.Core.Dtos.ProductPhoto;
using File.Core.Errors;
using File.Core.Extensions;
using File.Infrastructure.Documents;
using File.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using System.Net;

namespace File.Core.Services;

public interface IFileService
{
    Task<ResultDto<List<string>>> CreateListAsync(IFormFileCollection files, CancellationToken cancellationToken);

    Task DeleteManyAsync(List<string> ids, CancellationToken cancellationToken);

    Task<ResultDto<FileDocument>> GetByIdAsync(string id, CancellationToken cancellationToken);

    Task<ResultDto<List<ProductPhotoInfoDto>>> GetListInfoByIdsAsync(List<string> ids, CancellationToken cancellationToken);
}

internal class FileService(IFileRepository fileRepository) : BaseService, IFileService
{
    private readonly IFileRepository _fileRepository = fileRepository;

    public async Task<ResultDto<List<string>>> CreateListAsync(IFormFileCollection files, CancellationToken cancellationToken)
    {
        if (!files.Any())
            return Success<List<string>>([]);

        if (files.Any(x => x.Length == 0))
            return Error<List<string>>(HttpStatusCode.BadRequest, ExceptionMessage.ProductPhoto001OneOfFilesWasEmpty);

        var results = await _fileRepository.CreateListAsync(files.ToProductPhotoDocuments(), cancellationToken);

        return Success(results.Select(x => x.Id).ToList());
    }

    public Task DeleteManyAsync(List<string> ids, CancellationToken cancellationToken)
        => _fileRepository.DeleteManyByIdsAsync(ids, cancellationToken);

    public async Task<ResultDto<FileDocument>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _fileRepository.GetByIdAsync(id, cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<List<ProductPhotoInfoDto>>> GetListInfoByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var documents = await _fileRepository.GetListByAsync(x => ids.Contains(x.Id), x => new { x.Id, x.ContentType, x.Length, x.Name }, cancellationToken);
        var results = documents.Select(x => new ProductPhotoInfoDto(x.Id, x.ContentType, x.Length, x.Name)).ToList();

        return Success(results);
    }
}