using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "phoneEmailInfo")]
    public class PhoneEmailInfo
    {
        [XmlElement(ElementName = "PhoneEmailType")]
        public string PhoneEmailType { get; set; }

        [XmlElement(ElementName = "IsPhoneInfo")]
        public string IsPhoneInfo { get; set; }

        [XmlElement(ElementName = "PreferredFlag")]
        public string PreferredFlag { get; set; }

        [XmlElement(ElementName = "Email")] public string Email { get; set; }

        [XmlElement(ElementName = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [XmlElement(ElementName = "PhoneNumberCountryCode")]
        public string PhoneNumberCountryCode { get; set; }

        [XmlElement(ElementName = "PhoneNumberCityCode")]
        public string PhoneNumberCityCode { get; set; }

        [XmlElement(ElementName = "PhoneNumberLocalCode")]
        public string PhoneNumberLocalCode { get; set; }
    }
}