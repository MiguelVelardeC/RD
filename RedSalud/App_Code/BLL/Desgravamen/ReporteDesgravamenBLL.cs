using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Artexacta.App.Estudio;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ReporteDesgravamenBLL
    /// </summary>
    public class ReporteDesgravamenBLL
    {
        public ReporteDesgravamenBLL()
        {
            
        }

        public static List<ReporteCantidadCitasPorMedico> GetReporteCantidadCitasPorMedico(DateTime dtFechaInicial, DateTime dtFechaFinal, string strCiudadId, int intFinancieraId, int intClienteId)
        {
            /*var minYear = DateTime.MinValue.Year;
            if(year < minYear)
            {
                throw new ArgumentException("year cannot be less than " + minYear);
            }*/
            ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorMedicoTableAdapter adapter =
                new ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorMedicoTableAdapter();

            ReporteDesgravamenDS.ReporteCantidadCitasPorMedicoDataTable table = adapter.ReporteCantidadCitasPorMedico(dtFechaInicial, dtFechaFinal, strCiudadId, intFinancieraId,intClienteId);
            List<ReporteCantidadCitasPorMedico> list = new List<ReporteCantidadCitasPorMedico>();
            foreach (var row in table)
            {
                ReporteCantidadCitasPorMedico obj = new ReporteCantidadCitasPorMedico()
                {
                    Medico = row.medico,
                    Cantidad = row.cantidad
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<ReporteCantidadCitasPorFinanceraCiudad> GetReporteCantidadCitasPorFinanceraCiudad(DateTime dtFechaInicial, DateTime dtFechaFinal, string strCiudadId, int intFinancieraId, int intClienteId)
        {
            /*var minYear = DateTime.MinValue.Year;
            if(year < minYear)
            {
                throw new ArgumentException("year cannot be less than " + minYear);
            }*/
            ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorFinanceraCiudadTableAdapter adapter =
                new ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorFinanceraCiudadTableAdapter();

            ReporteDesgravamenDS.ReporteCantidadCitasPorFinanceraCiudadDataTable table = adapter.ReporteCantidadCitasPorFinanceraCiudad(dtFechaInicial, dtFechaFinal, strCiudadId, intFinancieraId, intClienteId);
            List<ReporteCantidadCitasPorFinanceraCiudad> list = new List<ReporteCantidadCitasPorFinanceraCiudad>();
            foreach (var row in table)
            {
                ReporteCantidadCitasPorFinanceraCiudad obj = new ReporteCantidadCitasPorFinanceraCiudad()
                {
                    Financiera = row.FINANCIERA,
                    Ciudad = row.CIUDAD,
                    Cantidad = row.CANTIDAD
                };
                list.Add(obj);
            }
            return list;
        }

        private static EstudioPrecio FillRecord(EstudiosDS.Estudio_PPPRow row)
        {
            EstudioPrecio objEstudioPrecio = new EstudioPrecio(
                row.nomestudio,
                 row.precioxplaza,
                 row.ciudad,
                 row.precioxciudad,
                 row.tipo
                );
            return objEstudioPrecio;
        }
        public static List<EstudioPrecio> getEstudioPrecioList()
        {
            List<EstudioPrecio> theList = new List<EstudioPrecio>();
            EstudioPrecio theEstudioPrecio = null;

            EstudiosDSTableAdapters.Estudio_PPPTableAdapter adapter = new EstudiosDSTableAdapters.Estudio_PPPTableAdapter();

                EstudiosDS.Estudio_PPPDataTable thetable = adapter.GetDataEstudioPrecioPP();

                if (thetable != null && thetable.Rows.Count > 0)
                {
                    foreach (EstudiosDS.Estudio_PPPRow row in thetable.Rows)
                    {
                        theEstudioPrecio = FillRecord(row);
                        theList.Add(theEstudioPrecio);
                    }
                }
           
            return theList;
        }

        public static DataTable GetReporteCantidadEstudiosPorPA(string whereSql, DateTime fechaInicio, DateTime fechaFin, int proveedorMedicoId, int clienteId, string ciudadId, int financieraId, string cobroCliente, int citaId, string propuestoAsegurado)
        {
            DataTable theTable = new DataTable();
            using (SqlConnection theConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RedSaludDBConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DESG_ReporteCantidadEstudiosPorPA", theConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@whereSql", SqlDbType.NVarChar).Value = whereSql;
                    cmd.Parameters.Add("@datFechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                    cmd.Parameters.Add("@datFechaFin", SqlDbType.DateTime).Value = fechaFin;
                    cmd.Parameters.Add("@intProveedorMedicoId", SqlDbType.Int).Value = proveedorMedicoId;
                    cmd.Parameters.Add("@intClienteId", SqlDbType.Int).Value = clienteId;
                    cmd.Parameters.Add("@varCiudad", SqlDbType.VarChar).Value = ciudadId;
                    cmd.Parameters.Add("@intFinancieraId", SqlDbType.Int).Value = financieraId;
                    cmd.Parameters.Add("@varCobroCliente", SqlDbType.VarChar).Value = cobroCliente;
                    cmd.Parameters.Add("@intCitaId", SqlDbType.Int).Value = citaId;
                    if(!string.IsNullOrEmpty(propuestoAsegurado))
                        cmd.Parameters.Add("@varPropuestoAsegurado", SqlDbType.VarChar).Value = propuestoAsegurado;
                    theConnection.Open();

                    using (SqlDataAdapter theAdapter = new SqlDataAdapter())
                    {
                        theAdapter.SelectCommand = cmd;

                        theAdapter.Fill(theTable);
                    }
                }
            }
            return theTable;
        }
        //

        public static DataTable GetListarPrecioEstudio()
        {
            DataTable theTable = new DataTable();
            using (SqlConnection theConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RedSaludDBConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_listar_estudio_precio", theConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    theConnection.Open();

                    using (SqlDataAdapter theAdapter = new SqlDataAdapter())
                    {
                        theAdapter.SelectCommand = cmd;

                        theAdapter.Fill(theTable);
                    }
                }
            }
            return theTable;
        }

        public static DataTable GetReporteCantidadEstudiosPorFinanciera(DateTime fechaInicio, DateTime fechaFin, int clienteId, int financieraId, int estudioId, bool tipoProducto, int intCobroFinanciera)
        {
            DataTable theTable = new DataTable();
            using (SqlConnection theConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RedSaludDBConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_DESG_ReporteCantidadEstudiosPorFinanciera", theConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@datFechaInicio", SqlDbType.DateTime).Value = fechaInicio;
                    cmd.Parameters.Add("@datFechaFin", SqlDbType.DateTime).Value = fechaFin;
                    cmd.Parameters.Add("@intClienteId", SqlDbType.Int).Value = clienteId;
                    cmd.Parameters.Add("@intFinancieraId", SqlDbType.Int).Value = financieraId;
                    cmd.Parameters.Add("@intEstudioId", SqlDbType.Int).Value = estudioId;
                    cmd.Parameters.Add("@intCobroFinanciera", SqlDbType.Int).Value = intCobroFinanciera;
                    //if (!string.IsNullOrEmpty(tipoProducto))
                    cmd.Parameters.Add("@CodigoTipoProducto", SqlDbType.Bit).Value = (tipoProducto?1:0);

                    theConnection.Open();

                    using (SqlDataAdapter theAdapter = new SqlDataAdapter())
                    {
                        theAdapter.SelectCommand = cmd;

                        theAdapter.Fill(theTable);
                    }
                }
            }
            return theTable;
        }

        public static List<ReporteEstadoEstudios> GetReporteEstadoEstudios(DateTime datFechaInicioAtencion, DateTime datFechaFinAtencion, DateTime datFechaInicioCita, DateTime datFechaFinCita, int intClienteId, int intEstudioId, string varCiudad, bool bitIncludeFechaCita, int bitEstadoAprobado, int bitEstadoRealizado, int intProveedorMedicoId)
        {
            ReporteDesgravamenDSTableAdapters.ReporteEstadoEstudiosTableAdapter adapter = 
                new ReporteDesgravamenDSTableAdapters.ReporteEstadoEstudiosTableAdapter();


            bool? isAprobado;
            bool? isRealizado;

            if (bitEstadoAprobado >= 0)
            {
                isAprobado = Convert.ToBoolean(bitEstadoAprobado);
            }
            else
            {
                isAprobado = null;
            }

            if (bitEstadoRealizado >= 0)
            {
                isRealizado = Convert.ToBoolean(bitEstadoRealizado);
            }
            else
            {
                isRealizado = null;
            }

            ReporteDesgravamenDS.ReporteEstadoEstudiosDataTable table =
                adapter.ReporteEstadoEstudios(datFechaInicioAtencion, datFechaFinAtencion, datFechaInicioCita, datFechaFinCita, intClienteId, intEstudioId, varCiudad, bitIncludeFechaCita, isAprobado, isRealizado, intProveedorMedicoId);

            List<ReporteEstadoEstudios> list = new List<ReporteEstadoEstudios>();
            if (table != null && table.Rows.Count > 0)
            {
                foreach (var row in table)
                {
                    ReporteEstadoEstudios obj = new ReporteEstadoEstudios()
                    {
                        CitaDesgravamenId = row.citaDesgravamenId,
                        CiudadId = row.ciudadId,
                        CiudadNombre = row.ciudadNombre,
                        ProveedorMedicoId = row.proveedorMedicoId,
                        ProveedorMedicoNombre = row.proveedorMedicoNombre,
                        NombreCompleto = row.nombreCompleto,
                        CarnetIdentidad = row.carnetIdentidad,
                        FinancieraId = row.financieraId,
                        FinancieraNombre = row.financieraNombre,
                        TipoId = row.tipoId,
                        TipoProductoDescripcion = row.tipoProductoDescripcion,
                        EstudioId = row.estudioId,
                        EstudioNombre = row.estudioNombre,
                        FechaCita = row.fechaCita,
                        FechaAtencion = row.fechaAtencion,
                        FechaRealizado = row.fechaRealizado,
                        Aprobado = row.aprobado,
                        EstadoAprobado = row.estadoAprobacion,
                        Realizado = row.realizado,
                        EstadoRealizado = row.estadoRealizado
                    };
                    list.Add(obj);
                }
            }

            /*ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorMedicoTableAdapter adapter =
                new ReporteDesgravamenDSTableAdapters.ReporteCantidadCitasPorMedicoTableAdapter();

            ReporteDesgravamenDS.ReporteCantidadCitasPorMedicoDataTable table = adapter.ReporteCantidadCitasPorMedico(dtFechaInicial, dtFechaFinal, strCiudadId, intFinancieraId, intClienteId);
            List<ReporteCantidadCitasPorMedico> list = new List<ReporteCantidadCitasPorMedico>();
            foreach (var row in table)
            {
                ReporteCantidadCitasPorMedico obj = new ReporteCantidadCitasPorMedico()
                {
                    Medico = row.medico,
                    Cantidad = row.cantidad
                };
                list.Add(obj);
            }*/
            return list;
        }
    }
}