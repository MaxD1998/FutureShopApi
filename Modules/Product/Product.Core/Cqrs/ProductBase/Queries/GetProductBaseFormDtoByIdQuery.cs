using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class GetProductBaseFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductBaseFormDtoByIdQuery, ResultDto<ProductBaseFormDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductBaseFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
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