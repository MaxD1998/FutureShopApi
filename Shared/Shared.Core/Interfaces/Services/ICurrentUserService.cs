using System;

namespace Shared.Core.Interfaces.Services;

public interface ICurrentUserService
{
    Guid? GetUserId();

    bool IsRecordOwner(Guid inputId);
}
