using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using log4net;
using Artexacta.App.Utilities.Document;
using System.IO;

namespace Artexacta.App.Documents
{

    /// <summary>
    /// An HTML Document File
    /// </summary>
    public class HTMLDocumentFile : DocumentFile
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public HTMLDocumentFile(int fileID, DateTime dateUploaded,
            long fileSize, string fileName, string fileExtension, string storagePath)
            : base(fileID, 
            dateUploaded, fileSize, fileName, fileExtension, storagePath)
        {
        }

        public override string[] ExtractCreationDateCandidatesFromFile()
        {
            return null;
        }

        public override string[] ExtractKeyWordCandidatesFromFile()
        {
            return null;
        }

        public override string[] ExtractAuthorCandidatesFromFile()
        {
            return null;
        }

        /// <summary>
        /// Get text from the binary using our custom HTML utilities
        /// </summary>
        /// <returns>The text of the HTML file or null if we could not process the text</returns>
        public override string GetTextFromDocumentBinary()
        {
            string text = null;

            // If we have no bytes then we can't do anything.
            if (Bytes == null || Bytes.Length == 0)
            {
                // Log the problem.
                log.Error("Tried to extract text from empty bytes for file " + Name);
                return null;
            }

            try
            {
                System.IO.StreamReader theReader = new StreamReader(new MemoryStream(Bytes));
                text = DocUtils.StripHTML(theReader.ReadToEnd());
            }
            catch (Exception e)
            {
                log.Error("Failed to get the text from the HTML file " + Name, e);
            }

            return text;
        }
    }
}