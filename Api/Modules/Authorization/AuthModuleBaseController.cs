using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Api.Bases;
using Shared.Core.Factories.FluentValidator;

namespace Api.Modules.Authorization;

[Route("AuthModule/[controller]")]
public class AuthModuleBaseController : BaseController
{
    public AuthModuleBaseController(IFluentValidatorFactory fluentValidatorFactory, IMediator mediator) : base(fluentValidatorFactory, mediator)
    {
    }
}