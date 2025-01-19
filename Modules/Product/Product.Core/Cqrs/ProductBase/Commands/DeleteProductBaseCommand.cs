using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Commands;
public record DeleteProductBaseCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductBaseCommandHandler : BaseService, IRequestHandler<DeleteProductBaseCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly RabbitMqContext _rabbitMqContext;

    public DeleteProductBaseCommandHandler(ProductPostgreSqlContext context, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto> Handle(DeleteProductBaseCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductBaseEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(request.Id, MessageType.Delete));

        return Success();
    }
}