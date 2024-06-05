using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Commands;
public record UpdateCategoryFormDtoCommand(Guid Id, CategoryFormDto Dto) : IRequest<CategoryFormDto>;

internal class UpdateCategoryFormDtoCommandHandler : BaseRequestHandler<ProductContext, UpdateCategoryFormDtoCommand, CategoryFormDto>
{
    public UpdateCategoryFormDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<CategoryFormDto> Handle(UpdateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        entity.Update(request.Dto.ToEntity(_context));

        await _context.SaveChangesAsync(cancellationToken);

        return new CategoryFormDto(entity);
    }
}