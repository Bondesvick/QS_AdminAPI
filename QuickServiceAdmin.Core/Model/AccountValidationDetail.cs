using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "detail")]
    public class AccountValidationDetail
    {
        [XmlElement(ElementName = "doCustomerInformationEnquiryResponse")]
        public DoCustomerInformationEnquiryResponse DoCustomerInformationEnquiryResponse { get; set; }
    }
}