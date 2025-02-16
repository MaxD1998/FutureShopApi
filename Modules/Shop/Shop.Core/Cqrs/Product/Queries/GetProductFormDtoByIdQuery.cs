using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Queries;
public record GetProductFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<ProductFormDto>>;

internal class GetProductFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductFormDtoByIdQuery, ResultDto<ProductFormDto>>
{
    private readonly ShopContext _context;

    public GetProductFormDtoByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ProductFormDto>> Handle(GetProductFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>()
            .Where(x => x.Id == request.Id)
            .Select(ProductFormDto.Map())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}