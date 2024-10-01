using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetCategoryIdNameDtoByIdQuery(Guid Id) : IRequest<IdNameDto>;

internal class GetCategoryIdNameDtoByIdQueryHandler : IRequestHandler<GetCategoryIdNameDtoByIdQuery, IdNameDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetCategoryIdNameDtoByIdQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<IdNameDto> Handle(GetCategoryIdNameDtoByIdQuery request, CancellationToken cancellationToken)
        => await _context
            .Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.Id == request.Id)
            .Select(x => new IdNameDto(x))
            .FirstOrDefaultAsync(cancellationToken);
}