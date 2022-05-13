using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class CardRequestDto
    {
        [Required] public string AccountNumber { get; set; }

        [Required] public string City { get; set; }

        [Required] public string CustomerTitle { get; set; }

        [Required] public string MaritalStatus { get; set; }

        [Required] public string CustomerGender { get; set; }

        [Required] public string InitiatingBranch { get; set; }

        [Required] public string CollectionBranch { get; set; }

        [Required] public string CardType { get; set; }

        [Required] public string PreferredNameOnCard { get; set; }

        [Required] public string AccountToDebit { get; set; }

        [Required] public string InitiatedBy { get; set; }

        [Required] public string ApprovedBy { get; set; }

        [Required] public string CustomerRequestTicketId { get; set; }
    }
}
