using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Filter;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuickService_AdminAPI.Controllers
{
    [ExcludeFromCodeCoverage]
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRequestController : ControllerBase
    {
        private readonly ICustomerRequestRepository _customerRequestRepository;
        private readonly string _declinedMessage;
        private readonly string _exceptionMessage;
        private readonly IRedboxService _redboxService;
        private readonly string _successMessage;

        public CustomerRequestController(ICustomerRequestRepository customerRequestRepository,
            IRedboxService redboxService, IConfiguration configuration)
        {
            _customerRequestRepository = customerRequestRepository;
            _successMessage = configuration["AppSettings:SuccessMessage"];
            _declinedMessage = configuration["AppSettings:DeclinedMessage"];
            _exceptionMessage = configuration["AppSettings:ExceptionMessage"];
            _redboxService = redboxService;
        }

        [HttpGet]
        [Route("GetCustomerRequest")]
        public async Task<ActionResult<GenericApiResponse<CustomerRequestDetails>>> GetCustomerRequest(
            [FromQuery] GetCustomerRequestParams getCustomerRequestParams)
        {
            var customerRequestDetails = await _customerRequestRepository.GetCustomerRequest(getCustomerRequestParams);
            if (customerRequestDetails == null)
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.Failure,
                    ResponseDescription = "Customer Request not found"
                });

            return Ok(new GenericApiResponse<CustomerRequestDetails>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = ResponseDescription.Success,
                Data = customerRequestDetails
            });
        }

        [HttpPost]
        [Route("{ticketId}/TreatCustomerRequest")]
        public async Task<ActionResult<CustomerRequest>> TreatCustomerRequest(string ticketId,
            [FromBody] TreatPayload treatPayload)
        {
            var customerRequest = await _customerRequestRepository.GetCustomerRequestByTicketId(ticketId);

            if (IsCustomerRequestCompleted(customerRequest))
            {
                throw new CustomErrorException("Customer request already completed", ResponseCodeConstants.BadRequest);
            }

            if (customerRequest.AssignedTo == null)
            {
                throw new CustomErrorException("Please assign customer request", ResponseCodeConstants.BadRequest);
            }

            if (customerRequest.AssignedBy == treatPayload.TreatedBy)
            {
                throw new CustomErrorException("Cannot treat customer requests you assigned", ResponseCodeConstants.BadRequest);
            }

            switch (treatPayload.Status)
            {
                case "EXCEPTION":
                    _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                        _exceptionMessage.Replace("[First Name]", customerRequest.AccountName)
                            .Replace("[Ticket ID]", customerRequest.TranId)
                            .Replace("[Request Type]", customerRequest.RequestType)
                            .Replace("[Reason]", customerRequest.RejectionReason));
                    break;
                case "RESOLVED":
                    _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                        _successMessage.Replace("[First Name]", customerRequest.AccountName)
                            .Replace("[Ticket ID]", customerRequest.TranId)
                            .Replace("[Request Type]", customerRequest.RequestType));
                    break;
                case "DECLINED":
                    _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                        _declinedMessage.Replace("[First Name]", customerRequest.AccountName)
                            .Replace("[Ticket ID]", customerRequest.TranId)
                            .Replace("[Request Type]", customerRequest.RequestType)
                            .Replace("[Reason]", customerRequest.Remarks));
                    break;
            }

            if (treatPayload.Remarks != null) customerRequest.Remarks = treatPayload.Remarks;
            customerRequest.TreatedDate = DateTime.Now;
            customerRequest.TreatedBy = treatPayload.TreatedBy;
            customerRequest.Status = treatPayload.Status;

            await _customerRequestRepository.SaveChanges();

            return Ok(new GenericApiResponse<CustomerRequest>
            {
                ResponseCode = ResponseCodeConstants.Failure,
                ResponseDescription = "Customer Request not found",
                Data = customerRequest
            });
        }

        [HttpPost]
        [Route("{ticketId}/AssignCustomerRequest")]
        public async Task<ActionResult<CustomerRequest>> AssignCustomerRequest(string ticketId,
            [FromBody] AssignPayload assignPayload)
        {
            var customerRequest = await _customerRequestRepository.GetCustomerRequestByTicketId(ticketId);

            if (IsCustomerRequestCompleted(customerRequest))
                // return BadRequest("Customer request already completed");
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.BadRequest,
                    ResponseDescription = "Customer request already completed"
                });

            customerRequest.AssignedTo = assignPayload.AssignedTo;
            customerRequest.AssignedBy = assignPayload.AssignedBy;
            customerRequest.Status = assignPayload.Status;

            if (assignPayload.Remarks != null) customerRequest.Remarks = assignPayload.Remarks;

            await _customerRequestRepository.SaveChanges();

            return Ok(new GenericApiResponse<CustomerRequest>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer request has been assigned successfully",
                Data = customerRequest
            });
        }

        private static bool IsCustomerRequestCompleted(CustomerRequest customerRequest)
        {
            return customerRequest.Status == "RESOLVED" || customerRequest.Status == "DECLINED" ||
                   customerRequest.Status == "EXCEPTION";
        }

        [HttpGet]
        [Route("GetAllCustomerRequests")]
        public async Task<ActionResult<List<CustomerRequest>>> GetAllCustomerRequests(
            [FromQuery] CustomerRequestFilterParams customerRequestFilterParams)
        {
            var customerRequests = await _customerRequestRepository.GetAllCustomerRequests(customerRequestFilterParams);
            // return Ok(customerRequests);
            return Ok(new GenericApiResponse<List<CustomerRequest>>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer requests retrieved successfully",
                Data = customerRequests
            });
        }

        [HttpGet]
        [Route("GetAllDashboardCustomerRequests")]
        public async Task<ActionResult<List<CustomerRequest>>> GetAllDashboardCustomerRequests(
            [FromQuery] CustomerRequestFilterParams customerRequestFilterParams)
        {
            var customerRequests =
                await _customerRequestRepository.GetAllDashboardCustomerRequests(customerRequestFilterParams);
            // return Ok(customerRequests);
            return Ok(new GenericApiResponse<List<CustomerRequest>>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer requests retrieved successfully",
                Data = customerRequests
            });
        }

        [HttpPost]
        [Route("UploadDocument/{ticketId}")]
        public async Task<ActionResult<CustomerRequestDocuments>> UploadDocument(string ticketId,
            [FromForm] FileUpload fileUpload)
        {
            var customerRequestDocument = await _customerRequestRepository.UploadDocument(ticketId, fileUpload.File);

            if (customerRequestDocument == null)
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.Failure,
                    ResponseDescription = "Not found"
                });

            return Ok(new GenericApiResponse<CustomerRequestDocuments>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer request document uploaded successfully",
                Data = customerRequestDocument
            });
        }

        [HttpDelete]
        [Route("{ticketId}/CustomerRequestDocument/{documentId}")]
        public async Task<ActionResult<bool>> DeleteDocument(string ticketId, long documentId)
        {
            await _customerRequestRepository.DeleteDocument(documentId);

            return Ok(new GenericApiResponse
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer request document deleted successfully"
            });
        }


        [HttpPatch]
        [Route("{ticketId}")]
        public async Task<ActionResult<CustomerRequest>> PatchCustomerRequest(string ticketId,
            [FromBody] JsonPatchDocument<CustomerRequest> customerRequestPatchDocument)
        {
            var customerRequest = await _customerRequestRepository.GetCustomerRequestByTicketId(ticketId);

            if (customerRequest == null)
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.Failure,
                    ResponseDescription = "Customer request not found"
                });

            var oldStatus = customerRequest.Status;

            customerRequestPatchDocument.ApplyTo(customerRequest, ModelState);

            if (!ModelState.IsValid)
                return Ok(new GenericApiResponse
                {
                    ResponseCode = ResponseCodeConstants.BadRequest,
                    ResponseDescription = "Bad Request"
                });

            if (customerRequest.Status != oldStatus)
                switch (customerRequest.Status)
                {
                    case "EXCEPTION":
                        customerRequest.TreatedDate = DateTime.Now;
                        _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                            _exceptionMessage.Replace("[First Name]", customerRequest.AccountName)
                                .Replace("[Ticket ID]", customerRequest.TranId)
                                .Replace("[Request Type]", customerRequest.RequestType)
                                .Replace("[Reason]", customerRequest.RejectionReason));
                        break;
                    case "RESOLVED":
                        customerRequest.TreatedDate = DateTime.Now;
                        _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                            _successMessage.Replace("[First Name]", customerRequest.AccountName)
                                .Replace("[Ticket ID]", customerRequest.TranId)
                                .Replace("[Request Type]", customerRequest.RequestType));
                        break;
                    case "DECLINED":
                        customerRequest.TreatedDate = DateTime.Now;
                        _redboxService.SendEmailNotification(customerRequest.AccountNumber,
                            _declinedMessage.Replace("[First Name]", customerRequest.AccountName)
                                .Replace("[Ticket ID]", customerRequest.TranId)
                                .Replace("[Request Type]", customerRequest.RequestType)
                                .Replace("[Reason]", customerRequest.Remarks));
                        break;
                }

            await _customerRequestRepository.SaveChanges();

            return Ok(new GenericApiResponse<CustomerRequest>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Customer request updated successfully",
                Data = customerRequest
            });
        }

        [HttpGet]
        [Route("GetLoggedInUserDetails")]
        public IActionResult GetLoggedInUserDetails(string user)
        {
            if (user == null) return NotFound();

            try
            {
                var getUserDetail = _customerRequestRepository.GetLoggedInUserDetails(user);

                if (getUserDetail == null) return NotFound();

                return Ok(getUserDetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetLoggedInUserNotification")]
        public async Task<IActionResult> GetLoggedInUserNotification(string user)
        {
            if (user == null) return NotFound();

            try
            {
                var getUserDetail = await _customerRequestRepository.GetLoggedInUserNotification(user);

                if (getUserDetail == null) return NotFound();

                return Ok(getUserDetail);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }

}
