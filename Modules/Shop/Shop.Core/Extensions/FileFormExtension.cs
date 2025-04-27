using Microsoft.AspNetCore.Http;
using Shared.Core.Helpers;
using Shop.Domain.Documents;

namespace Shop.Core.Extensions;

public static class FileFormExtension
{
    public static AdCampaignItemDocument ToAdCampaignItemDocument(this IFormFile file) => new()
    {
        ContentType = file.ContentType,
        Data = ConversionHelper.ToByte(file),
        Length = file.Length,
        Name = file.FileName,
    };
}