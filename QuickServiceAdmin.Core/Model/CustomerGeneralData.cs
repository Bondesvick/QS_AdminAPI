using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "CustomerGeneralData")]
    public class CustomerGeneralData
    {
        [XmlElement(ElementName = "ChannelId")]
        public string ChannelId { get; set; }

        [XmlElement(ElementName = "ChannelcustId")]
        public string ChannelcustId { get; set; }

        [XmlElement(ElementName = "CustId")] public string CustId { get; set; }

        [XmlElement(ElementName = "CustomerCreationDate")]
        public string CustomerCreationDate { get; set; }

        [XmlElement(ElementName = "CustTypeCode")]
        public string CustTypeCode { get; set; }

        [XmlElement(ElementName = "DefaultAddrType")]
        public string DefaultAddrType { get; set; }

        [XmlElement(ElementName = "DsaId")] public string DsaId { get; set; }

        [XmlElement(ElementName = "EmployeeId")]
        public string EmployeeId { get; set; }

        [XmlElement(ElementName = "GroupIdCode")]
        public string GroupIdCode { get; set; }

        [XmlElement(ElementName = "GstFlag")] public string GstFlag { get; set; }

        [XmlElement(ElementName = "LanguageCode")]
        public string LanguageCode { get; set; }

        [XmlElement(ElementName = "NameInNativeLanguage")]
        public string NameInNativeLanguage { get; set; }

        [XmlElement(ElementName = "PrimaryDocType")]
        public string PrimaryDocType { get; set; }

        [XmlElement(ElementName = "primaryRelationshipManagerId")]
        public string PrimaryRelationshipManagerId { get; set; }

        [XmlElement(ElementName = "RatingCode")]
        public string RatingCode { get; set; }

        [XmlElement(ElementName = "SalutationCode")]
        public string SalutationCode { get; set; }

        [XmlElement(ElementName = "SecondaryRelationshipManagerId")]
        public string SecondaryRelationshipManagerId { get; set; }

        [XmlElement(ElementName = "SectorCode")]
        public string SectorCode { get; set; }

        [XmlElement(ElementName = "Segment")] public string Segment { get; set; }

        [XmlElement(ElementName = "SegmentLevel")]
        public string SegmentLevel { get; set; }

        [XmlElement(ElementName = "SegmentNum")]
        public string SegmentNum { get; set; }

        [XmlElement(ElementName = "SegmentType")]
        public string SegmentType { get; set; }

        [XmlElement(ElementName = "ShortName")]
        public string ShortName { get; set; }

        [XmlElement(ElementName = "SicCode")] public string SicCode { get; set; }
        [XmlElement(ElementName = "IsStaff")] public string IsStaff { get; set; }

        [XmlElement(ElementName = "SubSectorCode")]
        public string SubSectorCode { get; set; }

        [XmlElement(ElementName = "IsSuspended")]
        public string IsSuspended { get; set; }

        [XmlElement(ElementName = "TertiaryRelationshipManagerId")]
        public string TertiaryRelationshipManagerId { get; set; }

        [XmlElement(ElementName = "BranchId")] public string BranchId { get; set; }
    }
}