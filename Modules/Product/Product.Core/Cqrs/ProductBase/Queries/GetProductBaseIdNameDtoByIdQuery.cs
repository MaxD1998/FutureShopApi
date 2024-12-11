using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseIdNameDtoByIdQuery(Guid Id) : IRequest<ResultDto<IdNameDto>>;

internal class GetProductBaseIdNameDtoByIdQueryHandler : BaseService, IRequestHandler<GetProductBaseIdNameDtoByIdQuery, ResultDto<IdNameDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductBaseIdNameDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IdNameDto>> Handle(GetProductBaseIdNameDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Where(x => x.Id == request.Id)
            .Select(IdNameDto.MapFromProductBase())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}