using System.Xml.Serialization;

namespace QuickServiceAdmin.Core.Model
{
    [XmlRoot(ElementName = "AccountInformation")]
    public class AccountInformation
    {
        [XmlElement(ElementName = "AccountBranchId")]
        public string AccountBranchId { get; set; }

        [XmlElement(ElementName = "AccountBranchName")]
        public string AccountBranchName { get; set; }

        [XmlElement(ElementName = "AccountBvn")]
        public string AccountBvn { get; set; }

        [XmlElement(ElementName = "AccountCurrencyCode")]
        public string AccountCurrencyCode { get; set; }

        [XmlElement(ElementName = "AccountName")]
        public string AccountName { get; set; }

        [XmlElement(ElementName = "AccountNumber")]
        public string AccountNumber { get; set; }

        [XmlElement(ElementName = "AccountOpenDate")]
        public string AccountOpenDate { get; set; }

        [XmlElement(ElementName = "AccountOwnership")]
        public string AccountOwnership { get; set; }

        [XmlElement(ElementName = "AccountSchemeCode")]
        public string AccountSchemeCode { get; set; }

        [XmlElement(ElementName = "AccountSchemeType")]
        public string AccountSchemeType { get; set; }

        [XmlElement(ElementName = "AccountSchemeDescription")]
        public string AccountSchemeDescription { get; set; }

        [XmlElement(ElementName = "AccountShortName")]
        public string AccountShortName { get; set; }

        [XmlElement(ElementName = "AccountStatus")]
        public string AccountStatus { get; set; }

        [XmlElement(ElementName = "AccountType")]
        public string AccountType { get; set; }

        [XmlElement(ElementName = "AvailableBalance")]
        public string AvailableBalance { get; set; }

        [XmlElement(ElementName = "CustomerFirstName")]
        public string CustomerFirstName { get; set; }

        [XmlElement(ElementName = "CustomerId")]
        public string CustomerId { get; set; }

        [XmlElement(ElementName = "CustomerLastName")]
        public string CustomerLastName { get; set; }

        [XmlElement(ElementName = "CustomerMiddleName")]
        public string CustomerMiddleName { get; set; }

        [XmlElement(ElementName = "CustomerSalutation")]
        public string CustomerSalutation { get; set; }

        [XmlElement(ElementName = "LedgerBalance")]
        public string LedgerBalance { get; set; }
    }
}