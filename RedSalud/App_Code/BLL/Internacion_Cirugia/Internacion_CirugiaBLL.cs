using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

/// <summary>
/// Summary description for Internacion_CirugiaBLL
/// </summary>
/// 
namespace Artexacta.App.Internacion_Cirugia.BLL
{
    public class Internacion_CirugiaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        private static Internacion_Cirugia FillRecord(Internacion_CirugiaDS.Internacion_CirugiaDSRow row)
        {
            Internacion_Cirugia objInternacion_Cirugia = new Internacion_Cirugia(
                row.InternacionId
                ,row.CiudadId
                ,row.MedicoId
                ,row.EspecialidadId
                ,row.detValorUma
                ,row.detCantidadUma
                ,row.detPorcentajeCirujano
                ,row.detMontoCirujano
                ,row.detPorcentajeAnestesiologo
                ,row.detMontoAnestesiologo
                ,row.detPorcentajeAyudante
                ,row.detMontoAyudante
                ,row.detPorcentajeInstrumentista
                ,row.detMontoInstrumentista
                , row.detMontoTotal
                , row.detPorcentajeCoPago
                , row.detMontoCoPago
                , row.detFecha
                );

            return objInternacion_Cirugia;
        }
        public Internacion_CirugiaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void InsertInternacionCirugia(Internacion_Cirugia obj)
        {
            int? preId = 0;
            try
            {
                Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter theAdapter = new Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter();
                theAdapter.InsertInternacion_Cirugia(ref preId,obj.InternacionId,obj.CiudadId,obj.MedicoId,obj.EspecialidadId,obj.detValorUma
                    ,obj.detCantidadUma,obj.detPorcentajeCirujano,obj.detMontoCirujano,obj.detPorcentajeAnestesiologo,obj.detMontoAnestesiologo
                    ,obj.detPorcentajeAyudante,obj.detMontoAyudante,obj.detPorcentajeInstrumentista,obj.detMontoInstrumentista,obj.detMontoTotal
                    ,obj.detPorcentajeCoPago,obj.detMontoCoPago,obj.detFecha);

                //log.Debug("Se insertó la Internacion Cirugia con ID:" + preId);

                if ( preId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
               // obj.preId = (int)preId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Internacion cirugia", q);
                throw q;
            }
        }
        public static bool UpdateInternacion_Cirugia(int InternacionId,decimal detValorUma, int detCantidadUma
            , decimal detPorcentajeCirujano,decimal detMontoCirujano, decimal detPorcentajeAnestesiologo, decimal detMontoAnestesiologo 
            ,decimal detPorcentajeAyudante,decimal detMontoAyudante ,decimal detPorcentajeInstrumentista, decimal detMontoInstrumentista
            ,decimal detMontoTotal)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");


            try
            {
                Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter theAdapter = new Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter();
                theAdapter.UpdateInternacion_Cirugia(InternacionId, detValorUma,detCantidadUma,detPorcentajeCirujano,detMontoCirujano,detPorcentajeAnestesiologo,detMontoAnestesiologo
                , detPorcentajeAyudante, detMontoAyudante,detPorcentajeInstrumentista,detMontoInstrumentista ,detMontoTotal);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Internacion", ex);
                throw;
            }
        }
        public static Internacion_Cirugia GetInternacionCirugiaxId(int InternacionId)
        {
            List<Internacion_Cirugia> theList = new List<Internacion_Cirugia>();
            Internacion_Cirugia ObjInternacionCirugia= null;
            try
            {
                Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter theAdapter = new Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter();
                Internacion_CirugiaDS.Internacion_CirugiaDSDataTable theTable = theAdapter.GetAllInternacionCirugia(InternacionId,"TTT",0,0);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (Internacion_CirugiaDS.Internacion_CirugiaDSRow row in theTable.Rows)
                    {
                        ObjInternacionCirugia = FillRecord(row);
                        theList.Add(ObjInternacionCirugia);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Internacion Cirugia", ex);
                throw;
            }
            return ObjInternacionCirugia;
        }
        public static bool DeleteInternacion_Cirugia(int InternacionId)
        {
            if (InternacionId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter theAdapter = new Internacion_CirugiaDSTableAdapters.Internacion_CirugiaDSTableAdapter();
                theAdapter.DeleteInternacion_Cirugia(InternacionId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Internacion_Cirugia", ex);
                throw;
            }
        }
    }
}