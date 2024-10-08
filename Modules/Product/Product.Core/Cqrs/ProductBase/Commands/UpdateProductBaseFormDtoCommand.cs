using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record UpdateProductBaseFormDtoCommand(Guid Id, ProductBaseFormDto Dto) : IRequest<ProductBaseFormDto>;

internal class UpdateProductBaseFormDtoCommandHandler : IRequestHandler<UpdateProductBaseFormDtoCommand, ProductBaseFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateProductBaseFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductBaseFormDto> Handle(UpdateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity);
    }
}