using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.IO;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Caso;
using Artexacta.App.Caso.BLL;
using Artexacta.App.Odontologia;
using Artexacta.App.Odontologia.BLL;
using Artexacta.App.CoPagos.BLL;
using Artexacta.App.CoPagos;
using Artexacta.App.Estudio.BLL;
using Artexacta.App.Estudio;
using Telerik.Web.UI;
public partial class CasoMedico_CoPagoDetail : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    #region "variables de Session"
    int DETID;
    int DETIDPrestaciones;
    string TIPOCASO;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblFechaForm.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            try
            {
                ProcessSessionParameters();

                if (TIPOCASO == "CASO_LABORATORIOSIMAGENOLOGIA")
                {
                    CargarDatosCasoLaboratorioImagenologia();
                }
                if (TIPOCASO == "CASO_ESPECIALIDAD")
                {
                    CargarDatosCasoEspecialidad();
                }
                if (TIPOCASO == "CASO_ODONTOLOGIA")
                {
                    CargarDatosCasoOdontologia();
                }

            }
            catch (Exception q)
            {
                log.Error("No se puede mostrar la pagina", q);

                SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
                //   Response.Redirect("~/MainPage.aspx");
                return;
            }
        }

    }
    private void CargarDatosCasoOdontologia()
    {

        try
        {
            CargarListasDeOdontologia();
            CasoOdontologia objCasoOdontologico = CoPagosBLL.GetCasoOdontologia(DETID);
            CoPagoPrecio.Text =Convert.ToDecimal(PrecioTotal.Text).ToString("N2")+ " BS";
            NombreMedico.Text = objCasoOdontologico.NombreMedico;
            NombrePaciente.Text = objCasoOdontologico.NombrePaciente;
            NombrePoliza.Text = objCasoOdontologico.NombrePoliza;
            NumeroPoliza.Text = objCasoOdontologico.NumeroPoliza;
            NombreEspecialidad.Text = objCasoOdontologico.NombreEspecilidad;
            Carnetidentidad.Text = objCasoOdontologico.CarnetIdentidad;
            CasoId.Text = objCasoOdontologico.CodigoCaso;
            TipoSolicitud.Text = objCasoOdontologico.Solicito;
            Sres.Text = objCasoOdontologico.NombreProveedor;
            Observaciones.Text = objCasoOdontologico.Observacion;
            DetalleDiagnostico.Text = objCasoOdontologico.Diagnostico;
            ordendeservicioh1title.InnerText = "Cobro CoPago - Odontologia";
            if (objCasoOdontologico.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                spbutton.InnerText = "COPAGO  cobrado";
                lblFechaForm.Text = objCasoOdontologico.detFechaCoPagoPagado.Replace(" 00:00:00", "");
                spbutton.Disabled = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;

            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }


        }
        catch (Exception q)
        {
            log.Error("No se puede mostrar la pagina", q);

            SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
            //   Response.Redirect("~/MainPage.aspx");
            return;
        }

    }
    private void CargarDatosCasoEspecialidad()
    {
        try
        {
            CargarListasDeEspecialidades();
            CasoEspecialidad objCasoEspecialidad = CoPagosBLL.GetCasoEspecialidad(DETID);
            CoPagoPorcentaje.Text = objCasoEspecialidad.detCoPagoPorcentaje.ToString();
           
            NombreMedico.Text = objCasoEspecialidad.NombreMedico;
            NombrePaciente.Text = objCasoEspecialidad.NombrePaciente;
            NombrePoliza.Text = objCasoEspecialidad.NombrePoliza;
            NumeroPoliza.Text = objCasoEspecialidad.NumeroPoliza;
            NombreEspecialidad.Text = objCasoEspecialidad.NombreEspecilidad;
            Carnetidentidad.Text = objCasoEspecialidad.CarnetIdentidad;
            CasoId.Text = objCasoEspecialidad.CodigoCaso;
            TipoSolicitud.Text = objCasoEspecialidad.Solicito;
            Sres.Text = objCasoEspecialidad.NombreProveedor;
            Observaciones.Text = objCasoEspecialidad.Observacion;
            DetalleDiagnostico.Text = objCasoEspecialidad.Diagnostico;

            if (objCasoEspecialidad.EsMedicoGeneral == 1)
            {
                ordendeservicioh1title.InnerText = "Cobro CoPago - Medico General";
                DetalleDiagnostico.Visible = false;
                LabelDiagnostico.Visible = false;
               
            }
            else
                ordendeservicioh1title.InnerText = "Cobro CoPago - Especialidad";

                CoPagoPrecio.Text =  (decimal.Parse( PrecioTotal.Text)).ToString("N2") + " Bs.";

            if (objCasoEspecialidad.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                lblFechaForm.Text = objCasoEspecialidad.detFechaCoPagoPagado.Replace(" 00:00:00", "");
                spbutton.InnerText = "COPAGO  cobrado";
                spbutton.Disabled = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }

            


        }
        catch (Exception q)
        {
            log.Error("No se puede mostrar la pagina", q);

            SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
            //   Response.Redirect("~/MainPage.aspx");
            return;

        }
    }
    private void CargarDatosCasoLaboratorioImagenologia()
    {
        try
        {
            CargarListasDeEstudios(DETID);
            CasoLaboratorioImagenologia objCasoLaboratorioImagenologia = CoPagosBLL.GetCasoLaboratorioImagenologia(DETID);

            NombreMedico.Text = objCasoLaboratorioImagenologia.NombreMedico;
            NombrePaciente.Text = objCasoLaboratorioImagenologia.NombrePaciente;
            NombrePoliza.Text = objCasoLaboratorioImagenologia.NombrePoliza;
            NumeroPoliza.Text = objCasoLaboratorioImagenologia.NumeroPoliza;
            NombreEspecialidad.Text = objCasoLaboratorioImagenologia.NombreEspecilidad;
            Carnetidentidad.Text = objCasoLaboratorioImagenologia.CarnetIdentidad;
            CasoId.Text = objCasoLaboratorioImagenologia.CodigoCaso;
            Sres.Text = objCasoLaboratorioImagenologia.NombreProveedor;
            Observaciones.Text = objCasoLaboratorioImagenologia.Observacion;
            DetalleDiagnostico.Text = objCasoLaboratorioImagenologia.Diagnostico;

            //para saber que tipo de estudio Es
            if (objCasoLaboratorioImagenologia.detEsImagenologia)
                ordendeservicioh1title.InnerText = "Cobro CoPago - Imagenologia";
            else
                ordendeservicioh1title.InnerText = "Cobro CoPago - Laboratorio";


            //Aqui Recuperamos el Id de Orden de Servicio
            int OrdendeServicioId = CoPagosBLL.GetCasoLaboratorioImagenologia(DETID).OrdenDeServicioId;
            decimal Total = 0;
            List<CasoLaboratorioImagenologia> _cache = new List<CasoLaboratorioImagenologia>();

            try
            {

                //aqui controlamos lo queremos buscar
                int _totalRows = CoPagosBLL.GetCasoLaboratorioImagenologiaxEstudio(_cache, 0, OrdendeServicioId, 0, 0);
                for (int i = 0; i < _cache.Count; i++)
                {
                    Total = Total + _cache[i].detMontoAPagar;
                }
                CoPagoPrecio.Text =  (decimal.Parse(PrecioTotal.Text)).ToString("N2") + ".BS";
            }
            catch (Exception q)
            {
                log.Error("No se puedo cargar los datos de la BD", q);
                SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
            }
            if (objCasoLaboratorioImagenologia.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                spbutton.InnerText = "COPAGO  cobrado";
                spbutton.Disabled = false;
                lblFechaForm.Text = objCasoLaboratorioImagenologia.detFechaCoPagoPagado.Replace(" 00:00:00", "");
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }


        }
        catch (Exception q)
        {
            log.Error("No se puede mostrar la pagina", q);

            SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
            //   Response.Redirect("~/MainPage.aspx");
            return;

        }
    }
    private void ProcessSessionParameters()
    {


        string SessionCaso;
        if (Session["TipoCaso"] != null && !string.IsNullOrEmpty(Session["TipoCaso"].ToString()))
        {
            try
            {
                SessionCaso = Convert.ToString(Session["TipoCaso"]);

                char separador = ';'; // separador de datos
                string[] arregloDeSubCadenas = SessionCaso.Split(separador);
            
                LTIPOCASO.InnerText = SessionCaso;
                if (arregloDeSubCadenas[0].Contains("CASO_LABORATORIOSIMAGENOLOGIA"))
                {
                    int OrdenDeServicioId = int.Parse(arregloDeSubCadenas[1].ToString());
                    DETID = CoPagosBLL.GetCasoLaboratorioImagenologiaxOrdenDeServicio(0, OrdenDeServicioId, 0, 0).detId;
                    TIPOCASO = arregloDeSubCadenas[0];
                }
                if (arregloDeSubCadenas[0].Contains("CASO_ESPECIALIDAD"))
                {
                    DETID = int.Parse(arregloDeSubCadenas[1].ToString());
                    TIPOCASO = arregloDeSubCadenas[0];
                }
                if (arregloDeSubCadenas[0].Contains("CASO_ODONTOLOGIA"))
                {
                    DETID = int.Parse(arregloDeSubCadenas[1].ToString());
                    TIPOCASO = arregloDeSubCadenas[0];
                }


            }
            catch
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el ID del Caso de Prestaciones");
                log.Error("no se pudo realizar la conversion de la session CasoCoPagoDetail:" + Session["TipoCaso"]);
                //   Response.Redirect("~/MainPage.aspx");
                return;
            }
        }
        // Session["TipoCaso"] = null;

    }

    protected void EditStatusHL_Click(object sender, EventArgs e)
    {
        TIPOCASO = LTIPOCASO.InnerText;
        if (TIPOCASO.Contains("CASO_LABORATORIOSIMAGENOLOGIA"))
        {
            UpdateEstadoCasoLaboratorio();
        }
        if (TIPOCASO.Contains("CASO_ESPECIALIDAD"))
        {
            UpdateEstadoCasoEspecialidad();
        }
        if (TIPOCASO.Contains("CASO_ODONTOLOGIA"))
        {
            UpdateEstadoCasoOdontologia();
        }
    }
    private void UpdateEstadoCasoLaboratorio()
    {
        try
        {
            char separador = ';'; // separador de datos
            string[] arregloDeSubCadenas = LTIPOCASO.InnerText.Split(separador);
            DETID = int.Parse(arregloDeSubCadenas[1].ToString());
            CoPagosBLL.UpdateCasoLaboratorioImagenologia(DETID, Convert.ToDateTime(lblFechaForm.Text));
            CasoLaboratorioImagenologia objCasoLaboratorioImagenologia = CoPagosBLL.GetCasoLaboratorioImagenologiaxOrdendeServicio(DETID);

            if (objCasoLaboratorioImagenologia.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                spbutton.InnerText = "COPAGO  cobrado";
                spbutton.Disabled = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
                return;
            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }

            SystemMessages.DisplaySystemMessage("Caso Laboratorio Imagenologia actualizada.");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar El Caso de Laboratorio Imagenologia.");
            log.Error("Error al actualizar el Caso Laboratorio Imagenologia", q);
        }
    }
    private void UpdateEstadoCasoOdontologia()
    {
        try
        {
            char separador = ';'; // separador de datos
            string[] arregloDeSubCadenas = LTIPOCASO.InnerText.Split(separador);
            DETID = int.Parse(arregloDeSubCadenas[1].ToString());
            CoPagosBLL.UpdateCasoOdontologia(DETID, Convert.ToDateTime(lblFechaForm.Text));
            CasoOdontologia objCasoOdontologia = CoPagosBLL.GetCasoOdontologia(DETID);

            if (objCasoOdontologia.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                spbutton.InnerText = "COPAGO  cobrado";
                spbutton.Disabled = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
                return;
            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }
            SystemMessages.DisplaySystemMessage("Caso Odontologia actualizada.");

        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar El Caso de Odontologia.");
            log.Error("Error al actualizar el Caso Odontologia", q);
        }
    }
    private void UpdateEstadoCasoEspecialidad()
    {
        try
        {
            char separador = ';'; // separador de datos
            string[] arregloDeSubCadenas = LTIPOCASO.InnerText.Split(separador);
            DETID = int.Parse(arregloDeSubCadenas[1].ToString());
            CoPagosBLL.UpdateCasoEspecialidad(DETID, Convert.ToDateTime(lblFechaForm.Text));
            CasoEspecialidad objCasoEspecialidad = CoPagosBLL.GetCasoEspecialidad(DETID);

            if (objCasoEspecialidad.detFechaCoPagoPagado != ("S/N"))
            {
                EditStatusHL.Enabled = false;
                spbutton.InnerText = "COPAGO  cobrado";
                spbutton.Disabled = false;
                spbutton.Visible = false;
                EditStatusHL.Visible = false;
                return;
            }
            else
            {
                EditStatusHL.Enabled = true;
                spbutton.InnerText = "cobrar copago";
            }
            SystemMessages.DisplaySystemMessage("Caso Especialidad actualizada.");
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("No se pudo Actualizar El Caso de Especialidad.");
            log.Error("Error al actualizar el Caso Especialidad", q);
        }
    }


    protected void pdfIcon_Click(object sender, ImageClickEventArgs e)
    {
        char separador = ';'; // separador de datos
        string[] arregloDeSubCadenas = LTIPOCASO.InnerText.Split(separador);
        DETID = int.Parse(arregloDeSubCadenas[1].ToString());
        if (DETID != 0)
            GetReportPdf(DETID);
    }

    private void GetReportPdf_HIQ(int detId)
    {

        string urlServer = "http://" + Request.ServerVariables["SERVER_NAME"];
        if (Request.ServerVariables["SERVER_PORT"].ToString() != "80")
            urlServer += ":" + Request.ServerVariables["SERVER_PORT"];

        byte[] pdfBytesOrdenServicioReport = null;
        pdfBytesOrdenServicioReport = GetOrdenDeServicioEnPdf_HIQ(detId, urlServer);


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

        response.AddHeader("Content-Disposition", String.Format("attachment; filename=CoPago_" + detId + ".pdf; size={0}", fullOutput.Length.ToString()));

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
    private byte[] GetOrdenDeServicioEnPdf_HIQ(int detId, string urlServer)
    {

        string queryString = "detId=" + detId.ToString();
        //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
        //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
        string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/CasoMedico/CoPagoDetail.aspx?" + queryString);
        urlToConvert = urlToConvert.Replace(" ", "%25");

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
    private void CargarListasDeEstudios(int detId)
    {
        //Aqui Recuperamos el Id de Orden de Servicio
        int OrdendeServicioId = CoPagosBLL.GetCasoLaboratorioImagenologia(detId).OrdenDeServicioId;
        decimal Total = 0;
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los Prestaciones leidos aquí. 
        List<CasoLaboratorioImagenologia> _cache = new List<CasoLaboratorioImagenologia>();

        try
        {

            //aqui controlamos lo queremos buscar
            _totalRows = CoPagosBLL.GetCasoLaboratorioImagenologiaxEstudio(_cache, 0, OrdendeServicioId, 0, 0);
            for (int i = 0; i < _cache.Count; i++)
            {
                Total = Total + _cache[i].detMontoAPagar;
                _cache[i].detPrecio = Convert.ToDecimal(_cache[i].detPrecio.ToString("N2"));
                _cache[i].detCoPagoReferencial = Convert.ToDecimal(_cache[i].detCoPagoReferencial.ToString("N2"));
            }
            PrecioTotal.Text = Total.ToString("N2");
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
        }



        // Actualizar el estado de habilitado los botones de navegación

        PrestacionesCoPagoGrid.DataSource = _cache;
        PrestacionesCoPagoGrid.DataBind();
    }
    private void CargarListasDeEspecialidades()
    {
        //Aqui Recuperamos el Id de Orden de Servicio
        
        decimal Total = 0;
        // Por ejemplo, si en la base de datos hay 1,500,000.

        // Ponemos los Prestaciones leidos aquí. 
        CasoEspecialidad objCasoEspecialidad=null;
        try
        {

            //aqui controlamos lo queremos buscar
            objCasoEspecialidad = CoPagosBLL.GetCasoEspecialidad(DETID);
            Total = Total + objCasoEspecialidad.detMontoAPagar;
            objCasoEspecialidad.detPrecio = Convert.ToDecimal(objCasoEspecialidad.detPrecio.ToString("N2"));
            objCasoEspecialidad.detCoPagoReferencial = Convert.ToDecimal(objCasoEspecialidad.detCoPagoReferencial.ToString("N2"));
            PrecioTotal.Text = Total.ToString("N2");
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
        }

        List<CasoEspecialidad> _cache = new List<CasoEspecialidad>();
        _cache.Add(objCasoEspecialidad);

        // Actualizar el estado de habilitado los botones de navegación

        PrestacionesCoPagoGrid.DataSource = _cache;
        PrestacionesCoPagoGrid.DataBind();


    }
    private void CargarListasDeOdontologia()
    {
        //Aqui Recuperamos el Id de Orden de Servicio

        decimal Total = 0;
        // Por ejemplo, si en la base de datos hay 1,500,000.

        // Ponemos los Prestaciones leidos aquí. 
        CasoOdontologia objCasoOdontologia = null;
        try
        {

            //aqui controlamos lo queremos buscar
            objCasoOdontologia = CoPagosBLL.GetCasoOdontologia(DETID);
            Total = Total + objCasoOdontologia.detMontoAPagar;
            objCasoOdontologia.detPrecio = Convert.ToDecimal(objCasoOdontologia.detPrecio.ToString("N2"));
            objCasoOdontologia.detCoPagoReferencial = Convert.ToDecimal(objCasoOdontologia.detCoPagoReferencial.ToString("N2"));
            PrecioTotal.Text = Total.ToString("N2");
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Copagos Pendientes Cobro Lista");
        }

        List<CasoOdontologia> _cache = new List<CasoOdontologia>();
        _cache.Add(objCasoOdontologia);

        // Actualizar el estado de habilitado los botones de navegación

        PrestacionesCoPagoGrid.DataSource = _cache;
        PrestacionesCoPagoGrid.DataBind();


    }
    protected void PrestacioneCoPagoGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;

        }
    }

    protected void PrestacioneCoPagoGrid_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
    {
        GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
    }

}