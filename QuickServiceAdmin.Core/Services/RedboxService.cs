using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickServiceAdmin.Core.Helpers;
using QuickServiceAdmin.Core.Interface;
using QuickServiceAdmin.Core.Model;

namespace QuickServiceAdmin.Core.Services
{
    [ExcludeFromCodeCoverage]
    public class RedboxService : IRedboxService
    {
        private readonly string _emailBaseUrl;

        private readonly ILogger<RedboxService> _logger;
        private readonly string _authorization;
        private readonly string _moduleId;
        private readonly string _redboxBaseUrl;

        public RedboxService(IConfiguration configuration, ILogger<RedboxService> logger)
        {
            _authorization = configuration["AppSettings:RedboxAuthorization"];
            _moduleId = configuration["AppSettings:RedboxModuleId"];
            _redboxBaseUrl = configuration["AppSettings:RedboxBaseEndPoint"];
            _emailBaseUrl = configuration["AppSettings:RedboxEmailBaseEndPoint"];

            _logger = logger;
        }

        public string GetAccountEmail(string accountNumber)
        {
            var responseString = RequestHelper.MakeRequestAndGetResponseGeneral(
                GetValidateAccountNumberAndPhoneRequest(accountNumber), _redboxBaseUrl, _authorization, _moduleId);

            _logger.LogInformation(responseString);

            var email = RequestHelper.GetFirstTagValue(responseString, "Email");

            return email;
        }

        public void SendEmail(EmailMessageRequestDto emailMessageRequestDto)
        {
            try
            {
                RequestHelper.MakeRequestAndGetResponseGeneral(
                   GetEmailRequest(emailMessageRequestDto),
                    _emailBaseUrl, _authorization, _moduleId, soapAction: "sendEmailMessage");
            }
            catch
            {
                _logger.LogError("Failed to send email - " + JsonConvert.SerializeObject(emailMessageRequestDto));
                throw;
            }
        }

        public void SendEmailNotification(string accountNumber, string mailBody)
        {
            var email = GetAccountEmail(accountNumber);

            _logger.LogInformation("Customer Email: " + email);

            if (!string.IsNullOrEmpty(email))
             {  
                  SendEmail(new EmailMessageRequestDto
                {
                    ToAddress = email,
                    MailBody = mailBody
                });
                }
        }

        public string GetValidateAccountNumberAndPhoneRequest(string accountNumber)
        {
            var reqTranId = Guid.NewGuid();
            var payload = $@"                                
                            <soapenv:Envelope xmlns:soap=""http://soap.request.manager.redbox.stanbic.com/"" xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"">
                                <soapenv:Header/>
                                <soapenv:Body>
                                    <soap:request>
                                        <channel>AGENT_NAME</channel>
                                        <type>CIF_ENQUIRY</type>
                                        <body>
                                            <![CDATA[<otherRequestDetails><cifId></cifId><accountNumber>{accountNumber}</accountNumber><moduleTranReferenceId>{reqTranId}</moduleTranReferenceId><cifType></cifType></otherRequestDetails>]]>
                                        </body>
                                        <submissionTime>{DateTime.Now:o}</submissionTime>
                                        <reqTranId>{reqTranId}</reqTranId>
                                    </soap:request>
                                </soapenv:Body>
                            </soapenv:Envelope>";

            return payload;
        }

        private static string GetEmailRequest(EmailMessageRequestDto emailMessageRequestDto)
        {
            return $@"
                   <soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:soap=""http://soap.messaging.outbound.redbox.stanbic.com/"">
                    <soapenv:Header/>
                    <soapenv:Body>
                        <soap:EMail>
                            <From>{emailMessageRequestDto.FromAddress}</From>
                            <To>{emailMessageRequestDto.ToAddress}</To>
                            <Cc>{emailMessageRequestDto.CcAddresss}</Cc>
                            <BCc/>
                            <Attachments/>
                            <Subject>{emailMessageRequestDto.Subject}</Subject>
                            <ContentType>{emailMessageRequestDto.ContentType}</ContentType>
                            <Body>
                                <![CDATA[{emailMessageRequestDto.MailBody}]]>
                            </Body>
                        </soap:EMail>
                    </soapenv:Body>
                </soapenv:Envelope>";
        }
    }
}
