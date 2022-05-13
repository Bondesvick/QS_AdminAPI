using System.Diagnostics.CodeAnalysis;

namespace QuickServiceAdmin.Core.Model
{
    [ExcludeFromCodeCoverage]
    public class ValidateAuthTokenResponse
    {
        public ValidateAuthTokenResponse()
        {
            Head = new ApiResponseHead();
            Body = "";
        }

        public ApiResponseHead Head { get; set; }

        public object Body { get; set; }
    }
}