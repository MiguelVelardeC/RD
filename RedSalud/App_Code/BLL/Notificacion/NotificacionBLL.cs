using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data;

/// <summary>
/// Descripción breve de Notificacion
/// </summary>
/// 
namespace Artexacta.App.Notificacion.BLL
{
    public class NotificacionBLL
    {

        private static readonly ILog log = LogManager.GetLogger("Standard");
        public NotificacionBLL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        private static Notificacion FillRecord(NotificacionDS.NotificacionListspRow row)
        {
            Notificacion objTipoEnfermedad = new
                Notificacion(
                    row.Notificacionid, row.TipoNotificacion, row.Descripcion,
                    row.GrupoNotificacion, row.Fecha_Creacion, row.FechaStart,
                    row.FechaEnd, row.Estado, row.PrioridadElemento
                );

            return objTipoEnfermedad;
        }
        
        public static List<Notificacion> getNotificacionListar()
        {
            List<Notificacion> theList = new List<Notificacion>();
            Notificacion theNotificacion = null;

            try
            {
                NotificacionDSTableAdapters.NotificacionListspTableAdapter theadapter = new NotificacionDSTableAdapters.NotificacionListspTableAdapter();
                NotificacionDS.NotificacionListspDataTable thetable = theadapter.GetNotificacionList();
                if (thetable != null && thetable.Rows.Count > 0)
                {
                    foreach (NotificacionDS.NotificacionListspRow row in thetable.Rows)
                    {
                        theNotificacion = FillRecord(row);
                        theList.Add(theNotificacion);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Notificacion", ex);
                throw ex;
            }

            return theList;
        }

        public static int NotificacionInsert(string Descripcion, int tiponotificacionid, string GrupoNotificacion,
            DateTime fechastart, DateTime fechaend, int Prioridad, string titulo, int fileid)
        {
            NotificacionDSTableAdapters.NotificacionListspTableAdapter tableAdapter =
                new NotificacionDSTableAdapters.NotificacionListspTableAdapter();
            int res= tableAdapter.NotificacionInsert(Descripcion, tiponotificacionid, GrupoNotificacion, fechastart,
                                                fechaend, Prioridad, titulo, fileid);

            return res;

        }

        public static DataTable getTipoNotificacionesListar() 
        { 
            DataTable dt = new DataTable();
            NotificacionDSTableAdapters.usp_TipoNotificacionListTableAdapter tableAdapter = new NotificacionDSTableAdapters.usp_TipoNotificacionListTableAdapter();
            dt = tableAdapter.GetTipoNotificacionList();

            return dt;
        
        }

        public static DataTable getGrupoNotificacionesListar() 
        {
            DataTable dt = new DataTable();
            NotificacionDSTableAdapters.usp_GrupoNotificacionListTableAdapter tableAdapter = new NotificacionDSTableAdapters.usp_GrupoNotificacionListTableAdapter();
            dt = tableAdapter.GetGrupoNotificacionList();
            return dt;
        }

        public static int FileNotificacionesInsert(string fileName, string extension, long fileSize, string filestoragePath) 
        {
            int fileid;

           
            NotificacionDSTableAdapters.FileNotificacionesTableAdapter tableAdapter = 
                new NotificacionDSTableAdapters.FileNotificacionesTableAdapter(); ;
            fileid = (int)tableAdapter.FileNotificacionesInsert(fileName,extension,fileSize,filestoragePath);
            

            return fileid;        
        
        }
        public static string FileNotificacionesUpdate(int fileid, string filestoragePath) 
        {
            NotificacionDSTableAdapters.FileNotificacionesTableAdapter tableAdapter =
                new NotificacionDSTableAdapters.FileNotificacionesTableAdapter();
            tableAdapter.FileNotificacionesUpdate(fileid, filestoragePath);

            return "ok";
                    
        }
        public static int NotificacionUpdateStatus(int notificacionid) 
        {
            int res;
            NotificacionDSTableAdapters.NotificacionListspTableAdapter tableAdapter =
                new NotificacionDSTableAdapters.NotificacionListspTableAdapter();

           res=(int)tableAdapter.NotificacionUpdateStatus(notificacionid);
            return res;
        }

    }
}