using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.TipoEnfermedad.BLL
{
    /// <summary>
    /// Summary description for TipoEnfermedadBLL
    /// </summary>
    public class TipoEnfermedadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TipoEnfermedadBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static TipoEnfermedad FillRecord(TipoEnfermedadDS.TipoEnfermedadRow row)
        {
            TipoEnfermedad objTipoEnfermedad = new 
                TipoEnfermedad(
                    row.TipoEnfermedadId
                    ,row.Nombre
                );

            return objTipoEnfermedad;
        }

        public static TipoEnfermedad GetTipoEnfermedadByTipoEnfermedadId(int tipoEnfermedadId)
        {
            if (tipoEnfermedadId <= 0)
                throw new ArgumentException("EstudioId no puede ser menor o igual a cero.");

            TipoEnfermedad TheTipoEnfermedad = null;
            try
            {
                TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter theAdapter = new TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter();
                TipoEnfermedadDS.TipoEnfermedadDataTable theTable = theAdapter.GetTipoEnfermedadById(tipoEnfermedadId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TipoEnfermedadDS.TipoEnfermedadRow row = theTable[0];
                    TheTipoEnfermedad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting TipoEnfermedad data", ex);
                throw;
            }
            return TheTipoEnfermedad;
        }

        public static List<TipoEnfermedad> getTipoEnfermedadList()
        {
            List<TipoEnfermedad> theList = new List<TipoEnfermedad>();
            TipoEnfermedad theTipoEnfermedad = null;

            try
            {
                TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter theAdapter = new TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter();
                TipoEnfermedadDS.TipoEnfermedadDataTable theTable = theAdapter.GetAllTipoEnfermedad();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEnfermedadDS.TipoEnfermedadRow row in theTable.Rows)
                    {
                        theTipoEnfermedad = FillRecord(row);
                        theList.Add(theTipoEnfermedad);
                    }
                }
                
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEnfermedad", ex);
                throw ex;
            }
            return theList;
        }

        public static int InsertTipoEnfermedad(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del TipoEnfermedad no puede ser nulo o vacio");
            }
            int? tipoEnfermedadId = 0;
            TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter theAdapter = new TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter();
            theAdapter.InsertTipoEnfermedad(nombre, ref tipoEnfermedadId);

            if (tipoEnfermedadId == null || tipoEnfermedadId.Value == 0)
            {
                throw new Exception("Ocurrio un error al generar la llave primaria de la tabla");
            }

            return tipoEnfermedadId.Value;
        }

        public static void UpdateTipoEnfermedad(int tipoEnfermedadId, string nombre)
        {
            if (tipoEnfermedadId <= 0)
            {
                throw new ArgumentException("TipoEnfermedadId no puede ser menor o igual que cero");
            }
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del TipoEnfermedad no puede ser nulo o vacio");
            }
            TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter theAdapter = new TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter();
            theAdapter.UpdateTipoEnfermedad(nombre, tipoEnfermedadId);

        }

        public static void DeleteTipoEnfermedad(int tipoEnfermedadId)
        {
            if (tipoEnfermedadId <= 0)
            {
                throw new ArgumentException("TipoEnfermedadId no puede ser menor o igual que cero");
            }
            TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter theAdapter = new TipoEnfermedadDSTableAdapters.TipoEnfermedadTableAdapter();
            theAdapter.DeleteTipoEnfermedadById(tipoEnfermedadId);

        }
    }
}