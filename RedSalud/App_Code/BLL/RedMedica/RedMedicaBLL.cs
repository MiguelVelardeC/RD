using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.RedMedica.BLL
{
    /// <summary>
    /// Summary description for RedMedicaBLL
    /// </summary>
    public class RedMedicaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedMedicaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedMedica FillRecord(RedMedicaDS.RedMedicaRow row)
        {
            RedMedica objRedMedica = new RedMedica(
                    row.RedMedicaId
                    , row.Codigo
                    , row.Nombre
                );

            return objRedMedica;
        }

        public static List<RedMedica> GetRedMedicaList()
        {
            List<RedMedica> theList = new List<RedMedica>();
            RedMedica theRedMedica = null;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetAllRedMedica();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
                    {
                        theRedMedica = FillRecord(row);
                        theList.Add(theRedMedica);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedMedica", ex);
                throw;
            }
            return theList;
        }
        public static List<RedMedica> GetRedMedicaListxCodigo(string Codigo)
        {
            List<RedMedica> theList = new List<RedMedica>();
            RedMedica theRedMedica = null;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetAllRedMedicaxCodigo(Codigo);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
                    {
                        theRedMedica = FillRecord(row);
                        theList.Add(theRedMedica);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedMedica", ex);
                throw;
            }
            return theList;
        }
        public static List<RedMedica> getRedMedicaListByClienteId ( int ClienteId )
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");

            List<RedMedica> theList = new List<RedMedica>();
            RedMedica theRedMedica = null;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaByClienteId(ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
                    {
                        theRedMedica = FillRecord(row);
                        theList.Add(theRedMedica);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedMedica by ClienteId", ex);
                throw;
            }
            return theList;
        }
        public static List<RedMedica> getRedMedicaListByProveedorId ( int ProveedorId )
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            List<RedMedica> theList = new List<RedMedica>();
            RedMedica theRedMedica = null;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaByProveedorId(ProveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
                    {
                        theRedMedica = FillRecord(row);
                        theList.Add(theRedMedica);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedMedica by ProveedorId", ex);
                throw;
            }
            return theList;
        }

        //public static List<RedMedica> getRedMedicaListByPacienteId(int PacienteId)
        //{
        //    if (PacienteId <= 0)
        //        throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

        //    List<RedMedica> theList = new List<RedMedica>();
        //    RedMedica theRedMedica = null;
        //    try
        //    {
        //        RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
        //        RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaByPacienteId(PacienteId);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
        //            {
        //                theRedMedica = FillRecord(row);
        //                theList.Add(theRedMedica);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting list RedMedica by RedMedicaId", ex);
        //        throw;
        //    }
        //    return theList;
        //}

        //public static List<RedMedica> getRedMedicaListByProveedorId(int ProveedorId)
        //{
        //    if (ProveedorId <= 0)
        //        throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

        //    List<RedMedica> theList = new List<RedMedica>();
        //    RedMedica theRedMedica = null;
        //    try
        //    {
        //        RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
        //        RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaByProveedorId(ProveedorId);
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        {
        //            foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
        //            {
        //                theRedMedica = FillRecord(row);
        //                theList.Add(theRedMedica);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting list RedMedica by ProveedorId", ex);
        //        throw;
        //    }
        //    return theList;
        //}

        public static RedMedica getRedMedicaListByRedMedicaId(int RedMedicaId)
        {
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");

            RedMedica theRedMedica = null;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaById(RedMedicaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theRedMedica = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedMedica by RedMedicaId", ex);
                throw;
            }
            return theRedMedica;
        }

        public static int InsertRedMedica(string Codigo, string Nombre)
        {
            if (string.IsNullOrEmpty(Codigo))
                throw new ArgumentException("Codigo cannot be null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");

            int? RedMedicaId = 0;
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                theAdapter.Insert(ref RedMedicaId, Codigo, Nombre);
            }
            catch (Exception ex)
            {

                log.Error("An error was ocurred while Inserting new RedMedica", ex);
                throw;
            }
            return (int)RedMedicaId;
        }

        public static bool UpdateRedMedica(int RedMedicaId, string Codigo, string Nombre)
        {
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Codigo))
                throw new ArgumentException("Codigo cannot be null or empty.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            try
            {

                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                theAdapter.Update(RedMedicaId,Codigo,Nombre);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating RedMedica", ex);
                throw;
            }
        }

        public static bool DeleteRedMedica(int RedMedicaId)
        {
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");

            try
            {

                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                theAdapter.Delete(RedMedicaId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting RedMedica", ex);
                throw;
            }
        }

        public static bool ExisteRedClienteRedMedica(int ClienteId, int RedMedicaId)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            if (RedMedicaId <= 0)
                throw new ArgumentException("RedMedicaId cannot be less than or equal to zero.");

            RedMedica ObjRedMedica = null;
            bool Existe = false;
            try
            {
                //List<RedMedica> theList = getRedMedicaListByClienteId(ClienteId);
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                RedMedicaDS.RedMedicaDataTable theTable = theAdapter.GetRedMedicaByClienteId(ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedMedicaDS.RedMedicaRow row in theTable.Rows)
                    {
                        ObjRedMedica = FillRecord(row);
                        if (ObjRedMedica.RedMedicaId == RedMedicaId)
                            Existe=true;
                    }
                }
                return Existe;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Validating ExisteRedClienteRedMedica", ex);
                throw;
            }
        }

        public static void InsertRedMedicaProveedor ( int ProveedorID, int RedMedicaID )
        {
            if (ProveedorID <= 0)
                throw new ArgumentException("ProveedorID cannot be <= 0.");
            if (RedMedicaID <= 0)
                throw new ArgumentException("RedMedicaID cannot be <= 0.");
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                theAdapter.InsertRedMedicaProveedor(RedMedicaID, ProveedorID);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting new RedMedicaProveedor", ex);
                throw;
            }
        }

        public static void DeleteRedMedicaProveedor ( int ProveedorID, int RedMedicaID )
        {
            if (ProveedorID <= 0)
                throw new ArgumentException("ProveedorID cannot be <= 0.");
            if (RedMedicaID <= 0)
                throw new ArgumentException("RedMedicaID cannot be <= 0.");
            try
            {
                RedMedicaDSTableAdapters.RedMedicaTableAdapter theAdapter = new RedMedicaDSTableAdapters.RedMedicaTableAdapter();
                theAdapter.DeleteRedMedicaProveedor(ProveedorID, RedMedicaID);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting new RedMedicaProveedor", ex);
                throw;
            }
        }
    }
}