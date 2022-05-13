using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CorporateCustomerInformation")]
    public class CorporateCustomerInformation
    {
        [XmlElement(ElementName = "CityOfIncorporation")]
        public string CityOfIncorporation { get; set; }

        [XmlElement(ElementName = "LegalEntityType")]
        public string LegalEntityType { get; set; }

        [XmlElement(ElementName = "PrincipalNatureOfBusiness")]
        public string PrincipalNatureOfBusiness { get; set; }

        [XmlElement(ElementName = "PrincipalPlaceOfOperation")]
        public string PrincipalPlaceOfOperation { get; set; }
    }
}