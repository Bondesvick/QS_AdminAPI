// using System;
// using System.Diagnostics.CodeAnalysis;
// using System.IO;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using QuickServiceAdmin.Core.Entities;
// using QuickServiceAdmin.Core.Filter;
// using QuickServiceAdmin.Core.Interface;
// using QuickServiceAdmin.Core.Model;

// namespace QuickService_AdminAPI.Controllers
// {
//     [ServiceFilter(typeof(AuthorizationFilter))]
//     [Route("api/[controller]")]
//     [ApiController]
//     [ExcludeFromCodeCoverage]
//     public class FacialIdentityRequestController : ControllerBase
//     {
//         private readonly IFacialIdentityRequestService _facialIdentityRequestService;
//         private readonly ICustomerRequestRepository _customerRequestRepository;

//         public FacialIdentityRequestController(IFacialIdentityRequestService facialIdentityRequestService,
//             ICustomerRequestRepository customerRequestRepository)
//         {
//             _facialIdentityRequestService = facialIdentityRequestService;
//             _customerRequestRepository = customerRequestRepository;
//         }


//         [HttpPost("UploadDocument")]
//         public async Task<ActionResult<GenericApiResponse<FileResponseDTO>>> ConvertFileToBase64(
//             [FromForm] FileRequestDto fileRequest)
//         {
//             var file = fileRequest.File;

//             string fileInBase64String;

//             await using (var ms = new MemoryStream())
//             {
//                 await file.CopyToAsync(ms);
//                 var fileBytes = ms.ToArray();
//                 fileInBase64String = Convert.ToBase64String(fileBytes);
//             }

//             return Ok(new GenericApiResponse<FileResponseDTO>()
//             {
//                 ResponseCode = ResponseCodeConstants.Success,
//                 ResponseDescription = "Success",
//                 Data = new FileResponseDTO
//                 {
//                     Base64 = fileInBase64String,
//                     FileName = file.FileName,
//                     ContentType = file.ContentType
//                 }
//             });

//         }

//         [HttpPost("Approve")]
//         public async Task<ActionResult<GenericApiResponse>> ApproveFacialIdentityRequest(
//             [FromBody] FacialIdentityRequestDto facialIdentityRequestDto)
//         {

//             var customerRequest =
//                 await _customerRequestRepository.GetCustomerRequestByTicketId(facialIdentityRequestDto
//                     .CustomerRequestTicketId);

//             if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
//             {
//                 throw new CustomErrorException("Please assign customer request first",
//                     ResponseCodeConstants.BadRequest);
//             }

//             if (customerRequest.AssignedBy == facialIdentityRequestDto.ApprovedBy)
//             {
//                 throw new CustomErrorException("Cannot approve a request you assigned",
//                     ResponseCodeConstants.BadRequest);
//             }

//             if (customerRequest.FacialIdentityRequestDetails?.Status == "SUCCESS")
//             {
//                 throw new CustomErrorException("Facial Identity request has already been made",
//                     ResponseCodeConstants.BadRequest);
//             }


//             var facialIdentityResponse = await _facialIdentityRequestService.RequestFacialIdentity(
//                 new FacialIdentityRequestParams
//                 {
//                     IdentityType = facialIdentityRequestDto.IdentityType,
//                     IdentityNumber = facialIdentityRequestDto.IdentityNumber,
//                     RequestImage = facialIdentityRequestDto.RequestPhoto
//                 });

//             customerRequest.FacialIdentityRequestDetails = new FacialIdentityRequestDetails
//             {
//                 ApprovedBy = facialIdentityRequestDto.ApprovedBy,
//                 InitiatedBy = facialIdentityRequestDto.InitiatedBy,
//                 RequestPhoto = facialIdentityRequestDto.RequestPhoto,
//                 ReportId = facialIdentityResponse.Id,
//                 IdentityType = facialIdentityRequestDto.IdentityType,
//                 IdentityNumber = facialIdentityRequestDto.IdentityNumber,
//                 FirstName = facialIdentityResponse.Response.FirstName,
//                 MiddleName = facialIdentityResponse.Response.MiddleName,
//                 LastName = facialIdentityResponse.Response.LastName,
//                 DateOfBirth = facialIdentityResponse.Response.Dob,
//                 ResponsePhoto = facialIdentityResponse.Response.Photo,
//                 Confidence = facialIdentityResponse.Response.FaceDetails.Confidence,
//                 Threshold = facialIdentityResponse.Response.FaceDetails.Threshold,
//                 Status = "SUCCESS",
//                 Comment = "Facial Identity request was successful"
//             };

//             await _customerRequestRepository.SaveChanges();

//             return Ok(new GenericApiResponse
//             {
//                 ResponseCode = ResponseCodeConstants.Success,
//                 ResponseDescription = ResponseDescription.FacialIdentityRequestSuccessful
//             });
//         }

//         [HttpPost("Initiate")]
//         public async Task<ActionResult<GenericApiResponse>> InitiateFacialIdentityRequest(
//             [FromBody] FacialIdentityRequestDto facialIdentityRequestDto)
//         {

//             var customerRequest =
//                 await _customerRequestRepository.GetCustomerRequestByTicketId(facialIdentityRequestDto
//                     .CustomerRequestTicketId);

//             if (customerRequest.AssignedTo == null || customerRequest.AssignedBy == null)
//             {
//                 throw new CustomErrorException("Please assign customer request first",
//                     ResponseCodeConstants.BadRequest);
//             }

//             switch (customerRequest.FacialIdentityRequestDetails?.Status)
//             {
//                 case "SUCCESS":
//                     throw new CustomErrorException("Facial Identity request has already been made",
//                         ResponseCodeConstants.BadRequest);
//                 case "INITIATED":
//                     throw new CustomErrorException("Facial Identity request has already been initiated",
//                         ResponseCodeConstants.BadRequest);
//             }

//             customerRequest.FacialIdentityRequestDetails = new FacialIdentityRequestDetails
//             {
//                 ApprovedBy = facialIdentityRequestDto.ApprovedBy,
//                 InitiatedBy = facialIdentityRequestDto.InitiatedBy,
//                 RequestPhoto = facialIdentityRequestDto.RequestPhoto,
//                 IdentityType = facialIdentityRequestDto.IdentityType,
//                 IdentityNumber = facialIdentityRequestDto.IdentityNumber,
//                 Status = "INITIATED",
//                 Comment = "Facial Identity request was successfully initiated"
//             };

//             await _customerRequestRepository.SaveChanges();

//             return Ok(new GenericApiResponse
//             {
//                 ResponseCode = ResponseCodeConstants.Success,
//                 ResponseDescription = "Facial Identity request was successfully initiated"
//             });
//         }

//     }
// }