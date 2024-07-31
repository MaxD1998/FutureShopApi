using Microsoft.AspNetCore.Http;

namespace Product.Core.Dtos.ProductPhoto;

public class ProductPhotoFormDto
{
    public IFormFile File { get; set; }

    public int Position { get; set; }
}