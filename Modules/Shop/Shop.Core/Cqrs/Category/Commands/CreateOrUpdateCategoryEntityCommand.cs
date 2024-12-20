using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Commands;
public record CreateOrUpdateCategoryEntityCommand(CategoryEntity Entity) : IRequest<ResultDto>;

internal class CreateOrUpdateCategoryEntityCommandHandler : BaseService, IRequestHandler<CreateOrUpdateCategoryEntityCommand, ResultDto>
{
    private readonly ShopContext _context;

    public CreateOrUpdateCategoryEntityCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(CreateOrUpdateCategoryEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);

        entity ??= new CategoryEntity();
        entity.Update(request.Entity);

        if (entity.Id == Guid.Empty)
            await _context.Set<CategoryEntity>().AddAsync(request.Entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}