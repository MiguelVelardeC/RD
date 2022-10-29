using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
/// <summary>
/// Summary description for PrestacionesBLL
/// </summary>
namespace Artexacta.App.CoPagos.BLL
{
    public class CoPagosBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CoPagosBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static CoPagos FillRecord(CoPagoDS.CoPagosDSRow row)
        {
            CoPagos objprestaciones = new CoPagos(

                row.PacienteNombre
                , row.ClienteNombre
                , row.ProveedorNombre
                , row.TipoProveedorId
                , row.detFecha
                , row.TipoCaso
                , row.detId
                , row.Estado
                , row.MontoTotal
                , row.MontoAPagar
                , row.ValorCoPago
                , row.NumeroPoliza
                , row.carnetIdentidad
                , row.CodigoCaso
                , row.Ciudad 
                );
            return objprestaciones;
        }
        private static CoPagos FillRecordMG(CoPagoDS.CoPagosDSRow row)
        {
            CoPagos objprestaciones = new CoPagos(

                row.PacienteNombre
                , row.ClienteNombre
                , row.ProveedorNombre
                , row.TipoProveedorId
                , row.detFecha
                , row.TipoCaso
                , row.detId
                , row.Estado
                );
            return objprestaciones;
        }
        //para los Prestaciones Casos Odontologicos
        private static CasoOdontologia FillRecord(CoPagoDS.CasoOdontologiaDSRow row)
        {
            CasoOdontologia objprestacionesCasoOdontologia = new CasoOdontologia(
                row.detId
                , row.CasoId
                , row.PrestacionOdontologicaId
                , row.detPrecio
                , row.detFechaCoPagoPagado
                , row.detFecha
                , row.PacienteNombre
                , row.CedulaIdentidad
                , row.NumeroPoliza
                , row.NombrePoliza
                , row.NombreMedico
                , row.NombreProveedor
                , row.Solicito
                , row.Especialidad
                , row.CodigoCaso
                , row.Diagnostico
                , row.Observacion
                , row.detMontoAPagar
                , row.detCopagoReferencial
                , row.detCopagoReferencialTipo
                );
            return objprestacionesCasoOdontologia;
        }
        //para los Prestaciones Casos Especialidad
        private static CasoEspecialidad FillRecord(CoPagoDS.CasoEspecialidadDSRow row)
        {
            CasoEspecialidad objprestacionesCasoEspecialidad = new CasoEspecialidad(
                row.detId
                , row.CasoId
                , row.detPrecio
                , row.EspecialidadId
                , row.detMontoAPagar
                , row.detCopagoReferencial
                , row.detCopagoReferencialTipo
                , row.detFechaCoPagoPagado
                , row.detFecha
                , row.PacienteNombre
                , row.CedulaIdentidad
                , row.NumeroPoliza
                , row.NombrePoliza
                , row.NombreMedico
                , row.NombreProveedor
                , row.Solicito
                , row.Especialidad
                , row.CodigoCaso
                 , row.Diagnostico
                , row.Observacion
               , row.EsMedicoGeneral
                );
            return objprestacionesCasoEspecialidad;
        }

        //para los Prestaciones Casos LaboratoriosImagenologia
        private static CasoLaboratorioImagenologia FillRecord(CoPagoDS.CasoLaboratoriosImagenologiaDSRow row)
        {
            CasoLaboratorioImagenologia objprestacionesCasoLaboratorioImagenologia = new CasoLaboratorioImagenologia(
                row.detId
                , row.CasoId
                , row.ProveedorId
                , row.EstudioId
                , row.OrdenDeServicioId
                , row.detPrecio
                , row.detCoPagoMonto
                , row.detCoPagoPorcentaje
                , row.detFechaCoPagoPagado
                , row.detFecha
                , row.PacienteNombre
                , row.CedulaIdentidad
                , row.NumeroPoliza
                , row.NombrePoliza
                , row.NombreMedico
                , row.NombreProveedor
                , row.Solicito
                , row.Especialidad
                , row.CodigoCaso
                , row.Diagnostico
                , row.Observacion
                , row.detEsImagenologia
                );
            return objprestacionesCasoLaboratorioImagenologia;
        }
        private static CasoLaboratorioImagenologia FillRecord(CoPagoDS.usp_Prestaciones_GetPrestacionesLabImaRow row)
        {
            CasoLaboratorioImagenologia objprestacionesCasoLaboratorioImagenologia = new CasoLaboratorioImagenologia(

                row.detId
                , row.CasoId
                , row.EstudioId
                , row.OrdenDeServicioId
                , row.Nombre
                , row.Observacion
                , row.detPrecio
                , row.detCopagoReferencial
                , row.detCopagoReferencialTipo
                , row.detMontoAPagar
                );
            return objprestacionesCasoLaboratorioImagenologia;
        }

