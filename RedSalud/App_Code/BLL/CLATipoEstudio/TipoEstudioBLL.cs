using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.TipoEstudio.BLL
{
    /// <summary>
    /// Summary description for TipoEstudioBLL
    /// </summary>
    public class TipoEstudioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TipoEstudioBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static TipoEstudio FillRecord(TipoEstudioDS.TipoEstudioRow row)
        {
            TipoEstudio objTipoEstudio = new TipoEstudio(
                row.EstudioId
                , row.Nombre
                , row.IsParentIdNull() ? 0 : row.ParentId
                , row.CantHijos
                );

            return objTipoEstudio;
        }

        public static List<TipoEstudio> getTipoEstudioList()
        {
            List<TipoEstudio> theList = new List<TipoEstudio>();
            TipoEstudio theTipoEstudio = null;
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetAllTipoEstudio();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEstudioDS.TipoEstudioRow row in theTable.Rows)
                    {
                        theTipoEstudio = FillRecord(row);
                        theList.Add(theTipoEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static List<TipoEstudio> getTipoEstudioListForDDL()
        {
            List<TipoEstudio> theList = new List<TipoEstudio>();
            theList.Add(new TipoEstudio(0, "", 0, 0));
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetAllTipoEstudio();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEstudioDS.TipoEstudioRow row in theTable.Rows)
                    {
                        TipoEstudio theTipoEstudio = FillRecord(row);
                        theList.Add(theTipoEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static List<TipoEstudio> getTipoEstudioParentList()
        {
            List<TipoEstudio> theList = new List<TipoEstudio>();
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetAllTipoEstudioParents();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEstudioDS.TipoEstudioRow row in theTable.Rows)
                    {
                        TipoEstudio theTipoEstudio = FillRecord(row);
                        if (theTipoEstudio.Nombre.Equals("EXÁMENES"))
                        {
                            theList.Insert(0, theTipoEstudio);
                        }
                        else
                        {
                            theList.Add(theTipoEstudio);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting parent list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static List<TipoEstudio> GetAllTipoEstudioChildrensList(int tipoEstudioId)
        {
            if (tipoEstudioId <= 0)
                throw new ArgumentException("TipoEstudioId no puede ser menor o igual que cero");

            List<TipoEstudio> theList = new List<TipoEstudio>();
            TipoEstudio theTipoEstudio = null;
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetAllTipoEstudioChildrens(tipoEstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEstudioDS.TipoEstudioRow row in theTable.Rows)
                    {
                        theTipoEstudio = FillRecord(row);
                        theList.Add(theTipoEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static TipoEstudio GetTipoEstudioById(int id)
        {
            List<TipoEstudio> theList = new List<TipoEstudio>();
            TipoEstudio theTipoEstudio = null;
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetTipoEstudioById(id);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TipoEstudioDS.TipoEstudioRow row = theTable[0];
                    theTipoEstudio = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw ex;
            }
            return theTipoEstudio;
        }

        public static List<TipoEstudio> getTipoEstudioListDDL()
        {
            List<TipoEstudio> theList = new List<TipoEstudio>();
            //TipoEstudio theTipoEstudioSelect = new TipoEstudio(0, "Selecciones un Tipo de Examen");
            //theList.Add(theTipoEstudioSelect);

            TipoEstudio theTipoEstudio = null;
            try
            {
                TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
                TipoEstudioDS.TipoEstudioDataTable theTable = theAdapter.GetAllTipoEstudio();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (TipoEstudioDS.TipoEstudioRow row in theTable.Rows)
                    {
                        theTipoEstudio = FillRecord(row);
                        theList.Add(theTipoEstudio);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static int InsertTipoEstudio(string nombre, int parentId)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del TipoEstudio no puede ser nulo o vacio");
            }
            int? tipoEstudioId = 0;
            TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
            theAdapter.InsertTipoEstudio(ref tipoEstudioId, nombre, parentId);

            if (tipoEstudioId == null || tipoEstudioId.Value == 0)
            {
                throw new Exception("Ocurrio un error al generar la llave primaria de la tabla");
            }

            return tipoEstudioId.Value;
        }

        public static void UpdateTipoEstudio(int tipoEstudioId, string nombre, int parentId)
        {
            if (tipoEstudioId <= 0)
            {
                throw new ArgumentException("TipoEstudioId no puede ser menor o igual que cero");
            }
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del TipoEstudio no puede ser nulo o vacio");
            }
            TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
            theAdapter.UpdateTipoEstudio(tipoEstudioId, nombre, parentId);

        }

        public static void DeleteTipoEstudio(int tipoEstudioId)
        {
            if (tipoEstudioId <= 0)
            {
                throw new ArgumentException("TipoEstudioId no puede ser menor o igual que cero");
            }
            TipoEstudioDSTableAdapters.TipoEstudioTableAdapter theAdapter = new TipoEstudioDSTableAdapters.TipoEstudioTableAdapter();
            theAdapter.DeleteTipoEstudio(tipoEstudioId);

        }
    }
}