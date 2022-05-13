using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("CORP_ACC_OPENING_DETAILS")]
    public class CorpAccOpeningDetails
    {
        [Key] [JsonIgnore] [Column("ID")] public long Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("ACCOUNT_CATEGORY")] public string AccountCategory { get; set; }
        [Column("ACCOUNT_TYPE")] public string AccountType { get; set; }
        [Column("AFFILIATION_TO_ANY_BODY")] public string AffiliationToAnyBody { get; set; }
        [Column("ANNUAL_TURNOVER")] public string AnnualTurnover { get; set; }
        [Column("AUTO_RENEWAL_EXP")] public string AutoRenewalExp { get; set; }
        [Column("BUSINESS_NAME")] public string BusinessName { get; set; }
        [Column("CARD_PREFERENCE")] public string CardPreference { get; set; }
        [Column("CERT_REGNO")] public string CertRegno { get; set; }
        [Column("CORP_BUSINESS_ADDRESS")] public string CorpBusinessAddress { get; set; }
        [Column("CURRENCY")] public string Currency { get; set; }
        [Column("DATE_OF_INCOP")] public string DateOfIncop { get; set; }
        [Column("ELECT_BANKING_PREF")] public string ElectBankingPref { get; set; }
        [Column("EMAIL")] public string Email { get; set; }
        [Column("INDUSTRY")] public string Industry { get; set; }
        [Column("NOK_ADDRESS")] public string NokAddress { get; set; }
        [Column("NOK_CITY")] public string NokCity { get; set; }
        [Column("NOK_DOB")] public string NokDob { get; set; }
        [Column("NOK_FIRST_NAME")] public string NokFirstName { get; set; }
        [Column("NOK_GENDER")] public string NokGender { get; set; }
        [Column("NOK_MOBILE_NUMBER")] public string NokMobileNumber { get; set; }
        [Column("NOK_OTHERNAME")] public string NokOthername { get; set; }
        [Column("NOK_RELATIONSHIP")] public string NokRelationship { get; set; }
        [Column("NOK_STATE")] public string NokState { get; set; }
        [Column("NOKSurname")] public string Noksurname { get; set; }
        [Column("NAME_OF_AFFILIATED_COMP")] public string NameOfAffiliatedComp { get; set; }
        [Column("NATURE_OF_BUS")] public string NatureOfBus { get; set; }
        [Column("NO_SIGNATORIES")] public int NoSignatories { get; set; }
        [Column("OPERATION_BUS_ADDRESS")] public string OperationBusAddress { get; set; }
        [Column("ORGANISATION_REGISTERED")] public string OrganisationRegistered { get; set; }
        [Column("PARENT_COMP_OF_INCORP")] public string ParentCompOfIncorp { get; set; }
        [Column("PHONE_NUMBER")] public string PhoneNumber { get; set; }
        [Column("PREFERED_BRANCH")] public string PreferedBranch { get; set; }
        [Column("PREFERED_NAME_ON_CARD")] public string PreferedNameOnCard { get; set; }
        public string HasColumnName { get; set; }
        [Column("SCUML")] public string Scuml { get; set; }
        [Column("SIGNATORY_TYPE")] public string SignatoryType { get; set; }
        [Column("SIGNATURE_CLASS")] public string SignatureClass { get; set; }
        [Column("SIGNING_MANDATE")] public string SigningMandate { get; set; }
        [Column("STATEMENT_PREFERENCE")] public string StatementPreference { get; set; }
        [Column("TIN")] public string Tin { get; set; }
        [Column("TRANSACTION_ALERT_PREF")] public string TransactionAlertPref { get; set; }
        [Column("TYPE_OF_AFFILIATION")] public string TypeOfAffiliation { get; set; }
        [Column("WANT_A_CHEQUE")] public string WantACheque { get; set; }

        [ForeignKey(nameof(CustomerReqId))]
        [JsonIgnore]
        [InverseProperty(nameof(CustomerRequest.CorpAccOpeningDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }
    }
}