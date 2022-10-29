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
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.RedCliente;
using System.Web.Security;
using Artexacta.App.User.BLL;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using Artexacta.App.Security.BLL;

public partial class Desgravamen_PropuestoAseguradoLista : Page
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

    public bool UserAuthorizedEdit
    {
        get
        {
            return Convert.ToBoolean(UserAuthorizedToEditHiddenField.Value);
        }
        set
        {
            UserAuthorizedToEditHiddenField.Value = value.ToString();
        }
    }



    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPA.Config = new PropuestoAseguradoSearchConfig();
        SearchPA.OnSearch += SearchPA_OnSearch;

        if (!IsPostBack)
        {
            SearchPA.Query = "(" + enlaceRapidoBusqueda.SelectedValue + ")";

            //string query = BuildQuery();

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
                /*if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_EXAMEN_MEDICO") ||
                    Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_VER_TODOSCASOS"))
                    userId = 0;*/
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
                    changeTitleScreen();
                    //throw new Exception("No se pudo encontrar el id del usuario");
                }
                else 
                {
                    ClienteIdHiddenField.Value = clienteId.ToString();
                }
            }
            catch (Exception)
            {
                SystemMessages.DisplaySystemWarningMessage("El Usuario [" + User.Identity.Name + "] no tiene un cliente asignado");
                // si es un usuario valido se queda con el id de usuario encontrado
            }

            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_PALIST");


                //UserAuthorizedAprobar = true;
                AdminPanel.Visible = true;
                EjecutivoPanel.Visible = false;
                clientesComboBox.Visible = true;
                UserAuthorizedEdit = true;
                enlaceRapidoBusqueda.Visible = false;
                enlaceRapidoBusqueda2.Visible = true;
                LoadClientesToCombo();

                citaIdEjecutivo.Visible = false;
                propuestoAseguradoEjecutivo.Visible = false;

                string[] roles = Roles.GetRolesForUser(User.Identity.Name);

                //bool b = Roles.IsUserInRole(roles[0], User.Identity.Name);

                bool b = SecurityBLL.IsUserInThisRole("Asistente Desgravamen");

                loadCiudadesComboBox(b);
                loadTipoProductoComboBox();
                loadFinancieraComboBox();
                loadEjecutivosComboBox();
                //load generic filter lists here
                SearchPA.Query = BuildQuery();
                //IsUserAuthorized.Value = Convert.ToString(true);
            }
            catch (Exception)
            {


                UserAuthorizedEdit = false;
                clientesComboBox.Visible = false;
                AdminPanel.Visible = false;
                EjecutivoPanel.Visible = true;
                //SearchPA.Visible = true;
                enlaceRapidoBusqueda.Visible = true;
                enlaceRapidoBusqueda2.Visible = false;
                citaIdEjecutivo.Visible = true;
                propuestoAseguradoEjecutivo.Visible = true;
                //IsUserAuthorized.Value = Convert.ToString(false);
                //throw;
            }
            //SearchPA.Query = BuildQuery(SearchPA.Query);

            //SearchPA.Visible = false;
        }
    }

    private void LoadClientesToCombo()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        List<RedCliente> modifiedList = new List<RedCliente>();
        modifiedList.Add(new RedCliente() 
        { 
            ClienteId = 0,
            NombreJuridico="Todos" 
        });
        foreach(RedCliente cliente in list){
            modifiedList.Add(cliente);
        }

        clientesComboBox.DataSource = modifiedList;
        clientesComboBox.DataValueField = "ClienteId";
        clientesComboBox.DataTextField = "NombreJuridico";
        clientesComboBox.DataBind();
    }

    void SearchPA_OnSearch()
    {
        PropuestoAseguradoGridView.DataBind();
    }

    protected void Pager_PageChanged(int row)
    {
        //PropuestoAseguradoGridView.DataBind();
    }

    protected void CitaButton_Click(object sender, EventArgs e)
    {
        Session["PropuestoAseguradoId"] = PaId.Text;
        Response.Redirect("~/Desgravamen/PropuestoAseguradoCita.aspx");
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
        if (Pager.CurrentRow > Pager.TotalRows)
        {
            Pager.CurrentRow = 0;
        }
        Pager.Visible = true;
        Pager.BuildPagination();
    }

    protected void PropuestoAseguradoGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerPA")
        {
            Session["PropuestoAseguradoId"] = e.CommandArgument;
            Response.Redirect("~/Desgravamen/PropuestoAsegurado.aspx");
            return;
        }

        if (e.CommandName == "Cita")
        {
            Session["CitaDesgravamenId"] = e.CommandArgument;
            Response.Redirect("~/Desgravamen/PropuestoAseguradoCita.aspx");
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
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);
            GetFullReportPdf(citaDesgravamenId);
            return;
        }

        if (e.CommandName == "ProgramacionCita")
        {
            Session["CitaDesgravamenId"] = e.CommandArgument;
            //Session["ClienteId"] = ClienteIdHiddenField.Value;
            Response.Redirect("~/Desgravamen/SeleccionHoraLibre.aspx");
        }

        if (e.CommandName == "AprobarPA")
        {
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);
            CitaDesgravamenBLL.MarcarAprobado(citaDesgravamenId);

            SystemMessages.DisplaySystemMessage("Se acaba de aprobar la cita con número " + citaDesgravamenId);
            PropuestoAseguradoGridView.DataBind();
            return;
        }

        if (e.CommandName == "DesaprobarPA")
        {
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);
            CitaDesgravamenBLL.MarcarNoAprobado(citaDesgravamenId);

            SystemMessages.DisplaySystemMessage("Se acaba de anular la aprobación de la cita con número " + citaDesgravamenId);
            PropuestoAseguradoGridView.DataBind();
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

        if (e.CommandName == "DescargarOrden")
        {
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);

            if (citaDesgravamenId != 0)
                GetReportPdf(citaDesgravamenId);
            return;
        }

        if (e.CommandName == "Delete")
        {
            int citaId = 0;
            int paId = 0;
            try
            {
                string ids = e.CommandArgument.ToString();
                char[] sep = {'-'};
                string[] idsSplitted = ids.Split(sep);
                citaId = Convert.ToInt32(idsSplitted[0]);
                paId = Convert.ToInt32(idsSplitted[1]);
            }
            catch {
                log.Error("No pudo convertir " + e.CommandArgument.ToString() + " a los ids");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                CitaDesgravamenBLL.DeleteCitaDesgravamen(citaId,paId);
                if (citaId == 0)
                {
                    log.Info("Se elimino completamente la información del propuesto asegurado " + paId.ToString());
                    theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Desgravamen", paId.ToString(), "El Id es el Prop. Asegurado");
                    SystemMessages.DisplaySystemMessage("La información del propuesto asegurado " + paId.ToString() + " ha sido eliminada");
                }
                else
                {
                    log.Info("La cita " + citaId + " ha sido eliminada por el usuario " + User.Identity.Name);
                    theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarCita, User.Identity.Name, "Desgravamen", citaId.ToString(), "Desde lista, id del PA es " + paId.ToString());
                    SystemMessages.DisplaySystemMessage("La cita " + citaId + " ha sido eliminada");
                }
                
            }
            catch(Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La cita " + citaId + " no pudo ser eliminada");
            }
            PropuestoAseguradoGridView.DataBind();
            return;
        }
    }

    private void GetFullReportPdf_EVO(int citaId)
    {
        string urlServer = "https://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "443")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        byte[] pdfBytesExamenMedico = null;
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
            if (pdfBytesExamenMedico == null) 
            {
                urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
                if (Request.ServerVariables["SERVER_PORT"].ToString() != "8080")
                    urlServer += ":" + Request.ServerVariables["SERVER_PORT"];
                pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
            }
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
        string urlServer = "https://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "443")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaId);
        byte[] pdfBytesExamenMedico = null;
        if (objCita.NecesitaExamen)
        {
            pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
            if (pdfBytesExamenMedico == null)
            {
                urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
                if (Request.ServerVariables["SERVER_PORT"].ToString() != "8080")
                    urlServer += ":" + Request.ServerVariables["SERVER_PORT"];
                pdfBytesExamenMedico = CitaMedica.GetExamenMedicoEnPdf(citaId, urlServer);
            }
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
                objEnlace.ClienteDesgravamenId = objCita.ClienteId;
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

        if (pdfBytesLabos.Count == 1 && pdfBytesImg.Count <= 0)
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

    protected void enlaceRapidoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchPA.Query))
        {
            if (!string.IsNullOrWhiteSpace(enlaceRapidoBusqueda.SelectedValue))
                SearchPA.Query = "(" + enlaceRapidoBusqueda.SelectedValue + ")";
        }
        else
        {
            string q = SearchPA.Query;
            int yaFiltrado = q.LastIndexOf(" AND (");
            if (yaFiltrado < 0)
                yaFiltrado = q.StartsWith("(") && q.EndsWith(")") ? 0 : -1;

            if (yaFiltrado >= 0)
            {
                q = q.Substring(0, yaFiltrado);
                if (!string.IsNullOrWhiteSpace(enlaceRapidoBusqueda.SelectedValue))
                    SearchPA.Query = q + (yaFiltrado == 0 ? "" : " AND ") + "(" + enlaceRapidoBusqueda.SelectedValue + ")";
                else
                    SearchPA.Query = q;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(enlaceRapidoBusqueda.SelectedValue))
                    SearchPA.Query += " AND (" + enlaceRapidoBusqueda.SelectedValue + ")";
            }
        }

        PropuestoAseguradoGridView.DataBind();
    }
    protected void PropuestoAseguradoGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            PropuestoAseguradoSearchResult objItem = (PropuestoAseguradoSearchResult)e.Item.DataItem;

            Image InfoPA = (Image)e.Item.FindControl("InfoPA");
            ImageButton ProgramacionCita = (ImageButton)e.Item.FindControl("ProgramacionCita");
            ImageButton AprobarPA = (ImageButton)e.Item.FindControl("AprobarPA");
            Image NoNecesitaExamen = (Image)e.Item.FindControl("NoNecesitaExamen");
            ImageButton ImprimirOrden = (ImageButton)e.Item.FindControl("ImprimirOrden");
            ImageButton DeleteCita = (ImageButton)e.Item.FindControl("DeletePA");
            ImageButton FullReportePdfButton = (ImageButton)e.Item.FindControl("FullReportePdfButton");
            ImageButton VerPA = (ImageButton)e.Item.FindControl("VerPA");
            ImageButton CrearCita = (ImageButton)e.Item.FindControl("CrearCita");
            TableCell Nombre = (TableCell)e.Item.Cells[10];

            Repeater LaboratoriosMainRepeater = (Repeater)e.Item.FindControl("LaboratoriosMainRepeater");
            LaboratoriosMainRepeater.DataSource = ProgramacionCitaLaboBLL.GetProgramacionCitaLabo(objItem.CitaDesgravamenId);
            LaboratoriosMainRepeater.DataBind();

            AprobarPA.Enabled = UserAuthorizedAprobar;
            //a pedido, solo los administradores que tengan el permiso especifico pueden modificar citas
            CrearCita.Visible = UserAuthorizedEdit;

            if (!objItem.CobroFinanciera)
            {
                Nombre.BackColor = System.Drawing.Color.FromArgb(1, 255, 153, 153);
            }

            if ((objItem.NecesitaExamenMedico && objItem.Aprobado) ||
                (objItem.NecesitaLaboratorio && objItem.AprobadoLabo))
            {
                ProgramacionCita.Visible = false;
                AprobarPA.ImageUrl = "../Images/Neutral/unlock.png";
                if (!UserAuthorizedAprobar)
                    AprobarPA.ToolTip = "Aprobado";
                else 
                    AprobarPA.ToolTip = "Desaprobar";
                AprobarPA.CommandName = "DesaprobarPA";
            }
            else
            {
                if (!UserAuthorizedAprobar)
                    AprobarPA.ToolTip = "No aprobado";
            }

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
                else
                {
                    ImprimirOrden.Visible = false;
                    AprobarPA.Visible = false;
                }
            }
            catch
            {
                ImprimirOrden.Visible = false;
                AprobarPA.Visible = false;
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

            // Lugar para colocar las condiciones generales de visibiildad
            //------------------------------------------------------------
            if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_ELIMINAR_CITA"))
                DeleteCita.Visible = true;
            if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_EXAMEN_MEDICO") && !Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_PALIST"))
            {
                InfoPA.Visible = false;
                ProgramacionCita.Visible = false;
                AprobarPA.Visible = false;
                NoNecesitaExamen.Visible = false;
                DeleteCita.Visible = false;
                ImprimirOrden.Visible = true;
                VerPA.Visible = false;
                CrearCita.Visible = false;
            }

            // Seguimos con las condiciones que cortan el flujo
            //---------------------------------------------------
            if (objItem.NecesitaExamenMedico && objItem.FechaHoraCita == DateTime.MinValue)
            {
                InfoPA.ToolTip = "No tiene programada una cita médica, está a medio registrar";
                InfoPA.ImageUrl = "~/Images/neutral/alert.gif";
                return;
            }

            if (objItem.NecesitaExamenMedico &&
                objItem.TieneExamenMedico == 0 &&
                objItem.FechaHoraCita < DateTime.Now)
            {
                InfoPA.ToolTip = "El Propuesto Asegurado faltó a su cita o el médico no pudo grabar el Examen Médico";
                InfoPA.ImageUrl = "~/Images/neutral/alert-red.gif";
                return;
            }

            if (objItem.TieneExamenMedico > 0 &&
                objItem.FechaHoraCita < DateTime.Now &&
                objItem.NecesitaLaboratorio &&
                objItem.LaboratorioFiles.Count > 0)
            {
                InfoPA.ToolTip = "Fue a su cita y tiene laboratorios";
                InfoPA.ImageUrl = "~/Images/neutral/complete.gif";
                ProgramacionCita.Visible = false;
                return;
            }

            if (objItem.TieneExamenMedico > 0 &&
                objItem.FechaHoraCita < DateTime.Now &&
                !objItem.NecesitaLaboratorio)
            {
                InfoPA.ToolTip = "Fue a su cita y no necesita laboratorios";
                InfoPA.ImageUrl = "~/Images/neutral/complete.gif";
                ProgramacionCita.Visible = false;
                return;
            }

            if (objItem.TieneExamenMedico > 0 &&
                objItem.FechaHoraCita < DateTime.Now &&
                objItem.NecesitaLaboratorio &&
                objItem.LaboratorioFiles.Count == 0)
            {
                InfoPA.ToolTip = "Fue a su cita pero necesita laboratorios o el laboratorio no los cargó todavía";
                InfoPA.ImageUrl = "~/Images/neutral/alert.gif";
                return;
            }

            if (objItem.FechaHoraCita > DateTime.Now)
            {
                InfoPA.ToolTip = "Tiene cita en tiempo futuro, tiene tiempo";

                InfoPA.ImageUrl = "~/Images/neutral/calendar.gif";
                return;
            }
            
            InfoPA.Visible = false;
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
    protected void PropuestoAseguradoGridView_DataBound(object sender, EventArgs e)
    {
        
    }
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load clientes desgravamen
        updateClienteHidden();

    }
    protected void LinkPropuestoAsegurado_Click(object sender, EventArgs e)
    {

        string clienteId = ClienteIdHiddenField.Value;

        if (clienteId != null && clienteId.Trim() != "0")
        {
            Session["ClienteId"] = ClienteIdHiddenField.Value;
            Response.Redirect("~/Desgravamen/PropuestoAsegurado.aspx");
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
                    ListPropuestoAseguradoTitle.InnerText = "Lista de Propuestos Asegurados para " + obj.NombreJuridico;
                }
            }
            else
            {
                ListPropuestoAseguradoTitle.InnerText = "Lista de Propuestos Asegurado";
            }
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    private string BuildQuery()
    {
        string parameterBuilder = "";

        if (enlaceRapidoBusqueda.SelectedIndex > 0)
        {
            parameterBuilder += @""+enlaceRapidoBusqueda2.SelectedValue;
        }

        if (ciudadComboBox.SelectedValue != "0")
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @CIUDADID " + ciudadComboBox.SelectedValue;
        }

        if (tipoProductoComboBox.SelectedValue != "TODOS")
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @PRODUCTO " + tipoProductoComboBox.SelectedItem.Text;
        }

        if (financieraComboBox.SelectedValue != "0")
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @FINANCIERACODIGO " + financieraComboBox.SelectedValue;
        }

        if (EjecutivoComboBox.SelectedValue != "0")
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @USUARIO " + EjecutivoComboBox.SelectedValue;
        }

        if (!string.IsNullOrEmpty(CitaId.Text))
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @ID " + CitaId.Text;
        }

        if (!string.IsNullOrEmpty(nombrePropuestoAsegurado.Text))
        {

            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @NOMBRE " + nombrePropuestoAsegurado.Text;
        }

        if (FechaInicioCita.SelectedDate != null && FechaFinCita.SelectedDate != null 
            && FechaInicioCita.SelectedDate != DateTime.MinValue && FechaFinCita.SelectedDate != DateTime.MinValue)
        {
            DateTime dateInicial = FechaInicioCita.SelectedDate.Value;
            DateTime dateFinal = FechaFinCita.SelectedDate.Value;

            dateInicial = dateInicial.AddDays(-1);
            dateFinal = dateFinal.AddDays(1);    

            string dtInicial = dateInicial.ToString("yyyy-MM-dd");
            string dtFinal = dateFinal.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(dtInicial) && !string.IsNullOrEmpty(dtFinal))
            {
                parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
                parameterBuilder += @" (@FECHACITA > " + dtInicial + " AND @FECHACITA < " + dtFinal + ")";
            }

            
        }


        return parameterBuilder;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        updateClienteHidden();
        SearchPA.Query = this.BuildQuery();
        PropuestoAseguradoGridView.DataBind();
    }

    protected void loadTipoProductoComboBox()
    {
        List<ComboContainer> tipoProductos = TipoProductoDesgravamenBLL.GetTipoProductosCombo();
        tipoProductos.Insert(0, new ComboContainer() 
        {
            ContainerId = "TODOS",
            ContainerName = "TODOS"
        });
        tipoProductoComboBox.DataSource = tipoProductos;
        tipoProductoComboBox.DataValueField = "ContainerId";
        tipoProductoComboBox.DataTextField = "ContainerName";
        tipoProductoComboBox.DataBind();
    }

    protected void loadFinancieraComboBox()
    {
        List<Financiera> financieras = FinancieraBLL.GetFinancieras(0, "");

        financieras.Insert(0, new Financiera()
        {
            FinancieraId = -1,
            Nombre = "SIN FINANCIERA"
        });

        financieras.Insert(0, new Financiera()
        {
            FinancieraId = 0,
            Nombre = "Todos"
        });
        
        financieraComboBox.DataSource = financieras;
        financieraComboBox.DataValueField = "FinancieraId";
        financieraComboBox.DataTextField = "Nombre";
        financieraComboBox.DataBind();
    }

    protected void loadEjecutivosComboBox()
    {
        List<ComboContainer> list = new List<ComboContainer>();
        string[] s  = Roles.GetUsersInRole("Ejecutivo Desgravamen");

        for (int i = 0; i < s.Length; i++)
        {
            Artexacta.App.User.User u = UserBLL.GetUserByUsername(s[i]);
            list.Add(new ComboContainer()
            {
                ContainerId = u.Username,
                ContainerName = u.FullName + " ("+u.Username+")"
            });

        }
        list.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "TODOS"
        });
        EjecutivoComboBox.DataSource = list;
        EjecutivoComboBox.DataValueField = "ContainerId";
        EjecutivoComboBox.DataTextField = "ContainerName";
        EjecutivoComboBox.DataBind();
    }

    private void loadCiudadesComboBox(bool isAsistente)
    {
        List<ComboContainer> ciudades = new List<ComboContainer>();
        if (isAsistente)
        {
            int user = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
            Artexacta.App.User.User u = UserBLL.GetUserById(user);

            Ciudad c = CiudadBLL.GetCiudadDetails(u.CiudadId);

            ciudades.Add(new ComboContainer()
            {
                ContainerId = c.CiudadId,
                ContainerName = c.Nombre
            });
        }
        else
        {
            //string ciudadesExistentes = "ALT,COB,MON,ORU,PTS,SCR,TRI,TRJ";
            ciudades = CiudadesDesgravamenBLL.GetCiudadesDesgravamenCombo();//CiudadBLL.getCiudadList(ciudadesExistentes);
            ciudades.Insert(0, new ComboContainer()
            {
                ContainerId = "0",
                ContainerName = "Todos"
            });
        }
        ciudadComboBox.DataSource = ciudades;
        ciudadComboBox.DataValueField = "ContainerId";
        ciudadComboBox.DataTextField = "ContainerName";
        ciudadComboBox.DataBind();
     
    }
    protected void PropuestoAseguradoDataSource_DataBinding(object sender, EventArgs e)
    {

    }

    private void updateClienteHidden()
    {
        string selectedValue = clientesComboBox.SelectedValue;
        //string isAccordionSelect = IsAccordionActive.Value;
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
    protected void btnSearchEjecutivo_Click(object sender, EventArgs e)
    {
        SearchPA.Query = BuildQueryEjecutivo();
        PropuestoAseguradoGridView.DataBind();
    }

    private string BuildQueryEjecutivo()
    {
        string parameterBuilder = "";

        if (enlaceRapidoBusqueda.SelectedIndex > 0)
        {
            parameterBuilder += @"" + enlaceRapidoBusqueda.SelectedValue;
        }


        if (!string.IsNullOrEmpty(citaIdEjecutivo.Text))
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @ID " + citaIdEjecutivo.Text;
        }

        if (!string.IsNullOrEmpty(propuestoAseguradoEjecutivo.Text))
        {

            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @NOMBRE " + propuestoAseguradoEjecutivo.Text;
        }
       
        return parameterBuilder;
    }


    private void GetReportPdf_HIQ(int citaDesgravamenId)
    {
        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaDesgravamenId);

        string urlServer = "https://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "443")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        byte[] pdfBytesOrdenServicioReport = null;
        pdfBytesOrdenServicioReport = GetOrdenDeServicioEnPdf_HIQ(citaDesgravamenId, urlServer);

        if (pdfBytesOrdenServicioReport == null) 
        {
            urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
            if (Request.ServerVariables["SERVER_PORT"].ToString() != "8080")
                urlServer += ":" + Request.ServerVariables["SERVER_PORT"];
            pdfBytesOrdenServicioReport = GetOrdenDeServicioEnPdf_HIQ(citaDesgravamenId, urlServer);

        }

        HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

        MemoryStream inputStreamOriginal = new MemoryStream(pdfBytesOrdenServicioReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        HiQPdf.PdfDocument documentToAddMergeOriginal = HiQPdf.PdfDocument.FromStream(inputStreamOriginal);
        completeDoc.AddDocument(documentToAddMergeOriginal);


        //MemoryStream inputStream = new MemoryStream(pdfBytesSiniestroReport);
        //HiQPdf.PdfDocument completeDoc = new HiQPdf.PdfDocument();
        //completeDoc.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
        //HiQPdf.PdfDocument documentToAddMerge = HiQPdf.PdfDocument.FromStream(inputStream);
        //completeDoc.AddDocument(documentToAddMerge);

        byte[] fullOutput = null;

        fullOutput = completeDoc.WriteToMemory();

        // send the PDF document as a response to the browser for download
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.Clear();
        response.AddHeader("Content-Type", "application/pdf");

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=OrdenServicio_" + objCita.CitaDesgravamenId + ".pdf; size={0}", fullOutput.Length.ToString()));

        response.BinaryWrite(fullOutput);
        // Note: it is important to end the response, otherwise the ASP.NET
        // web page will render its content to PDF document stream
        response.End();

        completeDoc.Close();

    }

    private void GetReportPdf(int citaDesgravamenId)
    {
        if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
        {
            GetReportPdf_HIQ(citaDesgravamenId);
            return;
        }
    }

    private byte[] GetOrdenDeServicioEnPdf_HIQ(int citaDesgravamenId, string urlServer)
    {

        string queryString = "CITADESGRAVAMENID=" + citaDesgravamenId.ToString();
        //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
        //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
        string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/Desgravamen/OrdenDeServicioImprimir.aspx?" + queryString);
        urlToConvert = urlToConvert.Replace(" ", "%20");

        return GetOrdenServicioEnPdf_HIQ(urlToConvert);
    }

    private byte[] GetOrdenServicioEnPdf_HIQ(string urlToConvert)
    {
        byte[] pdfBytes = null;

        try
        {
            // create the HTML to PDF converter
            HiQPdf.HtmlToPdf htmlToPdfConverter = new HiQPdf.HtmlToPdf();

            // set a demo serial number
            htmlToPdfConverter.SerialNumber = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();
            htmlToPdfConverter.BrowserWidth = 1024;
            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = HiQPdf.PdfPageSize.Letter;
            htmlToPdfConverter.Document.PageOrientation = HiQPdf.PdfPageOrientation.Portrait;
            htmlToPdfConverter.Document.PdfStandard = HiQPdf.PdfStandard.Pdf;
            htmlToPdfConverter.Document.Margins = new HiQPdf.PdfMargins(25, 25, 10, 10);
            htmlToPdfConverter.Document.Header.Enabled = false;
            htmlToPdfConverter.Document.Footer.Enabled = false;
            // set triggering mode; for WaitTime mode set the wait time before convert
            htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
            htmlToPdfConverter.MediaType = "hiqpdf";

            // convert HTML to PDF
            pdfBytes = htmlToPdfConverter.ConvertUrlToMemory(urlToConvert);
        }
        catch (Exception q)
        {
            log.Error("An error ocurred trying to generate the PDF report.", q);
            return null;
        }

        return pdfBytes;
    }
}