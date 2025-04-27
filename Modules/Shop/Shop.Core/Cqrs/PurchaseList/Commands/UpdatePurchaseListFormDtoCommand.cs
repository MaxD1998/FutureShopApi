using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using Shop.Core.Dtos.PurchaseList;
using Shop.Core.Errors;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.PurchaseList.Commands;
public record UpdatePurchaseListFormDtoCommand(Guid Id, PurchaseListFormDto Dto) : IRequest<ResultDto<PurchaseListFormDto>>;

internal class UpdatePurchaseListFormDtoWithUserIdFromJwtCommandHandler : BaseService, IRequestHandler<UpdatePurchaseListFormDtoCommand, ResultDto<PurchaseListFormDto>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePurchaseListFormDtoWithUserIdFromJwtCommandHandler(ShopPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<PurchaseListFormDto>> Handle(UpdatePurchaseListFormDtoCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var entity = await _context.Set<PurchaseListEntity>()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<PurchaseListFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        if (userId.HasValue && request.Dto.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.Id != request.Id && x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return Error<PurchaseListFormDto>(HttpStatusCode.BadRequest, ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return Success(await _context.Set<PurchaseListEntity>().AsNoTracking().Where(x => x.Id == entity.Id).Select(PurchaseListFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}