using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Warehouse;

[Route("WarehouseModule/[controller]")]
public abstract class WarehouseModuleBaseController : BaseController
{
    protected WarehouseModuleBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }
}