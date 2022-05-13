using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CustomerInformation")]
    public class CustomerInformation
    {
        [XmlElement(ElementName = "AccountNumber")]
        public string AccountNumber { get; set; }

        [XmlElement(ElementName = "Bvn")] public string Bvn { get; set; }

        [XmlElement(ElementName = "IsRetailCustomerFlag")]
        public string IsRetailCustomerFlag { get; set; }

        [XmlElement(ElementName = "RiskGrading")]
        public string RiskGrading { get; set; }

        [XmlElement(ElementName = "ContactDetails")]
        public ContactDetails ContactDetails { get; set; }

        [XmlElement(ElementName = "Demography")]
        public Demography Demography { get; set; }

        [XmlElement(ElementName = "CustomerAddressInformation")]
        public CustomerAddressInformation CustomerAddressInformation { get; set; }

        [XmlElement(ElementName = "CorporateCustomerInformation")]
        public CorporateCustomerInformation CorporateCustomerInformation { get; set; }

        [XmlElement(ElementName = "RetailCustomerInformation")]
        public RetailCustomerInformation RetailCustomerInformation { get; set; }

        [XmlElement(ElementName = "CustomerDocumentData")]
        public CustomerDocumentData CustomerDocumentData { get; set; }

        [XmlElement(ElementName = "CustomerGeneralData")]
        public CustomerGeneralData CustomerGeneralData { get; set; }

        [XmlElement(ElementName = "CustomerPhoneEmailInformation")]
        public CustomerPhoneEmailInformation CustomerPhoneEmailInformation { get; set; }

        [XmlElement(ElementName = "RelatedPartyInformation")]
        public string RelatedPartyInformation { get; set; }
    }
}