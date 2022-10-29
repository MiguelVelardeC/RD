using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

using Artexacta.App.Documents;

/// <summary>
/// Summary description for Internacion_InternacionBLL
/// </summary>
/// 

namespace Artexacta.App.Internacion_Internacion.BLL
{
    public class Internacion_InternacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        private static Internacion_Internacion FillRecord(Internacion_InternacionDS.Internacion_InternacionDSRow row)
        {
            Internacion_Internacion objInternacion_Internacion = new Internacion_Internacion(
                row.InternacionId
                , row.CiudadId
                , row.EnfermedadId
                , row.detMontoEmergencia
                , row.detMontoHonorariosMedicos
                , row.detMontoFarmacia
                , row.detMontoLaboratorios
                , row.detMontoEstudios
                , row.detMontoHospitalizacion
                , row.detMontoOtros
                , row.detMontoTotal
                , row.detPorcentajeCoPago
                , row.detMontoCoPago
                , row.detFecha
                );

            return objInternacion_Internacion;
        }

        public Internacion_InternacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void InsertInternacionInternacion(Internacion_Internacion obj)
        {
            int? preId = 0;
            try
            {
                Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter theAdapter = new Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter();
                theAdapter.InsertInternacionInternacion(ref preId, obj.InternacionId, obj.CiudadId, obj.EnfermedadId, obj.detMontoEmergencia, obj.detMontoHonorariosMedicos, obj.detMontoFarmacia
               , obj.detMontoLaboratorios, obj.detMontoEstudios, obj.detMontoHospitalizacion, obj.detMontoOtros, obj.detMontoTotal, obj.detPorcentajeCoPago, obj.detMontoCoPago, obj.detFecha);

                // log.Debug("Se insertó la Internacion  con ID:" + preId);

                if (preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                //obj.preId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Internacion Internacion", q);
                throw q;
            }
        }
        public static bool UpdateInternacion_Internacion(int InternacionId, decimal detMontoEmergencia,
            decimal detMontoHonorariosMedicos, decimal detMontoFarmacia, decimal detMontoLaboratorios,
            decimal detMontoEstudios, decimal detMontoHospitalizacion
            , decimal detMontoOtros, decimal detMontoTotal)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");


            try
            {
                Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter theAdapter = new Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter();
                theAdapter.UpdateInternacion_Internacion(InternacionId,detMontoEmergencia,detMontoHonorariosMedicos,detMontoFarmacia
                    ,detMontoLaboratorios,detMontoEstudios,detMontoHospitalizacion,detMontoOtros,detMontoTotal);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Internacion", ex);
                return false;
                throw;
            }
        }
        public static bool DeleteInternacion_Internacion(int InternacionId)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter theAdapter = new Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter();
                theAdapter.DeleteInternacion_Internacion(InternacionId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Internacion_Internacion", ex);
                throw;
            }
        }


        public static Internacion_Internacion GetInternacionxId(int InternacionId)
        {
            List<Internacion_Internacion> theList = new List<Internacion_Internacion>();
            Internacion_Internacion ObjInternacionInternacion = null;
            try
            {
                Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter theAdapter = new Internacion_InternacionDSTableAdapters.Internacion_InternacionDSTableAdapter();
                Internacion_InternacionDS.Internacion_InternacionDSDataTable theTable = theAdapter.GetAllInternacionInternacion(InternacionId,"TTT","TTT",0);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (Internacion_InternacionDS.Internacion_InternacionDSRow row in theTable.Rows)
                    {
                        ObjInternacionInternacion = FillRecord(row);
                        theList.Add(ObjInternacionInternacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Internacion Internacion", ex);
                throw;
            }
            return ObjInternacionInternacion;
        }
    }
}