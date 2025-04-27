using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Dtos.AdCampaign;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.AdCampaign.Commands;

public record UpdateAdCampaignFormDtoCommand(Guid Id, AdCampaignFormDto Dto) : IRequest<ResultDto<AdCampaignFormDto>>;

internal class UpdateAdCampaignFormDtoCommandHandler : BaseService, IRequestHandler<UpdateAdCampaignFormDtoCommand, ResultDto<AdCampaignFormDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public UpdateAdCampaignFormDtoCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<AdCampaignFormDto>> Handle(UpdateAdCampaignFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<AdCampaignEntity>()
            .Include(x => x.AdCampaignItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<AdCampaignFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync();

        return Success(await _context.Set<AdCampaignEntity>().AsNoTracking().Where(x => x.Id == entity.Id).Select(AdCampaignFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}