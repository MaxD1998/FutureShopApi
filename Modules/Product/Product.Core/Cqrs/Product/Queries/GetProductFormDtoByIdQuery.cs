using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Product.Queries;
public record GetProductFormDtoByIdQuery(Guid Id) : IRequest<ProductFormDto>;

internal class GetProductFormDtoByIdQueryHandler : BaseRequestHandler<ProductContext, GetProductFormDtoByIdQuery, ProductFormDto>
{
    public GetProductFormDtoByIdQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductFormDto> Handle(GetProductFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return result != null ? new ProductFormDto(result) : null;
    }
}