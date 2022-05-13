using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Filter;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using QuickServiceAdmin.Core.Helpers;
using System;

namespace QuickService_AdminAPI.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class AddressRequestController : ControllerBase
    {
        private readonly IAddressRequestService _addressRequestService;
        private readonly IRedboxService _redboxService;
        private readonly ICustomerRequestRepository _customerRequestRepository;
        private readonly ILogger<AddressRequestController> _logger;

        private readonly string _authorization;
        private readonly string _moduleId;
        private readonly string _redboxBaseUrl;

        public AddressRequestController(ILogger<AddressRequestController> logger, IConfiguration configuration, IAddressRequestService addressRequestService, IRedboxService redboxService, ICustomerRequestRepository customerRequestRepository)
        {
            _addressRequestService = addressRequestService;
            _redboxService = redboxService;
            _customerRequestRepository = customerRequestRepository;

            _authorization = configuration["AppSettings:RedboxAuthorization"];
            _moduleId = configuration["AppSettings:RedboxModuleId"];
            _redboxBaseUrl = configuration["AppSettings:RedboxBaseEndPoint"];

            _logger = logger;

        }


        [HttpGet("AddressRequestDetails")]
        public async Task<ActionResult<GenericApiResponse<AddressRequestDetailsResponse>>> GetAddressRequestDetails()
        {
            var addressRequestDetails = await _addressRequestService.GetAddressRequestDetails();

            return Ok(new GenericApiResponse<AddressRequestDetailsResponse>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Request Successful",
                Data = addressRequestDetails
            });
        }

        [HttpPost("Approve")]
        public async Task<ActionResult<GenericApiResponse>> ApproveAddressRequest([FromBody] AddressRequestDto addressRequestDto)
        {

            var customerRequest =
                await _customerRequestRepository.GetCustomerRequestByTicketId(addressRequestDto.CustomerRequestTicketId);

            if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
            {
                throw new CustomErrorException("Please assign customer request first", ResponseCodeConstants.BadRequest);
            }

            if (customerRequest.AddressRequestDetails?.Status == "SUCCESS")
            {
                throw new CustomErrorException("Address request has already been made", ResponseCodeConstants.BadRequest);
            }

            //         var responseString = RequestHelper.MakeRequestAndGetResponseGeneral(
            // _redboxService.GetValidateAccountNumberAndPhoneRequest("9301313065"), _redboxBaseUrl, _authorization, _moduleId);
            var responseString = RequestHelper.MakeRequestAndGetResponseGeneral(
                _redboxService.GetValidateAccountNumberAndPhoneRequest(addressRequestDto.AccountNumber), _redboxBaseUrl, _authorization, _moduleId);

            _logger.LogInformation("Response String {ResponseString}", responseString);

            var responseCode = RequestHelper.GetFirstTagValue(responseString, "responseCode");

            if (!(responseCode == "202" || responseCode == "00" || responseCode == "000"))
            {
                var responseDescription = RequestHelper.GetFirstTagValue(responseString, "responseDescription");
                var detail = RequestHelper.GetFirstTagValue(responseString, "detail");
                throw new CustomErrorException(!string.IsNullOrEmpty(detail) ? detail : responseDescription, ResponseCodeConstants.BadRequest);
            }


            var gender = RequestHelper.GetFirstTagValue(responseString, "Gender");
            var customerId = RequestHelper.GetFirstTagValue(responseString, "CustomerId") ?? RequestHelper.GetFirstTagValue(responseString, "CustId");
            var surname = RequestHelper.GetFirstTagValue(responseString, "Surname");
            var firstName = RequestHelper.GetFirstTagValue(responseString, "FirstName");
            var email = RequestHelper.GetFirstTagValue(responseString, "Email");
            var branchId = RequestHelper.GetFirstTagValue(responseString, "BranchId");
            var phoneNumber = RequestHelper.GetFirstTagValue(responseString, "PhoneNumber1");

            addressRequestDto.BranchId = branchId;
            addressRequestDto.PhoneNumber =!string.IsNullOrEmpty(addressRequestDto.PhoneNumber)? addressRequestDto.PhoneNumber: "0"+phoneNumber.Substring(Math.Max(0, phoneNumber.Length - 10));
            addressRequestDto.CustomerGender = gender;
            addressRequestDto.CifId = !string.IsNullOrEmpty(addressRequestDto.CifId) ? addressRequestDto.CifId : customerId;
            addressRequestDto.LastName = surname;
            addressRequestDto.FirstName = !string.IsNullOrEmpty(firstName) ? firstName : surname;
            addressRequestDto.EmailAddress = !string.IsNullOrEmpty(addressRequestDto.EmailAddress) ? addressRequestDto.EmailAddress : email;
            addressRequestDto.CompanyName = !string.IsNullOrEmpty(addressRequestDto.CompanyName) ? addressRequestDto.CompanyName : $@"{surname} {firstName}";

            if (string.IsNullOrEmpty(addressRequestDto.EmailAddress))
            {
                throw new CustomErrorException("Please provide an email address", ResponseCodeConstants.BadRequest);
            }
            
            if (string.IsNullOrEmpty(addressRequestDto.CifId))
            {
                throw new CustomErrorException("No customer ID found on account", ResponseCodeConstants.BadRequest);
            }
            var reqInitiationId = await _addressRequestService.RequestAddress(addressRequestDto);

            customerRequest.AddressRequestDetails = new AddressRequestDetails
            {
                AccountNumber = addressRequestDto.AccountNumber,
                ApprovedBy = addressRequestDto.ApprovedBy,
                City = addressRequestDto.City,
                CustomerGender = addressRequestDto.CustomerGender,
                InitiatedBy = addressRequestDto.InitiatedBy,
                Alias = addressRequestDto.Alias,
                Remark = addressRequestDto.Remarks,
                State = addressRequestDto.State,
                AddressLine1 = addressRequestDto.AddressLine1,
                AddressLine2 = addressRequestDto.AddressLine2,
                BranchId = addressRequestDto.BranchId,
                CifId = addressRequestDto.CifId,
                CompanyName = addressRequestDto.CompanyName,
                EmailAddress = addressRequestDto.EmailAddress,
                FirstName = addressRequestDto.FirstName,
                LandMark = addressRequestDto.LandMark,
                LastName = addressRequestDto.LastName,
                PhoneNumber = addressRequestDto.PhoneNumber,
                ReqInitiationId = reqInitiationId,
                Status = "SUCCESS",
                Comment = "Address request was successful",
            };

            await _customerRequestRepository.SaveChanges();

            return Ok(new GenericApiResponse
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = ResponseDescription.AddressRequestSuccessful
            });
        }

        //     [HttpPost("Initiate")]
        //     public async Task<ActionResult<GenericApiResponse>> InitiateAddressRequest([FromBody] AddressRequestDto addressRequestDto)
        //     {

        //         var customerRequest =
        //             await _customerRequestRepository.GetCustomerRequestByTicketId(addressRequestDto.CustomerRequestTicketId);

        //         if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
        //         {
        //             throw new CustomErrorException("Please assign customer request first",
        //                 ResponseCodeConstants.BadRequest);
        //         }

        //         switch (customerRequest.AddressRequestDetails?.Status)
        //         {
        //             case "SUCCESS":
        //                 throw new CustomErrorException("Address request has already been made", ResponseCodeConstants.BadRequest);
        //             case "INITIATED":
        //                 throw new CustomErrorException("Address request has already been initiated", ResponseCodeConstants.BadRequest);
        //         }

        //         customerRequest.AddressRequestDetails = new AddressRequestDetails
        //         {
        //             AccountNumber = addressRequestDto.AccountNumber,
        //             ApprovedBy = addressRequestDto.ApprovedBy,
        //             City = addressRequestDto.City,
        //             CustomerGender = addressRequestDto.CustomerGender,
        //             InitiatedBy = addressRequestDto.InitiatedBy,
        //             Alias = addressRequestDto.Alias,
        //             Remark = addressRequestDto.Remarks,
        //             State = addressRequestDto.State,
        //             AddressLine1 = addressRequestDto.AddressLine1,
        //             AddressLine2 = addressRequestDto.AddressLine2,
        //             BranchId = addressRequestDto.BranchId,
        //             CifId = addressRequestDto.CifId,
        //             CompanyName = addressRequestDto.CompanyName,
        //             EmailAddress = addressRequestDto.EmailAddress,
        //             FirstName = addressRequestDto.FirstName,
        //             LandMark = addressRequestDto.LandMark,
        //             LastName = addressRequestDto.LastName,
        //             PhoneNumber = addressRequestDto.PhoneNumber,
        //             Status = "INITIATED",
        //             Comment = "Address request was successfully initiated"
        //         };

        //         await _customerRequestRepository.SaveChanges();

        //         return Ok(new GenericApiResponse
        //         {
        //             ResponseCode = ResponseCodeConstants.Success,
        //             ResponseDescription = "Address request was successfully initiated"
        //         });
        //     }
    }
}
