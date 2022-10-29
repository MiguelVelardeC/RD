using EvoPdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for CitaMedica
    /// </summary>
    public class CitaMedica
    {
        public enum EstadoCita { Citada, Atendida, ErrorEstado };

        public static EstadoCita GetEstadoCita(string fromString)
        {
            if (fromString.ToLowerInvariant().Equals(EstadoCita.Citada.ToString().ToLowerInvariant()))
                return EstadoCita.Citada;
            if (fromString.ToLowerInvariant().Equals(EstadoCita.Atendida.ToString().ToLowerInvariant()))
                return EstadoCita.Atendida;

            return EstadoCita.ErrorEstado;
        }

        private static readonly ILog log = LogManager.GetLogger("Standard");

        public int CitaDesgravamenId { get; set; }
        public int PropuestoAseguradoId { get; set; }
        public int FinancieraId { get; set; }
        public string CiudadId { get; set; }
        public string NombreCiudad { get; set; }
        public bool NecesitaExamen { get; set; }
        public bool NecesitaLaboratorio { get; set; }
        public bool CobroFinanciera { get; set; }
        public string Referencia { get; set; }
        public int ProveedorMedicoId { get; set; }
        public int MedicoDesgravamenId { get; set; }
        public EstadoCita Estado { get; set; }
        public DateTime FechaHoraCita { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaUltimaModificacion { get; set; }
        public bool Aprobado { get; set; }

        public string FechaHoraCitaForDisplay
        {
            get
            {
                return String.Format("{0:dd/MMM/yyyy}", FechaHoraCita); 
            }
        }

        public CitaMedica()
        {
            Estado = EstadoCita.Citada;
            CitaDesgravamenId = 0;
            PropuestoAseguradoId = 0;
            FinancieraId = 0;
            CiudadId = "STC";
            NombreCiudad = "Santa Cruz";
            NecesitaExamen = false;
            NecesitaLaboratorio = false;
            CobroFinanciera = false;
            Referencia = "";
            ProveedorMedicoId = 0;
            MedicoDesgravamenId = 0;
            FechaCreacion = DateTime.MinValue;
            FechaHoraCita = DateTime.MinValue;
            FechaUltimaModificacion = DateTime.MinValue;
            Aprobado = false;
        }

        public static byte[] GetExamenMedicoEnPdf_EVO(int citaId, string urlServer)
        {
            byte[] pdfBytes = null;
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/Desgravamen/ExamenMedicoImprimir.aspx?f=1&cdid=" + citaId);
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
        public static byte[] GetExamenMedicoEnPdf_HIQ(int citaId, string urlServer)
        {
            byte[] pdfBytes = null;
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/Desgravamen/ExamenMedicoImprimir.aspx?f=1&cdid=" + citaId);
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

        public static byte[] GetExamenMedicoEnPdf(int citaId, string urlServer)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
                return GetExamenMedicoEnPdf_EVO(citaId, urlServer);
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetExamenMedicoEnPdf_HIQ(citaId, urlServer);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }
    }
}