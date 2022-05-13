using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "RetailCustomerInformation")]
    public class RetailCustomerInformation
    {
        [XmlElement(ElementName = "FirstName")]
        public string FirstName { get; set; }

        [XmlElement(ElementName = "LastName")] public string LastName { get; set; }

        [XmlElement(ElementName = "AdditionalName")]
        public string AdditionalName { get; set; }

        [XmlElement(ElementName = "BirthDate")]
        public string BirthDate { get; set; }

        [XmlElement(ElementName = "Gender")] public string Gender { get; set; }

        [XmlElement(ElementName = "MaritalStatusCode")]
        public string MaritalStatusCode { get; set; }

        [XmlElement(ElementName = "MaritalStatusDesc")]
        public string MaritalStatusDesc { get; set; }

        [XmlElement(ElementName = "NationalityCode")]
        public string NationalityCode { get; set; }

        [XmlElement(ElementName = "NationalityDesc")]
        public string NationalityDesc { get; set; }

        [XmlElement(ElementName = "OptOutInd")]
        public string OptOutInd { get; set; }

        [XmlElement(ElementName = "Race")] public string Race { get; set; }

        [XmlElement(ElementName = "RaceDescription")]
        public string RaceDescription { get; set; }

        [XmlElement(ElementName = "ResidingCountryCode")]
        public string ResidingCountryCode { get; set; }

        [XmlElement(ElementName = "ResidingCountryDescription")]
        public string ResidingCountryDescription { get; set; }

        [XmlElement(ElementName = "EmployementStatus")]
        public string EmployementStatus { get; set; }

        [XmlElement(ElementName = "NameOfEmployer")]
        public string NameOfEmployer { get; set; }

        [XmlElement(ElementName = "JobTitleCode")]
        public string JobTitleCode { get; set; }

        [XmlElement(ElementName = "JobTitleDesc")]
        public string JobTitleDesc { get; set; }

        [XmlElement(ElementName = "OccupationCode")]
        public string OccupationCode { get; set; }

        [XmlElement(ElementName = "OccupationDesc")]
        public string OccupationDesc { get; set; }

        [XmlElement(ElementName = "PeriodOfEmployment")]
        public string PeriodOfEmployment { get; set; }
    }
}