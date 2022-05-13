using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class FacialIdentityRequestDto
    {
        [Required] public string IdentityNumber { get; set; }

        [Required] public string IdentityType { get; set; }
        [Required] public string RequestPhoto { get; set; }
        [Required] public string CustomerRequestTicketId { get; set; }
        [Required] public string ApprovedBy { get; set; }
        [Required] public string InitiatedBy { get; set; }
    }
}