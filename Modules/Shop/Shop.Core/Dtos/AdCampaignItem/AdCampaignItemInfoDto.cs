namespace Shop.Core.Dtos.AdCampaignItem;

public class AdCampaignItemInfoDto
{
    public AdCampaignItemInfoDto(string id, string contentType, long length, string name)
    {
        Id = id;
        Name = name;
        Size = $"{Math.Round((double)length / (1024 * 1024), 2)} MB";
        Type = contentType;
    }

    public string Id { get; set; }

    public string Lang { get; set; }

    public string Name { get; set; }

    public string Size { get; set; }

    public string Type { get; set; }
}