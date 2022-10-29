using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.SOAT.Proveedor.BLL
{
    /// <summary>
    /// Summary description for ProveedorBLL
    /// </summary>
    public class ProveedorBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public ProveedorBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static Proveedor GetSOATProveedorById(int ProveedorId)
        {
            Proveedor theProveedor = new Proveedor();

            if (ProveedorId<= 0)
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                SOATProveedorDSTableAdapters.SOATProveedorTableAdapter adapter =
                    new SOATProveedorDSTableAdapters.SOATProveedorTableAdapter();
                
                SOATProveedorDS.SOATProveedorDataTable table = adapter.GetSOATProveedorById(ProveedorId);

                if (table != null && table.Rows.Count > 0)
                {
                    theProveedor = new Proveedor()
                    {
                        ProveedorId = table[0].ProveedorId,
                        Nombre = table[0].ProveedorNombre,
                        NIT = table[0].NIT,
                        CiudadId = table[0].CiudadId,
                        ProveedorAlianzaId = table[0].AlianzaProveedorId
                    };
                }
                /*
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                int? total = 0;
                ProveedorDS.ProveedorDataTable theTable = theAdapter.Get_AUTOCOMPLETE_SearchProveedor(TipoProveedorId, CiudadId
                    , redMedicaIdPaciente, firstRow, pageSize, Filter, ref total);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
                totalRows = (int)total;
                 */
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting list Proveedor SOAT Autocomplete", ex);
                throw;
            }
            return theProveedor;
        }

        public static List<Proveedor> GetProveedorAutocomplete(string ciudadId, int firstRow, int pageSize, string Filter, ref int totalRows)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (string.IsNullOrEmpty(ciudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                SOATProveedorDSTableAdapters.SOATProveedorTableAdapter adapter = 
                    new SOATProveedorDSTableAdapters.SOATProveedorTableAdapter();
                int? total = 0;
                SOATProveedorDS.SOATProveedorDataTable table = adapter.GetSOATProveedorByCiudadId(ciudadId, firstRow, pageSize, Filter, ref total);
                
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (SOATProveedorDS.SOATProveedorRow row in table.Rows)
                    {
                        Proveedor p = new Proveedor()
                        {
                            ProveedorId = row.ProveedorId,
                            Nombre = row.ProveedorNombre,
                            NIT = row.NIT,
                            CiudadId = row.CiudadId,
                            ProveedorAlianzaId = row.AlianzaProveedorId
                        };
                        theList.Add(p);
                    }
                }
                totalRows = total.Value;
                /*
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                int? total = 0;
                ProveedorDS.ProveedorDataTable theTable = theAdapter.Get_AUTOCOMPLETE_SearchProveedor(TipoProveedorId, CiudadId
                    , redMedicaIdPaciente, firstRow, pageSize, Filter, ref total);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
                totalRows = (int)total;
                 */
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting list Proveedor SOAT Autocomplete", ex);
                throw;
            }
            return theList;
        }
    }
}