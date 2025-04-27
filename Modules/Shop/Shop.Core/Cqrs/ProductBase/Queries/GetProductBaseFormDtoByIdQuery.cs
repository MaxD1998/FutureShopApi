using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class GetProductBaseFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductBaseFormDtoByIdQuery, ResultDto<ProductBaseFormDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetProductBaseFormDtoByIdQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(GetProductBaseFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Where(x => x.Id == request.Id)
            .Select(ProductBaseFormDto.Map())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}