using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.GastoDetalle.BLL
{
    /// <summary>
    /// Summary description for GastoDetalleBLL
    /// </summary>
    public class GastoDetalleBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public GastoDetalleBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static GastoDetalle FillRecord(GastoDS.GastoRow row)
        {
            GastoDetalle objG = new GastoDetalle(
                row.GastoDetalleId
                , row.GastoId
                , row.FechaCreacion
                , row.FechaGasto
                , row.Monto
                , row.NroFacturaRecibo
                , row.TipoDocumento
                , row.IsfileIdNull() ? 0 : row.fileId
                ,row.IsConsolidacionIdNull()? 0 :row.ConsolidacionId
                ,row.IsAceptadoNull()? false:row.Aceptado
                ,row.IsRechazadoNull()? false:row.Rechazado);

            return objG;
        }

        public static List<GastoDetalle> GetGastoDetalleList(int GastoId)
        { 
            if (GastoId<0)//se elimino la condicion =0 ya q cuando no tiene registrado GastoId debe mostrar el grid empty
                throw new ArgumentException("GastoId cannot be less than to zero.");

            List<GastoDetalle> theList = new List<GastoDetalle>();
            GastoDetalle ObjGastoDetalle = null;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                GastoDS.GastoDataTable theTable = theAdapter.GetAllGastoDetalleByGastoId(GastoId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (GastoDS.GastoRow row in theTable.Rows)
                    {
                        ObjGastoDetalle = FillRecord(row);
                        theList.Add(ObjGastoDetalle);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list GastoDetalle", ex);
                throw;
            }
            return theList;
        }

        public static bool DeleteGastoDetalle(int GastoDetalleId)
        {
            if (GastoDetalleId <= 0)
                throw new ArgumentException("GastoDetalleId cannot be less than or equal to zero.");
            try
            {
                //verifico si tiene consolidacionId entonces retorno false
                GastoDetalle objGastoDetalle = GetGastoDetalleDetails(GastoDetalleId);
                if (objGastoDetalle == null || objGastoDetalle.ConsolidacionId>0)
                    return false;

                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();

                theAdapter.DeleteGastoDetalleByGastoDetalleId(GastoDetalleId);
                return true;

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deletng GastoDetalle", ex);
                throw;
            }
        }

        
        public static List<GastoDetalle> GetAllGastoDetalleByProveedorId(int ProveedorId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than to zero.");

            List<GastoDetalle> theList = new List<GastoDetalle>();
            GastoDetalle ObjGastoDetalle = null;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                GastoDS.GastoDataTable theTable = theAdapter.GetAllGastoDetalleByProveedorId(ProveedorId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (GastoDS.GastoRow row in theTable.Rows)
                    {
                        ObjGastoDetalle = FillRecord(row);
                        theList.Add(ObjGastoDetalle);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list GastoDetalle by ProveedorId", ex);
                throw;
            }
            return theList;
        }

        public static GastoDetalle GetGastoDetalleDetails(int GastoDetalleId)
        {
            if (GastoDetalleId <= 0)
                throw new ArgumentException("GastoDetalleId cannot be less than to zero.");

            GastoDetalle ObjGastoDetalle = null;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                GastoDS.GastoDataTable theTable = theAdapter.GetGastoDetalleById(GastoDetalleId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ObjGastoDetalle = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Details GastoDetalle", ex);
                throw;
            }
            return ObjGastoDetalle;
        }

        public static bool ConslidarGastoDetalle(int GastoDetalleId, int ConsolidacionId, bool Aceptado, bool Rechazado)
        {
            if (GastoDetalleId <= 0)
                throw new ArgumentException("GastoDetalleId cannot be less than to zero.");
            if (ConsolidacionId <= 0)
                throw new ArgumentException("ConsolidacionId cannot be less than to zero.");

            try
            {
                GastoDSTableAdapters.GastoTableAdapter TheAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                TheAdapter.ConsolidarGastoDetalleByGDId(GastoDetalleId, ConsolidacionId, Aceptado, Rechazado);
                
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list GastoDetalle by ProveedorId", ex);
                return false;
            }
        }
    }
}