        #region "CoPagos"
        public static int GetPrestacionesCoPagoALL(List<CoPagos> _cache, string ciudadid, int ClienteId, string TipoProveedorId, int ProveedorId, int MedicoId, int detCoPagoPagado, string CodigoCaso, string nombre, string CarnetIdentidad, DateTime fechainicio, DateTime fechafinal, int pageSize, int firstRow)
        {

            int? intTotalRows = 0;
            try
            {

                CoPagoDSTableAdapters.CoPagosTableAdapter theAdapter = new CoPagoDSTableAdapters.CoPagosTableAdapter();

                CoPagoDS.CoPagosDSDataTable theTable = theAdapter.GetAllCoPagosNacional(ciudadid, ClienteId, TipoProveedorId, ProveedorId, MedicoId, detCoPagoPagado, CodigoCaso, nombre, CarnetIdentidad, fechainicio, fechafinal, pageSize, firstRow, ref intTotalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CoPagosDSRow row in theTable.Rows)
                    {
                        CoPagos PrestacionesCoPagos = FillRecord(row);
                        _cache.Add(PrestacionesCoPagos);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list PrestacionesCoPagos by CasoId", ex);
                throw;
            }
            return (int)intTotalRows;
        }
        public static int GetPrestacionesCoPagoALLNacional(List<CoPagos> _cache, string ciudadid, int ClienteId, string TipoProveedorId, int ProveedorId, int MedicoId, int detCoPagoPagado, string CodigoCaso, string nombre, string CarnetIdentidad, DateTime fechainicio, DateTime fechafinal, int pageSize, int firstRow)
        {

            int? intTotalRows = 0;
            try
            {

                CoPagoDSTableAdapters.CoPagosTableAdapter theAdapter = new CoPagoDSTableAdapters.CoPagosTableAdapter();

                CoPagoDS.CoPagosDSDataTable theTable = theAdapter.GetAllCoPagosNacional(ciudadid, ClienteId, TipoProveedorId, ProveedorId, MedicoId, detCoPagoPagado, CodigoCaso, nombre, CarnetIdentidad, fechainicio, fechafinal, pageSize, firstRow, ref intTotalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CoPagosDSRow row in theTable.Rows)
                    {
                        CoPagos PrestacionesCoPagos = FillRecord(row);
                        _cache.Add(PrestacionesCoPagos);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list PrestacionesCoPagos by CasoId", ex);
                throw;
            }
            return (int)intTotalRows;
        }

        public static int GetPrestacionesCoPagoALLMG(List<CoPagos> _cache, string ciudadid, int ClienteId, string TipoProveedorId, int ProveedorId, int detCoPagoPagado, string nombre, string CarnetIdentidad, DateTime fechainicio, DateTime fechafinal, int pageSize, int firstRow)
        {

            int? intTotalRows = 0;
            try
            {

                CoPagoDSTableAdapters.CoPagosTableAdapter theAdapter = new CoPagoDSTableAdapters.CoPagosTableAdapter();

                CoPagoDS.CoPagosDSDataTable theTable = theAdapter.GetAllCoPagosMG(ciudadid, ClienteId, TipoProveedorId, ProveedorId, detCoPagoPagado, null, nombre, CarnetIdentidad, fechainicio, fechafinal, pageSize, firstRow, ref intTotalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CoPagosDSRow row in theTable.Rows)
                    {
                        CoPagos PrestacionesCoPagos = FillRecordMG(row);
                        _cache.Add(PrestacionesCoPagos);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list PrestacionesCoPagos by CasoId", ex);
                throw;
            }
            return (int)intTotalRows;
        }
        #endregion
       
        #region "Odontologia"
        public static void InsertCasoOdontologia(CasoOdontologia obj)
        {


            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();
                theAdapter.InsertCaso_Odontologia(ref preId, obj.CasoId, obj.PrestacionOdontologicaId, obj.ProveedorId, obj.detPrecio
                , obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.Fecha);


                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                // obj.detId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso Odontologia", q);
                throw q;
            }
        }

        public static void InsertCasoOdontologiaConsultaMedica(CasoOdontologia obj)
        {
            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();
                theAdapter.InsertCaso_Odontologia(ref preId, obj.CasoId, obj.PrestacionOdontologicaId, obj.ProveedorId, obj.detPrecio
                , obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.Fecha);
                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                int IdOdo =Convert.ToInt32( preId);
                UpdateCasoOdontologia(IdOdo, obj.Fecha);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso Odontologia", q);
                throw q;
            }
        }
        public static CasoOdontologia GetCasoOdontologia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId no puede ser menor o igual a cero.");

            CasoOdontologia CasoOdontologico = null;
            try
            {

                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();

                CoPagoDS.CasoOdontologiaDSDataTable theTable = theAdapter.GetCasoOdontologia(detId,0,0,0);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CasoOdontologiaDSRow row in theTable.Rows)
                    {
                        CasoOdontologico = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return CasoOdontologico;
        }
        public static void UpdateCasoOdontologia(int detId, DateTime FechaPago)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");

            try
            {
                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();
                theAdapter.UpdateCasoOdontologia(detId, FechaPago);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso Odontologia", ex);
                throw;
            }
        }

