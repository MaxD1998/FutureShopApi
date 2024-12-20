using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.Basket;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Basket.Commands;

public record CreateBasketFormDtoCommand(BasketFormDto Dto) : IRequest<ResultDto<BasketFormDto>>;

internal class CreateBasketFormDtoCommandHandler : BaseService, IRequestHandler<CreateBasketFormDtoCommand, ResultDto<BasketFormDto>>
{
    private readonly ShopContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBasketFormDtoCommandHandler(ShopContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<BasketFormDto>> Handle(CreateBasketFormDtoCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var entity = request.Dto.ToEntity();

        entity.UserId = userId;

        var result = await _context.Set<BasketEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success(await _context.Set<BasketEntity>().AsNoTracking().Where(x => x.Id == result.Entity.Id).Select(BasketFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}