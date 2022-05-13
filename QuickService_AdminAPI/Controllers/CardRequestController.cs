using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickServiceAdmin.Core.Entities;
using QuickServiceAdmin.Core.Filter;
using QuickServiceAdmin.Core.Helpers;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickService_AdminAPI.Controllers
{
    [ServiceFilter(typeof(AuthorizationFilter))]
    [Route("api/[controller]")]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class CardRequestController : ControllerBase
    {
        private readonly ICardRequestService _cardRequestService;
        private readonly ICustomerRequestRepository _customerRequestRepository;

        public CardRequestController(ICardRequestService cardRequestService, ICustomerRequestRepository customerRequestRepository)
        {
            _cardRequestService = cardRequestService;
            _customerRequestRepository = customerRequestRepository;
        }

        [HttpPost("Approve")]
        public async Task<ActionResult<GenericApiResponse>> ApproveCardRequest([FromBody] CardRequestDto cardRequestDto)
        {

            var customerRequest =
                await _customerRequestRepository.GetCustomerRequestByTicketId(cardRequestDto.CustomerRequestTicketId);

            if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
            {
                throw new CustomErrorException("Please assign customer request first", ResponseCodeConstants.BadRequest);
            }

            if (customerRequest.AssignedBy == cardRequestDto.ApprovedBy)
            {
                throw new CustomErrorException("Cannot approve a request you assigned", ResponseCodeConstants.BadRequest);
            }

            if (customerRequest.CardRequestDetails?.Status == "SUCCESS")
            {
                throw new CustomErrorException("Card request has already been made", ResponseCodeConstants.BadRequest);
            }


            await _cardRequestService.RequestCard(new CardRequestParams
            {
                AccountNumber = cardRequestDto.AccountNumber,
                AccountToDebit = cardRequestDto.AccountToDebit,
                ApprovedBy = cardRequestDto.ApprovedBy,
                CardType = cardRequestDto.CardType,
                City = cardRequestDto.City,
                CollectionBranch = cardRequestDto.CollectionBranch,
                CustomerGender = cardRequestDto.CustomerGender,
                CustomerTitle = cardRequestDto.CustomerTitle,
                InitiatedBy = cardRequestDto.InitiatedBy,
                InitiatingBranch = cardRequestDto.InitiatingBranch,
                MaritalStatus = cardRequestDto.MaritalStatus,
                PreferredNameOnCard = cardRequestDto.PreferredNameOnCard
            });


            customerRequest.CardRequestDetails = new CardRequestDetails
            {
                AccountNumber = cardRequestDto.AccountNumber,
                AccountToDebit = cardRequestDto.AccountToDebit,
                ApprovedBy = cardRequestDto.ApprovedBy,
                CardType = cardRequestDto.CardType,
                City = cardRequestDto.City,
                CollectionBranch = cardRequestDto.CollectionBranch,
                CustomerGender = cardRequestDto.CustomerGender,
                CustomerTitle = cardRequestDto.CustomerTitle,
                InitiatedBy = cardRequestDto.InitiatedBy,
                InitiatingBranch = cardRequestDto.InitiatingBranch,
                MaritalStatus = cardRequestDto.MaritalStatus,
                PreferredNameOnCard = cardRequestDto.PreferredNameOnCard,
                Status = "SUCCESS",
                Comment = "Card request was successful"
            };

            await _customerRequestRepository.SaveChanges();


            return Ok(new GenericApiResponse
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = ResponseDescription.CardRequestSuccessful
            });
        }

        [HttpPost("Initiate")]
        public async Task<ActionResult<GenericApiResponse>> InitiateCardRequest([FromBody] CardRequestDto cardRequestDto)
        {

            var customerRequest =
                await _customerRequestRepository.GetCustomerRequestByTicketId(cardRequestDto.CustomerRequestTicketId);

            if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
            {
                throw new CustomErrorException("Please assign customer request first",
                    ResponseCodeConstants.BadRequest);
            }

            switch (customerRequest.CardRequestDetails?.Status)
            {
                case "SUCCESS":
                    throw new CustomErrorException("Card request has already been made", ResponseCodeConstants.BadRequest);
                case "INITIATED":
                    throw new CustomErrorException("Card request has already been initiated", ResponseCodeConstants.BadRequest);
            }

            customerRequest.CardRequestDetails = new CardRequestDetails
            {
                AccountNumber = cardRequestDto.AccountNumber,
                AccountToDebit = cardRequestDto.AccountToDebit,
                CardType = cardRequestDto.CardType,
                City = cardRequestDto.City,
                CollectionBranch = cardRequestDto.CollectionBranch,
                CustomerGender = cardRequestDto.CustomerGender,
                CustomerTitle = cardRequestDto.CustomerTitle,
                InitiatedBy = cardRequestDto.InitiatedBy,
                InitiatingBranch = cardRequestDto.InitiatingBranch,
                MaritalStatus = cardRequestDto.MaritalStatus,
                PreferredNameOnCard = cardRequestDto.PreferredNameOnCard,
                Status = "INITIATED",
                Comment = "Card request was successfully initiated"
            };

            await _customerRequestRepository.SaveChanges();

            return Ok(new GenericApiResponse
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Card request was successfully initiated"
            });
        }



        [HttpGet("CardRequestDetails")]
        public async Task<ActionResult<GenericApiResponse<CardRequestDetailsResponse>>> GetCardRequestDetails(
            [FromQuery] [Required] [StringLength(10)] [MinLength(10)] [Numeric]
            string accountNumber)
        {
            var cardRequestDetails = await _cardRequestService.GetCardRequestDetails(accountNumber);

            return Ok(new GenericApiResponse<CardRequestDetailsResponse>
            {
                ResponseCode = ResponseCodeConstants.Success,
                ResponseDescription = "Request Successful",
                Data = cardRequestDetails
            });
        }
    }
}
