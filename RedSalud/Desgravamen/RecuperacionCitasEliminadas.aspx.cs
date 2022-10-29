using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Desgravamen;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Desgravamen.BLL;
using System.IO;
using Artexacta.App.Utilities.Document;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.Documents;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;

public partial class Desgravamen_RecuperacionCitasEliminadas : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static Bitacora theBitacora = new Bitacora();

    public bool UserAuthorizedAprobar
    {
        get
        {
            return Convert.ToBoolean(UserAuthorizedAprobarHF.Value);
        }
        set
        {
            UserAuthorizedAprobarHF.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPA.Config = new PropuestoAseguradoSearchConfig();
        SearchPA.OnSearch += SearchPA_OnSearch;

        if (!IsPostBack)
        {
            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_APROBAR");
                UserAuthorizedAprobar = true;
            }
            catch (Exception q)
            {
                log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para aprobar usuario ", q);
                UserAuthorizedAprobar = false;
            }

            int userId = 0;
            try
            {
                userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
                if (userId == 0)
                {
                    userId = -1;
                    throw new Exception("No se pudo encontrar el id del usuario");
                }
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_VER_TODOSCASOS");
                //userId = 0;
            }
            catch (Exception q)
            {
                if (userId < 0) {
                    SystemMessages.DisplaySystemErrorMessage("No se pudo obtener el id del usuario " + User.Identity.Name);    
                }
                // si es un usuario valido se queda con el id de usuario encontrado
            }

            if (userId < 0)
            {
                Response.Redirect("~/MainPage.aspx");
                return;
            }
            UserIdHiddenField.Value = userId.ToString();
            loadClienteIdByUser(userId);
            LoadClientesToCombo();
        }
    }
    private void loadClienteIdByUser(int userId)
    {
        int clienteId = 0;
        try
        {
            clienteId = ClienteUsuarioBLL.GetClienteByUsuarioId(userId);
            if (clienteId != 0)
            {
                /*if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_EXAMEN_MEDICO") ||
                    Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_VER_TODOSCASOS"))                  
                    ClienteIdHiddenField.Value = 0;*/

                ClienteIdHiddenField.Value = clienteId.ToString();
                //changeTitleScreen();
                //throw new Exception("No se pudo encontrar el id del usuario");
            }
            else
            {
                ClienteIdHiddenField.Value = clienteId.ToString();
            }
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemWarningMessage("El Usuario [" + User.Identity.Name + "] no tiene un cliente asignado");
            // si es un usuario valido se queda con el id de usuario encontrado
        }
    }
    void SearchPA_OnSearch()
    {
        
    }

    protected void Pager_PageChanged(int row)
    {
        ;
    }

    protected void PropuestoAseguradoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Ocurrio un error al tratar de obtener la lista de Activos", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener la lista de Propuestos Asegurados");
            e.ExceptionHandled = true;
        }
        int totalRows = 0;
        try
        {
            totalRows = Convert.ToInt32(e.OutputParameters["totalRows"]);
        }
        catch (Exception ex)
        {
            log.Error("Failed to get OuputParameter 'totalRows'", ex);
        }
        Pager.TotalRows = totalRows;
        if (totalRows == 0)
        {
            Pager.Visible = false;
            return;
        }
        Pager.Visible = true;
        Pager.BuildPagination();
    }

    protected void PropuestoAseguradoGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerPA")
        {
            int citaDesgravamenID = Convert.ToInt32(e.CommandArgument);
            CitaDesgravamenBLL.UndeleteCitaDesgravamen(citaDesgravamenID);
            SystemMessages.DisplaySystemMessage("La cita " + citaDesgravamenID.ToString() + " fue recuperada junto con todos los estudios y archivos");
            theBitacora.RecordTrace(Bitacora.TraceType.DESGRecuperarCita, User.Identity.Name, "Desgravamen", citaDesgravamenID.ToString(), "Se recupero la cita " + citaDesgravamenID.ToString());
            PropuestoAseguradoGridView.DataBind();
            return;
        }

        if (e.CommandName == "Examen")
        {
            Session["CITADESGRAVAMENID"] = Convert.ToInt32(e.CommandArgument);
            Session["PAGINABACK"] = "PropuestoAseguradoLista.aspx";
            Response.Redirect("~/Desgravamen/ExamenMedico.aspx");
            return;
        }

        if (e.CommandName == "Completo")
        {
            GetFullReportPdf(Convert.ToInt32(e.CommandArgument));
            return;
        }

        if (e.CommandName == "Orden")
        {
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);
            Session["CITADESGRAVAMENID"] = citaDesgravamenId;
            Session["PAGINABACK"] = "PropuestoAseguradoLista.aspx";
            Response.Redirect("OrdenDeServicioImprimir.aspx");
            return;
        }

    }

    private void GetFullReportPdf_EVO(int citaId)
    {
        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        byte[] pdfBytesExamenMedico = null;
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
        }

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        EnlaceDesgravamenSISA objEnlace = null;
        try
        {
            objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(citaId, ref casoId);
            if (casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
                pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }
        List<string> pdfBytesImg = new List<string>();

        List<ProgramacionCitaLabo> listaLabos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaId);
        foreach (ProgramacionCitaLabo progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }

        byte[] fullOutput = null;

        if (pdfBytesLabos.Count == 1)
        {
            fullOutput = pdfBytesLabos[0];
        }
        else
        {
            EvoPdf.PdfMerge.PdfDocumentOptions pdfDocumentOptions = new EvoPdf.PdfMerge.PdfDocumentOptions();
            pdfDocumentOptions.PdfCompressionLevel = EvoPdf.PdfMerge.PDFCompressionLevel.Normal;
            pdfDocumentOptions.PdfPageSize = EvoPdf.PdfMerge.PdfPageSize.Letter;
            pdfDocumentOptions.PdfPageOrientation = EvoPdf.PdfMerge.PDFPageOrientation.Portrait;
            EvoPdf.PdfMerge.PDFMerge objMerger = new EvoPdf.PdfMerge.PDFMerge(pdfDocumentOptions);

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                objMerger.AppendPDFStream(inputStream, 0);
                inputStream.Close();
            }
            foreach (string imgPath in pdfBytesImg)
            {
                objMerger.AppendImageFile(imgPath);
            }

            fullOutput = objMerger.RenderMergedPDFDocument();
        }

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + citaId + ".pdf; size={0}", fullOutput.Length.ToString()));
        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();
    }

    private void GetFullReportPdf_HIQ(int citaId)
    {
        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        byte[] pdfBytesExamenMedico = null;
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
        }

        List<byte[]> pdfBytesLabos = new List<byte[]>();
        if (pdfBytesExamenMedico != null)
            pdfBytesLabos.Add(pdfBytesExamenMedico);

        // Caso Medico (la ficha de SISA)
        int casoId = 0;
        EnlaceDesgravamenSISA objEnlace = null;
        try
        {
            objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(citaId, ref casoId);
            if (objEnlace != null && casoId > 0)
            {
                byte[] pdfHistorialSISA = null;
                pdfHistorialSISA = Artexacta.App.Caso.Caso.GetHistorialEnPdf(objEnlace, urlServer);
                if (pdfHistorialSISA != null)
                    pdfBytesLabos.Add(pdfHistorialSISA);
            }
        }
        catch (Exception q)
        {
            log.Warn("No pudo obtener el caso de sisa", q);
        }

        // PDF delos adjuntos
        List<string> pdfBytesImg = new List<string>();
        List<ProgramacionCitaLabo> listaLabos = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(citaId);
        foreach (ProgramacionCitaLabo progLabo in listaLabos)
        {
            foreach (DocumentFile objDoc in progLabo.LaboratorioFiles)
            {
                if (objDoc.Extension.ToLower() == ".pdf")
                {
                    pdfBytesLabos.Add(objDoc.Bytes);
                }
                else
                {
                    pdfBytesImg.Add(objDoc.FileStoragePath);
                }
            }
        }

        // MERGE de todo

        byte[] fullOutput = null;

        if (pdfBytesLabos.Count == 1)
        {
            fullOutput = pdfBytesLabos[0];

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + citaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();
            return;
        }
        else
        {
            HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
            completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

            foreach (byte[] pdfBytes in pdfBytesLabos)
            {
                MemoryStream inputStream = new MemoryStream(pdfBytes);
                HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
                completeDoc.AddDocument(documentToAddMerge);
                
                //documentToAddMerge.Close();
                //inputStream.Close();
            }
            foreach (string imgPath in pdfBytesImg)
            {
                HiQPdf.PdfDocument documentToAddMerge = new HiQPdf.PdfDocument();
                // set a demo serial number
                documentToAddMerge.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // create a page in document
                HiQPdf.PdfPage page1 = documentToAddMerge.AddPage(HiQPdf.PdfPageSize.Letter, new HiQPdf.PdfDocumentMargins(25, 25, 10, 10), HiQPdf.PdfPageOrientation.Portrait);
                // set a background color for the page
                HiQPdf.PdfRectangle backgroundRectangle = new HiQPdf.PdfRectangle(page1.DrawableRectangle);
                backgroundRectangle.BackColor = System.Drawing.Color.White;
                page1.Layout(backgroundRectangle);

                // create the true type fonts that can be used in document text
                System.Drawing.Font sysFont = new System.Drawing.Font("Arial", 10, System.Drawing.GraphicsUnit.Point);
                HiQPdf.PdfFont pdfFont = documentToAddMerge.CreateFont(sysFont);
                HiQPdf.PdfFont pdfFontEmbed = documentToAddMerge.CreateFont(sysFont, true);

                float crtYPos = 20;
                float crtXPos = 5;

                // layout an opaque JPG image
                HiQPdf.PdfImage opaquePdfImage = new HiQPdf.PdfImage(crtXPos, crtYPos, imgPath);
                HiQPdf.PdfLayoutInfo imageLayoutInfo = page1.Layout(opaquePdfImage);

                completeDoc.AddDocument(documentToAddMerge);
                //documentToAddMerge.Close();
            }

            fullOutput = completeDoc.WriteToMemory();

            // send the PDF document as a response to the browser for download
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.AddHeader("Content-Type", "application/pdf");

            response.AddHeader("Content-Disposition", String.Format("attachment; filename=InformeCaso" + citaId + ".pdf; size={0}", fullOutput.Length.ToString()));
            response.BinaryWrite(fullOutput);
            // Note: it is important to end the response, otherwise the ASP.NET
            // web page will render its content to PDF document stream
            response.End();

            completeDoc.Close();

        }
    }

    private void GetFullReportPdf(int citaId)
    {
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
        {
            GetFullReportPdf_EVO(citaId);
            return;
        }
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
        {
            GetFullReportPdf_HIQ(citaId);
            return;
        }
    }
    protected void PropuestoAseguradoGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            PropuestoAseguradoSearchResult objItem = (PropuestoAseguradoSearchResult)e.Item.DataItem;

            Image NoNecesitaExamen = (Image)e.Item.FindControl("NoNecesitaExamen");
            ImageButton ImprimirOrden = (ImageButton)e.Item.FindControl("ImprimirOrden");
            ImageButton FullReportePdfButton = (ImageButton)e.Item.FindControl("FullReportePdfButton");

            Repeater LaboratoriosMainRepeater = (Repeater)e.Item.FindControl("LaboratoriosMainRepeater");
            LaboratoriosMainRepeater.DataSource = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(objItem.CitaDesgravamenId);
            LaboratoriosMainRepeater.DataBind();

            if (!objItem.NecesitaExamenMedico)
            {
                NoNecesitaExamen.Visible = true;
            }


            string resultadoOrden = "";
            try
            {
                resultadoOrden = DesgravamenManager.CheckOrdenDeServicioParaImprimir(objItem.CitaDesgravamenId, objItem.NecesitaExamenMedico);
                if (string.IsNullOrWhiteSpace(resultadoOrden))
                    ImprimirOrden.Visible = true;
                
            }
            catch
            {
                ImprimirOrden.Visible = false;
            }

            if ((
                    (objItem.NecesitaExamenMedico && objItem.TieneExamenMedico > 0 && objItem.FechaHoraCita > DateTime.MinValue) ||
                    (!objItem.NecesitaExamenMedico)
                ) &&
                (
                    (objItem.NecesitaLaboratorio && objItem.LaboratorioFiles.Count > 0) ||
                    (!objItem.NecesitaLaboratorio)
                ))
            {
                try
                {
                    int casoSisaId = 0;
                    EnlaceDesgravamenSISA objSisa = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(objItem.CitaDesgravamenId, ref casoSisaId);
                    if ((objSisa != null && !objSisa.Dirty) || (objSisa == null && objItem.NecesitaLaboratorio))
                        FullReportePdfButton.Visible = true;
                }
                catch(Exception q) 
                {
                    log.Warn("no se pudo obtener la ifnormación del caso sias", q);
                }
            } 
        }
    }
    protected void DocumentosRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
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
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ProgramacionCitaLabo objItem = (ProgramacionCitaLabo)e.Item.DataItem;

            Repeater DocumentosRepeater = (Repeater)e.Item.FindControl("DocumentosRepeater");
            DocumentosRepeater.DataSource = objItem.LaboratorioFiles;
            DocumentosRepeater.DataBind();
        }
    }
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load clientes desgravamen
        string selectedValue = clientesComboBox.SelectedValue;

        try
        {
            int intSelectedValue = Convert.ToInt32(selectedValue);
            ClienteIdHiddenField.Value = Convert.ToString(intSelectedValue);
            changeTitleScreen();
            PropuestoAseguradoGridView.Rebind();
        }
        catch (Exception q)
        {
            log.Error("There was an error converting selectedValue to int selectedValue: " + selectedValue, q);
            throw q;
        }
    }
    private void changeTitleScreen()
    {
        string clientId = ClienteIdHiddenField.Value;
        try
        {
            int intClienteId = Convert.ToInt32(clientId);
            if (intClienteId != 0)
            {
                RedCliente obj = RedClienteBLL.GetRedClienteByClienteId(intClienteId);
                if (obj != null)
                {
                    ListPropuestoAseguradoTitle.InnerText = "Recuperacion de Citas Eliminadas para " + obj.NombreJuridico;
                }
            }
            else
            {
                ListPropuestoAseguradoTitle.InnerText = "Recuperacion de Citas Eliminadas";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void LoadClientesToCombo()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        List<RedCliente> modifiedList = new List<RedCliente>();
        modifiedList.Add(new RedCliente()
        {
            ClienteId = 0,
            NombreJuridico = "Todos"
        });
        foreach (RedCliente cliente in list)
        {
            modifiedList.Add(cliente);
        }

        clientesComboBox.DataSource = modifiedList;
        clientesComboBox.DataValueField = "ClienteId";
        clientesComboBox.DataTextField = "NombreJuridico";
        clientesComboBox.DataBind();
    }
}