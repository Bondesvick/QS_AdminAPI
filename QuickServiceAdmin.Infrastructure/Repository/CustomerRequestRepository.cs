using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Infrastructure.Repository
{
    public class CustomerRequestRepository : ICustomerRequestRepository
    {
        private readonly QuickServiceContext _db;

        public CustomerRequestRepository(QuickServiceContext db)
        {
            _db = db;
        }

        public async Task<CustomerRequestDetails> GetCustomerRequest(GetCustomerRequestParams getCustomerRequestParams)
        {
            //try
            //{
            var query = _db.CustomerRequest.AsQueryable();
            query = query.Where(x => x.TranId == getCustomerRequestParams.TicketId);
            query = query.Include(x => x.CustomerRequestDocuments);
            query = query.Include(x => x.CardRequestDetails);
            query = query.Include(x => x.AddressRequestDetails);
            //query = query.Include(x => x.LoanRequestDetails).ThenInclude(x => x.CustomerRequest);
            //query = query.Include(x => x.DebitCardDetails).ThenInclude(x => x.Documents);
            // query = query.Include(x => x.FacialIdentityRequestDetails);
            query = query.Include(x => x.RequestAndStatusMgtDetails).ThenInclude(x => x.RequestAndStatusMgtDocs);

            var customerRequest = await query.FirstOrDefaultAsync();

            if (customerRequest == null)
            {
                throw new CustomErrorException("Customer request not found", ResponseCodeConstants.Failure);
            }

            var customerRequestDetails = new CustomerRequestDetails
            {
                Bvn = customerRequest.Bvn,
                Remarks = customerRequest.Remarks,
                Status = customerRequest.Status,
                AccountName = customerRequest.AccountName,
                AccountNumber = customerRequest.AccountNumber,
                AssignedBy = customerRequest.AssignedBy,
                AssignedTo = customerRequest.AssignedTo,
                CreatedDate = customerRequest.CreatedDate,
                RejectionReason = customerRequest.RejectionReason,
                RequestType = customerRequest.RequestType,
                TranId = customerRequest.TranId,
                TreatedBy = customerRequest.TreatedBy,
                TreatedDate = customerRequest.TreatedDate,
                CustomerAuthType = customerRequest.CustomerAuthType,
                TreatedByUnit = customerRequest.TreatedByUnit,
                CustomerRequestDocuments = customerRequest.CustomerRequestDocuments,
                CardRequestDetails = customerRequest.CardRequestDetails,
                AddressRequestDetails = customerRequest.AddressRequestDetails,
                // FacialIdentityRequestDetails = customerRequest.FacialIdentityRequestDetails,
                ReopenedDetails = customerRequest.RequestAndStatusMgtDetails
            };

            switch (getCustomerRequestParams.Module)
            {
                case "kyc-document-update":
                    customerRequestDetails.ModuleDetails = customerRequest.KycDocumentDetails;
                    customerRequestDetails.ModuleDocuments = customerRequest.KycDocumentDetails.KycDocumentDocs;
                    break;

                case "additional-account-opening":
                    var addAccOpeningDetails = customerRequest.AddAccOpeningDetails.ElementAtOrDefault(0) ??
                                               new AddAccOpeningDetails();
                    customerRequestDetails.ModuleDetails = addAccOpeningDetails;
                    customerRequestDetails.ModuleDocuments = addAccOpeningDetails.AddAccOpeningDocs;
                    break;

                case "account-upgrade":
                    customerRequestDetails.ModuleDetails = TypeMerger.TypeMerger.Merge(
                        customerRequest.AccountUpgradeDetails.ElementAtOrDefault(0) ?? new AccountUpgradeDetails(),
                        customerRequest.AccountUpgradeCifDetails.ElementAtOrDefault(0) ??
                        new AccountUpgradeCifDetails());
                    customerRequestDetails.ModuleDocuments = customerRequest.AccountUpgradeDoc;
                    break;

                case "data-mandate":
                    var dataUpdateDetails =
                        (customerRequest.DataUpdateDetails ?? new List<DataUpdateDetails>().ToList())
                        .ElementAtOrDefault(0);
                    customerRequestDetails.ModuleDetails = dataUpdateDetails;
                    customerRequestDetails.ModuleDocuments = dataUpdateDetails?.DataUpdateDocs;
                    break;

                case "failed-transaction":
                    customerRequestDetails.ModuleDetails =
                        customerRequest.FailedTransactionDetails.ElementAtOrDefault(0) ??
                        new FailedTransactionDetails();
                    customerRequestDetails.ModuleDocuments = customerRequest.FailedTransactionDoc;
                    break;

                case "sme-onboarding-request":
                    var smeOnboardingDetails = customerRequest.SmeOnboardingDetails.ElementAtOrDefault(0) ??
                                               new SmeOnboardingDetails();
                    customerRequestDetails.ModuleDetails = smeOnboardingDetails;
                    customerRequestDetails.ModuleDocuments = smeOnboardingDetails.SmeOnboardingDocs;
                    break;

                case "password-reset":
                    customerRequestDetails.ModuleDocuments = customerRequest.PasswordResetDoc;
                    //customerRequestDetails.ModuleDetails = query.Where(x => x.p)
                    break;

                case "account-reactivation":
                    customerRequestDetails.ModuleDetails =
                        customerRequest.AcctReactivationDetails.ElementAtOrDefault(0) ??
                        new AcctReactivationDetails();
                    customerRequestDetails.ModuleDocuments = customerRequest.AcctReactivationDoc;
                    break;

                case "corporate-account-opening":
                    customerRequestDetails.ModuleDetails = TypeMerger.TypeMerger.Merge(
                        customerRequest.CorpAccOpeningDetails.ElementAtOrDefault(0) ?? new CorpAccOpeningDetails(),
                        customerRequest.CorpAccOpeningSignatory.ElementAtOrDefault(0) ??
                        new CorpAccOpeningSignatory());
                    customerRequestDetails.ModuleDetails = TypeMerger.TypeMerger.Merge(
                        customerRequestDetails.ModuleDetails,
                        customerRequest.CorpAccOpeningDirectorDetails.ElementAtOrDefault(0) ??
                        new CorpAccOpeningDirectorDetails());

                    customerRequestDetails.ModuleDetails = TypeMerger.TypeMerger.Merge(
                        customerRequestDetails.ModuleDetails,
                        customerRequest.CorpAccOpeningEnterOnline.ElementAtOrDefault(0) ??
                        new CorpAccOpeningEnterOnline());
                    customerRequestDetails.ModuleDetails = TypeMerger.TypeMerger.Merge(
                        customerRequestDetails.ModuleDetails,
                        customerRequest.CorpAccOpeningShareHolder.ElementAtOrDefault(0) ??
                        new CorpAccOpeningShareHolder());
                    customerRequestDetails.ModuleDocuments = customerRequest.CorpAccOpeningDoc;
                    break;

                case "internet-banking-onboarding":
                    customerRequestDetails.ModuleDetails =
                        customerRequest.InternetBankingOnboardingDetails.ElementAtOrDefault(0) ??
                        new InternetBankingOnboardingDetails();
                    customerRequestDetails.ModuleDocuments = customerRequest.InternetBankingOnboardingDoc;
                    break;

                case "loan-initiation":
                    customerRequestDetails.ModuleDetails =
                        customerRequest.LoanRequestDetails.ElementAtOrDefault(0) ??
                        new LoanRequestDetails();
                    customerRequestDetails.ModuleDocuments = customerRequest.LoanRequestDoc;
                    break;

                case "loan-repayment":
                    customerRequestDetails.ModuleDetails = customerRequest.LoanRepaymentDetails ??
                                                           new LoanRepaymentDetails();
                    customerRequestDetails.ModuleDocuments = customerRequest.LoanRepaymentDocuments;
                    break;

                case "debit-card-request":
                    customerRequestDetails.ModuleDetails = customerRequest.DebitCardDetails.ElementAtOrDefault(0) ??
                                                           new DebitCardDetails();
                    //customerRequestDetails.ModuleDocuments = customerRequest.DebitCardDetails.ElementAtOrDefault(0)?.Documents ?? new List<DebitCardDocument>();

                    var query2 = _db.DebitCardDocuments.AsQueryable();

                    customerRequestDetails.ModuleDocuments = query2.Where(x =>
                        x.AccOpeningReqId.Equals(customerRequest.DebitCardDetails.FirstOrDefault().Id));
                    break;
            }

            return customerRequestDetails;
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
        }

        public Task<List<CustomerRequest>> GetAllDashboardCustomerRequests(
            CustomerRequestFilterParams customerRequestFilterParams)
        {
            var query = GetAllCustomerRequestsQueryable(customerRequestFilterParams);

            //pagination
            // var numberOfItemsToSkip = (customerRequestFilterParams.Page - 1);
            //
            // if (numberOfItemsToSkip != 0)
            // {
            //     query = query.Skip(numberOfItemsToSkip);
            // }
            //
            // query = query.Take(customerRequestFilterParams.Limit);

            //remove INCOMPLETE customer requests
            query = query.Where(x =>
                x.Status != "INCOMPLETE" &&
                x.Status != "EXCEPTION" &&
                x.Status != "RESOLVED" &&
                x.Status != "DECLINED");
            return query.ToListAsync();
        }

        public Task<List<CustomerRequest>> GetAllCustomerRequests(
            CustomerRequestFilterParams customerRequestFilterParams)
        {
            return GetAllCustomerRequestsQueryable(customerRequestFilterParams)
                .ToListAsync();
        }

        public async Task<CustomerRequestDocuments> UploadDocument(string ticketId,
            IFormFile file)
        {
            string fileInBase64String;
            await using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                fileInBase64String = Convert.ToBase64String(fileBytes);
            }

            var customerRequestDocument = new CustomerRequestDocuments
            {
                DocumentFile = fileInBase64String,
                DocumentFilename = file.FileName,
                DocumentType = file.ContentType
            };
            var customerRequest = await _db.CustomerRequest.FirstOrDefaultAsync(x => x.TranId == ticketId);
            if (customerRequest == null) return null;

            customerRequest.CustomerRequestDocuments.Add(customerRequestDocument);
            await _db.SaveChangesAsync();
            return customerRequestDocument;
        }

        public async Task<CustomerRequest> GetCustomerRequestByTicketId(string ticketId)
        {
            var customerRequest = await _db.CustomerRequest.Where(x => x.TranId == ticketId)
                .FirstOrDefaultAsync();

            if (customerRequest == null)
            {
                throw new CustomErrorException("Customer Request Not Found", ResponseCodeConstants.Failure);
            }

            return customerRequest;
        }

        public Task SaveChanges()
        {
            return _db.SaveChangesAsync();
        }

        public CustomerRequest GetLoggedInUserDetails(string user)
        {
            return _db.CustomerRequest.FirstOrDefault(x => x.TreatedBy == user);
        }

        public async Task<List<CustomerRequest>> GetLoggedInUserNotification(string user)
        {
            if (_db != null)
                return await _db.CustomerRequest.Where(x => x.TreatedBy == user && x.Status == "ASSIGNED")
                    .ToListAsync();

            return null;
        }

        public async Task<bool> DeleteDocument(long documentId)
        {
            var customerRequestDocument =
                await _db.CustomerRequestDocuments.FirstOrDefaultAsync(x => x.Id == documentId);
            if (customerRequestDocument == null) return false;

            _db.CustomerRequestDocuments.Remove(customerRequestDocument);
            return await _db.SaveChangesAsync() == 0;
        }

        private IQueryable<CustomerRequest> GetAllCustomerRequestsQueryable(
            CustomerRequestFilterParams customerRequestFilterParams)
        {
            var query = _db.CustomerRequest.AsQueryable();
            if (customerRequestFilterParams.TicketId != null)
                query = query.Where(x => x.TranId == customerRequestFilterParams.TicketId);

            if (customerRequestFilterParams.Bvn != null)
                query = query.Where(x => x.Bvn == customerRequestFilterParams.Bvn);

            if (customerRequestFilterParams.AccountNumber != null)
                query = query.Where(x => x.AccountNumber == customerRequestFilterParams.AccountNumber);

            if (customerRequestFilterParams.Module != null)
            {
                var existingIds = new List<long>();
                switch (customerRequestFilterParams.Module)
                {
                    case "kyc-document-update":
                        existingIds = _db.KycDocumentDetails.Select(x => x.CustomerRequestId)
                            .ToList();
                        break;

                    case "additional-account-opening":
                        existingIds = _db.AddAccOpeningDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "account-upgrade":
                        existingIds = _db.AccountUpgradeDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "data-mandate":
                        existingIds = _db.DataUpdateDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "failed-transaction":
                        existingIds = _db.FailedTransactionDetails.Select(x => x.CustomerRequestId)
                            .ToList();
                        break;

                    case "sme-onboarding-request":
                        existingIds = _db.SmeOnboardingDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "password-reset":
                        //existingIds = _db.PasswordResetDoc.Select(x => x.CustomerReqId)
                        //    .ToList();
                        existingIds = _db.CustomerRequest.Where(x => x.RequestType == "Password Reset").Select(x => x.Id)
                            .ToList();
                        break;

                    case "account-reactivation":
                        existingIds = _db.AcctReactivationDetails.Select(x => x.CustomerRequestId)
                            .ToList();
                        break;

                    case "corporate-account-opening":
                        existingIds = _db.CorpAccOpeningDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "internet-banking-onboarding":
                        existingIds = _db.InternetBankingOnboardingDetails.Select(x => x.CustomerReqId)
                            .ToList();
                        break;

                    case "loan-initiation":
                        existingIds = _db.LoanRequestDetails.Select(x => x.CustomerRequestId).ToList();
                        break;

                    case "loan-repayment":
                        existingIds = _db.LoanRepaymentDetails.Select(x => x.CustomerRequestId).ToList();
                        break;

                    case "debit-card-request":
                        existingIds = _db.DebitCardDetails.Select(x => x.CustomerReqId).ToList();
                        break;
                }

                query = query.Where(x => existingIds.Contains(x.Id));
            }

            if (customerRequestFilterParams.StartDate != null && customerRequestFilterParams.EndDate != null)
                query = query.Where(x =>
                        x.CreatedDate >= customerRequestFilterParams.StartDate
                        &&
                        x.CreatedDate <= customerRequestFilterParams.EndDate
                );

            if (customerRequestFilterParams.TreatedStartDate != null && customerRequestFilterParams.TreatedEndDate != null)
            {
                query = query.Where(x =>
                    (x.TreatedDate != null && x.TreatedDate >= customerRequestFilterParams.TreatedStartDate)
                &&
                    (x.TreatedDate != null && x.TreatedDate <= customerRequestFilterParams.TreatedEndDate)
                    );
            }

            if (customerRequestFilterParams.Status != null)
                query = query.Where(x => x.Status == customerRequestFilterParams.Status);

            query = query.OrderByDescending(x => x.CreatedDate);
            return query;
        }
    }
}