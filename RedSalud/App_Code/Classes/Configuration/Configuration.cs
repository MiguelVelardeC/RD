using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Configuration;
using System.Collections;

namespace Artexacta.App.Configuration
{
    /// <summary>
    /// Summary description for Configuration
    /// </summary>
    public class Configuration
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public Configuration()
        {
        }

        public static string GetTempDirectory()
        {
            return ConfigurationManager.AppSettings.Get("TempDirectory");
        }

        public static decimal GetMontoGestion ()
        {
            return decimal.Parse(ConfigurationManager.AppSettings.Get("CoberturaGestion"));
        }

        public static decimal GetMontoGestionFallecido()
        {
            decimal result = 0;
            decimal.TryParse(ConfigurationManager.AppSettings.Get("CoberturaGestionFallecido"), out result);
            
            return result;
        }

        public static int GetFortalezaDesgravamenId()
        {
            int result = 0;
            int.TryParse(ConfigurationManager.AppSettings.Get("FortalezaDesgravamenId"), out result);

            return result;
        }


        public static int[] GetDigitalSignatureDimension()
        {
            int[] result = new int[2];
            string sRAW = "";
            sRAW = ConfigurationManager.AppSettings.Get("FirmaDigitalDimensions");

            string[] arrDimensions = null;

            if(sRAW != null)
                arrDimensions = sRAW.Split('|');

            if (arrDimensions != null && arrDimensions.Length > 1)
            {
                int valueWidth = 0;
                int.TryParse(arrDimensions[0], out valueWidth);
                result[0] = valueWidth;

                int valueHeight = 0;
                int.TryParse(arrDimensions[1], out valueHeight);
                result[1] = valueHeight;
            }
            else
            {
                result[0] = 0;
                result[1] = 0;
            }

            return result;
        }


        /// <summary>
        /// Determines how long the system messages should be displayed to the users, in seconds.
        /// </summary>
        /// <returns>The number of seconds that a system message should be shown to the user</returns>
        public static int GetTimeToExpireSystemMessages()
        {
            int seconds = 360;  // Defaults to five minutes
            try
            {
                string configString = ConfigurationManager.AppSettings.Get("TimeToShowSystemMessages");
                if (configString == null || configString.Length == 0)
                {
                    throw new ConfigurationErrorsException(Resources.Configuration.MensajeErrorArchivoConfiguracion);
                }

                seconds = Convert.ToInt32(configString);
            }
            catch (Exception e)
            {
                log.Error(Resources.Configuration.MensajeErrorNumeroArchivoConfig, e);
            }

            return seconds;
        }

