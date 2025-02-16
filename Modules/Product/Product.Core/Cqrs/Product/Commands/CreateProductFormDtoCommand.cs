using MediatR;
using Product.Core.Cqrs.Product.Queries;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto) : IRequest<ResultDto<ProductFormDto>>;

internal class CreateProductFormDtoCommandHandler : IRequestHandler<CreateProductFormDtoCommand, ResultDto<ProductFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;
    private readonly RabbitMqContext _rabbitMqContext;

    public CreateProductFormDtoCommandHandler(ProductPostgreSqlContext context, IMediator mediator, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _mediator = mediator;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto<ProductFormDto>> Handle(CreateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(result.Entity, MessageType.AddOrUpdate));

        return await _mediator.Send(new GetProductFormDtoByIdQuery(result.Entity.Id), cancellationToken);
    }
}