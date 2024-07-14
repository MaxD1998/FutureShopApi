using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseIdNameDtoByIdQuery(Guid Id) : IRequest<IdNameDto>;

internal class GetProductBaseIdNameDtoByIdQueryHandler : BaseRequestHandler<ProductContext, GetProductBaseIdNameDtoByIdQuery, IdNameDto>
{
    public GetProductBaseIdNameDtoByIdQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<IdNameDto> Handle(GetProductBaseIdNameDtoByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .Where(x => x.Id == request.Id)
            .Select(x => new IdNameDto(x))
            .FirstOrDefaultAsync(cancellationToken);
}