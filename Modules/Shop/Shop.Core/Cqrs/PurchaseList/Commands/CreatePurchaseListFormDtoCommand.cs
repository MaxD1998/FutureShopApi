﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.PurchaseList;
using Shop.Core.Errors;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.PurchaseList.Commands;
public record CreatePurchaseListFormDtoCommand(PurchaseListFormDto Dto) : IRequest<ResultDto<PurchaseListFormDto>>;

internal class CreatePurchaseListFormDtoCommandHandler : BaseService, IRequestHandler<CreatePurchaseListFormDtoCommand, ResultDto<PurchaseListFormDto>>
{
    private readonly ShopContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreatePurchaseListFormDtoCommandHandler(ShopContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ResultDto<PurchaseListFormDto>> Handle(CreatePurchaseListFormDtoCommand request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.GetUserId();
        var dto = request.Dto;

        if (userId.HasValue && dto.IsFavourite)
        {
            var hasFavourite = await _context.Set<PurchaseListEntity>().AnyAsync(x => x.UserId == userId && x.IsFavourite, cancellationToken);

            if (hasFavourite)
                return Error<PurchaseListFormDto>(HttpStatusCode.BadRequest, ExceptionMessage.PurchaseList001UserHasFavouireList);
        }

        var entity = dto.ToEntity();

        entity.UserId = userId;

        var result = await _context.Set<PurchaseListEntity>().AddAsync(entity, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success(await _context.Set<PurchaseListEntity>().AsNoTracking().Where(x => x.Id == result.Entity.Id).Select(PurchaseListFormDto.Map()).FirstOrDefaultAsync(cancellationToken));
    }
}