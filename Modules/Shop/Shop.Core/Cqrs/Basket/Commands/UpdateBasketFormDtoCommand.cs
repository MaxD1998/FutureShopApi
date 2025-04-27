using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Basket;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Basket.Commands;
public record UpdateBasketFormDtoCommand(Guid Id, BasketFormDto Dto) : IRequest<ResultDto<BasketFormDto>>;

public class UpdateBasketFormDtoCommandHandler : BaseService, IRequestHandler<UpdateBasketFormDtoCommand, ResultDto<BasketFormDto>>
{
    private readonly ShopPostgreSqlContext _context;

    public UpdateBasketFormDtoCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<BasketFormDto>> Handle(UpdateBasketFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<BasketEntity>()
            .Include(x => x.BasketItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return Success(await _context.Set<BasketEntity>().AsNoTracking().Where(x => x.Id == request.Id).Select(BasketFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}