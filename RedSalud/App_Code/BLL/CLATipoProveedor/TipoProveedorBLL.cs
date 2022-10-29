using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.TipoProveedor.BLL
{
    /// <summary>
    /// Summary description for TipoProveedorBLL
    /// </summary>
    public class TipoProveedorBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TipoProveedorBLL ()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static TipoProveedor FillRecord ( TipoProveedorDS.TipoProveedorRow row )
        {
            TipoProveedor objTipoProveedor = new TipoProveedor(
                row.TipoProveedorId
                ,row.Nombre
                );
            return objTipoProveedor;
        }

        //public static TipoProveedor GetTipoProveedorByClienteId(string TipoProveedorId)
        //{
        //    if (string.IsNullOrEmpty(TipoProveedorId))
        //        throw new ArgumentException("TipoProveedorId no puede ser nulo o vacio.");

        //    TipoProveedor TheTipoProveedor = null;
        //    try
        //    {
        //        TipoProveedorDSTableAdapters.TipoProveedorTableAdapter theAdapter = new TipoProveedorDSTableAdapters.TipoProveedorTableAdapter();
        //        TipoProveedorDS.TipoProveedorDataTable theTable = theAdapter.GetTipoProveedorByTipoProveedorId(TipoProveedorId);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            TipoProveedorDS.TipoProveedorRow row = theTable[0];
        //            TheTipoProveedor = FillRecord(row);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting TipoProveedor data", ex);
        //        throw;
        //    }
        //    return TheTipoProveedor;
        //}

        public static List<TipoProveedor> getTipoProveedorList()
        {
            List<TipoProveedor> theList = new List<TipoProveedor>();
            TipoProveedor theTipoProveedor = null;
            try
            {
                TipoProveedorDSTableAdapters.TipoProveedorTableAdapter theAdapter = new TipoProveedorDSTableAdapters.TipoProveedorTableAdapter();
                TipoProveedorDS.TipoProveedorDataTable theTable = theAdapter.GetAllTipoProveedor();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoProveedorDS.TipoProveedorRow row in theTable.Rows)
                    {
                        theTipoProveedor = FillRecord(row);
                        theList.Add(theTipoProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Tipo Proveedor", ex);
                throw;
            }
            return theList;
        }
    }
}