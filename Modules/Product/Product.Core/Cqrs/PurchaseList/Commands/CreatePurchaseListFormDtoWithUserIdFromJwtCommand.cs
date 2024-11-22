using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.PurchaseList;
using Product.Core.Errors;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.PurchaseList.Commands;
public record CreatePurchaseListFormDtoWithUserIdFromJwtCommand(PurchaseListFormDto Dto) : IRequest<PurchaseListFormDto>;

internal class CreatePurchaseListFormDtoWithUserIdFromJwtCommandHandler : IRequestHandler<CreatePurchaseListFormDtoWithUserIdFromJwtCommand, PurchaseListFormDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatePurchaseListFormDtoWithUserIdFromJwtCommandHandler(ProductPostgreSqlContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PurchaseListFormDto> Handle(CreatePurchaseListFormDtoWithUserIdFromJwtCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var dto = request.Dto;

        if (userId.HasValue && dto.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                throw new BadRequestException(ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        var entity = dto.ToEntity();

        entity.UserId = userId;

        var result = await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(result.Entity);
    }
}