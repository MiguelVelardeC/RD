using Artexacta.App.Utilities.Text;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using Artexacta.App.Utilities.Import;
using System.Text;

namespace Artexacta.App.WebServices.alianza.BLL
{
    
    /// <summary>
    /// Summary description for EstadoAseguradoBLL
    /// </summary>
    public class EstadoAseguradoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EstadoAseguradoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string GetCsvLine (DataRow row)
        {
            string line = "";

            foreach (var item in row.ItemArray)
            {
                line += item.ToString() + "|";
            }
            return line.Remove(line.Length - 1);
        }

        public static DataTable GetDataTableFromWebService(ref int nbNoValidos, ref string errors)
        {
            bo.com.alianza.quality.InsuredSend querySrv = new bo.com.alianza.quality.InsuredSend();

            string srv = "";
            string relativeUrl = "";
            string usr = "";
            string pwd = "";
            try
            {
                srv = App.Configuration.Configuration.GetAlianzaServer();
                relativeUrl = App.Configuration.Configuration.GetAlianzaWebServiceRelativeUrl();
                usr = App.Configuration.Configuration.GetAlianzaWebServiceUser();
                pwd = App.Configuration.Configuration.GetAlianzaWebServicePassword();
            }
            catch (Exception q)
            {
                log.Error("Error reading config: ", q);
                throw q;
            }
            querySrv.Url = srv + relativeUrl;

            DataSet theData = new DataSet();
            StringReader theReader = null;
            try
            {
                XmlElement obj = querySrv.reaInsuredAMMicroinsurance(usr, pwd);

                theReader = new StringReader(obj.InnerXml);
                theData.ReadXml(theReader);
            }
            catch (Exception q)
            {
                log.Error("Error reading xml from alianza", q);
                throw q;
            }
            finally
            {
                if (theReader != null)
                    theReader.Close();
            }

            DataTable theTable = theData.Tables[0];
            DataTable returnTable = new DataTable();
            ImportElement.AddColumns(returnTable);

            string[] columnNames = ImportElement.GetAlianzaColumnNames();
            foreach (DataRow row in theTable.Rows)
            {
                var newRow = returnTable.NewRow();
                string minErrors = "";
                ImportElement.FillFromRow(row, ref newRow, ref minErrors);
                if (string.IsNullOrEmpty(errors))
                {
                    returnTable.Rows.Add(newRow);
                }
                else
                {
                    errors += minErrors;
                    nbNoValidos++;
                }
            }

            return returnTable;
        }

        public static List<EstadoAsegurado> GetListFromWebService()
        {
            List<EstadoAsegurado> result = new List<EstadoAsegurado>();
            bo.com.alianza.quality.InsuredSend querySrv = new bo.com.alianza.quality.InsuredSend();

            string srv = "";
            string relativeUrl = "";
            string usr = "";
            string pwd = "";
            try
            {
                srv = App.Configuration.Configuration.GetAlianzaServer();
                relativeUrl = App.Configuration.Configuration.GetAlianzaWebServiceRelativeUrl();
                usr = App.Configuration.Configuration.GetAlianzaWebServiceUser();
                pwd = App.Configuration.Configuration.GetAlianzaWebServicePassword();
            }
            catch (Exception q)
            {
                log.Error("Error reading config: ", q);
                throw q;
            }
            querySrv.Url = srv + relativeUrl;

            DataSet theData = new DataSet();
            StringReader theReader = null;
            try
            {
                XmlElement obj = querySrv.reaInsuredAMMicroinsurance(usr, pwd);

                theReader = new StringReader(obj.InnerXml);
                theData.ReadXml(theReader);
            }
            catch (Exception q)
            {
                log.Error("Error reading xml from alianza", q);
                throw q;
            }
            finally
            {
                if (theReader != null)
                    theReader.Close();
            }

            DataTable theTable = theData.Tables[0];
            foreach (DataRow row in theTable.Rows)
            {
                EstadoAsegurado objNuevo = FillEstadoAseguradoFromRow(row);
                result.Add(objNuevo);
            }

            return result;
        }

