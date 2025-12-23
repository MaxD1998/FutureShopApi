using Shop.Core.Models.Inpost;

namespace Shop.Core.Interfaces.Inpost;

public interface IInpostClient
{
    Task<InpostShipmentModel> CreateShipmentAsync(InpostShipmentModel request, CancellationToken cancellationToken);
}