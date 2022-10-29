using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Accidentado.BLL;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Documents;
using Artexacta.App.Documents.BLL;
using Artexacta.App.Documents.FileUpload;
using Artexacta.App.Emergencia.BLL;
using Artexacta.App.Estudio.BLL;
using Artexacta.App.Internacion.BLL;
using Artexacta.App.Medicamento.BLL;
using Artexacta.App.Odontologia.BLL;
using Artexacta.App.Receta.BLL;
using Artexacta.App.Siniestro.BLL;
using Artexacta.App.Utilities.Document;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Utilities.Email;

public partial class UserControls_FileManager : System.Web.UI.UserControl
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public enum ShowModeValue
    {
        Popup,
        Normal
    }

    public ShowModeValue ShowMode
    {
        set 
        { 
            ShowingModeHiddenField.Value = value.ToString();
            if (value == ShowModeValue.Normal)
            {
                NewFileHL.Visible = false;
                CloseAttacheFileLB.Visible = false;
                IsVisibleCheckBox.Visible = false;
                IsVisible = true;
                CloseFileListHL.CssClass = "";
                AttacheFilePanel.CssClass = "";
                FilesListPanel.CssClass = "";
                FilesListPanel.Visible = true;
                IsVisible = true;
                return;
            }

            if (value == ShowModeValue.Popup)
            {
                NewFileHL.Visible = true;
                CloseAttacheFileLB.Visible = true;

                CloseFileListHL.CssClass = "Default_Popup";
                AttacheFilePanel.CssClass = "Default_Popup";
            }
        }
        get { return ShowingModeHiddenField.Value == "Normal" ? ShowModeValue.Normal : ShowModeValue.Popup; }
    }

    public int ProveedorMedicoId
    {
        get
        {
            int proveedorMedicoId = 0;
            try
            {
                proveedorMedicoId = Convert.ToInt32(ProveedorMedicoIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'ProveedorMedicoIdHiddenField.Value' to int value", ex);
            }
            return proveedorMedicoId;
        }
        set
        {
            ProveedorMedicoIdHiddenField.Value = value.ToString();
        }
    }

    public int SiniestroId
    {
        get
        {
            int SiniestroId = 0;
            try
            {
                SiniestroId = Convert.ToInt32(ProveedorMedicoIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'ProveedorMedicoIdHiddenField.Value' to int value", ex);
            }
            return SiniestroId;
        }
        set
        {
            SiniestroIdHiddenField.Value = value.ToString();
        }
    }

    public int EstudioId
    {
        get
        {
            int EstudioId = 0;
            try
            {
                
                EstudioId = Convert.ToInt32(EstudioIdCitaLaboHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'EstudioIdCitaLaboHiddenField.Value' to int value", ex);
            }
            return EstudioId;
        }
        set
        {
            EstudioIdCitaLaboHiddenField.Value = value.ToString();
        }
    }


    public bool CanDeleteFiles
    {
        get
        {
            return Convert.ToBoolean(CanDeleteFilesHF.Value);
        }
        set
        {
            CanDeleteFilesHF.Value = value.ToString();

            FileUpload.Visible = value;
        }
    }

    public bool CanOnlyDeleteFiles
    {
        get
        {
            return Convert.ToBoolean(CanDeleteFilesHF.Value);
        }
        set
        {
            CanDeleteFilesHF.Value = value.ToString();

            //FileUpload.Visible = value;
        }
    }

    public int ObjectId
    {
        get { try {return Convert.ToInt32(ObjectIdHF.Value); } catch{return 0;}}
        set { ObjectIdHF.Value = value.ToString(); }
    }
    public string ObjectName
    {
        get {return ObjectNameHF.Value; }
        set { ObjectNameHF.Value = value; }
    }
    private bool IsVisible
    {
        get { return IsVisibleHF.Value == "1"; }
        set { IsVisibleHF.Value = value ? "1" : ""; }
    }
    public int UploadedFilesNumber
    {
        get { try { return Convert.ToInt32(UploadedFilesNumberHF.Value); } catch { return 0; } }
        set { UploadedFilesNumberHF.Value = value.ToString(); }
    }
    public int TotalEstudiosRealizadosNumber
    {
        get { try { return Convert.ToInt32(TotalEstudiosRealizadosNumberHF.Value); } catch { return 0; } }
        set { TotalEstudiosRealizadosNumberHF.Value = value.ToString(); }
    }
    public delegate void OnListFileChangeDelegate ( string ObjectName, string type );

    public event OnListFileChangeDelegate OnListFileChange;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        if (ShowMode == ShowModeValue.Normal)
            FileRadGrid_DataBind();

    }

    public void OpenFileManager(string objectName, int objectId)
    {
        OpenFileManager(objectName, objectId, false);
    }


    public void OpenFileManager ( string objectName, int objectId, bool isVisible )
    {
        ObjectName = objectName;
        ObjectId = objectId;
        IsVisible = isVisible;
        IsVisibleCheckBox.Visible = IsVisible;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "FILEMANAGER" + this.ClientID, "$(document).ready(function(){openFileManager();});", true);
        FileRadGrid_DataBind();
    }
    protected void FileButton_Command ( object sender, CommandEventArgs e )
    {
        
    }
    protected void FileRadGrid_DataBind ()
    {
        List<DocumentFile> list = new List<DocumentFile>();
        try
        {
            switch (ObjectName.ToUpper())
            {
                case "ODONTOLOGIA":
                    list = OdontologiaBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "RECETAS":
                    list = RecetaBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "ESTUDIO":
                    list = Artexacta.App.Estudio.BLL.EstudioBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "DERIVACIONES":
                    list = DerivacionBLL.getDerivacionFileList(ObjectId, IsVisible);
                    break;
                case "INTERNACION":
                    list = InternacionBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "EMERGENCIA":
                    list = EmergenciaBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "SINIESTRO":
                    list = SiniestroBLL.getFileList(ObjectId);
                    break;
                case "ACCIDENTADO":
                    list = AccidentadoBLL.getFileList(ObjectId);
                    break;
                case "MEDICAMENTO":
                    list = MedicamentoBLL.getFileList(ObjectId, IsVisible);
                    break;
                case "CITADESGRAVAMEN":
                    list = LaboratorioFileBLL.GetLaboratorioFiles(ObjectId, ProveedorMedicoId, EstudioId);
                    break;
                case "SINIESTROFILES":
                    list = SiniestroBLL.GetSiniestroFiles(SiniestroId);
                    break;
            }
        }
        catch (Exception ex)
        {
            log.Error("Cannot get File for object " + ObjectName + " with id " + ObjectId, ex);
            SystemMessages.DisplaySystemErrorMessage("No se pudo obtener la lista de archivos");
        }
        UploadedFilesNumber = list.Count;
        FileRadGrid.DataSource = list;
        FileRadGrid.DataBind();
    }
    protected void FileUpload_FilesLoaded ( object sender, FilesLoadedArgs e )
    {
        foreach (FileLoaded doc in e.FilesLoaded)
        {
            try
            {
                switch (ObjectName.ToUpper())
                {
                    case "ODONTOLOGIA":
                        OdontologiaBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "RECETAS":
                        RecetaBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "ESTUDIO":
                        Artexacta.App.Estudio.BLL.EstudioBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "DERIVACIONES":
                        DerivacionBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "INTERNACION":
                        InternacionBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "EMERGENCIA":
                        EmergenciaBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "SINIESTRO":
                        SiniestroBLL.InsertFile(ObjectId, doc.ID);
                        break;
                    case "ACCIDENTADO":
                        AccidentadoBLL.InsertFile(ObjectId, doc.ID);
                        break;
                    case "MEDICAMENTO":
                        MedicamentoBLL.InsertFile(ObjectId, doc.ID, IsVisible ? IsVisibleCheckBox.Checked : true);
                        break;
                    case "CITADESGRAVAMEN":
                        //int TotalFilesNumber = UploadedFilesNumber + e.FilesLoaded.Count;
                        //if (TotalEstudiosRealizadosNumber >= TotalFilesNumber)
                        if (TotalEstudiosRealizadosNumber >= 1)
                        {
                            LaboratorioFileBLL.InsertLaboratorioFile(ObjectId, doc.ID, ProveedorMedicoId, EstudioId);

                            string sawst = "SA Western Standard Time";
                            TimeZoneInfo sawstZone = TimeZoneInfo.FindSystemTimeZoneById(sawst);
                            DateTime timeInBolivia = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, sawstZone);
                            CitaDesgravamenBLL.UpdateCitaLabo(timeInBolivia, ObjectId, ProveedorMedicoId);
                            EmailUtilities.CheckFirstAndSendEmailLaboCitaConcluidos(ObjectId);
                        }
                        else
                        {
                            //"You must mark the Estudio as Realizado before upload the files."
                            throw new ArgumentException("Debe marcar un Estudio como Realizado antes de subir archivos.");
                        }

                        break;
                    case "SINIESTROFILES":
                        SiniestroBLL.InsertFile(ObjectId, doc.ID);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Cannot insert relationship between FileId " + doc.ID + " and object " + ObjectName + " with id " + ObjectId, ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo cargar el archivo seleccionado: " + ex.Message);
            }
            
        }
        if(ShowMode == ShowModeValue.Popup)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "FILEMANAGER" + this.ClientID, "$(document).ready(function(){openFileManager();});", true);
        FileRadGrid_DataBind();
        if (OnListFileChange != null)
            OnListFileChange(ObjectName.ToUpper(), "NewFile");
    }

    protected void FileRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "DELETE")
        {
            int fileId = 0;
            try
            {
                fileId = Convert.ToInt32(e.CommandArgument);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'e.CommandArgument' to integer value", ex);
            }
            if (fileId <= 0)
            {
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al eliminar el archivo seleccionado");
                return;
            }

            string path = "";
            try
            {
                DocumentFile objFile = DocumentFileBLL.GetDocumentFile(fileId);
                path = objFile.FileStoragePath;
            }
            catch (Exception ex)
            {
                log.Error("An error occurred trying to get File with id: " + fileId, ex);
            }

            try
            {
                switch (ObjectName.ToUpper())
                {
                    case "ODONTOLOGIA":
                        OdontologiaBLL.DeleteFile(fileId);
                        break;
                    case "RECETAS":
                        RecetaBLL.DeleteFile(fileId);
                        break;
                    case "ESTUDIO":
                        Artexacta.App.Estudio.BLL.EstudioBLL.DeleteFile(fileId);
                        break;
                    case "DERIVACIONES":
                        DerivacionBLL.DeleteFile(fileId);
                        break;
                    case "INTERNACION":
                        InternacionBLL.DeleteFile(fileId);
                        break;
                    case "EMERGENCIA":
                        EmergenciaBLL.DeleteFile(fileId);
                        break;
                    case "SINIESTRO":
                        SiniestroBLL.DeleteFile(fileId);
                        break;
                    case "ACCIDENTADO":
                        AccidentadoBLL.DeleteFile(fileId);
                        break;
                    case "MEDICAMENTO":
                        MedicamentoBLL.DeleteFile(fileId);
                        break;
                    case "CITADESGRAVAMEN":
                        LaboratorioFileBLL.DeleteLaboratorioFile(ObjectId, fileId);
                        break;
                    case "SINIESTROFILES":
                        SiniestroBLL.DeleteFile(fileId);
                        break;
                }
            }
            catch (Exception ex)
            {
                log.Error("Cannot delete file with id : " + fileId, ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al eliminar el archivo seleccionado");
                return;
            }

            if (!string.IsNullOrEmpty(path))
            {
                try
                {
                    FileInfo objFileInfo = new FileInfo(path);
                    if (objFileInfo.Exists)
                        objFileInfo.Delete();
                }
                catch (Exception ex)
                {
                    log.Error("An error ocurred when trying to delete file pysically. Remove it manually", ex);
                }
            }
            SystemMessages.DisplaySystemMessage("El archivo seleccionado se eliminó correctamente");
            FileRadGrid_DataBind();
            if (ShowMode == ShowModeValue.Popup)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FILEMANAGER" + this.ClientID, "$(document).ready(function(){openFileManager();});", true);
            if (OnListFileChange != null)
                OnListFileChange(ObjectName.ToUpper(), "DeletedFile");
        }
        else
        {

            //int fileId = 0;
            //try
            //{
            //    fileId = Convert.ToInt32(e.CommandArgument);
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Cannot convert 'e.CommandArgument' to integer value", ex);
            //}
            //if (fileId <= 0)
            //{
            //    SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al descargar el archivo seleccionado");
            //    return;
            //}

            //DocumentFile objFile = null;
            //try
            //{
            //    objFile = DocumentFileBLL.GetDocumentFile(fileId);
            //}
            //catch (Exception ex)
            //{
            //    log.Error("An error occurred trying to get File with id: " + fileId, ex);
            //}
            //if (objFile == null)
            //{
            //    SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al descargar el archivo seleccionado");
            //    return;
            //}
            FileInfo f = new FileInfo(e.CommandName);
            Response.Clear();
            string mimeType = FileUtilities.GetFileMIMEType(f.Extension);
            if (mimeType != null)
            {
                Response.ContentType = mimeType;
            }
            string path = e.CommandArgument.ToString();
            Response.AddHeader("Content-Disposition", "attachment;Filename=\"" + HttpUtility.UrlPathEncode(f.Name) + "\"");
            Response.AddHeader("Content-Length", new FileInfo(path).Length.ToString());
            Response.WriteFile(path);
            Response.Flush();
            Response.End();
        }
    }
    protected void FileRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            ImageButton DeleteImageButton = (ImageButton)e.Item.FindControl("DeleteImageButton");
            if (DeleteImageButton != null)
            {
                DeleteImageButton.Visible = CanDeleteFiles;
            }
        }
    }
}