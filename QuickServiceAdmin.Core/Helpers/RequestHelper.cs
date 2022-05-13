using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace QuickServiceAdmin.Core.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class RequestHelper
    {
        public static string MakeRequestAndGetResponseGeneral(string dataString, string url, string authorization,
            string moduleId, string method = "POST", string contentType = "text/xml", string soapAction = "treat")
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.ContentType = contentType; // "application/json";
                request.Method = method;
                if (!string.IsNullOrEmpty(soapAction)) request.Headers.Add("SOAPAction", soapAction);

                if (!string.IsNullOrEmpty(authorization)) request.Headers.Add("Authorization", authorization);
                if (!string.IsNullOrEmpty(moduleId)) request.Headers.Add("Module_Id", moduleId);

                if (url.Contains("https://"))
                    //suppress unsigned certificate
                    ServicePointManager.ServerCertificateValidationCallback = IgnoreCertificateErrorHandler;

                string serverResponse;

                if (method == "GET")
                {
                    using var response = (HttpWebResponse)request.GetResponse();
                    using var dataStream = response.GetResponseStream();
                    if (dataStream == null)
                    {
                        serverResponse = "";
                    }
                    else
                    {
                        using var streamReader = new StreamReader(dataStream);
                        var responseString = streamReader.ReadToEnd();
                        serverResponse = responseString;
                    }
                }
                else
                {
                    using var writer = new StreamWriter(request.GetRequestStream());
                    writer.WriteLine(dataString);
                    writer.Close();
                    // Send the data to the webserver
                    using var response = (HttpWebResponse)request.GetResponse();
                    using var dataStream = response.GetResponseStream();
                    if (dataStream == null)
                    {
                        serverResponse = "";
                    }
                    else
                    {
                        using var streamReader = new StreamReader(dataStream);
                        var responseString = streamReader.ReadToEnd();
                        serverResponse = responseString;
                    }
                }

                return serverResponse.Replace("&lt;", "<").Replace("&gt;", ">")
                        .Replace(@"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes""?>", "")
                        .Replace(@"<?xml version=""1.0""?>", "")
                        .Replace("ns2:", "")
                        .Replace(":ns2", "")
                        .Replace(@" xmlns=""http://soap.request.manager.redbox.stanbic.com/""", "")
                        .Replace(@" xmlns=""http://soap.messaging.outbound.redbox.stanbic.com/""", "")
                        .Replace(@" xmlns=""http://soap.finacle.redbox.stanbic.com/""", "")
                        .Replace("soap:", "")
                        .Replace(@" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/""", "")
                    ;
            }
            catch (WebException webEx)
            {
                var responseText = webEx.Message;

                var webResp = (HttpWebResponse)webEx.Response;

                if (webResp == null) throw new Exception(responseText);

                using var dataStream = webResp.GetResponseStream();

                if (dataStream == null) throw new Exception(responseText);

                using var reader = new StreamReader(dataStream, Encoding.ASCII);

                responseText = webResp.StatusCode + " at Query: " + reader.ReadToEnd();

                throw new Exception(responseText);
            }
        }


        private static bool IgnoreCertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public static string GetFirstTagValue(string xmlObject, string element, string namespacePrefix = "",
            bool retainTag = false)
        {
            try
            {
                xmlObject = xmlObject.Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("\t", "")
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">");
                var openingTag = string.IsNullOrEmpty(namespacePrefix)
                    ? $"<{element}>"
                    : $"<{namespacePrefix}:{element}>";
                var closingTag = string.IsNullOrEmpty(namespacePrefix)
                    ? $"</{element}>"
                    : $"</{namespacePrefix}:{element}>";
                var tagContent = string.Empty;
                if (xmlObject.Contains(openingTag))
                {
                    var firstIndexOfOTag = xmlObject.IndexOf(openingTag, StringComparison.Ordinal);
                    var indexOfClosingTag = xmlObject.IndexOf(closingTag, StringComparison.Ordinal);
                    tagContent = xmlObject.Substring(firstIndexOfOTag,
                        (indexOfClosingTag - firstIndexOfOTag) + closingTag.Length);
                }

                if (string.IsNullOrEmpty(tagContent)) return null;
                if (retainTag) return tagContent;
                var value = tagContent.Replace(openingTag, "").Replace(closingTag, "").Trim();
                return string.IsNullOrEmpty(value) ? null : value;
            }
            catch
            {
                return null;
            }
        }
    }
}
