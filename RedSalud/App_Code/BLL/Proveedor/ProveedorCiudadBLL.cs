using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Proveedor.BLL
{
    /// <summary>
    /// Summary description for ProveedorProveedorCiudadBLL
    /// </summary>
    public class ProveedorCiudadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public ProveedorCiudadBLL ()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static ProveedorCiudad FillRecord ( ProveedorCiudadDS.ProveedorCiudadRow row )
        {
            ProveedorCiudad objProveedorCiudad = new ProveedorCiudad(
                 row.ProveedorId
                , row.CiudadId
                , row.Nombre
                , row.IsDireccionNull() ? "" : row.Direccion
                , row.IsTelefonoNull() ? "" : row.Telefono
                , row.IsCelularNull() ? "" : row.Celular);
            return objProveedorCiudad;
        }

        public static ProveedorCiudad GetProveedorCiudadByCiudadIdAndProveedorId ( int ProveedorId, string CiudadId )
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId no puede ser <= 0.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId no puede ser nulo o vacio.");

            ProveedorCiudad TheProveedorCiudad = null;
            try
            {
                ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter theAdapter = new ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter();
                ProveedorCiudadDS.ProveedorCiudadDataTable theTable = theAdapter.GetProveedorCiudadByCiudadIdANDProveedorId(ProveedorId, CiudadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    ProveedorCiudadDS.ProveedorCiudadRow row = theTable[0];
                    TheProveedorCiudad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting ProveedorCiudad data", ex);
                throw;
            }
            return TheProveedorCiudad;
        }

        public static List<ProveedorCiudad> getProveedorCiudadByProveedorId ( int ProveedorId )
        {
            List<ProveedorCiudad> theList = new List<ProveedorCiudad>();
            try
            {
                ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter theAdapter = new ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter();
                ProveedorCiudadDS.ProveedorCiudadDataTable theTable = theAdapter.GetProveedorCiudadByProveedorId(ProveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorCiudadDS.ProveedorCiudadRow row in theTable.Rows)
                    {
                        ProveedorCiudad theProveedorCiudad = FillRecord(row);
                        theList.Add(theProveedorCiudad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list ProveedorCiudad by ProveedorId", ex);
                throw;
            }
            return theList;
        }
        public static void InsertProveedorCiudad ( ProveedorCiudad ciudad )
        {
            InsertProveedorCiudad( ciudad.ProveedorId, ciudad.CiudadId, ciudad.Direccion, ciudad.Telefono, ciudad.Celular);
        }

        public static void InsertProveedorCiudad ( int proveedorId, string ciudadId, string direccion, string telefono, string celular )
        {
            try
            {
                ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter theAdapter = new ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter();
                theAdapter.Insert(proveedorId, ciudadId, direccion, telefono, celular);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting list ProveedorCiudad by ProveedorId", ex);
                throw;
            }
        }
        public static void UpdateProveedorCiudad ( ProveedorCiudad ciudad )
        {
            UpdateProveedorCiudad(ciudad.ProveedorId, ciudad.CiudadId, ciudad.Direccion, ciudad.Telefono, ciudad.Celular);
        }

        public static void UpdateProveedorCiudad ( int ProveedorId, string CiudadId, string direccion, string telefono, string celular )
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId no puede ser <= 0.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId no puede ser nulo o vacio.");
            try
            {
                ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter theAdapter = new ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter();
                theAdapter.Update(ProveedorId, CiudadId, direccion, telefono, celular);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating list ProveedorCiudad by ProveedorId", ex);
                throw;
            }

        }

        public static void DeleteProveedorCiudad ( ProveedorCiudad ciudad )
        {
            DeleteProveedorCiudad(ciudad.ProveedorId, ciudad.CiudadId);
        }

        public static void DeleteProveedorCiudad ( int ProveedorId, string CiudadId )
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId no puede ser <= 0.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId no puede ser nulo o vacio.");
            try
            {
                ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter theAdapter = new ProveedorCiudadDSTableAdapters.ProveedorCiudadTableAdapter();
                theAdapter.Delete(ProveedorId, CiudadId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting list ProveedorCiudad by ProveedorId", ex);
                throw;
            }
        }
    }
}