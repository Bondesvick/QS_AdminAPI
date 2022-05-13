using System.ComponentModel.DataAnnotations;

namespace QuickServiceAdmin.Core.Model
{
    public class AddressRequestDto
    {
        [Required] public string AccountNumber { get; set; }
        public string Remarks { get; set; }

        [Required] public string City { get; set; }

        public string CifId { get; set; }

        [Required] public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required] public string State { get; set; }
        public string Alias { get; set; }
        public string CustomerGender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public string BranchId { get; set; }
        [Required] public string InitiatedBy { get; set; }
        [Required] public string ApprovedBy { get; set; }
        public string LandMark { get; set; }
        public string CompanyName { get; set; }

        [Required] public string CustomerRequestTicketId { get; set; }
    }
}
