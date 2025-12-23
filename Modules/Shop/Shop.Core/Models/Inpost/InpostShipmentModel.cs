namespace Shop.Core.Models.Inpost;

public class InpostShipmentModel
{
    public string[] AdditionalServices { get; set; }

    public InpostCodModel Cod { get; set; }

    public string Comments { get; set; }

    public InpostCustomAttributesModel CustomAttributes { get; set; }

    public string ExternalCustomerId { get; set; }

    public InpostInsuranceModel Insurance { get; set; }

    public bool IsReturn { get; set; }

    public string Mpk { get; set; }

    public bool OnlyChoiceOfOffer { get; set; }

    public InpostParcelsModel[] Parcels { get; set; }

    public InpostReceiverModel Receiver { get; set; }

    public string Reference { get; set; }

    public InpostSenderModel Sender { get; set; }
}