using Artexacta.App.Medico.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_FotoMedico : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public int FotoId
    {
        get
        {
            int fotoId = 0;
            try
            {
                fotoId = Convert.ToInt32(FotoIDHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return fotoId;
        }
        set
        {
            if (value <= 0)
            {
                FotoIDHiddenField.Value = "0";
                FotoMedUrl.ImageUrl = "~/Images/Neutral/paciente.jpg";
            }
            else
            {
                FotoIDHiddenField.Value = value.ToString();
                FotoMedUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";
            }
        }
    }
    public int MedicoId
    {
        get
        {
            int medicoId = 0;
            try
            {
                medicoId = Convert.ToInt32(MedicoIdHF.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return medicoId;
        }
        set
        {
            if (value < 0)
            {
                MedicoIdHF.Value = "0";
            }
            else
            {
                MedicoIdHF.Value = value.ToString();
            }
        }
    }

    public bool Editable
    {
        set
        {
            EditPhotoLB.Visible = value;
        }
    }

    public Unit Width
    {
        set
        {
            FotoMedUrl.Width = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FotoMedFileUpload.FilesLoaded += FotoMedFileUpload_FilesLoaded;
        FotoMedFileUpload.MaxFileInputCount = 1;
    }
    protected void EditPhotoLB_Click(object sender, EventArgs e)
    {
        FileUploadDiv.Visible = true;
        //FotoPAUrl.Visible = false;
        EditPhotoLB.Visible = false;
    }
    protected void FotoMedFileUpload_FilesLoaded(object sender, Artexacta.App.Documents.FileUpload.FilesLoadedArgs e)
    {
        if (e.FilesLoaded != null && e.FilesLoaded.Count > 0)
        {
            FotoId = e.FilesLoaded[0].ID;
            FotoMedUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";

            int MedicoId = Convert.ToInt32(MedicoIdHF.Value);
            if (MedicoId > 0)
            {
                MedicoBLL.UpdateFotoId(MedicoId, FotoId);
            }
        }
        FotoMedFileUpload.Visible = false;
        //FotoPAUrl.Visible = true;
        EditPhotoLB.Visible = true;
    }
    protected void CancelLinkButton_Click(object sender, EventArgs e)
    {
        FileUploadDiv.Visible = false;
        //FotoPAUrl.Visible = true;
        EditPhotoLB.Visible = true;
    }
}