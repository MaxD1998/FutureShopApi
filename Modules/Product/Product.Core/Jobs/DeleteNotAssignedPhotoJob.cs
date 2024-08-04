using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Domain.Entities;
using Product.Infrastructure;
using Quartz;

namespace Product.Core.Jobs;

[DisallowConcurrentExecution]
public class DeleteNotAssignedPhotoJob : IJob
{
    private readonly ProductMongoDbContext _mongoDbContext;
    private readonly ProductPostgreSqlContext _postgreSqlContext;

    public DeleteNotAssignedPhotoJob(ProductMongoDbContext mongoDbContext, ProductPostgreSqlContext postgreSqlContext)
    {
        _mongoDbContext = mongoDbContext;
        _postgreSqlContext = postgreSqlContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var fileIds = await _postgreSqlContext.Set<ProductPhotoEntity>().Select(p => p.FileId).ToListAsync(context.CancellationToken);
        await _mongoDbContext.Set<ProductPhotoDocument>().DeleteManyAsync(x => !fileIds.Contains(x.Id) && x.CreateTime.AddMinutes(1) < DateTime.UtcNow, context.CancellationToken);
    }
}