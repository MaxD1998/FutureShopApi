using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseIdNameDtoByIdQuery(Guid Id) : IRequest<IdNameDto>;

internal class GetProductBaseIdNameDtoByIdQueryHandler : IRequestHandler<GetProductBaseIdNameDtoByIdQuery, IdNameDto>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductBaseIdNameDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<IdNameDto> Handle(GetProductBaseIdNameDtoByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .Where(x => x.Id == request.Id)
            .Select(x => new IdNameDto(x))
            .FirstOrDefaultAsync(cancellationToken);
}