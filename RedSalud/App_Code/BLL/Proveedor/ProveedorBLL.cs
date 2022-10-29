using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using Artexacta.App.Medico.BLL;
namespace Artexacta.App.Proveedor.BLL
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

        private static Proveedor FillRecord(ProveedorDS.ProveedorRow row)
        {
            Proveedor objProveedor = new Proveedor(
                 row.ProveedorId
                , row.RedMedicaId
                , row.RedMedicaNombre
                , row.Nombres
                , row.Apellidos
                , row.NombreJuridico
                , row.Nit.ToString()
                , row.Direccion
                , row.TelefonoCasa
                , row.TelefonoOficina
                , row.Celular
                , row.Estado
                , row.Observacion
                , row.FechaActualizacion
                , row.TipoProveedorId
                , row.NombreTipoProveedor
                , row.EspecialistaId
                , row.EspecialidadId
                , row.NombreEspecialidad
                , row.Sedes
                , row.ColegioMedico
                , row.CostoConsulta
                , row.PorcentageDescuento
                , row.CiudadId
                , row.IsDireccionPCiudadNull() ? "" : row.DireccionPCiudad
                , row.IsTelefonoPCiudadNull() ? "" : row.TelefonoPCiudad
                , row.IsCelularPCiudadNull() ? "" : row.CelularPCiudad
                );

            if (!row.IsRowNumberNull())
            {
                objProveedor.RowNumber = row.RowNumber;
            }

            return objProveedor;
        }
        //
        private static Proveedor FillRecord(ProveedorDS.ProveedorNewRow row)
        {
            Proveedor objProveedor = new Proveedor(
                 row.ProveedorId
                , row.RedMedicaId
                , row.RedMedicaNombre
                , row.Nombres
                , row.Apellidos
                , row.NombreJuridico
                , row.Nit.ToString()
                , row.Direccion
                , row.TelefonoCasa
                , row.TelefonoOficina
                , row.Celular
                , row.Estado
                , row.Observacion
                , row.FechaActualizacion
                , row.TipoProveedorId
                , row.NombreTipoProveedor
                , row.EspecialistaId
                , row.EspecialidadId
                , row.userId
                , row.MedicoId
                , row.NombreUsuario
                , row.NombreEspecialidad
                , row.Sedes
                , row.ColegioMedico
                , row.CostoConsulta
                , row.PorcentageDescuento
                , row.CiudadId
                , row.IsDireccionPCiudadNull() ? "" : row.DireccionPCiudad
                , row.IsTelefonoPCiudadNull() ? "" : row.TelefonoPCiudad
                , row.IsCelularPCiudadNull() ? "" : row.CelularPCiudad
                ); 
            return objProveedor;
        }
        //
        private static Proveedor FillRecord(ProveedorDS.ProveedorBasicRow row)
        {
            Proveedor objProveedor = new Proveedor(
                 row.ProveedorId
                , row.Nombres
                , row.Apellidos
                , row.NombreJuridico
                , row.Nit.ToString()
                , row.Direccion
                , row.TelefonoCasa
                , row.TelefonoOficina
                , row.Celular
                , row.Estado
                , row.Observacion
                , row.FechaActualizacion
                , row.TipoProveedorId
                );

            return objProveedor;
        }
        private static Proveedor FillRecord(ProveedorDS.ProveedorBasicoRow row)
        {
            Proveedor objProveedor = new Proveedor(
                 row.ProveedorId
                , row.Nombres
                , row.Apellidos
                , row.NombreJuridico
                , "bueno"
                , "bueno"
                , "bueno"
                , "bueno"
                , "bueno"
                , "bueno"
                , "bueno"
                , DateTime.Now
                , "bueno"
                );

            return objProveedor;
        }

        public static List<Proveedor> getProveedorList(string TipoProveedorId, string CiudadId, int redMedicaIdPaciente)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (redMedicaIdPaciente <= 0)
                throw new ArgumentException("redMedicaIdPaciente cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot be null or empty.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                ProveedorDS.ProveedorDataTable theTable = theAdapter.GetAllProveedor(TipoProveedorId, CiudadId, redMedicaIdPaciente);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

        public static List<Proveedor> getProveedorListPag(string TipoProveedorId, string CiudadId, int redMedicaIdPaciente, int firstRow, int pageSize, ref int totalRows)
        {
            return getProveedorAutocomplete(TipoProveedorId, CiudadId, redMedicaIdPaciente, firstRow, pageSize, "", ref totalRows);
            //List<Proveedor> theList = new List<Proveedor>();

            //if (redMedicaIdPaciente <= 0)
            //    throw new ArgumentException("redMedicaIdPaciente cannot be less than or equal to zero.");
            //if (string.IsNullOrEmpty(TipoProveedorId))
            //    throw new ArgumentException("TipoProveedorId cannot be null or empty.");
            //if (string.IsNullOrEmpty(CiudadId))
            //    throw new ArgumentException("CiudadId cannot be null or empty.");

            //try
            //{
            //    ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
            //    int? total = 0;
            //    ProveedorDS.ProveedorDataTable theTable = theAdapter.GetAllProveedorPag(TipoProveedorId, CiudadId, redMedicaIdPaciente,firstRow,pageSize,ref total);
            //    if (theTable != null && theTable.Rows.Count > 0)
            //    {
            //        foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
            //        {
            //            Proveedor theProveedor = FillRecord(row);
            //            theList.Add(theProveedor);
            //        }
            //    }
            //    totalRows = (int)total;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("An error was ocurred while geting list Proveedor", ex);
            //    throw;
            //}
            //return theList;
        }
        public static int SearchProveedor(ref List<Proveedor> theList, string where, int firstRow, int pageSize)
        {
            return SearchProveedor(ref theList, where, "", firstRow, pageSize);
        }
        public static int SearchProveedor(ref List<Proveedor> theList, string where, string ciudadId, int firstRow, int pageSize)
        {
            int? total = 0;

            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                ProveedorDS.ProveedorDataTable theTable = theAdapter.SearchProveedor(where, ciudadId, pageSize, firstRow, ref total);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serching list Proveedor", ex);
                throw;
            }
            return (int)total;
        }
        public static void InsertProveedor(Proveedor newProveedor)
        {
            InsertProveedor(ref newProveedor);
        }
        public static void InsertProveedor(ref Proveedor newProveedor)
        {
            if ((string.IsNullOrEmpty(newProveedor.Nombres) && string.IsNullOrEmpty(newProveedor.Apellidos))
                && (string.IsNullOrEmpty(newProveedor.NombreJuridico)))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (newProveedor.RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be <= 0.");
            if (string.IsNullOrEmpty(newProveedor.TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot null or empty.");

            try
            {
                int? intProveedorId = 0;
                int? intEspecialistaId = 0;
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                theAdapter.InsertProveedor(ref intProveedorId, ref intEspecialistaId, newProveedor.RedMedicaId, newProveedor.EspecialidadID,
                    newProveedor.Sedes, newProveedor.ColegioMedico, newProveedor.CostoConsulta, newProveedor.PorcentageDescuento,
                    newProveedor.Nombres, newProveedor.Apellidos, newProveedor.NombreJuridico, decimal.Parse(newProveedor.Nit),
                    newProveedor.Direccion, newProveedor.TelefonoCasa, newProveedor.TelefonoOficina, newProveedor.Celular,
                    newProveedor.Estado, newProveedor.Observaciones, newProveedor.TipoProveedorId);

                newProveedor.ProveedorId = (int)intProveedorId;
                newProveedor.EspecialistaId = (int)intEspecialistaId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Proveedor", ex);
                throw;
            }
        }
        public static void InsertProveedorNew(ref Proveedor newProveedor)
        {
            if ((string.IsNullOrEmpty(newProveedor.Nombres) && string.IsNullOrEmpty(newProveedor.Apellidos))
                && (string.IsNullOrEmpty(newProveedor.NombreJuridico)))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (newProveedor.RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be <= 0.");
            if (string.IsNullOrEmpty(newProveedor.TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot null or empty.");
            if (newProveedor.UserId==0 & newProveedor.MedicoId>0)
            {
               newProveedor.UserId=MedicoBLL.getMedicoByMedicoId(newProveedor.MedicoId).UserId;
            }

            try
            {
                int? intProveedorId = 0;
                int? intEspecialistaId = 0;
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                theAdapter.InsertProveedorNew(ref intProveedorId, ref intEspecialistaId,newProveedor.MedicoId,newProveedor.UserId, newProveedor.RedMedicaId, newProveedor.EspecialidadID,
                    newProveedor.Sedes, newProveedor.ColegioMedico, newProveedor.CostoConsulta, newProveedor.PorcentageDescuento,
                    newProveedor.Nombres, newProveedor.Apellidos, newProveedor.NombreJuridico, decimal.Parse(newProveedor.Nit),
                    newProveedor.Direccion, newProveedor.TelefonoCasa, newProveedor.TelefonoOficina, newProveedor.Celular,
                    newProveedor.Estado, newProveedor.Observaciones, newProveedor.TipoProveedorId);
                    newProveedor.ProveedorId = (int)intProveedorId;
                    newProveedor.EspecialistaId = (int)intEspecialistaId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Proveedor", ex);
                throw;
            }
        }

        public static void UpdateProveedor(Proveedor theProveedor)
        {
            if (theProveedor.ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if ((string.IsNullOrEmpty(theProveedor.Nombres) && string.IsNullOrEmpty(theProveedor.Apellidos))
                && (string.IsNullOrEmpty(theProveedor.NombreJuridico)))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (string.IsNullOrEmpty(theProveedor.TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot null or empty.");

            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                theAdapter.UpdateProveedor(theProveedor.ProveedorId, theProveedor.EspecialistaId, theProveedor.EspecialidadID,
                    theProveedor.Sedes, theProveedor.ColegioMedico, theProveedor.CostoConsulta, theProveedor.PorcentageDescuento, theProveedor.Nombres,
                    theProveedor.Apellidos, theProveedor.NombreJuridico, decimal.Parse(theProveedor.Nit), theProveedor.Direccion, theProveedor.TelefonoCasa,
                    theProveedor.TelefonoOficina, theProveedor.Celular, theProveedor.Estado, theProveedor.Observaciones, theProveedor.TipoProveedorId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating Proveedor", ex);
                throw;
            }
        }

        public static void UpdateProveedorNew(Proveedor theProveedor)
        {
            if (theProveedor.ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            if ((string.IsNullOrEmpty(theProveedor.Nombres) && string.IsNullOrEmpty(theProveedor.Apellidos))
                && (string.IsNullOrEmpty(theProveedor.NombreJuridico)))
                throw new ArgumentException("Nombre cannot null or empty.");
            if (string.IsNullOrEmpty(theProveedor.TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot null or empty.");

            if (theProveedor.UserId == 0 & theProveedor.MedicoId > 0)
            {
                theProveedor.UserId = MedicoBLL.getMedicoByMedicoId(theProveedor.MedicoId).UserId;
            }
            try
            {
                ProveedorDSTableAdapters.ProveedorNewTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorNewTableAdapter();
                theAdapter.UpdateProveedor(theProveedor.ProveedorId, theProveedor.EspecialistaId,theProveedor.UserId, theProveedor.EspecialidadID,
                    theProveedor.Sedes, theProveedor.ColegioMedico, theProveedor.CostoConsulta, theProveedor.PorcentageDescuento, theProveedor.Nombres,
                    theProveedor.Apellidos, theProveedor.NombreJuridico, decimal.Parse(theProveedor.Nit), theProveedor.Direccion, theProveedor.TelefonoCasa,
                    theProveedor.TelefonoOficina, theProveedor.Celular, theProveedor.Estado, theProveedor.Observaciones, theProveedor.TipoProveedorId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating Proveedor", ex);
                throw;
            }
        }
        public static void DeleteProveedor(int proveedorID)
        {
            if (proveedorID <= 0)
                throw new ArgumentException("proveedorID cannot be less than or equal to zero.");

            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                theAdapter.DeleteProveedor(proveedorID);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
        }
        public static Proveedor getProveedorByID(int proveedorID)
        {
            Proveedor theProveedor = new Proveedor();

            if (proveedorID <= 0)
                throw new ArgumentException("proveedorID cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                ProveedorDS.ProveedorDataTable theTable = theAdapter.GetProveedorByID(proveedorID);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Proveedor by ID", ex);
                throw;
            }
            return theProveedor;
        }
        public static Proveedor getProveedorByIDNew(int proveedorID)
        {
            Proveedor theProveedor = new Proveedor();

            if (proveedorID <= 0)
                throw new ArgumentException("proveedorID cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorNewTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorNewTableAdapter();
                ProveedorDS.ProveedorNewDataTable theTable = theAdapter.GetProveedorByID(proveedorID);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Proveedor by ID", ex);
                throw;
            }
            return theProveedor;
        }

        public static List<Proveedor> getProveedorBasicList(string CiudadId)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            Proveedor theProveedor = null;
            try
            {
                ProveedorDSTableAdapters.ProveedorBasicTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorBasicTableAdapter();
                ProveedorDS.ProveedorBasicDataTable theTable = theAdapter.GetProveedorByCiudadId(CiudadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorBasicRow row in theTable.Rows)
                    {
                        theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list ProveedorBasic", ex);
                throw;
            }
            return theList;
        }

        public static List<Proveedor> getProveedorByTipoProveedorAndCiudadId(string TipoProveedorId, string CiudadId)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (string.IsNullOrEmpty(TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot be null or empty.");

            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            Proveedor theProveedor = null;
            try
            {
                ProveedorDSTableAdapters.ProveedorBasicTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorBasicTableAdapter();
                ProveedorDS.ProveedorBasicDataTable theTable = theAdapter.GetProveedorByTipoPAndCiudadId(TipoProveedorId, CiudadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorBasicRow row in theTable.Rows)
                    {
                        theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list getProveedorByTipoProveedorAndCiudadId", ex);
                throw;
            }
            return theList;
        }

        public static List<Proveedor> getProveedorAutocomplete(string TipoProveedorId, string CiudadId, int redMedicaIdPaciente, int firstRow, int pageSize, string Filter, ref int totalRows)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (redMedicaIdPaciente < 0)
                throw new ArgumentException("redMedicaIdPaciente cannot be less than zero.");
            if (string.IsNullOrEmpty(TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot be null or empty.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
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
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

        public static List<Proveedor> getProveedorxEspeciliadAutocomplete(string TipoProveedorId, string CiudadId, int redMedicaIdPaciente,int EspecialidadId ,int firstRow, int pageSize, string Filter, ref int totalRows)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (redMedicaIdPaciente < 0)
                throw new ArgumentException("redMedicaIdPaciente cannot be less than zero.");
            if (string.IsNullOrEmpty(TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot be null or empty.");
            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                int? total = 0;
                ProveedorDS.ProveedorDataTable theTable = theAdapter.Get_AutoComplete_searchProveedorxEspecialidad(TipoProveedorId, CiudadId
                    , redMedicaIdPaciente, EspecialidadId, firstRow, pageSize, Filter, ref total);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
                totalRows = (int)total;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

        public static Proveedor getProveedorByCasoIdReceta(int casoId)
        {
            Proveedor theProveedor = new Proveedor();

            if (casoId <= 0)
                throw new ArgumentException("casoId cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorBasicTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorBasicTableAdapter();
                ProveedorDS.ProveedorBasicDataTable theTable = theAdapter.GetProveedorByCasoIdReceta(casoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Proveedor by ID", ex);
                throw;
            }
            return theProveedor;
        }
        public static List<Proveedor> GetProveedorActivo()
        {
            List<Proveedor> theList = new List<Proveedor>();
            Proveedor theProveedor = null;
            try
            {
                ProveedorDSTableAdapters.ProveedorBasicTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorBasicTableAdapter();
                ProveedorDS.ProveedorBasicDataTable theTable = theAdapter.GetProveedorActivo();

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorBasicRow row in theTable.Rows)
                    {
                        theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting list RedCliente", ex);
                throw;
            }
            return theList;
        }
        public static List<Proveedor> getProveedorByTipoProveedorAndCiudadIdNull(string TipoProveedorId, string CiudadId)
        {
            List<Proveedor> theList = new List<Proveedor>();

            if (string.IsNullOrEmpty(TipoProveedorId))
                throw new ArgumentException("TipoProveedorId cannot be null or empty.");

            if (string.IsNullOrEmpty(CiudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            Proveedor theProveedor = null;
            try
            {
                ProveedorDSTableAdapters.ProveedorBasicoTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorBasicoTableAdapter();
                ProveedorDS.ProveedorBasicoDataTable theTable = theAdapter.GetProveedorByTipoPAndCiudadIdNull(TipoProveedorId, CiudadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorBasicoRow row in theTable.Rows)
                    {
                        theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list getProveedorByTipoProveedorAndCiudadId", ex);
                throw;
            }
            return theList;
        }
    }
    public class ProveedorMedicoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");


        //edwin suyo
        private static Proveedor FillRecord(ProveedorDS.ProveedorMedicoRow row)
        {
            Proveedor objProveedor = new Proveedor(
                 row.ProveedorId
                ,row.MedicoIdProv
                ,row.userIdProv
                ,row.NombreMedico
                ,row.NombreEspecialidad
                ,row.CostoConsulta
                ,row.PorcentageDescuento
                ,row.CiudadId
                );
            return objProveedor;
        }

        public static Proveedor GetProveedorPrecioByProveedorId(int ProveedorId)
        {
            Proveedor theProveedor = null;

            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorMedicoTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorMedicoTableAdapter();
                ProveedorDS.ProveedorMedicoDataTable theTable = theAdapter.GetProveedorPrecioByMedicoId(null,null,null,null,ProveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Proveedor by ID", ex);
                throw;
            }
            return theProveedor;
        }
        public static Proveedor GetProveedorPrecioByMedicoId(int MedicoId)
        {
            Proveedor theProveedor = null;

            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorMedicoTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorMedicoTableAdapter();
                ProveedorDS.ProveedorMedicoDataTable theTable = theAdapter.GetProveedorPrecioByMedicoId(MedicoId, null, null, null, null);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Medico by ID", ex);
                throw;
            }
            return theProveedor;
        }
        public static Proveedor GetProveedorPrecioByUserId(int UserId)
        {
            Proveedor theProveedor = null;

            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            try
            {
                ProveedorDSTableAdapters.ProveedorMedicoTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorMedicoTableAdapter();
                ProveedorDS.ProveedorMedicoDataTable theTable = theAdapter.GetProveedorPrecioByMedicoId(null, null, UserId, null, null);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theProveedor = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Proveedor by ID", ex);
                throw;
            }
            return theProveedor;
        }


    }

}