using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Core.Errors;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.PurchaseList.Commands;
public record UpdatePurchaseListFormDtoCommand(Guid Id, PurchaseListFormDto Dto) : IRequest<PurchaseListFormDto>;

internal class UpdatePurchaseListFormDtoWithUserIdFromJwtCommandHandler : IRequestHandler<UpdatePurchaseListFormDtoCommand, PurchaseListFormDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdatePurchaseListFormDtoWithUserIdFromJwtCommandHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PurchaseListFormDto> Handle(UpdatePurchaseListFormDtoCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var entity = await _context.Set<PurchaseListEntity>()
            .Include(x => x.PurchaseListItems)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        if (userId.HasValue && request.Dto.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.Id != request.Id && x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                throw new BadRequestException(ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity);
    }
}