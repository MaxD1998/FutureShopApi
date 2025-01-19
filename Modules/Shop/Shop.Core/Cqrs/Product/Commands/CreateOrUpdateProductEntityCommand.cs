using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Commands;
public record CreateOrUpdateProductEntityCommand(ProductEntity Entity) : IRequest<ResultDto>;

internal class CreateOrUpdateProductEntityCommandHandler : BaseService, IRequestHandler<CreateOrUpdateProductEntityCommand, ResultDto>
{
    private readonly ShopContext _context;

    public CreateOrUpdateProductEntityCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(CreateOrUpdateProductEntityCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.ProductPhotos)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Entity.Id, cancellationToken);

        entity ??= new ProductEntity();
        entity.Update(request.Entity);

        if (entity.Id == Guid.Empty)
            await _context.Set<ProductEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}