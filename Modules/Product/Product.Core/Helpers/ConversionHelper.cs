﻿using Microsoft.AspNetCore.Http;

namespace Product.Core.Helpers;

public static class ConversionHelper
{
    public static byte[] ToByte(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}