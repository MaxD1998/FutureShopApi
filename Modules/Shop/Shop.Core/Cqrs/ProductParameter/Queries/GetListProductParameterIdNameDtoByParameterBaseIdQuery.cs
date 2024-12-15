using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductParameter.Queries;

public record GetListProductParameterIdNameDtoByParameterBaseIdQuery(Guid Id) : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler : BaseService, IRequestHandler<GetListProductParameterIdNameDtoByParameterBaseIdQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ShopContext _context;

    public GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<IdNameDto>>> Handle(GetListProductParameterIdNameDtoByParameterBaseIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<ProductParameterEntity>()
            .AsNoTracking()
            .Where(x => x.ProductBaseId == request.Id)
            .Select(IdNameDto.MapFromProductParameter(null))
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<IdNameDto>>(results);
    }
}