        private static EstadoAsegurado FillEstadoAseguradoFromRow(DataRow row)
        {
            EstadoAsegurado obj = new EstadoAsegurado();
            try
            {
                obj.nBranch = (row["nBranch"] == null ? -1 : Convert.ToInt32(row["nBranch"]));
                obj.nProduct = (row["nProduct"] == null ? -1 : Convert.ToInt32(row["nProduct"]));
                obj.nPolicy = (row["nPolicy"] == null ? -1 : Convert.ToInt32(row["nPolicy"]));
                obj.nCertif = (row["nCertif"] == null ? -1 : Convert.ToInt32(row["nCertif"]));
                obj.dEffectdate = (row["dEffecdate"] == null ? DateTime.MinValue : Convert.ToDateTime(row["dEffecdate"]));
                obj.dExpirdat = (row["dExpirdat"] == null ? DateTime.MinValue : Convert.ToDateTime(row["dExpirdat"]));
                obj.sClient = (row["sClient"] == null ? "" : Convert.ToString(row["sClient"]));
                obj.sCliename = (row["sCliename"] == null ? "" : Convert.ToString(row["sCliename"]));
                obj.nRates = (row["nRate"] == null ? Convert.ToDecimal(-1.0) : Convert.ToDecimal(row["nRate"]));
                obj.sStatus = (row["sStatus"] == null ? "" : Convert.ToString(row["sStatus"]));
            }
            catch (Exception q)
            {
                log.Error("Error converting data from webservice", q);
            }

            obj.validate();
            return obj;
        }

        public static bool ActualizarRegistroDeWebServiceAlianza(Poliza.Poliza objPoliza, EstadoAsegurado obj)
        {
            string nuevoEstado = obj.Activo ? "Activo" : "Inactivo";

            try
            {
                Poliza.BLL.PolizaBLL.UpdatePoliza(objPoliza.PolizaId, objPoliza.NumeroPoliza,
                objPoliza.FechaInicio, objPoliza.FechaFin,
                objPoliza.MontoTotal, objPoliza.MontoFarmacia, objPoliza.Cobertura, objPoliza.NombrePlan, objPoliza.Lugar, 
                    objPoliza.CodigoAsegurado, objPoliza.ClienteId, objPoliza.Relacion, nuevoEstado, DateTime.Now);

                return true;
            }
            catch (Exception q)
            {
                log.Error("Aquí el error al acutalizar poliza " + obj.ToString(), q);
                return false;
            }
        }

        public static bool InsertarRegistroDeWebServiceAlianza(EstadoAsegurado obj)
        {
            char[] comma = { ','};
            //string[] apellidoNombre = obj.sCliename.Split(comma);

            Paciente.Paciente objPaciente = new Paciente.Paciente();
           // objPaciente.Apellido = apellidoNombre[0];
            objPaciente.Nombre = obj.sCliename;//apellidoNombre[1];
            objPaciente.Direccion = "NA";
            objPaciente.Telefono = "NA";
            objPaciente.EstadoCivil = "SOLTERO";
            objPaciente.FechaNacimiento = SqlDateTime.MinValue.Value;

            int idPaciente = 0;
            int idCliente = App.Configuration.Configuration.GetAlianzaClienteId();

            try
            {
                idPaciente = Paciente.BLL.PacienteBLL.InsertOrGetPaciente(objPaciente);
                if (idPaciente <= 0)
                    throw new Exception("NO se pudo crear el paciente " + objPaciente.Nombre);
            }
            catch (Exception q)
            {
                log.Error("InsertOrGetPaciente", q);
                return false;
            }

            string estado = obj.Activo ? "ACTIVO" : "INACTIVO";

            string relacionPoliza = obj.nCertif > 0 ? "DEPENDIENTE" : "TITULAR";

            int idPoliza = 0;

            idPoliza = Poliza.BLL.PolizaBLL.InsertPoliza(obj.sClient, idCliente, idPaciente, relacionPoliza,
                obj.nPolicy.ToString(), obj.dEffectdate, obj.dExpirdat, 5000, 0, "", obj.nProduct.ToString() + "-" + obj.nBranch.ToString(),
                "", estado, DateTime.Now);

            return true;
        }
    }
}