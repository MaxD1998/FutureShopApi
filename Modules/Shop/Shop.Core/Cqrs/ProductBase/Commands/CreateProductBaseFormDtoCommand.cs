using MediatR;
using Shared.Core.Dtos;
using Shop.Core.Cqrs.ProductBase.Queries;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Commands;

public record CreateProductBaseFormDtoCommand(ProductBaseFormDto Dto) : IRequest<ResultDto<ProductBaseFormDto>>;

internal class CreateProductBaseFormDtoCommandHanlder : IRequestHandler<CreateProductBaseFormDtoCommand, ResultDto<ProductBaseFormDto>>
{
    private readonly ShopContext _context;
    private readonly IMediator _mediator;

    public CreateProductBaseFormDtoCommandHanlder(ShopContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ResultDto<ProductBaseFormDto>> Handle(CreateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return await _mediator.Send(new GetProductBaseFormDtoByIdQuery(result.Entity.Id));
    }
}