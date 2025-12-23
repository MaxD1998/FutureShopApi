using Shop.Core.Interfaces.Inpost;
using Shop.Core.Models.Inpost;
using System.Net.Http.Json;

namespace Shop.Infrastructure.Inpost;

internal class InpostClient : IInpostClient
{
    private readonly HttpClient _client = new();

    public async Task<InpostShipmentModel> CreateShipmentAsync(InpostShipmentModel request, CancellationToken cancellationToken)
    {
        var responseMessage = await _client.PostAsync("", new StringContent(""), cancellationToken);
        var response = await responseMessage.Content.ReadFromJsonAsync<InpostShipmentModel>(cancellationToken);

        return response;
    }
}