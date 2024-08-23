using Microsoft.AspNetCore.Http;
using Product.Core.Helpers;
using Product.Domain.Documents;

namespace Product.Core.Extensions;

public static class FileFormExtension
{
    public static ProductPhotoDocument ToProductPhotoDocument(this IFormFile file) => new()
    {
        ContentType = file.ContentType,
        Data = ConversionHelper.ToByte(file),
        Length = file.Length,
        Name = file.FileName,
    };
}