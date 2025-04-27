using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.AdCampaign;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.AdCampaign.Commands;

public record CreateAdCampaignFormDtoCommand(AdCampaignFormDto Dto) : IRequest<ResultDto<AdCampaignFormDto>>;

internal class CreateAdCampaignFormDtoCommandHandler : BaseService, IRequestHandler<CreateAdCampaignFormDtoCommand, ResultDto<AdCampaignFormDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public CreateAdCampaignFormDtoCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<AdCampaignFormDto>> Handle(CreateAdCampaignFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = request.Dto.ToEntity();

        var result = await _context.Set<AdCampaignEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Success(await _context.Set<AdCampaignEntity>().AsNoTracking().Where(x => x.Id == result.Entity.Id).Select(AdCampaignFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}