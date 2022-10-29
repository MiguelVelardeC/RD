using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.CLAPrestacionOdontologica.BLL
{
    /// <summary>
    /// Summary description for PrestacionOdontologicaBLL
    /// </summary>
    public class PrestacionOdontologicaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PrestacionOdontologicaBLL () {}
        private static PrestacionOdontologica FillRecord(PrestacionOdontologicaDS.PrestacionOdontologicaRow row)
        {
            PrestacionOdontologica objTipoMed = new PrestacionOdontologica(
                    row.PrestacionOdontologicaId,
                    row.Nombre,
                    row.Categoria);
            return objTipoMed;
        }

        //public static List<PrestacionOdontologica> GetPrestacionOdontologicaList()
        //{
        //    List<PrestacionOdontologica> theList = new List<PrestacionOdontologica>();
        //    PrestacionOdontologica objTipoMed = null;

        //    try
        //    {
        //        PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter theAdapter = new PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter();
        //        PrestacionOdontologicaDS.PrestacionOdontologicaDataTable theTable = theAdapter.GetAllPrestacionOdontologica();
        //        if (theTable != null && theTable.Rows.Count > 0)
        //        foreach(PrestacionOdontologicaDS.PrestacionOdontologicaRow row in theTable.Rows)
        //        {
        //            objTipoMed = FillRecord(row);
        //            theList.Add(objTipoMed);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("An error was ocurred while geting list PrestacionOdontologica", ex);
        //        throw;
        //    }
        //    return theList;
        //}
        public List<PrestacionOdontologica> getAllPrestacionOdontologica ()
        {
            List<PrestacionOdontologica> _cache = new List<PrestacionOdontologica>();
            SearchPrestacionOdontologicaBy(ref _cache, 0, int.MaxValue, "");
            return _cache;
        }

        public static List<PrestacionOdontologica> getListAllPrestacionOdontologica()
        {
            List<PrestacionOdontologica> _cache = new List<PrestacionOdontologica>();
            SearchPrestacionOdontologicaBy(ref _cache, 0, int.MaxValue, "");
            return _cache;
        }
        public static int SearchPrestacionOdontologicaBy ( ref List<PrestacionOdontologica> _cache, int firstRow, int PageSize, string filter )
        {
            int? Total = 0;
            try
            {
                PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter theAdapter = new PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter();
                PrestacionOdontologicaDS.PrestacionOdontologicaDataTable theTable = theAdapter.Search(firstRow, PageSize, filter, ref Total);
                foreach (PrestacionOdontologicaDS.PrestacionOdontologicaRow row in theTable.Rows)
                {
                    PrestacionOdontologica objTipoMed = FillRecord(row);
                    _cache.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PrestacionOdontologica list", ex);
                throw;
            }
            return Total.Value;
        }
        public static List<PrestacionOdontologica> getAllPrestacionOdontologicaNew(int clienteId,int ProveedorId)
        {
            List<PrestacionOdontologica> _cache = new List<PrestacionOdontologica>();
            SearchPrestacionOdontologicaByCliYPro(ref _cache,clienteId,ProveedorId, 0, int.MaxValue, "");
            return _cache;
        }
        public static  int SearchPrestacionOdontologicaByCliYPro(ref List<PrestacionOdontologica> _cache,int ClienteId,int ProveedorId, int firstRow, int PageSize, string filter)
        {
            int? Total = 0;
            try
            {
                PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter theAdapter = new PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter();
                PrestacionOdontologicaDS.PrestacionOdontologicaDataTable theTable = theAdapter.SearchClientexProveedor(ClienteId,ProveedorId,firstRow, PageSize, filter, ref Total);
                foreach (PrestacionOdontologicaDS.PrestacionOdontologicaRow row in theTable.Rows)
                {
                    PrestacionOdontologica objTipoMed = FillRecord(row);
                    _cache.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PrestacionOdontologica list", ex);
                throw;
            }
            return Total.Value;
        }
        //Este muestra la lista de prestaciones Odontologicas incluyendo la consulta medica
        public static List<PrestacionOdontologica> getPrestacionAllOdontologicaNew(int clienteId, int ProveedorId)
        {
            List<PrestacionOdontologica> _cache = new List<PrestacionOdontologica>();
            SearchPrestacionOdontologicaByCliYProAll(ref _cache, clienteId, ProveedorId, 0, int.MaxValue, "");
            return _cache;
        }
        public static int SearchPrestacionOdontologicaByCliYProAll(ref List<PrestacionOdontologica> _cache, int ClienteId, int ProveedorId, int firstRow, int PageSize, string filter)
        {
            int? Total = 0;
            try
            {
                PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter theAdapter = new PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter();
                PrestacionOdontologicaDS.PrestacionOdontologicaDataTable theTable = theAdapter.SearchPrestacionAllOdontologicaNew(ClienteId, ProveedorId, firstRow, PageSize, filter, ref Total);
                foreach (PrestacionOdontologicaDS.PrestacionOdontologicaRow row in theTable.Rows)
                {
                    PrestacionOdontologica objTipoMed = FillRecord(row);
                    _cache.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PrestacionOdontologica list", ex);
                throw;
            }
            return Total.Value;
        }


        public static PrestacionOdontologica GetPrestacionOdontologicaById ( int PrestacionOdontologicaId )
        {
            if (PrestacionOdontologicaId <= 0)
                throw new ArgumentException("PrestacionOdontologicaId cannot be null or empty.");

            PrestacionOdontologica objTipoMed = new PrestacionOdontologica();

            try
            {
                PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter theAdapter = new PrestacionOdontologicaDSTableAdapters.PrestacionOdontologicaTableAdapter();
                PrestacionOdontologicaDS.PrestacionOdontologicaDataTable theTable = theAdapter.GetPrestacionOdontologica(PrestacionOdontologicaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objTipoMed = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting PrestacionOdontologica list", ex);
                throw;
            }
            return objTipoMed;
        }
    }
}