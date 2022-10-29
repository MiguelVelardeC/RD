using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Especialidad.BLL
{
    /// <summary>
    /// Summary description for EspecialidadBLL
    /// </summary>
    public class EspecialidadBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public EspecialidadBLL()
        {
        }

        private static Especialidad FillRecord(EspecialidadDS.EspecialidadRow row)
        {
            Especialidad objEspecialidad = new Especialidad(row.EspecialidadId, row.Nombre, row.Estado, row.espTiempoAtencionVD);
            return objEspecialidad;
        }
        public static List<Especialidad> getEspecialidadListForDisplay()
        {
            List<Especialidad> theList = new List<Especialidad>();
            Especialidad theEspecialidad = null;
            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetAllEspecialidadActive();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EspecialidadDS.EspecialidadRow row in theTable.Rows)
                    {
                        theEspecialidad = FillRecord(row);
                        theList.Add(theEspecialidad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Especialidad", ex);
                throw;
            }
            return theList;
        }
        public static List<Especialidad> getEspecialidadList()
        {
            List<Especialidad> theList = new List<Especialidad>();
            Especialidad theEspecialidad = null;
            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetAllEspecialidad();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EspecialidadDS.EspecialidadRow row in theTable.Rows)
                    {
                        theEspecialidad = FillRecord(row);
                        theList.Add(theEspecialidad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Especialidad", ex);
                throw;
            }
            return theList;
        }

        public static List<Especialidad> GetEspecialidadesForAutoComplete(int? start, int? numItems, string filter, ref int? totalRows)
        {
            List<Especialidad> theList = new List<Especialidad>();
            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.SearchEspecialidad(numItems, start, filter, ref totalRows);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EspecialidadDS.EspecialidadRow row in theTable.Rows)
                    {
                        theList.Add(FillRecord(row));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Especialidad", ex);
                throw;
            }
            return theList;
        }

        public static Especialidad GetEspecialidadById(int id)
        {
            List<Especialidad> theList = new List<Especialidad>();
            Especialidad theEspecialidad = null;
            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetEspecialidadById(id);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EspecialidadDS.EspecialidadRow row = theTable[0];
                    theEspecialidad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Especialidad", ex);
                throw ex;
            }
            return theEspecialidad;
        }
        public static Especialidad GetEspecialidadxNombre(string NombreEspecialidad)
        {
            Especialidad theEspecialidad = null;

            if (string.IsNullOrEmpty(NombreEspecialidad))
                throw new ArgumentException("El Nombre de la Especialidad No puede Estar Vacio");

            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetEspecialidadByName(NombreEspecialidad);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EspecialidadDS.EspecialidadRow row = theTable[0];
                    theEspecialidad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Especialidad by Nombre Especialidad: " + NombreEspecialidad, ex);
                throw ex;
            }
            return theEspecialidad;
        }

        public static Especialidad GetEspecialidadByUserId(int UserId)
        {
            Especialidad theEspecialidad = null;

            if (UserId <= 0)
                throw new ArgumentException("El Id del Usuario no puede ser <= 0");

            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetEspecialidadByUserId(UserId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    EspecialidadDS.EspecialidadRow row = theTable[0];
                    theEspecialidad = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Especialidad by UserId: " + UserId, ex);
                throw ex;
            }
            return theEspecialidad;
        }

        public static List<Especialidad> getEspecialidadListDDL()
        {
            List<Especialidad> theList = new List<Especialidad>();
            //Especialidad theEspecialidadSelect = new Especialidad(0, "Selecciones un Tipo de Examen");
            //theList.Add(theEspecialidadSelect);

            Especialidad theEspecialidad = null;
            try
            {
                EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
                EspecialidadDS.EspecialidadDataTable theTable = theAdapter.GetAllEspecialidad();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (EspecialidadDS.EspecialidadRow row in theTable.Rows)
                    {
                        theEspecialidad = FillRecord(row);
                        theList.Add(theEspecialidad);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Especialidad", ex);
                throw;
            }
            return theList;
        }

        public static int InsertEspecialidad(string nombre, bool estado, int tiempoAtencion)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del Especialidad no puede ser nulo o vacio");
            }
            int? EspecialidadId = 0;
            EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
            theAdapter.InsertEspecialidad(nombre, estado, tiempoAtencion, ref EspecialidadId);

            if (EspecialidadId == null || EspecialidadId.Value == 0)
            {
                throw new Exception("Ocurrio un error al generar la llave primaria de la tabla");
            }

            return EspecialidadId.Value;
        }

        public static void UpdateEspecialidad(int EspecialidadId, string nombre, bool estado, int tiempoAtencion)
        {
            if (EspecialidadId <= 0)
            {
                throw new ArgumentException("EspecialidadId no puede ser menor o igual que cero");
            }
            if (string.IsNullOrEmpty(nombre))
            {
                throw new ArgumentException("El Nombre del Especialidad no puede ser nulo o vacio");
            }
            EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
            theAdapter.UpdateEspecialidad(nombre, estado, tiempoAtencion, EspecialidadId);

        }

        public static void DeleteEspecialidad(int EspecialidadId)
        {
            if (EspecialidadId <= 0)
            {
                throw new ArgumentException("EspecialidadId no puede ser menor o igual que cero");
            }
            EspecialidadDSTableAdapters.EspecialidadTableAdapter theAdapter = new EspecialidadDSTableAdapters.EspecialidadTableAdapter();
            theAdapter.DeleteEspecialidad(EspecialidadId);
        }
    }
}