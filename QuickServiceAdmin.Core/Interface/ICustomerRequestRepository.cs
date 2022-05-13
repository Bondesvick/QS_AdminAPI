using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface ICustomerRequestRepository
    {
        Task<CustomerRequestDetails> GetCustomerRequest(GetCustomerRequestParams getCustomerRequestParams);

        Task<List<CustomerRequest>> GetAllDashboardCustomerRequests(
            CustomerRequestFilterParams customerRequestFilterParams);

        Task<List<CustomerRequest>> GetAllCustomerRequests(CustomerRequestFilterParams customerRequestFilterParams);
        Task<CustomerRequestDocuments> UploadDocument(string ticketId, IFormFile file);
        Task<CustomerRequest> GetCustomerRequestByTicketId(string ticketId);
        Task SaveChanges();
        CustomerRequest GetLoggedInUserDetails(string user);
        Task<List<CustomerRequest>> GetLoggedInUserNotification(string user);
        Task<bool> DeleteDocument(long documentId);
    }
}