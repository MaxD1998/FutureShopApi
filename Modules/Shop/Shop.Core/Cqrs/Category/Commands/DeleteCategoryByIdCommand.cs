﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Commands;
public record DeleteCategoryByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteCategoryByIdCommandHandler : BaseService, IRequestHandler<DeleteCategoryByIdCommand, ResultDto>
{
    private readonly ShopContext _context;

    public DeleteCategoryByIdCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<CategoryEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Success();
    }
}