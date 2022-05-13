using System;
using System.Collections.Generic;
using QuickServiceAdmin.Core.Entities;

namespace QuickServiceAdmin.Core.Model
{
    public class CustomerRequestDetails
    {
        public string TranId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string CustomerAuthType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TreatedBy { get; set; }
        public DateTime? TreatedDate { get; set; }
        public string RequestType { get; set; }
        public string TreatedByUnit { get; set; }
        public string RejectionReason { get; set; }
        public string Remarks { get; set; }
        public string Bvn { get; set; }
        public string AssignedBy { get; set; }
        public string AssignedTo { get; set; }
        public object ModuleDetails { get; set; }
        public object ModuleDocuments { get; set; }
        public ICollection<CustomerRequestDocuments> CustomerRequestDocuments { get; set; }
        public CardRequestDetails CardRequestDetails { get; set; }
        public AddressRequestDetails AddressRequestDetails { get; set; }
        public RequestAndStatusMgtDetails ReopenedDetails { get; set; }
        public FacialIdentityRequestDetails FacialIdentityRequestDetails { get; set; }
    }
}
