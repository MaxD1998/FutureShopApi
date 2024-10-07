using MediatR;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Category.Commands;
public record CreateCategoryFormDtoCommand(CategoryFormDto Dto) : IRequest<CategoryFormDto>;

internal class CreateCategoryFormDtoCommandHandler : IRequestHandler<CreateCategoryFormDtoCommand, CategoryFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public CreateCategoryFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<CategoryFormDto> Handle(CreateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>().AddAsync(request.Dto.ToEntity(_context), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(result.Entity);
    }
}