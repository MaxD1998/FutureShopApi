using Shared.Core.Dtos;

namespace Shared.Core.Interfaces;

public interface IValidator
{
    bool Validate(out ResultDto result);
}