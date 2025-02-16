using MediatR;
using Product.Core.Cqrs.ProductBase.Queries;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record CreateProductBaseFormDtoCommand(ProductBaseFormDto Dto) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class CreateProductBaseFormDtoCommandHanlder : IRequestHandler<CreateProductBaseFormDtoCommand, ResultDto<ProductBaseFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;
    private readonly RabbitMqContext _rabbitMqContext;

    public CreateProductBaseFormDtoCommandHanlder(ProductPostgreSqlContext context, IMediator mediator, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _mediator = mediator;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(CreateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(result.Entity, MessageType.AddOrUpdate));

        return await _mediator.Send(new GetProductBaseFormDtoByIdQuery(result.Entity.Id), cancellationToken);
    }
}