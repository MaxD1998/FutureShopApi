using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseIdNameDtoByIdQuery(Guid Id) : IRequest<ResultDto<IdNameDto>>;

internal class GetProductBaseIdNameDtoByIdQueryHandler(ShopContext context) : BaseService, IRequestHandler<GetProductBaseIdNameDtoByIdQuery, ResultDto<IdNameDto>>
{
    private readonly ShopContext _context = context;

    public async Task<ResultDto<IdNameDto>> Handle(GetProductBaseIdNameDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Where(x => x.Id == request.Id)
            .Select(IdNameDto.MapFromProductBase())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}