        public static string GetDBConnectionString()
        {

            // The system databse connection string should be in the system 
            // configuration file and should be called SilverTrackConnectionString

            string name = "";

            try
            {
                // Get the connectionStrings.
                ConnectionStringSettingsCollection connectionStrings =
                    ConfigurationManager.ConnectionStrings;

                // Get the collection enumerator.
                IEnumerator connectionStringsEnum = connectionStrings.GetEnumerator();

                // Loop through the collection and search for valid connectionString
                int i = 0;
                while (connectionStringsEnum.MoveNext())
                {
                    name = connectionStrings[i++].Name;
                    string value = connectionStrings[name].ToString();
                    if (name == "RedSaludDBConnectionString")
                    {
                        return value;
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Failed to get the RedSaludDBConnectionString connection string", e);
                throw e;
            }

            string mensaje = "";
            mensaje = "Error de conexion a:" + name;

            throw new ConfigurationErrorsException(mensaje);
        }

        public static string GetReturnEmailAddress()
        {
            return Resources.Configuration.EmailAddress;
        }

        public static string GetReturnEmailName()
        {
            return Resources.Configuration.EmailName;
        }

        public static string GetCreationEmailSubject()
        {
            return "Creación de cuenta en el sistema";
        }

        public static string GetConfirmationPasswordSubject()
        {
            return "Reseteo de contraseña";
        }

        public static int GetAccountLockTime()
        {
            return 1;
        }

        public static string GetHTMLParagraphs(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            text = HttpContext.Current.Server.HtmlEncode(text);

            System.Text.StringBuilder res = new System.Text.StringBuilder();
            string[] paragraphs = text.Split(new char[] { '\n' });

            for (int i = 0; i < paragraphs.Length; i++)
            {
                if (String.IsNullOrEmpty(paragraphs[i]))
                    continue;

                res.Append("<p class=\"displayParagraph\">" + paragraphs[i] + "</p>\n");
            }

            return res.ToString();
        }

        public static string GetFeedbackUrl()
        {
            string feedbackBase = ConfigurationManager.AppSettings.Get("FeedbackBaseUrl");
            if (string.IsNullOrEmpty(feedbackBase))
            {
                log.Error("Feedback is not correctly configured, check web.config");
                feedbackBase = "";
            }
            return feedbackBase += "/Feedback/FeedbackEntry.aspx";
        }

        public static string GetApplicationName()
        {
            string feedbackName = ConfigurationManager.AppSettings.Get("FeedbackApplicationName");
            if (string.IsNullOrEmpty(feedbackName))
            {
                log.Error("Feedback NAME is not correctly configured, check web.config");
                feedbackName = "";
            }
            return feedbackName;
        }

        public static string GetApplicationKey()
        {
            string feedbackKey = ConfigurationManager.AppSettings.Get("FeedbackApplicationKey");
            if (string.IsNullOrEmpty(feedbackKey))
            {
                log.Error("Feedback KEY is not correctly configured, check web.config");
                feedbackKey = "";
            }
            return feedbackKey;
        }

        public static int GetViewStateExpirationForDataBase()
        {
            return 30;
        }

        public static int GetNumeroDiasCasoAbierto ()
        {
            try
            {
                ConfigurationDSTableAdapters.Configuracion conf = new ConfigurationDSTableAdapters.Configuracion();
                return (int)conf.GetNumeroDiasCasoAbierto();
            }
            catch (Exception q)
            {
                log.Error("NumeroDiasCasoAbierto is not correctly configured, check the table tbl_Configuration");
                throw q;
            }
        }

        public static decimal GetPorcentajeSiniestralidadAlerta ()
        {
            try
            {
                ConfigurationDSTableAdapters.Configuracion conf = new ConfigurationDSTableAdapters.Configuracion();
                return (decimal)conf.GetPorcentajeSiniestralidadAlerta();
            }
            catch (Exception q)
            {
                log.Error("PorcentajeSiniestralidadAlerta is not correctly configured, check the table tbl_Configuration");
                throw q;
            }
        }

        public static decimal GetMontoMinimoEnPoliza ()
        {
            try
            {
                ConfigurationDSTableAdapters.Configuracion conf = new ConfigurationDSTableAdapters.Configuracion();
                return (decimal)conf.GetMontoMinimoEnPoliza();
            }
            catch (Exception q)
            {
                log.Error("MontoMinimoEnPoliza is not correctly configured, check the table tbl_Configuration");
                throw q;
            }
        }

        /// <summary>
        /// Get a list of file extensions that are forbidden for upload
        /// </summary>
        /// <returns>The list of extensions or null if there are none</returns>
        public static string[] GetListOfForbiddenFileExtensions ()
        {
            return GetListOfExtensionsForParameter("FileLimitExtensionList");
        }

        /// <summary>
        /// Get a list of file extensions that are allowed for upload
        /// </summary>
        /// <returns>The list of extensions or null if there are none</returns>
        public static string[] GetListOfAllowedFileExtensions ()
        {
            return GetListOfExtensionsForParameter("FileAllowExtensionList");
        }
        private static string[] GetListOfExtensionsForParameter ( string parameter )
        {
            string extensionList = null;
            try
            {
                extensionList = ConfigurationManager.AppSettings.Get(parameter);
                if (extensionList == null || extensionList.Length == 0)
                {
                    return null;
                }

                string[] list = extensionList.Split(new char[] { ',' });
                List<string> cleanExtensionList = new List<string>();
                for (int i = 0; i < list.Length; i++)
                {
                    string extension = list[i].Trim();
                    if (!string.IsNullOrEmpty(extension) && extension.StartsWith("."))
                    {
                        cleanExtensionList.Add(extension);
                    }
                }

                if (cleanExtensionList.Count > 0)
                {
                    return cleanExtensionList.ToArray();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                log.Error("Cannot find a valid " + parameter + " configuration string in system configuration file", e);
                return null;
            }
        }
        /// <summary>
        /// Get the directory where we store all the binary documents that are loaded into the 
        /// digital library
        /// </summary>
        /// <returns>The directory path, as configured in the SWeb.Config file</returns>
        public static string GetDocumentStorageDirectory ()
        {
            return ConfigurationManager.AppSettings.Get("DocumentStorageDirectory");
        }
        public static string GetUploadTempDirectory ()
        {
            return ConfigurationManager.AppSettings.Get("TempDirectory");
        }

        public static string GetAlianzaServer()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaServer");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaServer en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaWebServiceRelativeUrl()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaWebServiceRelativeUrl");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaWebServiceRelativeUrl en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaWebServiceUser()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaWebServiceUser");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaWebServiceUser en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaWebServicePassword()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaWebServicePassword");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaWebServicePassword en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaErrorMailSubject()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaErrorMailSubject");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaErrorMailSubject en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaErrorMailBody(string message)
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaErrorMailBody");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaErrorMailBody en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }

            result = result.Replace("{MESSAGE}", message);

            return result;
        }

