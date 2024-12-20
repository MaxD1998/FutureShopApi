using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.Cqrs.Category.Commands;
public record DeleteCategoryByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteCategoryByIdCommandHandler : BaseService, IRequestHandler<DeleteCategoryByIdCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly RabbitMqContext _rabbitMqContext;

    public DeleteCategoryByIdCommandHandler(ProductPostgreSqlContext context, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto> Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<CategoryEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(request.Id, MessageType.Delete));

        return Success();
    }
}