using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shop.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetCategoryIdNameDtoByIdQuery(Guid Id) : IRequest<ResultDto<IdNameDto>>;

internal class GetCategoryIdNameDtoByIdQueryHandler : BaseService, IRequestHandler<GetCategoryIdNameDtoByIdQuery, ResultDto<IdNameDto>>
{
    private readonly ShopContext _context;
    private readonly IHeaderService _headerService;

    public GetCategoryIdNameDtoByIdQueryHandler(IHeaderService headerService, ShopContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<IdNameDto>> Handle(GetCategoryIdNameDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context
            .Set<CategoryEntity>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(IdNameDto.MapFromCategory())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}