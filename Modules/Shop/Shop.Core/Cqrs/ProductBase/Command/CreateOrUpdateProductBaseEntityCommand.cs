using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Command;

public record CreateOrUpdateProductBaseEntityCommand(ProductBaseEntity Entity) : IRequest<ResultDto>;

internal class CreateOrUpdateProductBaseEntityCommandHandler : BaseService, IRequestHandler<CreateOrUpdateProductBaseEntityCommand, ResultDto>
{
    private readonly ShopContext _context;

    public CreateOrUpdateProductBaseEntityCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(CreateOrUpdateProductBaseEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);

        if (entity is null)
            await _context.Set<ProductBaseEntity>().AddAsync(request.Entity, cancellationToken);
        else
            entity.Update(request.Entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}