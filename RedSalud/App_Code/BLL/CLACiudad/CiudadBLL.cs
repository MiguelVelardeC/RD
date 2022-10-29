using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Desgravamen;

namespace Artexacta.App.Ciudad.BLL
{
    /// <summary>
    /// Summary description for CiudadBLL
    /// </summary>
    public class CiudadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CiudadBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Ciudad FillRecord(CiudadDS.CiudadRow row)
        {
            Ciudad objCiudad = new Ciudad(
                row.CiudadId
                , row.Nombre
                );
            return objCiudad;
        }

        private static Ciudad FillRecord(CiudadDS.CiudadFromPolizaRow row)
        {
            Ciudad objCiudad = new Ciudad(
                row.CiudadId
                , row.Nombre
                );
            return objCiudad;
        }

        public static Ciudad GetCiudadDetails(string CiudadId)
        {
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot null or empty.");

            Ciudad TheCiudad = null;
            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                CiudadDS.CiudadDataTable theTable = theAdapter.GetCiudadById(CiudadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    CiudadDS.CiudadRow row = theTable[0];
                    TheCiudad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Ciudad data", ex);
                throw;
            }
            return TheCiudad;
        }

        public static List<Ciudad> getCiudadList(string ciudadesExistentes)
        {
            List<Ciudad> listCiudades = getCiudadList();
            if (!string.IsNullOrWhiteSpace(ciudadesExistentes))
            {
                List<string> ciudades = new List<string>(ciudadesExistentes.Split(new char[] { ',' }));
                List<Ciudad> newListCiudades = new List<Ciudad>();

                foreach (Ciudad ciudad in listCiudades)
                {
                    if (!ciudades.Contains(ciudad.CiudadId))
                    {
                        newListCiudades.Add(ciudad);
                    }
                }
                listCiudades = newListCiudades;
            }
            return listCiudades;
        }

        public static List<Ciudad> getCiudadListDesgravamen(int intClienteId)
        {
            CiudadesDesgravamenDSTableAdapters.CiudadesDesgravamenTableAdapter adapter =
                new CiudadesDesgravamenDSTableAdapters.CiudadesDesgravamenTableAdapter();
            CiudadesDesgravamenDS.CiudadesDesgravamenDataTable table = adapter.GetCiudadesByProveedoresDesgravamenActivas(intClienteId);

            List<Ciudad> list = new List<Ciudad>();

            foreach (CiudadesDesgravamenDS.CiudadesDesgravamenRow row in table)
            {
                Ciudad obj = new Ciudad()
                {
                    CiudadId = row.ciudadId,
                    Nombre = row.nombre
                };
                list.Add(obj);
            }
            return list;
        }


        public static List<Ciudad> getCiudadList()
        {
            List<Ciudad> theList = new List<Ciudad>();
            Ciudad theCiudad = null;
            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                CiudadDS.CiudadDataTable theTable = theAdapter.GetAllCiudad();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CiudadDS.CiudadRow row in theTable.Rows)
                    {
                        theCiudad = FillRecord(row);
                        theList.Add(theCiudad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Ciudad", ex);
                throw;
            }
            return theList;
        }

        public static List<Ciudad> getCiudadListFromPoliza()
        {
            List<Ciudad> theList = new List<Ciudad>();
            Ciudad theCiudad = null;
            try
            {
                CiudadDSTableAdapters.CiudadFromPolizaTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadFromPolizaTableAdapter();
                CiudadDS.CiudadFromPolizaDataTable theTable = theAdapter.GetAllCiudadFromPoliza();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CiudadDS.CiudadFromPolizaRow row in theTable.Rows)
                    {
                        theCiudad = FillRecord(row);
                        theList.Add(theCiudad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Ciudad", ex);
                throw;
            }
            return theList;
        }

        public static bool InsertCiudad(string CiudadId, string Nombre)
        {
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                theAdapter.Insert(CiudadId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Ciudad", ex);
                throw;
            }
        }

        public static bool UpdateCiudad(string CiudadId, string Nombre)
        {
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                theAdapter.Update(CiudadId, Nombre);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Ciudad", ex);
                throw;
            }
        }

        public static bool DeleteCiudad(string CiudadId)
        {
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot null or empty.");

            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                theAdapter.Delete(CiudadId);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Ciudad", ex);
                throw;
            }
        }

        public static List<Ciudad> getCiudadListByProveedor(int proveedorId)
        {
            if (proveedorId <= 0)
                throw new ArgumentException("proveedorId cannot be zero or negative");


            List<Ciudad> theList = new List<Ciudad>();
            Ciudad theCiudad = null;
            try
            {
                CiudadDSTableAdapters.CiudadTableAdapter theAdapter = new CiudadDSTableAdapters.CiudadTableAdapter();
                CiudadDS.CiudadDataTable theTable = theAdapter.GetCiudadesByProveedorId(proveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CiudadDS.CiudadRow row in theTable.Rows)
                    {
                        theCiudad = FillRecord(row);
                        theList.Add(theCiudad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Ciudad by proveedor", ex);
                throw;
            }
            return theList;
        }
    }
}