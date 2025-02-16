using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.ProductParameter;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductParameter.Queries;

public record GetListProductParameterIdNameDtoByProductIdQuery(Guid Id) : IRequest<ResultDto<IEnumerable<ProductParameterFlatDto>>>;

internal class GetListProductParameterIdNameDtoByProductIdQueryHandler(ShopContext context) : BaseService, IRequestHandler<GetListProductParameterIdNameDtoByProductIdQuery, ResultDto<IEnumerable<ProductParameterFlatDto>>>
{
    private readonly ShopContext _context = context;

    public async Task<ResultDto<IEnumerable<ProductParameterFlatDto>>> Handle(GetListProductParameterIdNameDtoByProductIdQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<ProductParameterEntity>()
            .AsNoTracking()
            .Where(x => x.ProductBase.Products.Any(x => x.Id == request.Id))
            .Select(ProductParameterFlatDto.Map(request.Id))
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<ProductParameterFlatDto>>(results);
    }
}