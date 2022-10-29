using EvoPdf;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Artexacta.App.Accidentado
{
    public class Accidentado
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        private int _accidentadoId;
        private string _nombre;
        private string _carnetIdentidad;
        private bool _genero;
        private DateTime _fechaNacimiento;
        private string _estadoCivil;
        private char _licenciaConducir;
        private bool _conductor;
        private char _tipo;
        private bool _estado;
        private decimal _SiniestroPreliquidacion;
        private decimal _SiniestroPagado;
        private string _estadoSeguimiento;
        private int _fileCount;

        public Accidentado () { }

        public Accidentado ( int accidentadoId, string nombre, string carnetIdentidad, bool genero, DateTime fechaNacimiento,
                             string estadoCivil, char licenciaConducir, bool titular, string tipo, bool estado )
        {
            this._accidentadoId = accidentadoId;
            this._nombre = nombre;
            this._carnetIdentidad = carnetIdentidad;
            this._genero = genero;
            this._fechaNacimiento = fechaNacimiento;
            this._estadoCivil = estadoCivil;
            this._licenciaConducir = licenciaConducir;
            this._conductor = titular;
            this._tipo = tipo.ToCharArray()[0];
            this._estado = estado;
        }


        public int AccidentadoId
        {
            get { return _accidentadoId; }
            set { _accidentadoId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string CarnetIdentidad
        {
            get { return _carnetIdentidad; }
            set { _carnetIdentidad = value; }
        }

        public bool Genero
        {
            get { return _genero; }
            set { _genero = value; }
        }

        public DateTime FechaNacimiento
        {
            get { return _fechaNacimiento; }
            set { _fechaNacimiento = value; }
        }

        public string EstadoCivil
        {
            get { return _estadoCivil; }
            set { _estadoCivil = value; }
        }

        public string LicenciaConducir
        {
            get { return "" + _licenciaConducir; }
            set { _licenciaConducir = value.Length > 0 ? value.ToCharArray()[0] : '-'; }
        }

        public bool Conductor
        {
            get { return _conductor; }
            set { _conductor = value; }
        }

        public string Tipo
        {
            get { return "" + _tipo; }
            set { _tipo = value.ToCharArray()[0]; }
        }

        public bool Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public decimal SiniestroPagado
        {
            get { return _SiniestroPagado; }
            set { _SiniestroPagado = value; }
        }

        public string SiniestroPagadoForDisplay
        {
            get { return _SiniestroPagado.ToString("#,##0.00"); }
        }

        public decimal SiniestroPreliquidacion
        {
            get { return _SiniestroPreliquidacion; }
            set { _SiniestroPreliquidacion = value; }
        }

        public string SiniestroPreliquidacionForDisplay
        {
            get { return _SiniestroPreliquidacion.ToString("#,##0.00"); }
        }

        public string EstadoSeguimiento
        {
            get { return _estadoSeguimiento; }
            set { _estadoSeguimiento = value; }
        }

        public string EstadoForDisplay
        {
            get
            {
                string Result = "";

                if (_tipo == '-')
                    Result = " - ";

                if (_estado && !IsIncapacidadTotal)
                {
                    Result = "ACCIDENTADO";
                }
                else if (_estado && IsIncapacidadTotal)
                {
                    Result = "INCAPACIDAD TOTAL";
                }
                else if (!_estado)
                {
                    Result = "FALLECIDO";
                }

                return Result;
            }
        }

        public string TipoForDisplay
        {
            get { return _tipo == 'E' ? "PEATON" : (_tipo == 'A' ? "PASAJERO" : " - "); }
        }

        public string GeneroForDisplay
        {
            get { return _genero ? "MASCULINO" : "FEMENINO"; }
        }

        public string FechaNacimientoForDisplay
        {
            get {
                if (_fechaNacimiento == SqlDateTime.MinValue)
                    return "-";
                return _fechaNacimiento.ToString("dd/MMM/yyyy"); 
            }
        }

        public string LicenciaConducirForDisplay
        {
            get { return _licenciaConducir == '-' ? " - " : (_licenciaConducir == 'S' ? "SI" : "NO"); }
        }

        public string NombreForDisplay
        {
            get { return _nombre + (_conductor ? " (CONDUCTOR)" : ""); }
        }
        public int FileCount
        {
            set { _fileCount = value; }
            get { return _fileCount; }
        }
        public string FileCountForDisplay
        {
            get { return _fileCount.ToString("00"); }
        }

        public int SiniestroId { get; set; }
        public decimal Reserva { get; set; }
        public bool IsIncapacidadTotal { get; set; }

        public static byte[] GetAccidentadoEnPdf_EVO(EnlaceSOATSISA objEnlace, string urlServer)
        {
            byte[] pdfBytes = null;
            string queryString = "siniestro=" + objEnlace.SiniestroId.ToString()+"&accidentado="+objEnlace.AccidentadoId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/SOAT/AccidentadoImprimir.aspx?" + queryString);
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
        public static byte[] GetAccidentadoEnPdf_HIQ(string urlToConvert)
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

        public static byte[] GetAccidentadoEnPdf_HIQ(EnlaceSOATSISA objEnlace, string urlServer)
        {

            string queryString = "siniestro=" + objEnlace.SiniestroId.ToString();
            //string queryString = "PacienteId=" + objEnlace.PacienteId.ToString() +
            //    "&CasoId=" + objEnlace.CasoId.ToString() + "&VM=P";
            string urlToConvert = urlServer + VirtualPathUtility.ToAbsolute("~/SOAT/SiniestroImprimir.aspx?" + queryString);
            urlToConvert = urlToConvert.Replace(" ", "%20");

            return GetAccidentadoEnPdf_HIQ(urlToConvert);
        }

        public static byte[] GetAccidentadoEnPdf(EnlaceSOATSISA objEnlace, string urlServer)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "EVO")
                return GetAccidentadoEnPdf_EVO(objEnlace, urlServer);
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetAccidentadoEnPdf_HIQ(objEnlace, urlServer);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }

        public static byte[] GetAccidentadoEnPdf(string urlToConvert)
        {
            if (Artexacta.App.Configuration.Configuration.GetLibraryPdf() == "HIQ")
                return GetAccidentadoEnPdf_HIQ(urlToConvert);

            log.Warn("No se definio la variable libraryPDF en el web conf convalores validos (EVO o HIQ)");
            return null;
        }
    }
}
