using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Helper
{
    public class Log
    {
        public Log()
        {}
        static Log()
        {}

        public static void Guardar(string Mensaje, string Usuario, string Origen,string TipoLog = "")
        {
            try
            {
                if (string.IsNullOrEmpty(Usuario))
                {
                    Usuario = "_";
                }
                if (string.IsNullOrEmpty(Origen))
                {
                    Origen = "_";
                }
                string log = string.Format("{0} || {1} || {2} || MENSAJE [{3}]", DateTime.Now, Usuario, Origen, Mensaje);
                string fileName = string.Format("{0}_{1}_{2}_Log.txt",DateTime.Now.Year,DateTime.Now.Month,TipoLog);
                string fileDirectory = System.Configuration.ConfigurationManager.AppSettings["ExcepcionesDir"];
                if (string.IsNullOrEmpty(fileDirectory))
                {
                    fileDirectory = @"C:\";
                } 
                using (StreamWriter writer = new StreamWriter(string.Format("{0}{1}",fileDirectory,fileName) , true))
                {
                    writer.WriteLine(log);
                }
            }
            catch 
            {}
        }
        public static void Guardar(Exception ex, string Usuario, string Origen, string TipoLog = "")
        {
            string inner = "";
            try
            {
                inner = JsonConvert.SerializeObject(ex.InnerException);
            }
            catch { }
            string stackTrace = "";
            try
            {
                stackTrace = JsonConvert.SerializeObject(ex.StackTrace);
            }
            catch { }
            string msg = string.Format("Source [{0}], Exception [{1}], InnerException [{2}], StackTrace [{3}],", ex.Source, ex.Message, inner, stackTrace);
            string efError = "";
            if (ex is DbEntityValidationException)
            {
                var dbEx = ex as DbEntityValidationException;
                foreach (DbEntityValidationResult item in dbEx.EntityValidationErrors)
                {
                    foreach (DbValidationError error in item.ValidationErrors)
                    {
                        string emsg = string.Format(" {0} | {1},", error.PropertyName, error.ErrorMessage);
                        efError += emsg;
                    }
                }
                msg = string.Format("Source [{0}], Exception [{1}], InnerException [{2}], Other [{3}],", dbEx.Source, dbEx.Message
                , dbEx.InnerException == null ? "" : dbEx.InnerException.Message
                , efError);
            }
            Guardar(msg, Usuario, Origen, TipoLog);
        }
        public static void Guardar(string Mensaje, string Usuario, string Origen, string TipoLog, string CorreoTO)
        {
            Guardar(Mensaje,Usuario,Origen,TipoLog);
            string Respuesta = string.Empty;
        }
    }
}
