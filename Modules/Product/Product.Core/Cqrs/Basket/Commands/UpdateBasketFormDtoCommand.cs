using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Basket.Commands;
public record UpdateBasketFormDtoCommand(Guid Id, BasketFormDto Dto) : IRequest<ResultDto<BasketFormDto>>;

public class UpdateBasketFormDtoCommandHandler : BaseService, IRequestHandler<UpdateBasketFormDtoCommand, ResultDto<BasketFormDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateBasketFormDtoCommandHandler(ProductPostgreSqlContext context)
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