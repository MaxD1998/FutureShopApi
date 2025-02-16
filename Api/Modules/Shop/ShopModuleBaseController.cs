using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Shop;

[Route("ShopModule/[controller]")]
public class ShopModuleBaseController : BaseController
{
    public ShopModuleBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }
}