﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.Product.Commands;
public record UpdateProductFormDtoCommand(Guid Id, ProductFormDto Dto) : IRequest<ProductFormDto>;

internal class UpdateProductFormDtoCommandHandler : IRequestHandler<UpdateProductFormDtoCommand, ProductFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateProductFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductFormDto> Handle(UpdateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.ProductPhotos)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity);
    }
}