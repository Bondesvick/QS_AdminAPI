using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using QuickServiceAdmin.Core.Interface;

namespace QuickServiceAdmin.Core.Helpers
{
    [ExcludeFromCodeCoverage]
    public class JsonRequestHelper : IJsonRequestHelper
    {
        private readonly ILogger<JsonRequestHelper> _logger;

        public JsonRequestHelper(ILogger<JsonRequestHelper> logger)
        {
            _logger = logger;
        }

        public async Task<string> MakeJsonRequest(string method, string requestUri, HttpClient httpClient,
            StringContent request)
        {
            try
            {
                _logger.LogInformation("Request body {Body}", await request.ReadAsStringAsync());
                
                var requestMessage = new HttpRequestMessage
                {
                    Content = request,
                    Method = new HttpMethod(method),
                    RequestUri = new Uri(requestUri, UriKind.Relative)
                };
                
           
                //suppress unsigned certificate
                ServicePointManager.ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;     
                
                var response = await httpClient.SendAsync(requestMessage);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponseString = await response.Content.ReadAsStringAsync();
                    throw new Exception("Received invalid HTTP response status " + (int) response.StatusCode + " " +
                                        errorResponseString);
                }

                var responseString = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("JSON Request Response String: {ResponseString}", responseString);

                return responseString;
            }
            catch (WebException webException)
            {
                _logger.LogError("WebException: {Message}", webException.Message);
                var responseText = webException.Message;
                var webResp = (HttpWebResponse) webException.Response;

                if (webResp == null) throw new Exception(responseText);

                await using var dataStream = webResp.GetResponseStream();

                if (dataStream == null) throw new Exception(responseText);

                using var reader = new StreamReader(dataStream, Encoding.UTF8);
                responseText = webResp.StatusCode + " at ReQuery: " + await reader.ReadToEndAsync();
                throw new Exception(responseText);
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError("HttpRequestException: {Message}", exception.Message);
                throw new Exception(exception.Message);
            }
        }
    }
}