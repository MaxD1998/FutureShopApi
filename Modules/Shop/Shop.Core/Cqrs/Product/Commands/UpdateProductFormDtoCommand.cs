using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using Shop.Core.Cqrs.Product.Queries;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;
using System.Net;

namespace Shop.Core.Cqrs.Product.Commands;
public record UpdateProductFormDtoCommand(Guid Id, ProductFormDto Dto) : IRequest<ResultDto<ProductFormDto>>;

internal class UpdateProductFormDtoCommandHandler : BaseService, IRequestHandler<UpdateProductFormDtoCommand, ResultDto<ProductFormDto>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IMediator _mediator;

    public UpdateProductFormDtoCommandHandler(ShopPostgreSqlContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<ProductFormDto>> Handle(UpdateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
            return Error<ProductFormDto>(HttpStatusCode.NotFound, CommonExceptionMessage.C007RecordWasNotFound);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetProductFormDtoByIdQuery(request.Id), cancellationToken);
    }
}