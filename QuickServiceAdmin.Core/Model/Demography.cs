using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "Demography")]
    public class Demography
    {
        [XmlElement(ElementName = "Salutation")]
        public string Salutation { get; set; }

        [XmlElement(ElementName = "Surname")] public string Surname { get; set; }

        [XmlElement(ElementName = "MiddleName")]
        public string MiddleName { get; set; }

        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "Gender")] public string Gender { get; set; }

        [XmlElement(ElementName = "DateOfBirth")]
        public string DateOfBirth { get; set; }

        [XmlElement(ElementName = "MaritalStatus")]
        public string MaritalStatus { get; set; }

        [XmlElement(ElementName = "Nationality")]
        public string Nationality { get; set; }
    }
}