        public static string GetAlianzaMailTo()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaMailTo");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaMailTo en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaWebServiceUpdateMailSubject()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaWebServiceUpdateMailSubject");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaMailTo en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return result;
        }

        public static string GetAlianzaWebServiceUpdateMailBody(DateTime dateTime, string resumen)
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaWebServiceUpdateMailBody");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaMailTo en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }

            result = result.Replace("{DATE}", dateTime.ToLongDateString());
            result = result.Replace("{RESUMEN}", resumen);

            return result;
        }

        public static int GetAlianzaClienteId()
        {
            string result = ConfigurationManager.AppSettings.Get("AlianzaClienteId");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina AlianzaClienteId en web.config con un valor valido");
                throw new Exception("Defina AlianzaServer en web.config con un valor valido");
            }
            return Convert.ToInt32(result);
        }

        public static int GetNacionalVidaClienteId ()
        {
            string result = ConfigurationManager.AppSettings.Get("NacionalVidaClienteId");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina NacionalVidaClienteId en web.config con un valor valido");
                throw new Exception("Defina el ClienteId de Nacional Vida en web.config con un valor valido");
            }
            return Convert.ToInt32(result);
        }

        public static DateTime ConvertToClientTimeZone (DateTime defaultTime)
        {
            TimeZoneInfo boZone = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings.Get("ClientTimeZone"));
            return defaultTime.Add(boZone.GetUtcOffset(defaultTime));
            //defaultTime = DateTime.SpecifyKind(defaultTime, DateTimeKind.Unspecified);
            //return TimeZoneInfo.ConvertTimeFromUtc(defaultTime, boZone);
        }

        public static DateTime ConvertToUTCFromClientTimeZone ( DateTime defaultTime )
        {
            TimeZoneInfo boZone = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings.Get("ClientTimeZone"));
            return defaultTime.Add(boZone.GetUtcOffset(defaultTime).Negate());
            /*
            defaultTime = DateTime.SpecifyKind(defaultTime, DateTimeKind.Unspecified);
            return TimeZoneInfo.ConvertTimeToUtc(defaultTime, boZone);
             * */
        }

        public static DateTime ConvertToUTCFromServerTimeZone ( DateTime defaultTime )
        {
            TimeZoneInfo localZone = TimeZoneInfo.Local;

            return defaultTime.Add(localZone.GetUtcOffset(defaultTime).Negate());
        }

        public static int GetDESGNumeroDiasHoraLibre()
        {
            int result = 5;
            string resultStr = ConfigurationManager.AppSettings.Get("DESGNumeroDiasHoraLibre");
            if (string.IsNullOrWhiteSpace(resultStr))
            {
                log.Warn("Defina DESGNumeroDiasHoraLibre en web.config con un entero. Por defecto ahora utiliza el valor 5");
                return result;
            }
            try
            {
                result = Convert.ToInt32(resultStr);
                return result;
            }
            catch 
            {
                log.Warn("El valor de DESGNumeroDiasHoraLibre no es un entero, se usará 5");
                return 5;
            }
        }

        public static string GetDESGExecutiveRoleName()
        {
            string resultStr = ConfigurationManager.AppSettings.Get("DESGExecutiveRoleName");

            return resultStr;
        }



        public static string GetNacionalDesgravamenMail()
        {
            string result = ConfigurationManager.AppSettings.Get("NacionalDesgravamenMail");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina NacionalDesgravamenMail en web.config con un valor valido");
                throw new Exception("Defina NacionalDesgravamenMail en web.config con un valor valido");
            }
            return result;
        }

        public static string GetLibraryPdf()
        {
            string result = ConfigurationManager.AppSettings.Get("LibraryPDF");
            if (string.IsNullOrWhiteSpace(result))
            {
                log.Error("Defina LibraryPDF en web.config con un valor valido (EVO, HIQ), por defecto EVO");
                return "EVO";
            }
            return result;
        }

        public static string GetLibraryPdfKey()
        {
            string libPdf = GetLibraryPdf();
            string libPdfKey = ConfigurationManager.AppSettings.Get("LibraryPDFKey");
            if (string.IsNullOrWhiteSpace(libPdfKey))
            {
                if (libPdf == "EVO")
                    return "4W9+bn19bn5ue2B+bn1/YH98YHd3d3c=";
                if (libPdf == "HIQ")
                    return "YCgJMTAE-BiwJAhIB-EhlWTlBA-UEBRQFBA-U1FOUVJO-WVlZWQ==";
            } else
                return libPdfKey;

            return "";
        }

        public static int GetHusoHorario()
        {
            
            int hh = 0;
            try
            {
                string husoHorario = ConfigurationManager.AppSettings.Get("HusoHorario");
                hh = Convert.ToInt32(husoHorario);
                return hh;
            }
            catch (Exception q)
            {
                log.Warn("No se encuentra bien configurada la variable HusoHorario en el web.config", q);
                return 0;
            }
        }
    }
}