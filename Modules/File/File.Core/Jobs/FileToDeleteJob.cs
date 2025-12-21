using File.Core.JobServices.Interfaces;
using Quartz;

namespace File.Core.Jobs;

[DisallowConcurrentExecution]
public class FileToDeleteJob(IFileJobService fileJobService) : IJob
{
    private readonly IFileJobService _fileJobService = fileJobService;

    public Task Execute(IJobExecutionContext context)
        => _fileJobService.SendIdBatchesForAssignmentCheck(context.CancellationToken);
}