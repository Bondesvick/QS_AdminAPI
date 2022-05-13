using System.Diagnostics.CodeAnalysis;

namespace QuickServiceAdmin.Core.Model
{
    [ExcludeFromCodeCoverage]
    public sealed class ResponseCode
    {
        public const string Success = "00";
        public const string Failure = "999";
        public const string GetTagValueError = "-999-";
        public const string RedisFailure = "70";
        public const string SuccessWithTripleZero = "000";
        public const string SuccessWith202 = "202";

        private ResponseCode()
        {
        }
    }
}