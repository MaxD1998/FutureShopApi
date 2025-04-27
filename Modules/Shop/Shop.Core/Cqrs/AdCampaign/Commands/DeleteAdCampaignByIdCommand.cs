using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaign.Commands;
public record DeleteAdCampaignByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteAdCampaignByIdCommandHandler : BaseService, IRequestHandler<DeleteAdCampaignByIdCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context;

    public DeleteAdCampaignByIdCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteAdCampaignByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<AdCampaignEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}