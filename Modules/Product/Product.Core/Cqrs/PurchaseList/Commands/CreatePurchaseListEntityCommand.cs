using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Errors;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.PurchaseList.Commands;
public record CreatePurchaseListEntityCommand(PurchaseListEntity Entity) : IRequest<PurchaseListEntity>;

public class CreatePurchaseListEntityCommandHandler : IRequestHandler<CreatePurchaseListEntityCommand, PurchaseListEntity>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatePurchaseListEntityCommandHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PurchaseListEntity> Handle(CreatePurchaseListEntityCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var entity = request.Entity;

        if (userId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                throw new BadRequestException(ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        entity.UserId = userId;

        var result = await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}