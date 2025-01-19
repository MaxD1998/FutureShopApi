using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record DeleteProductByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductByIdCommandHandler : BaseService, IRequestHandler<DeleteProductByIdCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly RabbitMqContext _rabbitMqContext;

    public DeleteProductByIdCommandHandler(ProductPostgreSqlContext context, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(request.Id, MessageType.Delete));

        return Success();
    }
}