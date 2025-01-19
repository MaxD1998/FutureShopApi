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
            .FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);

        entity ??= new ProductBaseEntity();
        entity.Update(request.Entity);

        if (entity.Id == Guid.Empty)
            await _context.Set<ProductBaseEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}