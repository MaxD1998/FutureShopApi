using MediatR;
using Product.Core.Cqrs.Category.Queries;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Category.Commands;
public record CreateCategoryFormDtoCommand(CategoryFormDto Dto) : IRequest<ResultDto<CategoryFormDto>>;

internal class CreateCategoryFormDtoCommandHandler : IRequestHandler<CreateCategoryFormDtoCommand, ResultDto<CategoryFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;

    public CreateCategoryFormDtoCommandHandler(ProductPostgreSqlContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<CategoryFormDto>> Handle(CreateCategoryFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>().AddAsync(request.Dto.ToEntity(_context), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetCategoryFormDtoByIdQuery(result.Entity.Id));
    }
}