using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Category.Queries;
public record GetCategoryFormDtoByIdQuery(Guid Id) : IRequest<ResultDto<CategoryFormDto>>;

internal class GetCategoryFormDtoByIdQueryHandler : BaseService, IRequestHandler<GetCategoryFormDtoByIdQuery, ResultDto<CategoryFormDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetCategoryFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
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