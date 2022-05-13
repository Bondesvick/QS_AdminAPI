using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "Address")]
    public class Address
    {
        [XmlElement(ElementName = "addressCategory")]
        public string AddressCategory { get; set; }

        [XmlElement(ElementName = "addressLine1")]
        public string AddressLine1 { get; set; }

        [XmlElement(ElementName = "addressLine2")]
        public string AddressLine2 { get; set; }

        [XmlElement(ElementName = "addressLine3")]
        public string AddressLine3 { get; set; }

        [XmlElement(ElementName = "country")] public string Country { get; set; }

        [XmlElement(ElementName = "houseNumber")]
        public string HouseNumber { get; set; }

        [XmlElement(ElementName = "isPreferredAddr")]
        public string IsPreferredAddr { get; set; }

        [XmlElement(ElementName = "postalCode")]
        public string PostalCode { get; set; }

        [XmlElement(ElementName = "startDate")]
        public string StartDate { get; set; }

        [XmlElement(ElementName = "streetName")]
        public string StreetName { get; set; }

        [XmlElement(ElementName = "streetNumber")]
        public string StreetNumber { get; set; }

        [XmlElement(ElementName = "endDate")] public string EndDate { get; set; }

        [XmlElement(ElementName = "buildingLevel")]
        public string BuildingLevel { get; set; }

        [XmlElement(ElementName = "cityCode")] public string CityCode { get; set; }

        [XmlElement(ElementName = "countryCode")]
        public string CountryCode { get; set; }

        [XmlElement(ElementName = "domicile")] public string Domicile { get; set; }

        [XmlElement(ElementName = "freeTextAddress")]
        public string FreeTextAddress { get; set; }

        [XmlElement(ElementName = "localityName")]
        public string LocalityName { get; set; }

        [XmlElement(ElementName = "premiseName")]
        public string PremiseName { get; set; }

        [XmlElement(ElementName = "stateCode")]
        public string StateCode { get; set; }

        [XmlElement(ElementName = "suburb")] public string Suburb { get; set; }
        [XmlElement(ElementName = "town")] public string Town { get; set; }
    }
}