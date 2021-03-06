namespace Microsoft.Protocols.TestSuites.MS_OXCMAPIHTTP
{
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Microsoft.Protocols.TestSuites.Common;
    using Microsoft.Protocols.TestTools;

    /// <summary>
    /// The Implementation of the SUT Control Adapter interface.
    /// </summary>
    public class MS_OXCMAPIHTTPSUTControlAdapter : ManagedAdapterBase, IMS_OXCMAPIHTTPSUTControlAdapter
    {
        /// <summary>
        /// Initialize the adapter.
        /// </summary>
        /// <param name="testSite">Test site.</param>
        public override void Initialize(ITestSite testSite)
        {
            base.Initialize(testSite);
        }

        /// <summary>
        /// A method used to send an email to the specified user account.
        /// </summary>
        /// <returns>A Boolean value indicates whether send mail successfully.</returns>
        public bool SendMailItem()
        {
            string adminUserName = Common.GetConfigurationPropertyValue("AdminUserName", this.Site);
            string adminUserPassword = Common.GetConfigurationPropertyValue("AdminUserPassword", this.Site);
            string domainName = Common.GetConfigurationPropertyValue("Domain", this.Site);
            string ewsUrl = Common.GetConfigurationPropertyValue("EwsUrl", this.Site);

            StringBuilder soapRequestBuilder = new StringBuilder();
            soapRequestBuilder.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            soapRequestBuilder.AppendLine("<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:t=\"http://schemas.microsoft.com/exchange/services/2006/types\">");
            soapRequestBuilder.AppendLine("<soap:Body>");
            soapRequestBuilder.AppendLine("<CreateItem MessageDisposition=\"SendAndSaveCopy\" xmlns=\"http://schemas.microsoft.com/exchange/services/2006/messages\">");
            soapRequestBuilder.AppendLine("<Items><t:Message>");
            soapRequestBuilder.AppendLine("<t:ItemClass>IPM.Note</t:ItemClass>");
            soapRequestBuilder.AppendLine("<t:Subject>This is an interval event test mail.</t:Subject>");
            soapRequestBuilder.AppendLine("<t:Body BodyType=\"Text\">The body part is not important, these words are totally useless!</t:Body>");
            soapRequestBuilder.AppendLine("<t:ToRecipients><t:Mailbox>");
            soapRequestBuilder.AppendFormat("<t:EmailAddress>{0}</t:EmailAddress>", adminUserName + "@" + domainName);
            soapRequestBuilder.AppendLine("</t:Mailbox></t:ToRecipients><t:IsRead>false</t:IsRead></t:Message></Items>");
            soapRequestBuilder.AppendLine("</CreateItem></soap:Body></soap:Envelope>");

            byte[] requestBytes = System.Text.Encoding.UTF8.GetBytes(soapRequestBuilder.ToString());
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(ewsUrl);
            request.Method = "POST";
            request.ContentType = "text/xml;utf-8";
            request.Headers.Add("Translate", "F");
            request.Credentials = new NetworkCredential(adminUserName, adminUserPassword, domainName);
            request.ContentLength = requestBytes.Length;
            Stream webRequestStream = request.GetRequestStream();
            webRequestStream.Write(requestBytes, 0, requestBytes.Length);
            webRequestStream.Flush();
            webRequestStream.Dispose();

            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8);
            string responseXml = reader.ReadToEnd();
            reader.Close();
            webResponse.Close();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXml);
            this.Site.Assert.AreEqual<string>("NoError", doc.DocumentElement.InnerText, "Send a mail to specified user should successfully.");
            return true;
        }
    }
}