        public static bool DeleteOdontologia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();
                theAdapter.DeleteCasoOdontologia(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Caso Odontologia", ex);
                throw;
            }
        }

        public static void InsertCoPagoOdontologia(CasoOdontologia obj)
        {
            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoOdontologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoOdontologiaTableAdapter();
                theAdapter.InsertCaso_Odontologia(ref preId, obj.CasoId, obj.PrestacionOdontologicaId, obj.ProveedorId, obj.detPrecio, obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.Fecha);

                if (preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                // obj.preId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting CoPago Odontologia", q);
                throw q;
            }
        }
        #endregion

        #region "Especialidad"

        public static void InsertCasoEspecialidad(CasoEspecialidad obj)
        {

            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoEspecialidadTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoEspecialidadTableAdapter();
                theAdapter.InsertCaso_Especialidad(ref preId, obj.CasoId, obj.EspecialidadId, obj.ProveedorId, obj.detPrecio
                , obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.Fecha);

                //   log.Debug("Se insertó el Caso con ID:" + preId);

                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                //  obj.detId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso Especialidad", q);
                throw q;
            }
        }
        public static void InsertCasoEspecialidadMedicoGeneral(CasoEspecialidad obj)
        {

            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoEspecialidadTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoEspecialidadTableAdapter();
                theAdapter.InsertCaso_EspecialidadMedicoGeneral(ref preId, obj.CasoId, obj.EspecialidadId, obj.detPrecio
                , obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.Fecha, obj.MedicoId);

                //   log.Debug("Se insertó el Caso con ID:" + preId);

                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                //  obj.detId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso Especialidad", q);
                throw q;
            }
        }
        public static CasoEspecialidad GetCasoEspecialidad(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId no puede ser menor o igual a cero.");

            CasoEspecialidad CasoEspecilidad = null;
            try
            {

                CoPagoDSTableAdapters.CasoEspecialidadTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoEspecialidadTableAdapter();

                CoPagoDS.CasoEspecialidadDSDataTable theTable = theAdapter.GetCasoEspecialidad(detId, 0, 0,0);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CasoEspecialidadDSRow row in theTable.Rows)
                    {
                        CasoEspecilidad = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return CasoEspecilidad;
        }
        public static void UpdateCasoEspecialidad(int detId, DateTime FechaPago)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");

            try
            {
                CoPagoDSTableAdapters.CasoEspecialidadTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoEspecialidadTableAdapter();
                theAdapter.UpdateCasoEspecialidad(detId, FechaPago);


            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso Especialidad", ex);
                throw;
            }
        }
        public static bool DeleteEspecialidad(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                CoPagoDSTableAdapters.CasoEspecialidadTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoEspecialidadTableAdapter();
                theAdapter.DeleteCasoEspecialidad(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Especialidad", ex);
                throw;
            }
        }
        #endregion

        #region "LaboratorioImagenologia"
        public static CasoLaboratorioImagenologia GetCasoLaboratorioImagenologia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId no puede ser menor o igual a cero.");

            CasoLaboratorioImagenologia CasoLaboratorioImagenologia = null;
            try
            {

                CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter();

               CoPagoDS.CasoLaboratoriosImagenologiaDSDataTable theTable = theAdapter.GetCasoLaboratoriosImagenologia(detId,0, 0, 0,0);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CasoLaboratoriosImagenologiaDSRow row in theTable.Rows)
                    {
                        CasoLaboratorioImagenologia = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return CasoLaboratorioImagenologia;
        }
        public static CasoLaboratorioImagenologia GetCasoLaboratorioImagenologiaxOrdendeServicio(int OrdendeServicio)
        {
            if (OrdendeServicio <= 0)
                throw new ArgumentException("OrdendeServicio no puede ser menor o igual a cero.");

            CasoLaboratorioImagenologia CasoLaboratorioImagenologia = null;
            try
            {

                CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter();

                CoPagoDS.CasoLaboratoriosImagenologiaDSDataTable theTable = theAdapter.GetCasoLaboratoriosImagenologia(0, 0, 0, 0, OrdendeServicio);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.CasoLaboratoriosImagenologiaDSRow row in theTable.Rows)
                    {
                        CasoLaboratorioImagenologia = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return CasoLaboratorioImagenologia;
        }
        public static CasoLaboratorioImagenologia GetCasoLaboratorioImagenologiaxOrdenDeServicio( int detId, int OrdenDeServicioId, int CasoId, int EstudioId)
        {
            CasoLaboratorioImagenologia CasoLaboratorioImagenologia = null;

            try
            {
              
                CoPagoDSTableAdapters.usp_Prestaciones_GetPrestacionesLabImaTableAdapter theAdapter = new CoPagoDSTableAdapters.usp_Prestaciones_GetPrestacionesLabImaTableAdapter();

                CoPagoDS.usp_Prestaciones_GetPrestacionesLabImaDataTable theTable = theAdapter.GetPrestacionesLabIma(detId, OrdenDeServicioId, CasoId, EstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.usp_Prestaciones_GetPrestacionesLabImaRow row in theTable.Rows)
                    {
                 
                        CasoLaboratorioImagenologia = FillRecord(theTable[0]);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return CasoLaboratorioImagenologia;
        }

        public static int GetCasoLaboratorioImagenologiaxEstudio(List<CasoLaboratorioImagenologia> _cache, int detId,int OrdenDeServicioId,int CasoId,int EstudioId )
        {
            try
            {

                CoPagoDSTableAdapters.usp_Prestaciones_GetPrestacionesLabImaTableAdapter theAdapter = new CoPagoDSTableAdapters.usp_Prestaciones_GetPrestacionesLabImaTableAdapter();

                CoPagoDS.usp_Prestaciones_GetPrestacionesLabImaDataTable theTable = theAdapter.GetPrestacionesLabIma(detId,OrdenDeServicioId,CasoId,EstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CoPagoDS.usp_Prestaciones_GetPrestacionesLabImaRow row in theTable.Rows)
                    {
                        CasoLaboratorioImagenologia CasoLaboratorioImagenologia  = FillRecord(row);
                        _cache.Add(CasoLaboratorioImagenologia);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Caso Odontologia data", ex);
                throw;
            }
            return _cache.Count();
        }

        public static void UpdateCasoLaboratorioImagenologia(int detId,DateTime FechaPago)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");

            try
            {
                CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter();
                theAdapter.UpdateCasoLaboratoriosImagenologia(detId,FechaPago);
              
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Caso Laboratorio Imagenologia", ex);
                throw;
            }
        }
      
        public static void InsertCasoLaboratorioImagenologia(CasoLaboratorioImagenologia obj)
        {
       
            int? preId = 0;
            try
            {
                CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter();
                theAdapter.InsertCaso_LaboratoriosImagenologia(ref preId, obj.CasoId, obj.ProveedorId, obj.EstudioId, obj.OrdenDeServicioId
                    , obj.detPrecio, obj.detCoPagoMonto, obj.detCoPagoPorcentaje, obj.detEsImagenologia, obj.Fecha);
                
                log.Debug("Se insertó el Caso con ID:" + preId);

                if (preId == null || preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
             //   obj.detId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso LaboratorioImagenologia", q);
                throw q;
            }
        }

        public static bool DeleteLaboratorioImagenologia(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter theAdapter = new CoPagoDSTableAdapters.CasoLaboratoriosImagenologiaTableAdapter();
                theAdapter.DeleteCasoLaboratorioImagenologia(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Caso LabImagenologia", ex);
                throw;
            }
        }
        #endregion


    }
}