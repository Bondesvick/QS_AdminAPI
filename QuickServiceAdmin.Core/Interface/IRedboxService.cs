using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Interface
{
    public interface IRedboxService
    {
        string GetAccountEmail(string accountNumber);
        void SendEmail(EmailMessageRequestDto emailMessageRequestDto);
        void SendEmailNotification(string accountNumber, string mailBody);
        string GetValidateAccountNumberAndPhoneRequest(string accountNumber);
    }
}