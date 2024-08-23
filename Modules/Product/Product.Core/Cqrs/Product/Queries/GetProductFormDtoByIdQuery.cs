using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Product.Queries;
public record GetProductFormDtoByIdQuery(Guid Id) : IRequest<ProductFormDto>;

internal class GetProductFormDtoByIdQueryHandler : IRequestHandler<GetProductFormDtoByIdQuery, ProductFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductFormDto> Handle(GetProductFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.ProductPhotos)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return result != null ? new ProductFormDto(result) : null;
    }
}