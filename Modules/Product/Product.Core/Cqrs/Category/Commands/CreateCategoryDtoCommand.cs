using AutoMapper;
using MediatR;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Commands;
public record CreateCategoryDtoCommand(CategoryInputDto Dto) : IRequest<CategoryDto>;

internal class CreateCategoryDtoCommandHandler : BaseRequestHandler<ProductContext, CreateCategoryDtoCommand, CategoryDto>
{
    public CreateCategoryDtoCommandHandler(ProductContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<CategoryDto> Handle(CreateCategoryDtoCommand request, CancellationToken cancellationToken)
        => await CreateAsync<CategoryEntity, CategoryDto>(request.Dto);
}