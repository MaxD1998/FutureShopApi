using MediatR;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Commands;
public record CreateCategoryDtoCommand(CategoryInputDto Dto) : IRequest<CategoryDto>;

internal class CreateCategoryDtoCommandHandler : BaseRequestHandler<ProductContext, CreateCategoryDtoCommand, CategoryDto>
{
    public CreateCategoryDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<CategoryDto> Handle(CreateCategoryDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>().AddAsync(request.Dto.ToEntity());

        await _context.SaveChangesAsync();

        return new CategoryDto(result.Entity);
    }
}