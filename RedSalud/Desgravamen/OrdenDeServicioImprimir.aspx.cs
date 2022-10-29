using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Security.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.User;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Configuration;
using System.IO;
using Artexacta.App.Siniestro;

public partial class Desgravamen_OrdenDeServicioImprimir : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int CitaDesgravamenId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(CitaDesgravamenIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                CitaDesgravamenIdHiddenField.Value = "0";
            else
                CitaDesgravamenIdHiddenField.Value = value.ToString();
        }
    }

    public int PropuestoAseguradoId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(PropuestoAseguradoIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                PropuestoAseguradoIdHiddenField.Value = "0";
            else
                PropuestoAseguradoIdHiddenField.Value = value.ToString();
        }
    }

    public int ProveedorMedicoId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(ProveedorMedicoIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                ProveedorMedicoIdHiddenField.Value = "0";
            else
                ProveedorMedicoIdHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ProcessSessionParameters();
                CargarDirecciones();

                CitaIdLabel.Text = CitaDesgravamenId.ToString();

                CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
                CitaMedica objCitaMedica = CitaMedicaBLL.GetCitaMedicaById(CitaDesgravamenId);
                ProveedorDesgravamen objProveedorMedico = null;
                if (objCitaMedica.ProveedorMedicoId > 0)
                    objProveedorMedico = CargarProveedor(objCitaMedica.ProveedorMedicoId);
                MedicoDesgravamen objMedico  = null;

                if (objCita.CobroFinanciera)
                {
                    FacturarPAPanel.Visible = false;
                }

                if (objCitaMedica.MedicoDesgravamenId > 0)
                {
                    objMedico = CargarME(objCitaMedica.MedicoDesgravamenId);
                }
                else
                {
                    MENombre.Text = "N/A";
                }
                if (objCitaMedica.NecesitaExamen && objMedico != null)
                {
                    FechaCita.Text = objCitaMedica.FechaHoraCita.ToShortDateString() +
                        " " + objCitaMedica.FechaHoraCita.ToShortTimeString();
                }
                PropuestoAsegurado objPA = CargarPA(objCitaMedica);
                CitaReferencia.Text = objCita.Referencia;
                /*if (ProveedorMedicoId > 0)
                {
                    FirmaFooterLabel.Text = "Proveedor Médico";
                }
                else
                {
                    FirmaFooterLabel.Text = "Nacional Vida";
                }*/
                RedCliente cl = RedClienteBLL.GetRedClienteByClienteId(objCita.ClienteId);

                //ordendeservicioh1title.InnerText = "Orden de Servicio " + cl.NombreJuridico;
                ordenServicioCliente.InnerText = cl.NombreJuridico;
                User u = UserBLL.GetUserById(objCita.EjecutivoId);

                if (u.SignatureFileId > 0)
                {
                    digitalSignatureLeftPadding.Visible = true;
                    digitalSignature.Visible = true;
                    digitalSignatureImage.Visible = true;
                    int[] imageDimensions = Configuration.GetDigitalSignatureDimension();
                    if (u.SignatureFileId > 0)
                    {
                        digitalSignatureImage.ImageUrl = "~/ImageResize.aspx?ID=" + u.SignatureFileId.ToString() + "&W=" + imageDimensions[0] + "&H=" + imageDimensions[1];
                        digitalSignatureImage.Visible = true;
                    }
                }

                FirmaFooterLabel.Text = "SOLICITANTE - "+ u.FullName;
            }
            catch (Exception q)
            {
                log.Error("No se puede mostrar la pagina", q);

                SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
                Response.Redirect("~/MainPage.aspx");
                return;
            }
        }
    }

    private void CargarDirecciones()
    {
        string title = System.Configuration.ConfigurationManager.AppSettings["DESGProveedoAddressSectionTitle"];

        if (title != null)
        {
            AddressSectionTitle.Text = title;
        }

        string AddT1 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress1"];
        string[] st1 = AddT1.Split('|');
        if(AddT1 != null && st1.Length > 1)
        {
            AddressTitle1.Text = st1[0];
            AddressContent1.Text = st1[1];
        }

        string AddT2 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress2"];
        string[] st2 = AddT2.Split('|');
        if (AddT2 != null && st2.Length > 1)
        {
            AddressTitle2.Text = st2[0];
            AddressContent2.Text = st2[1];
        }

        string AddT3 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress3"];
        string[] st3 = AddT3.Split('|');
        if (AddT3 != null && st3.Length > 1)
        {
            AddressTitle3.Text = st3[0];
            AddressContent3.Text = st3[1];
        }

        string AddT4 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress4"];
        string[] st4 = AddT4.Split('|');
        if (AddT4 != null && st4.Length > 1)
        {
            AddressTitle4.Text = st4[0];
            AddressContent4.Text = st4[1];
        }

        string AddT5 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress5"];
        string[] st5 = AddT5.Split('|');
        if (AddT5 != null && st5.Length > 1)
        {
            AddressTitle5.Text = st5[0];
            AddressContent5.Text = st5[1];
        }

        string AddT6 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress6"];
        string[] st6 = AddT6.Split('|');
        if (AddT6 != null && st6.Length > 1)
        {
            AddressTitle6.Text = st6[0];
            AddressContent6.Text = st6[1];
        }

        string AddT7 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress7"];
        string[] st7 = AddT7.Split('|');
        if (AddT7 != null && st7.Length > 1)
        {
            AddressTitle7.Text = st7[0];
            AddressContent7.Text = st7[1];
        }

        string AddT8 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress8"];
        string[] st8 = AddT8.Split('|');
        if (AddT8 != null && st8.Length > 1)
        {
            AddressTitle8.Text = st8[0];
            AddressContent8.Text = st8[1];
        }

        string AddT9 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress9"];
        string[] st9 = AddT9.Split('|');
        if (AddT9 != null && st9.Length > 1)
        {
            AddressTitle9.Text = st9[0];
            AddressContent9.Text = st9[1];
        }

        string AddT10 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress10"];
        string[] st10 = AddT10.Split('|');
        if (AddT10 != null && st10.Length > 1)
        {
            AddressTitle10.Text = st10[0];
            AddressContent10.Text = st10[1];
        }

        string AddT11 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress11"];
        string[] st11 = AddT11.Split('|');
        if (AddT11 != null && st11.Length > 1)
        {
            AddressTitle11.Text = st11[0];
            AddressContent11.Text = st11[1];
        }


        string AddT12 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress12"];
        string[] st12 = AddT12.Split('|');
        if (AddT12 != null && st12.Length > 1)
        {
            AddressTitle12.Text = st12[0];
            AddressContent12.Text = st12[1];
        }

        string AddT13 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress13"];
        string[] st13 = AddT13.Split('|');
        if (AddT13 != null && st13.Length > 1)
        {
            AddressTitle13.Text = st13[0];
            AddressContent13.Text = st13[1];
        }

        string AddT14 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress14"];
        string[] st14 = AddT14.Split('|');
        if (AddT14 != null && st14.Length > 1)
        {
            AddressTitle14.Text = st14[0];
            AddressContent14.Text = st14[1];
        }

        string AddT15 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress15"];
        string[] st15 = AddT15.Split('|');
        if (AddT15 != null && st15.Length > 1)
        {
            AddressTitle15.Text = st15[0];
            AddressContent15.Text = st15[1];
        }

        string AddT16 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress16"];
        string[] st16 = AddT16.Split('|');
        if (AddT16 != null && st16.Length > 1)
        {
            AddressTitle16.Text = st16[0];
            AddressContent16.Text = st16[1];
        }

        string AddT17 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress17"];
        string[] st17 = AddT17.Split('|');
        if (AddT17 != null && st17.Length > 1)
        {
            AddressTitle17.Text = st17[0];
            AddressContent17.Text = st17[1];
        }

        string AddT18 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress18"];
        string[] st18 = AddT18.Split('|');
        if (AddT18 != null && st18.Length > 1)
        {
            AddressTitle18.Text = st18[0];
            AddressContent18.Text = st18[1];
        }

        string AddT19 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress19"];
        string[] st19 = AddT19.Split('|');
        if (AddT19 != null && st19.Length > 1)
        {
            AddressTitle19.Text = st19[0];
            AddressContent19.Text = st19[1];
        }

        string AddT20 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress20"];
        string[] st20 = AddT20.Split('|');
        if (AddT20 != null && st20.Length > 1)
        {
            AddressTitle20.Text = st20[0];
            AddressContent20.Text = st20[1];
        }

        string AddT21 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress21"];
        string[] st21 = AddT21.Split('|');
        if (AddT21 != null && st21.Length > 1)
        {
            AddressTitle21.Text = st21[0];
            AddressContent21.Text = st21[1];
        }

        string AddT22 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress22"];
        string[] st22 = AddT22.Split('|');
        if (AddT22 != null && st22.Length > 1)
        {
            AddressTitle22.Text = st22[0];
            AddressContent22.Text = st22[1];
        }

        string AddT23 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress23"];
        string[] st23 = AddT23.Split('|');
        if (AddT23 != null && st23.Length > 1)
        {
            AddressTitle23.Text = st23[0];
            AddressContent23.Text = st23[1];
        }


        string AddT24 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress24"];
        string[] st24 = AddT24.Split('|');
        if (AddT24 != null && st24.Length > 1)
        {
            AddressTitle24.Text = st24[0];
            AddressContent24.Text = st24[1];
        }

        string AddT25 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress25"];
        string[] st25 = AddT25.Split('|');
        if (AddT25 != null && st25.Length > 1)
        {
            AddressTitle25.Text = st25[0];
            AddressContent25.Text = st25[1];
        }

        string AddT26 = System.Configuration.ConfigurationManager.AppSettings["DESGProveedorAddress26"];
        string[] st26 = AddT26.Split('|');
        if (AddT26 != null && st26.Length > 1)
        {
            AddressTitle26.Text = st26[0];
            AddressContent26.Text = st26[1];
        }

    }

    private ProveedorDesgravamen CargarProveedor(int proveedorMedicoId)
    {
        ProveedorDesgravamen result = ProveedorMedicoBLL.GetProveedorMedicoId(proveedorMedicoId);
        NombreCiudad.Text = result.CiudadNombre;

        return result;
    }

    private PropuestoAsegurado CargarPA(CitaMedica objCita)
    {
        PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCita.PropuestoAseguradoId);
        PACI.Text = objPA.CarnetIdentidad;
        PAFechaNacimiento.Text = objPA.FechaNacimientoForDisplay;
        PANombre.Text = objPA.NombreCompleto;
        PATelefonoCelular.Text = objPA.TelefonoCelular;
        return objPA;
    }

    private MedicoDesgravamen CargarME(int medicoDesgravamenId)
    {
        MedicoDesgravamen objMedico = MedicoDesgravamenBLL.GetMedicoDesgravamenById(medicoDesgravamenId);
        MENombre.Text = objMedico.Nombre;
        
        return objMedico;
    }

    private void ProcessSessionParameters()
    {

        int cdid = 0;
        string paginaBack = "PropuestoAseguradoLista.aspx";
        bool isQueryString = false;
        try
        {
            string sessionCitaDesgravamen = (Session["CITADESGRAVAMENID"] != null) ? Session["CITADESGRAVAMENID"].ToString() : "";
            
            int.TryParse(sessionCitaDesgravamen, out cdid);

            string queryStringCitaDesgravamen = (Request.QueryString["CITADESGRAVAMENID"] != null) ? Request.QueryString["CITADESGRAVAMENID"].ToString() : "";

            if (cdid <= 0)
            {
                int.TryParse(queryStringCitaDesgravamen, out cdid);
                isQueryString = (cdid > 0);
            }

            if (cdid <= 0)
                throw new ArgumentException("No puede ser el id menor o igual a 0");


            if (Session["PAGINABACK"] != null && !string.IsNullOrWhiteSpace(Session["PAGINABACK"].ToString()))
                paginaBack = Session["PAGINABACK"].ToString();

            CitaDesgravamenId = cdid;
            Session["CITADESGRAVAMENID"] = null;
            Session["PAGINABACK"] = null;

            PaginaBackHiddenField.Value = paginaBack;
        }
        catch (Exception q)
        {
            log.Warn("Identificador de la cita falta", q);
            SystemMessages.DisplaySystemWarningMessage("Se llamó la página sin el identificador de la cita");
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        if (!isQueryString)
        {
            int userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
            List<ProveedorDesgravamen> objProv = ProveedorMedicoBLL.GetProveedorMedicoByUserId(userId);
            if (objProv != null && objProv.Count == 1)
            {
                ProveedorMedicoId = objProv[0].ProveedorMedicoId;
                pdfIcon.Visible = false;
            }
            else
            {
                ProveedorMedicoId = 0;
            }
        }
        else
        {
            ProveedorMedicoId = 0;
        }
    }

    protected void cmdVolver_Click(object sender, EventArgs e)
    {
        Session["PropuestoAseguradoId"] = PropuestoAseguradoId;
        Session["CitaDesgravamenId"] = CitaDesgravamenId;

        Response.Redirect(PaginaBackHiddenField.Value);
    }
    protected void EstudiosDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void pdfIcon_Click(object sender, ImageClickEventArgs e)
    {
        if(CitaDesgravamenId != 0)
        GetReportPdf(CitaDesgravamenId);
    }

    private void GetReportPdf_HIQ(int citaDesgravamenId)
    {
        CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(citaDesgravamenId);

        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        byte[] pdfBytesOrdenServicioReport = null;
        pdfBytesOrdenServicioReport = GetOrdenDeServicioEnPdf_HIQ(citaDesgravamenId, urlServer);


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