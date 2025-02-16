using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Cqrs.Product.Queries;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Core.Errors;
using Shared.Infrastructure;
using System.Net;

namespace Product.Core.Cqrs.Product.Commands;
public record UpdateProductFormDtoCommand(Guid Id, ProductFormDto Dto) : IRequest<ResultDto<ProductFormDto>>;

internal class UpdateProductFormDtoCommandHandler : BaseService, IRequestHandler<UpdateProductFormDtoCommand, ResultDto<ProductFormDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IMediator _mediator;
    private readonly RabbitMqContext _rabbitMqContext;

    public UpdateProductFormDtoCommandHandler(ProductPostgreSqlContext context, IMediator mediator, RabbitMqContext rabbitMqContext)
    {
        _context = context;
        _mediator = mediator;
        _rabbitMqContext = rabbitMqContext;
    }

    public async Task<ResultDto<ProductFormDto>> Handle(UpdateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductPhotos)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<ProductFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        return await _mediator.Send(new GetProductFormDtoByIdQuery(request.Id), cancellationToken);
    }
}