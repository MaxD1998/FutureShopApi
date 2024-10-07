using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Errors;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.Category.Commands;
public record UpdateCategoryFormDtoCommand(Guid Id, CategoryFormDto Dto) : IRequest<CategoryFormDto>;

internal class UpdateCategoryFormDtoCommandHandler : IRequestHandler<UpdateCategoryFormDtoCommand, CategoryFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public UpdateCategoryFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<CategoryFormDto> Handle(UpdateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            throw new NotFoundException(CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity(_context));

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity);
    }
}