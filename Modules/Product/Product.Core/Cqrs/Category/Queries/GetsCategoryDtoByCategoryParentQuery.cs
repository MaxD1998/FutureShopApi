using AutoMapper;
using MediatR;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Queries;
public record GetsCategoryDtoByCategoryParentQuery(Guid? CategoryParentId) : IRequest<IEnumerable<CategoryDto>>;

internal class GetsCategoryDtoByCategoryParentQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryDtoByCategoryParentQuery, IEnumerable<CategoryDto>>
{
    public GetsCategoryDtoByCategoryParentQueryHandler(ProductContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<IEnumerable<CategoryDto>> Handle(GetsCategoryDtoByCategoryParentQuery request, CancellationToken cancellationToken)
        => await GetsAsync<CategoryEntity, CategoryDto>(x => x.ParentCategoryId == request.CategoryParentId);
}