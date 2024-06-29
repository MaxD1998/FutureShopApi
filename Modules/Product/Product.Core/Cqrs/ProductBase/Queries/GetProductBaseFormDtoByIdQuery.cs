using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseFormDtoByIdQuery(Guid Id) : IRequest<ProductBaseFormDto>;

internal class GetProductBaseFormDtoByIdQueryHandler : BaseRequestHandler<ProductContext, GetProductBaseFormDtoByIdQuery, ProductBaseFormDto>
{
    public GetProductBaseFormDtoByIdQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductBaseFormDto> Handle(GetProductBaseFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Include(x => x.Products)
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return result is null ? null : new ProductBaseFormDto(result);
    }
}