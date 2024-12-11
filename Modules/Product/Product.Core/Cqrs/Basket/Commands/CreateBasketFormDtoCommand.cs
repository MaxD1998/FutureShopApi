using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Basket;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Basket.Commands;

public record CreateBasketFormDtoCommand(BasketFormDto Dto) : IRequest<ResultDto<BasketFormDto>>;

internal class CreateBasketFormDtoCommandHandler : BaseService, IRequestHandler<CreateBasketFormDtoCommand, ResultDto<BasketFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateBasketFormDtoCommandHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
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