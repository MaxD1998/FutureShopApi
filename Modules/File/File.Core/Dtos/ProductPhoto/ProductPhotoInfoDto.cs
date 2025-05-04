namespace File.Core.Dtos.ProductPhoto;

public class ProductPhotoInfoDto
{
    public ProductPhotoInfoDto(string id, string contentType, long length, string name)
    {
        Id = id;
        Name = name;
        Size = $"{Math.Round((double)length / (1024 * 1024), 2)} MB";
        Type = contentType;
    }

    public string Id { get; set; }

    public string Name { get; set; }

    public string Size { get; set; }

    public string Type { get; set; }
}