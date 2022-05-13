using System.Diagnostics.CodeAnalysis;

namespace QuickServiceAdmin.Core.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class ResponseDescription
    {
        public const string Success = "Request successfull";

        public const string GeneralFailure =
            "Your request could not be processed at the moment, please try again later.";

        public const string AlreadyExist = "There is a pending action for this supplier";
        public const string InvalidRequest = "Invalid Request.";
        public const string AuthFailure = "Unauthorized profile supplied or session expired";
        public const string UnkownActionType = "The action type is invalid";
        public const string Failure = "Failed to Save changes for approval";
        public const string GetSuccessful = "Get thrive supplier successful";
        public const string GetFailure = "Get thrive unsuccessful";
        public const string ApproveSuccessfully = "Request Approved successfully";
        public const string ApproveFailure = "Request Approval Failed";
        public const string Declined = "Thrive Supplier Application Declined";
        public const string DeclinedFailed = "Thrive Supplier Application Decline failed";
        public const string CreateSuccessfully = " Request successful awaiting approval";
        public const string CreateFailure = "Request unsuccessful approval failed";
        public const string RequestReturnNull = "Information not available";
        public const string RequestUnauthourized = "You are not allowed to perform this action";
        public const string UpdateSuccessfully = "Request successful awaiting approval";
        public const string UpdateFailure = "Request unsuccessful approval failed";
        public const string DeleteFailure = "Request unsuccessful approval failed";
        public const string DeleteSuccessfully = "Request successful awaiting approval";
        public const string CardRequestSuccessful = "Card request was successful";
        public const string AddressRequestSuccessful = "Address request was successful";
        public const string FacialIdentityRequestSuccessful = "Facial Identity request was successful";

        public const string RuntimeException =
            "Your request could not be processed at the moment, please try again later.";
    }
}