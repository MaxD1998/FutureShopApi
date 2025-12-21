using File.Core.Interfaces.Repositories;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Interfaces;
using File.Core.JobServices.Interfaces;

namespace File.Core.JobServices;

internal class FileJobService(IFileRepository fileRepository, IRabbitMqContext rabbitMqContext) : IFileJobService
{
    private readonly IFileRepository _fileRepository = fileRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto> SendIdBatchesForAssignmentCheck(CancellationToken cancellationToken)
    {
        var recordsCount = await _fileRepository.CountAsync(cancellationToken);
        var pageSize = 1000;
        var totalPages = recordsCount / pageSize;

        for (var i = 0; i < totalPages; i++)
        {
            var ids = await _fileRepository.Get1000IdAsync(i, cancellationToken);

            await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.FileModduleToDelete, EventMessageDto.Create(ids, MessageType.CheckMissingFileIds));
        }

        return ResultDto.Success();
    }
}