using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.TipoConcentracion.BLL
{
    /// <summary>
    /// Summary description for TipoConcentracionBLL
    /// </summary>
    public class TipoConcentracionBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TipoConcentracionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static TipoConcentracion FillRecord(TipoConcentracionDS.TipoConcentracionRow row)
        {
            TipoConcentracion objTipoMed = new TipoConcentracion(
                    row.TipoConcentracionId,
                    row.Nombre);
            return objTipoMed;
        }

        public static List<TipoConcentracion> GetTipoConcentracionList()
        {
            List<TipoConcentracion> theList = new List<TipoConcentracion>();
            TipoConcentracion objTipoMed = null;

            try
            {
                TipoConcentracionDSTableAdapters.TipoConcentracionTableAdapter theAdapter = new TipoConcentracionDSTableAdapters.TipoConcentracionTableAdapter();
                TipoConcentracionDS.TipoConcentracionDataTable theTable = theAdapter.GetAllTipoConcentracion();
                if (theTable != null && theTable.Rows.Count > 0)
                foreach(TipoConcentracionDS.TipoConcentracionRow row in theTable.Rows)
                {
                    objTipoMed = FillRecord(row);
                    theList.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoConcentracion", ex);
                throw;
            }
            return theList;
        }

        public static List<TipoConcentracion> GetTipoConcentracionByMedicamentoClaId ( int MedicamentoClaId, string TipoMedicamentoId )
        {
            if (MedicamentoClaId <= 0)
                throw new ArgumentException("MedicamentoClaId cannot be null or empty.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("MedicamentoClaId cannot be null or empty.");

            List<TipoConcentracion> listTipoMed = new List<TipoConcentracion>();

            try
            {
                TipoConcentracionDSTableAdapters.TipoConcentracionTableAdapter theAdapter = new TipoConcentracionDSTableAdapters.TipoConcentracionTableAdapter();
                TipoConcentracionDS.TipoConcentracionDataTable theTable = theAdapter.GetTipoConcentracionByMedicamentoId(MedicamentoClaId, TipoMedicamentoId);
                foreach (TipoConcentracionDS.TipoConcentracionRow row in theTable.Rows)
                {
                    TipoConcentracion objTipoMed = FillRecord(row);
                    listTipoMed.Add(objTipoMed);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting TipoConcentracion list", ex);
                throw;
            }
            return listTipoMed;
        }
    }
}