using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for EstudioBLL
    /// </summary>
    public class EstudioBLL
    {
        public EstudioBLL()
        {
            
        }

        public static List<Estudio> GetEstudios(int intClienteId, string varCiudad, bool boolDeshabilitado)
        {
            EstudiosDSTableAdapters.EstudiosTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosTableAdapter();
            EstudiosDS.EstudiosDataTable table = adapter.GetEstudios(intClienteId, varCiudad, boolDeshabilitado);

            List<Estudio> list = new List<Estudio>();
            foreach (var row in table)
            {
                Estudio obj = new Estudio()
                {
                    EstudioId = row.estudioId,
                    NombreEstudio = row.nombre,
                    EstudioGrupoIds = row.estudioGrupoIds,
                    Realizado = false,
                    ParentEstudioId = row.parentEstudioId
                };
                list.Add(obj);
            }

            return list;
            
        }

        public static List<Estudio> GetEstudiosPadres()
        {
            EstudiosDSTableAdapters.EstudiosTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosTableAdapter();
            EstudiosDS.EstudiosDataTable table = adapter.GetEstudiosPadres();

            List<Estudio> list = new List<Estudio>();
            foreach (var row in table)
            {
                Estudio obj = new Estudio()
                {
                    EstudioId = row.estudioId,
                    NombreEstudio = row.nombre,
                    EstudioGrupoIds = "",
                    Realizado = false,
                    ParentEstudioId = 0
                };
                list.Add(obj);
            }

            return list;

        }

        public static List<Estudio> GetEstudiosByCitaDesgravamenId(int citaDesgravamenId, int estudioId)
        {
            return GetEstudiosByCitaDesgravamenId(citaDesgravamenId, 0, estudioId);
        }

        public static List<Estudio> GetEstudiosByCitaDesgravamenIdForEjecutivo(int citaDesgravamenId)
        {
            return GetEstudiosByCitaDesgravamenIdForEjecutivo(citaDesgravamenId, 0);
        }

        public static List<Estudio> GetEstudiosByCitaDesgravamenIdForEjecutivo(int citaDesgravamenId, int proveedorMedicoId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            if (proveedorMedicoId < 0)
            {
                throw new ArgumentException("ProveedorMedicoId cannot be equals or less than zero");
            }

            EstudiosDSTableAdapters.EstudioCitaDesgravamenTableAdapter adapter =
                new EstudiosDSTableAdapters.EstudioCitaDesgravamenTableAdapter();
            EstudiosDS.EstudioCitaDesgravamenDataTable table = adapter.GetEstudiosByCitaDesgravamenForEjecutivos(citaDesgravamenId, proveedorMedicoId);

            List<Estudio> list = new List<Estudio>();
            foreach (var row in table)
            {
                Estudio obj = new Estudio()
                {
                    EstudioId = row.estudioId,
                    NombreEstudio = row.nombre,
                    EstudioGrupoIds = row.estudioGrupoIds,
                    Realizado = row.realizado,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    NombreProveedor = row.nombreProveedor,
                    FechaCita = row.IsfechaCitaNull() ? DateTime.MinValue : row.fechaCita,
                    NecesitaCita = row.IsnecesitaCitaNull() ? false : row.necesitaCita,
                    FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita
                };
                list.Add(obj);
            }

            return list;
        }

        public static List<Estudio> GetEstudiosByCitaDesgravamenId(int citaDesgravamenId, int proveedorMedicoId, int estudioId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            EstudiosDSTableAdapters.EstudioCitaDesgravamenTableAdapter adapter =
                new EstudiosDSTableAdapters.EstudioCitaDesgravamenTableAdapter();
            EstudiosDS.EstudioCitaDesgravamenDataTable table = adapter.GetEstudiosByCitaDesgravamen(citaDesgravamenId, proveedorMedicoId, estudioId);

            List<Estudio> list = new List<Estudio>();
            foreach (var row in table)
            {
                Estudio obj = new Estudio()
                {
                    EstudioId = row.estudioId,
                    NombreEstudio = row.nombre,
                    EstudioGrupoIds = row.estudioGrupoIds,
                    Realizado = row.realizado,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    NombreProveedor = row.nombreProveedor,
                    FechaCita = row.IsfechaCitaNull() ? DateTime.MinValue : row.fechaCita,
                    NecesitaCita = row.IsnecesitaCitaNull() ? false : row.necesitaCita,
                    FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita
                };
                list.Add(obj);
            }

            return list;

        }

        public static List<EstudioGrupo> GetEstudioGrupo()
        {
            EstudiosDSTableAdapters.GrupoEstudioTableAdapter adapter = new EstudiosDSTableAdapters.GrupoEstudioTableAdapter();
            EstudiosDS.GrupoEstudioDataTable table = adapter.GetGruposEstudio();

            List<EstudioGrupo> list = new List<EstudioGrupo>();
            foreach (var row in table)
            {
                EstudioGrupo obj = new EstudioGrupo()
                {
                    EstudioGrupoId = row.estudioGrupoId,
                    NombreGrupo = row.nombreGrupo
                };
                list.Add(obj);
            }

            return list;
        }

        public static void MarcarEstudiosComoRealizado(int citaDesgravamenId, List<int> estudiosAMarcar)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            if (estudiosAMarcar == null)
            {
                throw new ArgumentException("estudiosAMarcar cannot be null");
            }

            DataTable tblEstudios = new DataTable();
            tblEstudios.Columns.Add("objectId", typeof(int));

            foreach (int estudioId in estudiosAMarcar)
            {
                DataRow row = tblEstudios.NewRow();
                row["objectId"] = estudioId;
                tblEstudios.Rows.Add(row);
            }

            EstudiosDSTableAdapters.EstudiosTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosTableAdapter();
            adapter.MarcarEstudiosComoRealizado(citaDesgravamenId, tblEstudios);
        }

        public static void MarcarEstudiosComoNoRealizado(int citaDesgravamenId, List<int> estudiosAMarcar)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("CitaDesgravamenId cannot be equals or less than zero");
            }

            if (estudiosAMarcar == null)
            {
                throw new ArgumentException("estudiosAMarcar cannot be null");
            }

            DataTable tblEstudios = new DataTable();
            tblEstudios.Columns.Add("objectId", typeof(int));

            foreach (int estudioId in estudiosAMarcar)
            {
                DataRow row = tblEstudios.NewRow();
                row["objectId"] = estudioId;
                tblEstudios.Rows.Add(row);
            }

            EstudiosDSTableAdapters.EstudiosTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosTableAdapter();
            adapter.MarcarEstudiosComoNoRealizado(citaDesgravamenId, tblEstudios);
        }

        public static List<Estudio> GetEstudiosAll(int intClienteId, bool boolDeshabilitado)
        {
            EstudiosDSTableAdapters.EstudiosTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosTableAdapter();
            EstudiosDS.EstudiosDataTable table = adapter.GetEstudiosAll(intClienteId, boolDeshabilitado);

            List<Estudio> list = new List<Estudio>();
            foreach (var row in table)
            {
                Estudio obj = new Estudio()
                {
                    EstudioId = row.estudioId,
                    NombreEstudio = row.nombre,
                    ParentEstudioId = row.parentEstudioId
                };
                list.Add(obj);
            }
            return list;

        }

        public static List<ComboContainer> GetEstudiosAllCombo()
        {
            EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosComboTableAdapter();
            EstudiosDS.EstudiosComboDataTable table = adapter.GetEstudiosAllCombo();

            List<ComboContainer> list = new List<ComboContainer>();
            foreach (var row in table)
            {
                ComboContainer obj = new ComboContainer()
                {
                    ContainerId = row.estudioId.ToString(),
                    ContainerName = row.estudioNombre
                };
                list.Add(obj);
            }
            return list;

        }

        public static List<ComboContainer> GetCategoriasEstudio()
        {
            EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosComboTableAdapter();
            EstudiosDS.EstudiosComboDataTable table = adapter.GetCategoriasCombo();

            List<ComboContainer> list = new List<ComboContainer>();
            foreach (var row in table)
            {
                ComboContainer obj = new ComboContainer()
                {
                    ContainerId = row.categoriaId.ToString(),
                    ContainerName = row.categoriaNombre
                };
                list.Add(obj);
            }
            return list;

        }

        public static EstudioDesgravamen GetEstudioById(int estudioId)
        {
            EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosComboTableAdapter();
            EstudiosDS.EstudiosComboDataTable table = adapter.GetEstudioById(estudioId);

            EstudioDesgravamen estudio = null;

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                estudio = new EstudioDesgravamen()
                {
                    EstudioId = table[0].estudioId,
                    EstudioNombre = table[0].estudioNombre,
                    CategoriaId = table[0].categoriaId,
                    CategoriaNombre = table[0].categoriaNombre
                };
            }

            return estudio;
        }

        public static int InsertEstudio(EstudioDesgravamen obj)
        {
            int? estudioId = 0;
            int estudioResult = 0;
            if (!string.IsNullOrEmpty(obj.EstudioNombre))
            {

                EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosComboTableAdapter();
                /*EstudiosDS.EstudiosComboDataTable table = */
                estudioResult = adapter.InsertEstudio(obj.EstudioNombre, obj.CategoriaId, ref estudioId);
                estudioResult = estudioId.Value;
            }

            return estudioResult;
        }

        public static void UpdateEstudio(EstudioDesgravamen obj)
        {
            if (!string.IsNullOrEmpty(obj.EstudioNombre))
            {
                EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter = new EstudiosDSTableAdapters.EstudiosComboTableAdapter();
                /*EstudiosDS.EstudiosComboDataTable table = */
                adapter.UpdateEstudio(obj.EstudioId, obj.EstudioNombre, obj.CategoriaId);
            }
        }

        public static int CountEstudioByName(string estudioNombre)
        {
            if (estudioNombre == null)
            {
                throw new ArgumentException("estudioNombre cannot be null or empty");
            }

            int? cantEstudios = 0;
            EstudiosDSTableAdapters.EstudiosComboTableAdapter adapter =
                new EstudiosDSTableAdapters.EstudiosComboTableAdapter();

            adapter.CountEstudioByName(estudioNombre, ref cantEstudios);

            if (cantEstudios == null || cantEstudios.Value < 0)
            {
                throw new Exception("There was a problem in the execution of the SP");
            }

            return cantEstudios.Value;
        }
    }
}