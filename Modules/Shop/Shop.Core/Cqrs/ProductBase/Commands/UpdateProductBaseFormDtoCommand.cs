using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Errors;
using Shared.Infrastructure;
using Shop.Core.Cqrs.ProductBase.Queries;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.ProductBase.Commands;

public record UpdateProductBaseFormDtoCommand(Guid Id, ProductBaseFormDto Dto) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class UpdateProductBaseFormDtoCommandHandler : BaseService, IRequestHandler<UpdateProductBaseFormDtoCommand, ResultDto<ProductBaseFormDto>>
{
    private readonly ShopContext _context;
    private readonly IMediator _mediator;
    private readonly RabbitMqContext _rabbitMqContext;

    public UpdateProductBaseFormDtoCommandHandler(ShopContext context, IMediator mediator, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _mediator = mediator;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(UpdateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<ProductBaseFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        return await _mediator.Send(new GetProductBaseFormDtoByIdQuery(request.Id), cancellationToken);
    }
}