using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Category.Queries;
public record GetCategoryFormDtoByIdQuery(Guid Id) : IRequest<CategoryFormDto>;

internal class GetCategoryFormDtoByIdQueryHandler : IRequestHandler<GetCategoryFormDtoByIdQuery, CategoryFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public GetCategoryFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<CategoryFormDto> Handle(GetCategoryFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return result is null ? null : new CategoryFormDto(result);
    }
}