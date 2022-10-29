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
    /// A class describing an allowable file type
    /// </summary>
    public class FileType
    {
        private string fileExtension;
        private string fileDescription;
        private string fileIcon;
        private string fileProvider;
        private bool fileImage;

        /// <summary>
        /// Get or set the extension for the file type (for example .pdf)
        /// </summary>
        public string Extension
        {
            get { return fileExtension; }
            set { fileExtension = value; }
        }

        /// <summary>
        /// Get or set the description for the file type (for example, Movie)
        /// </summary>
        public string Description
        {
            get { return fileDescription; }
            set { fileDescription = value; }
        }

        /// <summary>
        /// Get or set the icon for the file type (for example, pdf.gif).  This image should be in the 
        /// neutral locale images folder
        /// </summary>
        public string Icon
        {
            get { return fileIcon; }
            set { fileIcon = value; }
        }

        /// <summary>
        /// Get or set the name of the text extraction provider for this file type.  
        /// </summary>
        public string Provider
        {
            get { return fileProvider; }
            set { fileProvider = value; }
        }

        /// <summary>
        /// Get or set if the file is an image file
        /// </summary>
        public bool Image
        {
            get { return fileImage; }
            set { fileImage = value; }
        }

        public FileType(string extension, string description, string icon, string provider, bool image)
        {
            this.fileExtension = extension;
            this.fileDescription = description;
            this.fileIcon = icon;
            this.fileProvider = provider;
            this.fileImage = image;
        }
    }
}