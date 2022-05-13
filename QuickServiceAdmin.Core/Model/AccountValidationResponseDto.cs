using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "response")]
    public class AccountValidationResponseDto
    {
        [XmlElement(ElementName = "trackingId")]
        public string TrackingId { get; set; }

        [XmlElement(ElementName = "responseCode")]
        public string ResponseCode { get; set; }

        [XmlElement(ElementName = "responseDescription")]
        public string ResponseDescription { get; set; }

        [XmlElement(ElementName = "detail")] public AccountValidationDetail Detail { get; set; }
    }
}