using File.Infrastructure.Documents;
using Microsoft.AspNetCore.Http;
using Shared.Core.Helpers;

namespace File.Core.Extensions;

public static class FileFormExtension
{
    public static FileDocument ToProductPhotoDocument(this IFormFile file) => new()
    {
        ContentType = file.ContentType,
        Data = ConversionHelper.ToByte(file),
        Length = file.Length,
        Name = file.FileName,
    };

    public static List<FileDocument> ToProductPhotoDocuments(this IFormFileCollection files)
        => files.Select(x => x.ToProductPhotoDocument()).ToList();
}