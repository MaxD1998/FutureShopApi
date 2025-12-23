namespace Shop.Core.Models.Inpost;

public class InpostParcelsModel
{
    public InpostDimensionsModel Dimensions { get; set; }

    public string Id { get; set; }

    public bool IsNonStandard { get; set; }

    public string Template { get; set; }

    public InpostWeightModel Weight { get; set; }
}