using MediatR;
using Shared.Core.Dtos;
using Shop.Core.Cqrs.Product.Queries;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto) : IRequest<ResultDto<ProductFormDto>>;

internal class CreateProductFormDtoCommandHandler : IRequestHandler<CreateProductFormDtoCommand, ResultDto<ProductFormDto>>
{
    private readonly ShopContext _context;
    private readonly IMediator _mediator;

    public CreateProductFormDtoCommandHandler(ShopContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<ProductFormDto>> Handle(CreateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetProductFormDtoByIdQuery(result.Entity.Id));
    }
}