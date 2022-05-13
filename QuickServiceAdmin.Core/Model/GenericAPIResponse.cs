using System.Diagnostics.CodeAnalysis;

namespace QuickServiceAdmin.Core.Model
{
    public class GenericApiResponse
    {
        public GenericApiResponse()
        {
        }

        [ExcludeFromCodeCoverage]
        public GenericApiResponse(string responseCode, string responseDescription)
        {
            ResponseCode = responseCode;
            ResponseDescription = responseDescription;
        }

        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
    }

    public class GenericApiResponse<T> : GenericApiResponse
    {
        [ExcludeFromCodeCoverage]
        public GenericApiResponse(string responseCode, string responseDescription, T data)
            : base(responseCode, responseDescription)
        {
            Data = data;
        }

        public GenericApiResponse()
        {
        }

        public T Data { get; set; }
    }
}