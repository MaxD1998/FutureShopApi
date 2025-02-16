using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Commands;
public record CreateOrUpdateProductEventDtoCommand(ProductEventDto Dto) : IRequest<ResultDto>;

internal class CreateOrUpdateProductEventDtoCommandHandler : BaseService, IRequestHandler<CreateOrUpdateProductEventDtoCommand, ResultDto>
{
    private readonly ShopContext _context;

    public CreateOrUpdateProductEventDtoCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(CreateOrUpdateProductEventDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductPhotos)
            .FirstOrDefaultAsync(x => x.ExternalId == request.Dto.Id, cancellationToken);

        var eventEntity = request.Dto.Map(_context);

        if (entity is null)
            await _context.Set<ProductEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);

        return Success();
    }
}