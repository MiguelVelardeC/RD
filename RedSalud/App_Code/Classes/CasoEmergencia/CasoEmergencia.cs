using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using Artexacta.App.CasoEmergecia.BLL;
using log4net;
using EvoPdf;
/// <summary>
/// Summary description for CEmergencia
/// </summary>
/// 


namespace Artexacta.App.CasoEmergecia
{
    public class CasoEmergencia
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        #region "Atributos"

        private int _detId;
        private int _casoId;
        private decimal _detMontoEmergencia;
        private decimal _detMontoHonorariosMedicos;
        private decimal _detMontoFarmacia;
        private decimal _detMontoLaboratorios;
        private decimal _detMontoEstudios;
        private decimal _detMontoOtros;
        private decimal _detMontoTotal;
        private decimal _detPorcentajeCopago;
        private decimal _detMontoCoPago;
        private DateTime _detFecha;

        #endregion
        public int detid
        {
            set { this._detId = value; }
            get { return this._detId; }
        }
        public int casoId
        {
            set { this._casoId = value; }
            get { return this._casoId; }
        }
        public decimal detMontoEmergencia
        {
            set { this._detMontoEmergencia = value; }
            get { return this._detMontoEmergencia; }

        }
        public decimal detMontoHonorariosMedicos
        {
            set { this._detMontoHonorariosMedicos = value; }
            get { return this._detMontoHonorariosMedicos; }
        }
        public decimal detMontoFarmacia
        {
            set { this._detMontoFarmacia = value; }
            get { return this._detMontoFarmacia; }

        }
        public decimal detMontoLaboratorios
        {
            set { this._detMontoLaboratorios = value; }
            get { return this._detMontoLaboratorios; }

        }
        public decimal detMontoEstudios
        {
            set { this._detMontoEstudios = value; }
            get { return this._detMontoEstudios; }

        }
        public decimal detMontoOtros
        {
            set { this._detMontoOtros = value; }
            get { return this._detMontoOtros; }

        }
        public decimal detMontoTotal
        {
            set { this._detMontoTotal = value; }
            get { return this._detMontoTotal; }

        }
        public decimal detPorcentajeCopago
        {
            set { this._detPorcentajeCopago = value; }
            get { return this._detPorcentajeCopago; }

        }
        public decimal detMontoCoPago
        {
            set { this._detMontoCoPago = value; }
            get { return this._detMontoCoPago; }
        }
        public DateTime detFecha
        {
            set { this._detFecha = value; }
            get { return this._detFecha; }

        }
        #region "Propiedades"
        #endregion
        public CasoEmergencia()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public CasoEmergencia(
         int detId, int casoId, decimal detMontoEmergencia, decimal detMontoHonorariosMedicos,
         decimal detMontoFarmacia, decimal detMontoLaboratorios, decimal detMontoEstudios,
         decimal detMontoOtros, decimal detMontoTotal, decimal detPorcentajeCopago, decimal detMontoCoPago,
         DateTime detFecha)
        {
            this._detId = detId;
            this._casoId = casoId;
            this._detMontoEmergencia = detMontoEmergencia;
            this._detMontoHonorariosMedicos = detMontoHonorariosMedicos;
            this._detMontoFarmacia = detMontoFarmacia;
            this._detMontoLaboratorios = detMontoLaboratorios;
            this._detMontoEstudios = detMontoEstudios;
            this._detMontoOtros = detMontoOtros;
            this._detMontoTotal = detMontoTotal;
            this._detPorcentajeCopago = detPorcentajeCopago;
            this._detMontoCoPago = detMontoCoPago;
            this._detFecha = detFecha;
        }

        public List<DocumentFile> LaboratorioFiles
        {
            get
            {
                if (detid <= 0)
                    return new List<DocumentFile>();
                List<DocumentFile> list = null;
                try
                {
                    list = CasoEmergenciaBLL.GetEmergenciaFiles(detid);
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get List of Files for CitaDesgravamen", ex);
                    list = new List<DocumentFile>();
                }
                return list;
            }
        }

        public static byte[] GetExamenMedicoEnPdf(int EmergenciaId, string urlServer)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
                return GetExamenMedicoEnPdf_EVO(EmergenciaId, urlServer);
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetExamenMedicoEnPdf_HIQ(EmergenciaId, urlServer);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }
        public static byte[] GetExamenMedicoEnPdf_EVO(int EmergenciaId, string urlServer)
        {
            byte[] pdfBytes = null;
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/Desgravamen/ExamenMedicoImprimir.aspx?f=1&cdid=" + EmergenciaId);
            urlToConvert = urlToConvert.Replace(" ", "%20");

            try
            {
                // Create the PDF converter. Optionally the HTML viewer width can be specified as parameter
                // The default HTML viewer width is 1024 pixels.
                PdfConverter pdfConverter = new PdfConverter();

                // set the license key - required
                pdfConverter.LicenseKey = Artexacta.App.Configuration.Configuration.GetLibraryPdfKey();

                // set the converter options - optional
                pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.Letter;
                pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal;
                pdfConverter.PdfDocumentOptions.PdfPageOrientation = PdfPageOrientation.Portrait;


                // set if header and footer are shown in the PDF - optional - default is false 
                pdfConverter.PdfDocumentOptions.ShowHeader = false;
                pdfConverter.PdfDocumentOptions.ShowFooter = false;
                // set if the HTML content is resized if necessary to fit the PDF page width - default is true
                pdfConverter.PdfDocumentOptions.FitWidth = true;

                // set the margins of the page
                pdfConverter.PdfDocumentOptions.TopMargin = 30;
                pdfConverter.PdfDocumentOptions.BottomMargin = 25;

                // set the embedded fonts option - optional - default is false
                pdfConverter.PdfDocumentOptions.EmbedFonts = false;
                // set the live HTTP links option - optional - default is true
                pdfConverter.PdfDocumentOptions.LiveUrlsEnabled = true;

                pdfConverter.MediaType = "print";

                // set if the JavaScript is enabled during conversion to a PDF - default is true
                pdfConverter.JavaScriptEnabled = true;

                // set if the images in PDF are compressed with JPEG to reduce the PDF document size - default is true
                pdfConverter.PdfDocumentOptions.JpegCompressionEnabled = true;

                // enable auto-generated bookmarks for a specified list of HTML selectors (e.g. H1 and H2)

                pdfConverter.PdfBookmarkOptions.HtmlElementSelectors = new string[] { "H1", "H2" };

                pdfConverter.NavigationTimeout = 3600;

                // Performs the conversion and get the pdf document bytes that can 
                // be saved to a file or sent as a browser response
                pdfBytes = pdfConverter.ConvertUrl(urlToConvert);
            }
            catch (Exception q)
            {
                log.Error("An error ocurred trying to generate the PDF report.", q);
                return null;
            }

            return pdfBytes;
        }
        public static byte[] GetExamenMedicoEnPdf_HIQ(int EmergenciaId, string urlServer)
        {
            byte[] pdfBytes = null;
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/Desgravamen/ExamenMedicoImprimir.aspx?f=1&cdid=" + EmergenciaId);
            urlToConvert = urlToConvert.Replace(" ", "%20");

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

                // set triggering mode; for WaitTime mode set the wait time before convert
                htmlToPdfConverter.TriggerMode = HiQPdf.ConversionTriggerMode.Auto;
                htmlToPdfConverter.MediaType = "print";
                // set the document security
                htmlToPdfConverter.Document.Security.AllowPrinting = true;

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
}