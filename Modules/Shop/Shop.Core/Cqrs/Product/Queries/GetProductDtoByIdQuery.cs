using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Queries;
public record GetProductDtoByIdQuery(Guid Id, Guid? FavouriteId) : IRequest<ResultDto<ProductDto>>;

internal class GetProductDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductDtoByIdQuery, ResultDto<ProductDto>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHeaderService _headerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetProductDtoByIdQueryHandler(IHeaderService headerService, IHttpContextAccessor httpContextAccessor, ShopPostgreSqlContext context)
    {
        _headerService = headerService;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResultDto<ProductDto>> Handle(GetProductDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();

        var result = await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductParameterValues)
                .ThenInclude(x => x.ProductParameter)
                    .ThenInclude(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.Id == request.Id)
            .Select(ProductDto.Map(_headerService.GetHeader(HeaderNameConst.Lang), userId, request.FavouriteId))
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}