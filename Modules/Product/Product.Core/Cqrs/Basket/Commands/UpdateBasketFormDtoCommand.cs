using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Basket.Commands;
public record UpdateBasketFormDtoCommand(Guid Id, BasketFormDto Dto) : IRequest<BasketFormDto>;

public class UpdateBasketFormDtoCommandHandler : IRequestHandler<UpdateBasketFormDtoCommand, BasketFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateBasketFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<BasketFormDto> Handle(UpdateBasketFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<BasketEntity>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity);
    }
}