using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Product.Queries;
public record GetProductDtoByIdQuery(Guid Id) : IRequest<ProductDto>;

internal class GetProductDtoByIdQueryHandler : IRequestHandler<GetProductDtoByIdQuery, ProductDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetProductDtoByIdQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductDtoByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductParameterValues)
                .ThenInclude(x => x.ProductParameter)
                    .ThenInclude(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Include(x => x.ProductPhotos.OrderBy(y => y.Position))
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductDto(x))
            .FirstOrDefaultAsync(cancellationToken);
}