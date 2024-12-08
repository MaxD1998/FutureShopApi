using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Core.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Extensions;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Product.Queries;
public record GetProductDtoByIdQuery(Guid Id, Guid? FavouriteId) : IRequest<ProductDto>;

internal class GetProductDtoByIdQueryHandler : IRequestHandler<GetProductDtoByIdQuery, ProductDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProductDtoByIdQueryHandler(IHeaderService headerService, IHttpContextAccessor httpContextAccessor, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ProductDto> Handle(GetProductDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        return await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductParameterValues)
                .ThenInclude(x => x.ProductParameter)
                    .ThenInclude(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Include(x => x.ProductPhotos.OrderBy(y => y.Position))
            .Include(x => x.PurchaseListItems.Where(y => (y.PurchaseList.UserId != null && y.PurchaseList.UserId == userId) || y.PurchaseListId == request.FavouriteId))
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductDto(x))
            .FirstOrDefaultAsync(cancellationToken);
    }
}