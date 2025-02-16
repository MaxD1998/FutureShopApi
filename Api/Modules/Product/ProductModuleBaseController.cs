using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Product;

[Route("ProductModule/[controller]")]
public class ProductModuleBaseController : BaseController
{
    public ProductModuleBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }
}