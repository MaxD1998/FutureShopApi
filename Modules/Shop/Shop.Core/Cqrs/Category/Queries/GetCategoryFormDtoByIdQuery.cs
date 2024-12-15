using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetCategoryFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<CategoryFormDto>>;

internal class GetCategoryFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetCategoryFormDtoByIdQuery, ResultDto<CategoryFormDto>>
{
    private readonly ShopContext _context;

    public GetCategoryFormDtoByIdQueryHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<CategoryFormDto>> Handle(GetCategoryFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Select(CategoryFormDto.Map())
            .FirstOrDefaultAsync(cancellationToken);

        return Success(result);
    }
}