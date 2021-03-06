namespace Microsoft.Protocols.TestSuites.MS_OXWSCORE
{
    using System.Xml.XPath;
    using Microsoft.Protocols.TestSuites.Common;
    using Microsoft.Protocols.TestTools;

    /// <summary>
    /// The adapter interface which provides methods FindFolder and FindItem defined in MS-OXWSSRCH.
    /// </summary>
    public interface IMS_OXWSSRCHAdapter : IAdapter
    {
        /// <summary>
        /// Gets the raw XML request sent to protocol SUT
        /// </summary>
        IXPathNavigable LastRawRequestXml { get; }

        /// <summary>
        /// Gets the raw XML response received from protocol SUT
        /// </summary>
        IXPathNavigable LastRawResponseXml { get; }

        /// <summary>
        /// Search specific folders.
        /// </summary>
        /// <param name="findRequest">Specify a request for a FindFolder operation</param>
        /// <returns>A response to FindFolder operation request</returns>
        FindFolderResponseType FindFolder(FindFolderType findRequest);
        
        /// <summary>
        /// Search specified items.
        /// </summary>
        /// <param name="findRequest">Specify a request for a FindItem operation.</param>
        /// <returns>A response to FindItem operation request.</returns>
        FindItemResponseType FindItem(FindItemType findRequest);

        /// <summary>
        /// Switch the current user to a new one, with the identity of the new user to communicate with Exchange Web Service.
        /// </summary>
        /// <param name="userName">The name of a user.</param>
        /// <param name="userPassword">The password of a user.</param>
        /// <param name="userDomain">The domain which the user belongs to.</param>
        void SwitchUser(string userName, string userPassword, string userDomain);
    }
}