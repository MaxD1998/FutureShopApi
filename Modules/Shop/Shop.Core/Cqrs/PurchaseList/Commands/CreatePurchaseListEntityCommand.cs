using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Errors;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.PurchaseList.Commands;
public record CreatePurchaseListEntityCommand(PurchaseListEntity Entity) : IRequest<ResultDto<PurchaseListEntity>>;

public class CreatePurchaseListEntityCommandHandler : BaseService, IRequestHandler<CreatePurchaseListEntityCommand, ResultDto<PurchaseListEntity>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatePurchaseListEntityCommandHandler(ShopPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<PurchaseListEntity>> Handle(CreatePurchaseListEntityCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var entity = request.Entity;

        if (userId.HasValue && entity.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return Error<PurchaseListEntity>(HttpStatusCode.BadRequest, ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        entity.UserId = userId;

        var result = await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success(result.Entity);
    }
}