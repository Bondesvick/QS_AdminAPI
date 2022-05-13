using System;

namespace QuickServiceAdmin.Core.Model
{
    public class CustomErrorException : Exception
    {
        public string StatusCode;

        public CustomErrorException(string message, string statusCode = ResponseCodeConstants.InternalException) :
            base(message)
        {
            StatusCode = statusCode;
        }
    }
}