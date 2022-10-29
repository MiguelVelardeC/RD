using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities;
using Artexacta.App.Documents;
using Artexacta.App.Documents.BLL;
using Artexacta.App.Utilities.Document;
using Artexacta.App.Documents.IFilter;
using log4net;
using Artexacta.App.Documents.FileUpload;
using Telerik.Web.UI;

public partial class UserControls_FileUpload : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

	public int MaxFileInputCount
	{
		get { return Convert.ToInt32(MaxFileCountHiddenField.Value); }
		set { MaxFileCountHiddenField.Value = value.ToString(); }
	}

	public int InitialFileInputCount
	{
		get { return Convert.ToInt32(InitialFileCountHiddenField.Value); }
		set { InitialFileCountHiddenField.Value = value.ToString(); }
	}

    protected void Page_Load(object sender, EventArgs e)
    {
		string language = "es-ES";
		RadUpload1.Culture = new System.Globalization.CultureInfo(language);
		RadProgressArea1.Culture = new System.Globalization.CultureInfo(language);

		if (!IsPostBack)
		{
            if (MaxFileInputCount > 0)
            {
                RadUpload1.MaxFileInputsCount = MaxFileInputCount;
            }
			RadUpload1.InitialFileInputsCount = InitialFileInputCount;
		}
    }

	protected void UploadButton_Click(object sender, EventArgs e)
    {
		List<FileLoaded> theFilesLoaded = new List<FileLoaded>();
		foreach (UploadedFile oInfo in RadUpload1.UploadedFiles)
		{
			try
			{
				if (string.IsNullOrEmpty(oInfo.FileName) || oInfo.ContentLength == 0)
					continue;
				// Create the base object.  This was created without the actual file content
				DocumentFile theFile = null;

				theFile = DocumentFile.CreateNewTypedDocumentFileObject(
						0, DateTime.Now, oInfo.ContentLength, oInfo.GetName(),
						 oInfo.GetExtension(),"");

				// Add the actual bytes read to the object
				theFile.Bytes = FileUtilities.ReadFully(oInfo.InputStream);

				// Now extract the text form the files read
				theFile.Text = theFile.GetTextFromDocumentBinary();

				// And finally save the file to the database
				string fileStoragePath = "";
				int fileID = DocumentFileBLL.CreateDocumentFile(theFile, ref fileStoragePath);
				if (fileID > 0)
					theFilesLoaded.Add(new FileLoaded(fileID, theFile.Name, theFile.Extension, fileStoragePath));
			}
			catch (Exception q)
			{
				log.Error("Could not load file for evaluation", q);
				//TODO:  Que hacemos si hay error?
			}
		}

		// Now throw an event to the caller, telling him that we loaded a bunch of files

		FilesLoadedArgs theEventArgs = new FilesLoadedArgs();
		theEventArgs.FilesLoaded = theFilesLoaded;
		OnFilesLoaded(theEventArgs);
    }

    public delegate void FilesLoadedHandler(object sender, FilesLoadedArgs e);

    public event FilesLoadedHandler FilesLoaded;

    public virtual void OnFilesLoaded(FilesLoadedArgs e)
    {
        if (FilesLoaded != null)
        {
            FilesLoaded(this, e);
        }
    }
}
