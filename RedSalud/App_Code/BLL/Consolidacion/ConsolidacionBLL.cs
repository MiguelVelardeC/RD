using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Artexacta.App.Consolidacion.BLL
{
    /// <summary>
    /// Summary description for ConsolidacionBLL
    /// </summary>
    public class ConsolidacionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ConsolidacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Consolidacion FillRecord(ConsolidacionDS.ConsolidacionRow row)
        {
            Consolidacion objConsolidacion= new Consolidacion(
                row.ConsolidacionId
                ,row.ProveedorId
                ,row.FechaHasta
                ,row.MontoTotal
                ,row.UserId
                ,row.fechaCreacion
                );
            return objConsolidacion;
        }

        public static Consolidacion GetConsolidacionDetails(int ConsolidacionId)
        {
            if (ConsolidacionId <= 0)
                throw new ArgumentException("ConsolidacionId cannot be less than to zero.");

            Consolidacion ObjConsolidacion = null;
            try
            {
                ConsolidacionDSTableAdapters.ConsolidacionTableAdapter theAdapter = new ConsolidacionDSTableAdapters.ConsolidacionTableAdapter();
                ConsolidacionDS.ConsolidacionDataTable theTable = theAdapter.GetConsolidacionDetails(ConsolidacionId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ObjConsolidacion = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ConsolidacionDetails", ex);
                throw;
            }
            return ObjConsolidacion;
        }

        public static List<Consolidacion> GetListConsolidacion(int ProveedorId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than to zero.");

            List<Consolidacion> theList = new List<Consolidacion>();
            Consolidacion ObjConsolidacion = null;
            try
            {
                ConsolidacionDSTableAdapters.ConsolidacionTableAdapter theAdapter = new ConsolidacionDSTableAdapters.ConsolidacionTableAdapter();
                ConsolidacionDS.ConsolidacionDataTable theTable = theAdapter.GetConsolidacionByProveedorId(ProveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ConsolidacionDS.ConsolidacionRow row in theTable.Rows)
                    {
                        ObjConsolidacion = FillRecord(row);
                        theList.Add(ObjConsolidacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Consolidacion List by ProveedorId", ex);
                throw;
            }
            return theList;
        }

        public static int InsertConsolidacion(int ProveedorId, DateTime FechaHasta,
                decimal MontoTotal, int UserId, DateTime FechaCreacion, GridTableView TableView)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than to zero.");

            int? ConsolidacionId = 0;
            try
            {
                ConsolidacionDSTableAdapters.ConsolidacionTableAdapter theAdapter = new ConsolidacionDSTableAdapters.ConsolidacionTableAdapter();
                theAdapter.Insert(ref ConsolidacionId,ProveedorId, FechaHasta 
                    ,MontoTotal,UserId,FechaCreacion);

                if (ConsolidacionId > 0)
                {
                    ////modificar los gastosDetalle con el idConsolidacion y Aceptado/Rechazado
                    //foreach (GridDataItem item in TableView.Items)
                    //{
                    //    RadioButton AceptadoRb = (RadioButton)item.FindControl("AceptadoRb");
                    //    bool Aceptado = AceptadoRb.Checked;

                    //    RadioButton RechazadoRb = (RadioButton)item.FindControl("RechazadoRb");
                    //    bool Rechazado = RechazadoRb.Checked;

                    //    //int GastoId = Convert.ToInt32(item.GetDataKeyValue("GastoId").ToString());
                    //}
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Consolidacion", ex);
                throw;
            }
            return (int)ConsolidacionId;
        }
    }
}