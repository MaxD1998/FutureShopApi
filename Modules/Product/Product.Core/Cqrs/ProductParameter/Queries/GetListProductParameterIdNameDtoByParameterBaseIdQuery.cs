using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductParameter.Queries;

public record GetListProductParameterIdNameDtoByParameterBaseIdQuery(Guid Id) : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler : BaseService, IRequestHandler<GetListProductParameterIdNameDtoByParameterBaseIdQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetsProductParameterIdNameDtoByParameterBaseIdQueryHandler(ProductPostgreSqlContext context)
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