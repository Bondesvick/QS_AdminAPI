using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Entities
{
    [Table("ADD_ACC_OPENING_DETAILS")]
    public class AddAccOpeningDetails
    {
        public AddAccOpeningDetails()
        {
            AddAccOpeningDocs = new HashSet<AddAccOpeningDocs>();
        }

        [Key] [Column("ID")] [JsonIgnore] public int Id { get; set; }

        [Column("CUSTOMER_REQ_ID")]
        [JsonIgnore]
        public long CustomerReqId { get; set; }

        [Column("REQUESTED_ACC_TYPE")]
        [StringLength(100)]
        public string RequestedAccType { get; set; }

        [Column("EXISTING_ACC_TYPE")]
        [StringLength(100)]
        public string ExistingAccType { get; set; }

        [Column("EXISTING_ACC_SEGMENT")]
        [StringLength(100)]
        public string ExistingAccSegment { get; set; }

        [Column("CURRENCY")]
        [StringLength(50)]
        public string Currency { get; set; }

        [Column("BVN")] [StringLength(20)] public string Bvn { get; set; }

        [Column("PHONE_NUMBER")]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [Column("EXTRA_ACC_CLASS")]
        [StringLength(50)]
        public string ExtraAccClass { get; set; }

        [Column("REQUESTED_DEBIT_CARD")] public bool? RequestedDebitCard { get; set; }

        [Column("NAME_ON_CARD")]
        [StringLength(100)]
        public string NameOnCard { get; set; }

        [Column("CARD_PICKUP_BRANCH")]
        [StringLength(100)]
        public string CardPickupBranch { get; set; }

        [Column("FIRST_REF_NAME")]
        [StringLength(100)]
        public string FirstRefName { get; set; }

        [Column("FIRST_REF_BANK")]
        [StringLength(100)]
        public string FirstRefBank { get; set; }

        [Column("FIRST_REF_ACC_NUM")]
        [StringLength(100)]
        public string FirstRefAccNum { get; set; }

        [Column("SECOND_REF_NAME")]
        [StringLength(100)]
        public string SecondRefName { get; set; }

        [Column("SECOND_REF_BANK")]
        [StringLength(100)]
        public string SecondRefBank { get; set; }

        [Column("SECOND_REF_ACC_NUM")]
        [StringLength(100)]
        public string SecondRefAccNum { get; set; }

        [Column("EMPLOYMENT_STATUS")]
        [StringLength(100)]
        public string EmploymentStatus { get; set; }

        [Column("OCCUPATION")]
        [StringLength(100)]
        public string Occupation { get; set; }

        [Column("INDUSTRY")]
        [StringLength(100)]
        public string Industry { get; set; }

        [Column("NATURE_OF_BUSINESS")]
        [StringLength(100)]
        public string NatureOfBusiness { get; set; }

        [Column("INCOME_RANGE")]
        [StringLength(100)]
        public string IncomeRange { get; set; }

        [Column("NAME_OF_EMPLOYER")]
        [StringLength(100)]
        public string NameOfEmployer { get; set; }

        [Column("LEVEL_OF_EDUCATION")]
        [StringLength(100)]
        public string LevelOfEducation { get; set; }

        [Column("NAME_OF_INSTITUTION")]
        [StringLength(100)]
        public string NameOfInstitution { get; set; }

        [Column("ID_TYPE")]
        [StringLength(100)]
        public string IdType { get; set; }

        [Column("ID_NUMBER")]
        [StringLength(100)]
        public string IdNumber { get; set; }

        [Column("CASE_ID")] [StringLength(50)] public string CaseId { get; set; }

        [Column("CURRENT_STEP")]
        [StringLength(100)]
        public string CurrentStep { get; set; }

        [Column("SUBMITTED")] public bool? Submitted { get; set; }

        [Column("PURPOSE_OF_ACCOUNT")]
        [StringLength(100)]
        public string PurposeOfAccount { get; set; }

        [Column("SOURCE_OF_FUNDS")]
        [StringLength(100)]
        public string SourceOfFunds { get; set; }

        [Column("EXPECTED_CUMMULATIVE_BALANCE")]
        [StringLength(100)]
        public string ExpectedCummulativeBalance { get; set; }

        [Column("I_ACCEPT_TERMS_AND_CONDITIONS")]
        public bool? AcceptTermsAndConditions { get; set; }

        [Column("DATE_OF_ACCEPTING_T_AND_C", TypeName = "datetime")]
        public DateTime? DateOfAcceptingTAndC { get; set; }

        [Column("NOK_FULLNAME")]
        [StringLength(100)]
        public string NokFullname { get; set; }

        [Column("NOK_RELATIONSHIP")]
        [StringLength(100)]
        public string NokRelationship { get; set; }

        [Column("NOK_DATE_OF_BIRTH")]
        [StringLength(100)]
        public string NokDateOfBirth { get; set; }

        [Column("NOK_ADDRESS")]
        [StringLength(100)]
        public string NokAddress { get; set; }

        [Column("NOK_PHONE_NUMBER")]
        [StringLength(100)]
        public string NokPhoneNumber { get; set; }

        [Column("NOK_EMAIL")]
        [StringLength(100)]
        public string NokEmail { get; set; }

        [Column("NOK_GENDER")]
        [StringLength(100)]
        public string NokGender { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(CustomerReqId))]
        [InverseProperty(nameof(CustomerRequest.AddAccOpeningDetails))]
        public virtual CustomerRequest CustomerReq { get; set; }

        [InverseProperty("AccOpeningReq")] public virtual ICollection<AddAccOpeningDocs> AddAccOpeningDocs { get; set; }
    }
}