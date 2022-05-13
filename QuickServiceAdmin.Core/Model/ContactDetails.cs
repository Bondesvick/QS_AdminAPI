using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "ContactDetails")]
    public class ContactDetails
    {
        [XmlElement(ElementName = "ResidentialAddress")]
        public string ResidentialAddress { get; set; }

        [XmlElement(ElementName = "StateOfResidence")]
        public string StateOfResidence { get; set; }

        [XmlElement(ElementName = "LGAOfResidence")]
        public string LgaOfResidence { get; set; }

        [XmlElement(ElementName = "Landmarks")]
        public string Landmarks { get; set; }

        [XmlElement(ElementName = "PhoneNumber1")]
        public string PhoneNumber1 { get; set; }
    }
}