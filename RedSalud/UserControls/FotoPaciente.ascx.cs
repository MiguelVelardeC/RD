using Artexacta.App.Paciente.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_FotoPaciente : System.Web.UI.UserControl
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
                FotoPAUrl.ImageUrl = "~/Images/Neutral/paciente.jpg";
            }
            else
            {
                FotoIDHiddenField.Value = value.ToString();
                FotoPAUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";
            }
        }
    }
    public int PacienteId
    {
        get
        {
            int pacienteId = 0;
            try
            {
                pacienteId = Convert.ToInt32(PacienteIdHF.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return pacienteId;
        }
        set
        {
            if (value < 0)
            {
                PacienteIdHF.Value = "0";
            }
            else
            {
                PacienteIdHF.Value = value.ToString();
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
            FotoPAUrl.Width = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        FotoPAFileUpload.FilesLoaded += FotoPAFileUpload_FilesLoaded;
        FotoPAFileUpload.MaxFileInputCount = 1;
    }
    protected void EditPhotoLB_Click(object sender, EventArgs e)
    {
        FileUploadDiv.Visible = true;
        //FotoPAUrl.Visible = false;
        EditPhotoLB.Visible = false;
    }
    protected void FotoPAFileUpload_FilesLoaded(object sender, Artexacta.App.Documents.FileUpload.FilesLoadedArgs e)
    {
        if (e.FilesLoaded != null && e.FilesLoaded.Count > 0)
        {
            FotoId = e.FilesLoaded[0].ID;
            FotoPAUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";

            int PacienteId = Convert.ToInt32(PacienteIdHF.Value);
            if (PacienteId > 0)
            {
                PacienteBLL.UpdateFotoId(PacienteId, FotoId);
            }
        }
        FotoPAFileUpload.Visible = false;
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