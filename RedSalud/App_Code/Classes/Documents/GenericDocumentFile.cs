using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Artexacta.App.Documents
{
    /// <summary>
    /// A generic file of which we know nothing about
    /// </summary>
    public class GenericDocumentFile : DocumentFile
    {
        public GenericDocumentFile(int fileID, DateTime dateUploaded,
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
    }
}