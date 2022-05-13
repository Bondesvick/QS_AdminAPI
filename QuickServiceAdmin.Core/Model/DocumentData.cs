using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "DocumentData")]
    public class DocumentData
    {
        [XmlElement(ElementName = "CountryOfIssue")]
        public string CountryOfIssue { get; set; }

        [XmlElement(ElementName = "DocumentCode")]
        public string DocumentCode { get; set; }

        [XmlElement(ElementName = "IssueDate")]
        public string IssueDate { get; set; }

        [XmlElement(ElementName = "TypeCode")] public string TypeCode { get; set; }

        [XmlElement(ElementName = "PlaceOfIssue")]
        public string PlaceOfIssue { get; set; }

        [XmlElement(ElementName = "UniqueId")] public string UniqueId { get; set; }
    }
}