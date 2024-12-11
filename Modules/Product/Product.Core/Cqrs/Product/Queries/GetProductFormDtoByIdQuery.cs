using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Product.Queries;
public record GetProductFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<ProductFormDto>>;

internal class GetProductFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductFormDtoByIdQuery, ResultDto<ProductFormDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
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