using Shared.Core.Dtos;
using System.Threading.Tasks;
using System.Threading;

namespace File.Core.JobServices.Interfaces;

public interface IFileJobService
{
    Task<ResultDto> SendIdBatchesForAssignmentCheck(CancellationToken cancellationToken);
}
