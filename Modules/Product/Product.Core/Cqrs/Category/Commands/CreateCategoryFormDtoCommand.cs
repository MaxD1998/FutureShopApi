using MediatR;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Commands;
public record CreateCategoryFormDtoCommand(CategoryFormDto Dto) : IRequest<CategoryFormDto>;

internal class CreateCategoryFormDtoCommandHandler : BaseRequestHandler<ProductContext, CreateCategoryFormDtoCommand, CategoryFormDto>
{
    public CreateCategoryFormDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<CategoryFormDto> Handle(CreateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>().AddAsync(request.Dto.ToEntity(_context), cancellationToken);

        await _context.SaveChangesAsync();

        return new CategoryFormDto(result.Entity);
    }
}