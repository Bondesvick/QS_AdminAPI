using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace QuickServiceAdmin.Core.Model
{
    public class CardRequestParams
    {
        [Required]
        [JsonProperty("account_no")]
        public string AccountNumber { get; set; }

        [Required] [JsonProperty("city")] public string City { get; set; }

        [Required]
        [JsonProperty("customer_title")]
        public string CustomerTitle { get; set; }

        [Required]
        [JsonProperty("marital_status")]
        public string MaritalStatus { get; set; }

        [Required]
        [JsonProperty("customer_gender")]
        public string CustomerGender { get; set; }

        [Required]
        [JsonProperty("initiatingBranch")]
        public string InitiatingBranch { get; set; }

        [Required]
        [JsonProperty("collectionBranch")]
        public string CollectionBranch { get; set; }

        [Required] [JsonProperty("cardType")] public string CardType { get; set; }

        [Required]
        [JsonProperty("preferred_name_on_card")]
        public string PreferredNameOnCard { get; set; }

        [Required]
        [JsonProperty("account_to_debit")]
        public string AccountToDebit { get; set; }

        [Required]
        [JsonProperty("intiatedBy")]
        public string InitiatedBy { get; set; }

        [Required]
        [JsonProperty("approvedBy")]
        public string ApprovedBy { get; set; }
    }
}