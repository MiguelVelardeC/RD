using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;
using Artexacta.App.TipoEstudio;
namespace Artexacta.App.ProveedorPrestaciones.BLL
{
    /// <summary>
    /// Summary description for RedClientePrestacionesBLL
    /// </summary>
    
    public class TiposEstudiosProvPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public TiposEstudiosProvPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected static object[] GetTiposEstudios()
        {
            string sTiposEstudios = System.Web.Configuration.WebConfigurationManager.AppSettings["TiposEstudios"];
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            object[] table = jss.Deserialize<dynamic>(sTiposEstudios);
            return table;
        }

        private static TiposEstudiosProvPrestaciones FillRecord(string id, string nombre)
        {
            TiposEstudiosProvPrestaciones theNewRecord = new TiposEstudiosProvPrestaciones(
                id
                , nombre);

            return theNewRecord;
        }
        private static TiposEstudiosProvPrestacionesCategoria FillRecord(ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesCategoriaEstudioRow row)
        {
            TiposEstudiosProvPrestacionesCategoria theNewRecord = new TiposEstudiosProvPrestacionesCategoria(
                 row.ProveedorId
               , row.NombreCategoria
               , row.CategoriaId);

            return theNewRecord;
        }

        public static List<TiposEstudiosProvPrestaciones> GetAllTipoPrestaciones()
        {
            List<TiposEstudiosProvPrestaciones> theList = new List<TiposEstudiosProvPrestaciones>();
            TiposEstudiosProvPrestaciones theRedTipoPrestaciones = null;
            try
            {
                object[] theTable = GetTiposEstudios();

                if (theTable != null && theTable.Count() > 0)
                {
                    foreach (Dictionary<string, object> row in theTable)
                    {
                        theRedTipoPrestaciones = FillRecord(row["id"].ToString(), row["nombre"].ToString());
                        theList.Add(theRedTipoPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedTipoPrestaciones", ex);
                throw;
            }
            return theList;
        }

        private static Artexacta.App.TipoEstudio.TipoEstudio FillRecord(ProveedorPrestacionesDS.Prov_LabImgCar_Prestaciones_TipoEstudioRow row)
        {
            Artexacta.App.TipoEstudio.TipoEstudio objTipoEstudio = new Artexacta.App.TipoEstudio.TipoEstudio(
                row.EstudioId
                , row.Nombre
                , row.IsParentIdNull() ? 0 : row.ParentId
                , row.CantHijos
                );

            return objTipoEstudio;
        }
        public static List<Artexacta.App.TipoEstudio.TipoEstudio> getTipoEstudioParentList(int ClienteId,int ProveedorId,string Categoria)
        {
            List<Artexacta.App.TipoEstudio.TipoEstudio> theList = new List<Artexacta.App.TipoEstudio.TipoEstudio>();
            List<Artexacta.App.TipoEstudio.TipoEstudio> theListResult = new List<Artexacta.App.TipoEstudio.TipoEstudio>();

            try
            {
                ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_Prestaciones_TipoEstudioTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_Prestaciones_TipoEstudioTableAdapter() ;
                ProveedorPrestacionesDS.Prov_LabImgCar_Prestaciones_TipoEstudioDataTable theTable = theAdapter.GetProv_LabImgCar_Prestaciones_TipoEstudio(ClienteId,ProveedorId,Categoria,0);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.Prov_LabImgCar_Prestaciones_TipoEstudioRow row in theTable.Rows)
                    {
                        Artexacta.App.TipoEstudio.TipoEstudio theTipoEstudio = FillRecord(row);
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
            for (int i = 0; i < theList.Count; i++)
            {
                if (theList[i].CantHijos > 0 && theList[i].PadreId == 0)
                    theListResult.Add(theList[i]);
                else
                {
                    Artexacta.App.TipoEstudio.TipoEstudio theList2 = new Artexacta.App.TipoEstudio.TipoEstudio();
                    theList2.Nombre = "----Existe Este Estudio Pero El Proveedor No Ofrece Ningun Servicio---";
                    theList2.TipoEstudioId = 0;
                    theList[i].CantHijos = -1;
                    theListResult.Add(theList[i]);
                }
            }
            return theListResult;
            
        }
        public static List<Artexacta.App.TipoEstudio.TipoEstudio> GetAllTipoEstudioChildrensList(int ProveedorId,  int tipoEstudioId)
        {
            if (tipoEstudioId <= 0)
                throw new ArgumentException("TipoEstudioId no puede ser menor o igual que cero");

            List<Artexacta.App.TipoEstudio.TipoEstudio> theList = new List<Artexacta.App.TipoEstudio.TipoEstudio>();
            Artexacta.App.TipoEstudio.TipoEstudio theTipoEstudio = null;
            try
            {
                ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_Prestaciones_TipoEstudioTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_Prestaciones_TipoEstudioTableAdapter();
                ProveedorPrestacionesDS.Prov_LabImgCar_Prestaciones_TipoEstudioDataTable theTable = theAdapter.GetProv_LabImgCar_Prestaciones_TipoEstudioChildren(ProveedorId,tipoEstudioId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.Prov_LabImgCar_Prestaciones_TipoEstudioRow row in theTable.Rows)
                    {
                        theTipoEstudio = FillRecord(row);
                        theList.Add(theTipoEstudio);
                    }
                }
                if (theList.Count==0)
                {
                    Artexacta.App.TipoEstudio.TipoEstudio SinEstudio=new Artexacta.App.TipoEstudio.TipoEstudio();
                    SinEstudio.Nombre = "- No Existe Estudio Para Esta Categoria Consulte Con su Proveedor -";
                    SinEstudio.TipoEstudioId = 0;
                    theList.Add(SinEstudio);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list TipoEstudio", ex);
                throw;
            }
            return theList;
        }

        public static List<TiposEstudiosProvPrestacionesCategoria> GetAllEstudioxProveedorxCategoria(int ProveedorId)
        {
            if (ProveedorId <= 0)
                throw new ArgumentException("TipoEstudioId no puede ser menor o igual que cero");

            List<TiposEstudiosProvPrestacionesCategoria> theList = new List<TiposEstudiosProvPrestacionesCategoria>();
            TiposEstudiosProvPrestacionesCategoria theTipoEstudio = null;
            try
            {
                ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarPrestacionesCategoriaEstudioTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarPrestacionesCategoriaEstudioTableAdapter();
                ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesCategoriaEstudioDataTable theTable = theAdapter.GetProv_LabImgCar_Prestaciones_CategoriaEstudio(ProveedorId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesCategoriaEstudioRow row in theTable.Rows)
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
    }

    public class RedProvLabImgCarPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedProvLabImgCarPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedProvLabImgCarPrestaciones FillRecord(ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesRow row)
        {
            RedProvLabImgCarPrestaciones theNewRecord = new RedProvLabImgCarPrestaciones(
                  row.EstudioId
                , row.Estudio
                , row.CategoriaId);

            return theNewRecord;
        }

        public static List<RedProvLabImgCarPrestaciones> GetProvLabImgCarPrestaciones(string TipoEstudio)
        {
            List<RedProvLabImgCarPrestaciones> theList = new List<RedProvLabImgCarPrestaciones>();
            RedProvLabImgCarPrestaciones theRedProvLabImgCarPrestaciones = null;
            try
            {
                ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarPrestacionesTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarPrestacionesTableAdapter();
                ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesDataTable theTable = theAdapter.GetProvLabImgCarPrestacionesNotSaved(TipoEstudio);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.RedProvLabImgCarPrestacionesRow row in theTable.Rows)
                    {
                        theRedProvLabImgCarPrestaciones = FillRecord(row);
                        theList.Add(theRedProvLabImgCarPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedProvLabImgCarPrestaciones", ex);
                throw;
            }
            return theList;
        }
    }

    public class RedProvLabImgCarDetallePrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedProvLabImgCarDetallePrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedProvLabImgCarDetallePrestaciones FillRecord(ProveedorPrestacionesDS.RedProvLabImgCarDetallePrestacionesRow row)
        {
            RedProvLabImgCarDetallePrestaciones theNewRecord = new RedProvLabImgCarDetallePrestaciones(
                row.detId
                , row.EstudioId
                , row.Estudio
                , row.ParentId
                , row.CategoriaId
                , row.detPrecio);

            return theNewRecord;
        }

        public static List<RedProvLabImgCarDetallePrestaciones> GetProvLabImgCarDetallePrestaciones(int ProveedorId, string TipoEstudio, int ParentId)
        {
            List<RedProvLabImgCarDetallePrestaciones> theList = new List<RedProvLabImgCarDetallePrestaciones>();
            RedProvLabImgCarDetallePrestaciones theRedProvLabImgCarDetallePrestaciones = null;
            try
            {
                ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter();
                ProveedorPrestacionesDS.RedProvLabImgCarDetallePrestacionesDataTable theTable = theAdapter.GetProvLabImgCarDetallePrestaciones(ProveedorId, TipoEstudio, ParentId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.RedProvLabImgCarDetallePrestacionesRow row in theTable.Rows)
                    {
                        theRedProvLabImgCarDetallePrestaciones = FillRecord(row);
                        theList.Add(theRedProvLabImgCarDetallePrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedProvLabImgCarDetallePrestaciones", ex);
                throw;
            }
            return theList;
        }

        //Para Recuperar El Precio x Estudio de la lista de red proveedores lab Imagenologia
        private static RedProvLabImgCarDetallePrestaciones FillRecord(ProveedorPrestacionesDS.Prov_LabImgCar_DetallePrestacionRow row)
        {
            RedProvLabImgCarDetallePrestaciones theNewRecord = new RedProvLabImgCarDetallePrestaciones(
                row.detId
                ,row.EstudioId
                ,row.ParentId
                ,row.CategoriaId
                ,row.detPrecio);

            return theNewRecord;
        }

        public static List<RedProvLabImgCarDetallePrestaciones> GetProvLabImgCarDetallePrestacionesXProveedoresAndEstudioId(int ProveedorId, int TipoEstudioId)
        {
            List<RedProvLabImgCarDetallePrestaciones> theList = new List<RedProvLabImgCarDetallePrestaciones>();
            RedProvLabImgCarDetallePrestaciones theRedProvLabImgCarDetallePrestaciones = null;
            try
            {
                ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_DetallePrestacionTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.Prov_LabImgCar_DetallePrestacionTableAdapter();
                ProveedorPrestacionesDS.Prov_LabImgCar_DetallePrestacionDataTable theTable = theAdapter.GetProv_LabImgCar_DetallePrestacionxEstudio(0,ProveedorId,0,TipoEstudioId,"TT");

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorPrestacionesDS.Prov_LabImgCar_DetallePrestacionRow row in theTable.Rows)
                    {
                        theRedProvLabImgCarDetallePrestaciones = FillRecord(row);
                        theList.Add(theRedProvLabImgCarDetallePrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedProvLabImgCarDetallePrestaciones", ex);
                throw;
            }
            return theList;
        }


        public static int UpdateProvLabImgCarDetallePrestaciones(int detId, int ProveedorId, string CategoriaId, int ParentId, int EstudioId, decimal detPrecio)
        {
            int? newDetId = detId;
            if (ProveedorId <= 0)
                throw new ArgumentException("ProveedorId cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter();
                    theAdapter.InsertProvLabImgCarDetallePrestaciones(ref newDetId, ProveedorId, CategoriaId, ParentId, EstudioId, detPrecio);
                }
                else
                {
                    ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter();
                    theAdapter.UpdateProvLabImgCarDetallePrestaciones(detId, ProveedorId, CategoriaId, ParentId, EstudioId, detPrecio);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating ProvLabImgCarDetallePrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteProvLabImgCarDetallePrestaciones(int detId, string CategoriaId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(CategoriaId))
                throw new ArgumentException("CategoriaId cannot be empty or null.");
            try
            {
                ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter theAdapter = new ProveedorPrestacionesDSTableAdapters.RedProvLabImgCarDetallePrestacionesTableAdapter();
                theAdapter.DeleteProvLabImgCarDetallePrestaciones(detId, CategoriaId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting ProvLabImgCarDetallePrestaciones", ex);
                throw;
            }
        }
    }
}
