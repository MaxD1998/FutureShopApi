using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Commands;
public record CreateOrUpdateCategoryEventDtoCommand(CategoryEventDto Dto) : IRequest<ResultDto>;

internal class CreateOrUpdateCategoryEventDtoCommandHandler : BaseService, IRequestHandler<CreateOrUpdateCategoryEventDtoCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context;

    public CreateOrUpdateCategoryEventDtoCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(CreateOrUpdateCategoryEventDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .FirstOrDefaultAsync(x => x.ExternalId == request.Dto.Id, cancellationToken);

        var eventEntity = request.Dto.Map(_context);

        if (entity is null)
            await _context.Set<CategoryEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}