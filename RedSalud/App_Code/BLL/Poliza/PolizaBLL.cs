using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Poliza.BLL
{
    /// <summary>
    /// Summary description for PolizaBLL
    /// </summary>
    public class PolizaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PolizaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private static Poliza FillRecord(PolizaDS.PolizaRow row)
        {
            DateTime FechaMax = DateTime.MaxValue;
            Poliza objPoliza = new
                Poliza(
                    row.PolizaId
                    ,row.NumeroPoliza
                    ,row.AseguradoId
                    ,row.IsFechaInicioNull()? DateTime.MinValue: row.FechaInicio
                    ,row.IsFechaFinNull()? FechaMax:row.FechaFin
                    ,row.MontoTotal
                    ,row.MontoFarmacia
                    ,row.Cobertura
                    ,row.NombrePlan
                    ,row.Lugar
                    ,row.IsEstadoNull()? "Inactivo":row.Estado
                    ,row.Codigo
                    ,row.ClienteId
                    ,row.PacienteId
                    ,row.Relacion
                    ,row.Nombre
                    , row.IsCarnetIdentidadNull()? "": row.CarnetIdentidad
                    , row.NombreJuridico
                    ,row.IsGastoTotalNull()? 0:row.GastoTotal
                );

            return objPoliza;
        }

        public static int GetPolizaBySearchByPolizaOrAseguradoOrPaciente ( ref List<Poliza> _cache, string search, int pageSize, int firstRow, string orderBy )
        {
            if (string.IsNullOrEmpty(search))
                search = " 1=1";

            try
            {
                int? tempTotalRows = 0;
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable theTable = theAdapter.GetSearchPolizaForList(search, pageSize, firstRow, ref tempTotalRows, orderBy);
                
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PolizaDS.PolizaRow row in theTable.Rows)
                    {
                        Poliza ThePoliza = FillRecord(row);
                        _cache.Add(ThePoliza);
                    }
                }
                return tempTotalRows.Value;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza data", ex);
                throw;
            }
        }

        public static int GetPolizaBySearchByPolizaOrAseguradoOrPaciente(List<Poliza> _cache, int ClienteId, int pageSize, int firstRow, string where, string orderBy )
        {
            int? TotalRows = 0;

            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable theTable = theAdapter.GetPolizaBySearch(ClienteId, pageSize, firstRow, ref TotalRows, where, orderBy);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PolizaDS.PolizaRow row in theTable.Rows)
                    {
                        Poliza ThePoliza = FillRecord(row);
                        ThePoliza.RowNumber = row.RowNumber;
                        ThePoliza.CasoId = row.CasoId;
                        ThePoliza.CasoCritico = row.CasoCritico;
                        _cache.Add(ThePoliza);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza data", ex);
                throw;
            }
            return (int)TotalRows;
        }

        public static Poliza GetPolizaByPolizaAndCodigoCliente(string numeroPoliza, string codigoCliente)
        {
            if (string.IsNullOrWhiteSpace(numeroPoliza) ||
                string.IsNullOrWhiteSpace(codigoCliente))
                throw new ArgumentException("NumeroPoliza y codigoCliente deben tener datos");

            Poliza objPoliza = null;
            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable thetable = theAdapter.GetPolizaByPolizaAndCliente(numeroPoliza, codigoCliente, 1);
                if (thetable != null && thetable.Rows.Count > 0)
                {
                    objPoliza = FillRecord(thetable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaAndCodigoCliente", ex);
                throw;
            }
            return objPoliza;
        }

        //sin uso por ahora
        public static Poliza GetPolizaByPolizaId(int PolizaId)
        {
            if (PolizaId <= 0)
                throw new ArgumentException("PolizaId cannot be less than or equal to zero.");
            Poliza objPoliza = new Poliza();
            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable thetable = theAdapter.GetPolizaByPolizaId(PolizaId);
                if (thetable != null && thetable.Rows.Count > 0)
                {
                    objPoliza = FillRecord(thetable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaId", ex);
                throw;
            }
            return objPoliza;
        }

        public static int InsertPoliza (Poliza poliza)
        {
            return InsertPoliza(poliza.CodigoAsegurado, poliza.ClienteId
                , poliza.PacienteId, poliza.Relacion, poliza.NumeroPoliza
                , poliza.FechaInicio, poliza.FechaFin
                , poliza.MontoTotal, poliza.MontoFarmacia, poliza.Cobertura
                , poliza.NombrePlan, poliza.Lugar
                , poliza.Estado, poliza.FechaEstado);
        }
        public static int InsertPoliza(string CodigoAsegurado, int ClienteId
                , int PacienteId, string Relacion, string NumeroPoliza
                , DateTime FechaInicio, DateTime FechaFin
                , decimal MontoTotal, decimal MontoFarmacia, string Cobertura, string NombrePlan, string Lugar
                , string EstadoPoliza, DateTime FechaEstado)
        {
            if (string.IsNullOrEmpty(CodigoAsegurado))
                throw new ArgumentException("CodigoAsegurado cannot null or empty.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(NumeroPoliza))
                throw new ArgumentException("NumeroPoliza cannot null or empty.");
            if (MontoTotal <= 0)
                throw new ArgumentException("MontoTotal cannot be less than or equal to zero.");
            if (MontoFarmacia <= 0)
                throw new ArgumentException("MontoFarmacia cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Cobertura))
                throw new ArgumentException("Cobertura cannot null or empty.");
            if (string.IsNullOrEmpty(Relacion))
                Relacion = "TITULAR";

            if (string.IsNullOrEmpty(NombrePlan))
                throw new ArgumentException("NombrePlan cannot null or empty.");

            try
            {
                int? PolizaId=0;
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                theAdapter.InsertPoliza(ref PolizaId, NumeroPoliza, FechaInicio, FechaFin, MontoTotal, MontoFarmacia, Cobertura
                    , NombrePlan, Lugar, CodigoAsegurado, ClienteId, PacienteId, Relacion, EstadoPoliza, FechaEstado);

                return (int)PolizaId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaId", ex);
                throw;
            }
        }

        public static void UpdatePoliza ( Poliza poliza )
        {
            UpdatePoliza(poliza.PolizaId, poliza.NumeroPoliza
                , poliza.FechaInicio, poliza.FechaFin
                , poliza.MontoTotal, poliza.MontoFarmacia, poliza.Cobertura
                , poliza.NombrePlan, poliza.Lugar
                , poliza.CodigoAsegurado, poliza.ClienteId, poliza.Relacion
                , poliza.Estado, poliza.FechaEstado);
        }
        public static void UpdatePoliza ( int PolizaId, string NumeroPoliza
                , DateTime FechaInicio, DateTime FechaFin
                , decimal MontoTotal, decimal MontoFarmacia, string Cobertura
                , string NombrePlan, string Lugar
                ,string CodigoAsegurado, int ClienteId, string Relacion
                , string EstadoPoliza, DateTime FechaEstado )
        {
            if (PolizaId <= 0)
                throw new ArgumentException("PolizaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(NumeroPoliza))
                throw new ArgumentException("NumeroPoliza cannot null or empty.");

            if (string.IsNullOrEmpty(NombrePlan))
                throw new ArgumentException("NombrePlan cannot null or empty.");

            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                theAdapter.UpdatePoliza(PolizaId, NumeroPoliza, FechaInicio, FechaFin, MontoTotal, MontoFarmacia, Cobertura, NombrePlan, Lugar
                        , CodigoAsegurado, ClienteId, Relacion, EstadoPoliza, FechaEstado);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaId", ex);
                throw;
            }
        }

        public static List<Poliza> GetPolizaByPacienteId(int PacienteId)
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            List<Poliza> theList = new List<Poliza>();
            Poliza objPoliza = null;
            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable thetable = theAdapter.GetPolizaByPacienteId2(PacienteId);
                if (thetable != null && thetable.Rows.Count > 0)
                {
                    foreach (PolizaDS.PolizaRow row in thetable.Rows)
                    {
                        objPoliza = FillRecord(row);
                        theList.Add(objPoliza);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaId", ex);
                throw;
            }
            return theList;
        }
       
        public static Poliza GetPolizaDetailsByCasoId(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            Poliza objPoliza = null;
            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                PolizaDS.PolizaDataTable thetable = theAdapter.GetPolizaByCasoId(CasoId);
                if (thetable != null && thetable.Rows.Count > 0)
                {
                        objPoliza = FillRecord(thetable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza Details By CasoId", ex);
                throw;
            }
            return objPoliza;
        }

        //si es superior al monto minimo establecido en DB entonces retorna true, 
        //puede aprobar el detalle del caso Medico
        public static bool BoolMontoMinimoEnPolizaPacienteSuperior(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            bool BoolMontoMinimoPacienteSuperior = false;
            Poliza objPoliza = GetPolizaDetailsByCasoId(CasoId);
            if (objPoliza != null)
            {
                if (objPoliza.MontoTotal < 0)
                {
                    return true;
                }
                else
                {
                    decimal MontoMinimoEnPolizaPaciente = objPoliza.MontoTotal - objPoliza.GastoTotal;
                    if (MontoMinimoEnPolizaPaciente > Configuration.Configuration.GetMontoMinimoEnPoliza())
                        return true;
                }
            }

            return BoolMontoMinimoPacienteSuperior;
        }

        public static void DeletePoliza ( int PolizaId )
        {
            if (PolizaId <= 0)
                throw new ArgumentException("PolizaId cannot be less than or equal to zero.");

            try
            {
                PolizaDSTableAdapters.PolizaTableAdapter theAdapter = new PolizaDSTableAdapters.PolizaTableAdapter();
                theAdapter.DeletePoliza(PolizaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Poliza", ex);
                throw;
            }
        }
        //edwin suyo

        private static PolizaValidation FillRecord(PolizaValidationDS.GetAllPolizaValidationRow row)
        {

            PolizaValidation objPoliza = new
                PolizaValidation(
                row.PolizaId
                ,row.AseguradoId
                ,row.ClienteId
                ,row.PacienteId
                ,row.NumeroPoliza
                ,row.FechaInicio
                ,row.FechaFin
                ,row.VigenciaPoliza
                ,row.Codigo
                ,row.Relacion
                ,row.Nombre
                ,row.CarnetIdentidad
                ,row.NombreJuridico
                ,row.EstadoPoliza
                 );

            return objPoliza;
        }
        //sirve para validar si la poliza esta vencida
        public static List<PolizaValidation> GetAllPolizaValidation(int PolizaId,string NumeroPoliza,int AseguradoId,int ClienteId,int PacienteId,
                            string NombrePaciente,string CarnetIdentidad)
        {
            if (
                string.IsNullOrWhiteSpace(CarnetIdentidad) & PolizaId==0)
                throw new ArgumentException(" dato ci del paciente deben tener datos");

            List<PolizaValidation> ListobjPoliza = new List<PolizaValidation>();
            try
            {
                PolizaValidationDSTableAdapters.GetAllPolizaValidationTableAdapter theAdapter = new PolizaValidationDSTableAdapters.GetAllPolizaValidationTableAdapter();
                PolizaValidationDS.GetAllPolizaValidationDataTable theTable = theAdapter.GetAllPolizaValidation(PolizaId,null,null,null, null ,null,null);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PolizaValidationDS.GetAllPolizaValidationRow row in theTable.Rows)
                    {
                        PolizaValidation objPoliza = null;
                        objPoliza = FillRecord(row);
                        ListobjPoliza.Add(objPoliza);
                    }
                
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaAndCodigoCliente", ex);
                throw;
            }
            return ListobjPoliza;
        }
        public static List<PolizaValidation> GetAllPolizaValidation(int PolizaId)
        {
            if (
               (PolizaId)<0)
                throw new ArgumentException(" dato ci del paciente deben tener datos");

            List<PolizaValidation> ListobjPoliza = new List<PolizaValidation>();
            try
            {
                PolizaValidationDSTableAdapters.GetAllPolizaValidationTableAdapter theAdapter = new PolizaValidationDSTableAdapters.GetAllPolizaValidationTableAdapter();
                PolizaValidationDS.GetAllPolizaValidationDataTable theTable = theAdapter.GetAllPolizaValidation(PolizaId, null, null, null, null, null, null);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PolizaValidationDS.GetAllPolizaValidationRow row in theTable.Rows)
                    {
                        PolizaValidation objPoliza = null;
                        objPoliza = FillRecord(row);
                        ListobjPoliza.Add(objPoliza);
                    }

                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Poliza By PolizaAndCodigoCliente", ex);
                throw;
            }
            return ListobjPoliza;
        }

    }

}