using File.Core.Services;
using Shared.Core.Interfaces;

namespace File.Core.EventHandlers;

public class DeleteNotAssignedFilesEventHandler(IFileService fileService) : IMessageEventHandler
{
    private readonly IFileService _fileService = fileService;

    public string Exchange => throw new NotImplementedException();

    public string QueueName => throw new NotImplementedException();

    public Task ExecuteAsync(string message, CancellationToken cancellationToken) => throw new NotImplementedException();
}