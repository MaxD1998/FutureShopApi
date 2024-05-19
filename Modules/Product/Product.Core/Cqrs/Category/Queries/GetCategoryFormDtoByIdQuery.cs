using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Queries;
public record GetCategoryFormDtoByIdQuery(Guid Id) : IRequest<CategoryFormDto>;

internal class GetCategoryFormDtoByIdQueryHandler : BaseRequestHandler<ProductContext, GetCategoryFormDtoByIdQuery, CategoryFormDto>
{
    public GetCategoryFormDtoByIdQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<CategoryFormDto> Handle(GetCategoryFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        return result is null ? null : new CategoryFormDto(result);
    }
}