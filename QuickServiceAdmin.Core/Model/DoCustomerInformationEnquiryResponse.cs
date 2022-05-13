using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "doCustomerInformationEnquiryResponse")]
    public class DoCustomerInformationEnquiryResponse
    {
        [XmlElement(ElementName = "SinkTransactionRef")]
        public string SinkTransactionRef { get; set; }

        [XmlElement(ElementName = "ResponseCode")]
        public string ResponseCode { get; set; }

        [XmlElement(ElementName = "ResponseStatus")]
        public string ResponseStatus { get; set; }

        [XmlElement(ElementName = "ResponseDescription")]
        public string ResponseDescription { get; set; }

        [XmlElement(ElementName = "CustomerInformation")]
        public CustomerInformation CustomerInformation { get; set; }

        [XmlElement(ElementName = "AccountInformation")]
        public AccountInformation AccountInformation { get; set; }
    }
}