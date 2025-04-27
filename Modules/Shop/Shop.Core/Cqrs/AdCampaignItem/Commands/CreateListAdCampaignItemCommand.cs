using MediatR;
using Microsoft.AspNetCore.Http;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Errors;
using Shop.Core.Extensions;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.AdCampaignItem.Commands;

public record CreateListAdCampaignItemCommand(IFormFileCollection Files) : IRequest<ResultDto<IEnumerable<string>>>;

internal class CreateListAdCampaignItemCommandHandler : BaseService, IRequestHandler<CreateListAdCampaignItemCommand, ResultDto<IEnumerable<string>>>
{
    private readonly ShopMongoDbContext _context;

    public CreateListAdCampaignItemCommandHandler(ShopMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<string>>> Handle(CreateListAdCampaignItemCommand request, CancellationToken cancellationToken)
    {
        if (!request.Files.Any())
            return Success<IEnumerable<string>>([]);

        if (request.Files.Any(x => x.Length == 0))
            return Error<IEnumerable<string>>(HttpStatusCode.BadRequest, ExceptionMessage.AdCampaignItem002OneOfFilesWasEmpty);

        var productPhotos = request.Files.Select(x => x.ToAdCampaignItemDocument()).ToList();
        await _context.AddRangeAsync(productPhotos, cancellationToken);

        return Success(productPhotos.Select(x => x.Id));
    }
}