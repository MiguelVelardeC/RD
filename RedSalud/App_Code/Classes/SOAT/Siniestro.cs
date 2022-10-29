using EvoPdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Siniestro
{
    public class Siniestro
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        private int _SiniestroId;
        private int _ClienteId;
        private DateTime _FechaSiniestro;
        private DateTime _FechaDenuncia;
        private string _TipoCausa;
        private string _Causa;
        private string _LugarDpto;
        private string _LugarProvincia;
        private string _Zona;
        private string _Sindicato;
        private string _operacionId;
        private string _numeroRoseta;
        private string _numeroPoliza;
        private string _LugarVenta;
        private string _nombreTitular;
        private string _ciTitular;
        private string _placa;
        private string _tipo;
        private string _cilindrada;
        private string _sector;

        public int SiniestroId
        {
            get { return this._SiniestroId; }
            set { this._SiniestroId = value; }
        }
        public int ClienteId
        {
            get { return this._ClienteId; }
            set { this._ClienteId = value; }
        }
        public DateTime FechaSiniestro
        {
            get { return this._FechaSiniestro; }
            set { this._FechaSiniestro = value; }
        }
        public DateTime FechaDenuncia
        {
            get { return this._FechaDenuncia; }
            set { this._FechaDenuncia = value; }
        }
        public string TipoCausa
        {
            get { return this._TipoCausa; }
            set { this._TipoCausa = string.IsNullOrWhiteSpace(value) ? "" : "" + value.ToCharArray()[0]; }
        }
        public string Causa
        {
            get { return this._Causa; }
            set { this._Causa = value; }
        }
        public string LugarDpto
        {
            get { return this._LugarDpto; }
            set { this._LugarDpto = value; }
        }
        public string LugarProvincia
        {
            get { return this._LugarProvincia; }
            set { this._LugarProvincia = value; }
        }
        public string Zona
        {
            get { return this._Zona; }
            set { this._Zona = value; }
        }
        public string Sindicato
        {
            get { return this._Sindicato; }
            set { this._Sindicato = value; }
        }
        public bool HasSindicato
        {
            get { return !string.IsNullOrWhiteSpace(this._Sindicato); }
        }

        public string OperacionId
        {
            get { return _operacionId; }
            set { _operacionId = value; }
        }

        public string NumeroRoseta
        {
            get { return _numeroRoseta; }
            set { _numeroRoseta = value; }
        }

        public string LugarVenta
        {
            get { return this._LugarVenta; }
            set { this._LugarVenta = value; }
        }

        public string NombreTitular
        {
            get { return _nombreTitular; }
            set { _nombreTitular = value; }
        }

        public string CITitular
        {
            get { return _ciTitular; }
            set { _ciTitular = value; }
        }

        public string NumeroPoliza
        {
            get { return _numeroPoliza; }
            set { _numeroPoliza = value; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Cilindrada
        {
            get { return this._cilindrada; }
            set { this._cilindrada = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string NroMotor { get; set; }

        public string NroChasis { get; set; }

        public string NombreInspector { get; set; }

        public Siniestro() {}

        public Siniestro(int siniestroId, int clienteId, DateTime fechaSiniestro, DateTime fechaDenuncia, string causa, string lugarDpto, string lugarProvincia, 
                            string zona, string sindicato, string operacionId, string numeroRoseta, string numeroPoliza, string lugarVenta, 
                            string nombreTitular, string ciTitular, string placa, string tipo, string nroChasis, string nroMotor, string sector, string nombreInspector )
        {
            this._SiniestroId = siniestroId;
            this._ClienteId = clienteId;
            this._FechaSiniestro = fechaSiniestro;
            this._FechaDenuncia = fechaDenuncia;
            this._TipoCausa = string.IsNullOrWhiteSpace(causa) ? "" : "" + causa.ToCharArray()[0];
            this._Causa = string.IsNullOrWhiteSpace(causa) ? "" : causa.Substring(1);
            this._LugarDpto = lugarDpto;
            this._LugarProvincia = lugarProvincia;
            this._Zona = zona;
            this._Sindicato = sindicato;
            this._operacionId = operacionId;
            this._numeroRoseta = numeroRoseta;
            this._numeroPoliza = numeroPoliza;
            this._LugarVenta = lugarVenta;
            this._nombreTitular = nombreTitular;
            this._ciTitular = ciTitular;
            this._placa = placa;
            this._tipo = tipo;
            //this._cilindrada = cilindrada;
            this.NroChasis = nroChasis;
            this.NroMotor = nroMotor;
            this.NombreInspector = nombreInspector;
            this._sector = sector;
        }

        public static byte[] GetSiniestroEnPdf_EVO(EnlaceSOATSISA objEnlace, string urlServer)
        {
            byte[] pdfBytes = null;
            string queryString = "siniestro=" + objEnlace.SiniestroId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/SOAT/SiniestroImprimir.aspx?" + queryString);
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
        public static byte[] GetSiniestroEnPdf_HIQ(string urlToConvert)
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

        public static byte[] GetSiniestroEnPdf_HIQ(EnlaceSOATSISA objEnlace, string urlServer)
        {

            string queryString = "siniestro=" + objEnlace.SiniestroId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/SOAT/SiniestroImprimir.aspx?" + queryString);
            urlToConvert = urlToConvert.Replace(" ", "%20");

            return GetSiniestroEnPdf_HIQ(urlToConvert);
        }

        public static byte[] GetSiniestroEnPdf(EnlaceSOATSISA objEnlace, string urlServer)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
                return GetSiniestroEnPdf_EVO(objEnlace, urlServer);
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetSiniestroEnPdf_HIQ(objEnlace, urlServer);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }

        public static byte[] GetSiniestroEnPdf(string urlToConvert)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetSiniestroEnPdf_HIQ(urlToConvert);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }
    }
}