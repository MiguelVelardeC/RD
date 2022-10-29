using Artexacta.App.Desgravamen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ProveedorEstudiosBLL
    /// </summary>
    public class ProveedorEstudiosBLL
    {
        public ProveedorEstudiosBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<ProveedorEstudios> GetProveedorEstudiosBySearch(string varSql, int pageSize, int firstRow,
            ref int? totalRows, int intProveedorId, int intClienteId)
        {
            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
                new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();

            ProveedoresEstudioDS.ProveedorEstudiosDataTable table =
                adapter.GetProveedorEstudiosBySearch(varSql, pageSize, firstRow, ref totalRows, intProveedorId, intClienteId);

            List<ProveedorEstudios> list = new List<ProveedorEstudios>();

            foreach (ProveedoresEstudioDS.ProveedorEstudiosRow row in table)
            {
                ProveedorEstudios obj = new ProveedorEstudios()
                {
                    ClienteId = row.clienteId,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    EstudioId = row.estudioId,
                    EstudioNombre = row.estudioNombre,
                    NecesitaCita = row.necesitaCita,
                    NecesitaCitaEstado = row.necesitaCitaEstado,
                    Deshabilitado = row.deshabilitado,
                    DeshabilitadoEstado = row.deshabilitadoEstado,
                    HoraInicio = row.horaInicio,
                    HoraFin = row.horaFin,
                    HoraInicioDisplay = row.horaInicio.ToString(),
                    HoraFinDisplay = row.horaFin.ToString()
                };

                list.Add(obj);
            }

            return list;
        }

        public static void InsertEstudioProveedor(int intEstudioId, int intProveedorId, int intClienteId, bool bitNecesitaEstudio, bool bitDeshabilitado, TimeSpan timeInicio, TimeSpan timeFin)
        {
            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
               new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();
            adapter.InsertEstudioProveedor(intEstudioId, intProveedorId, intClienteId, bitNecesitaEstudio, bitDeshabilitado, timeInicio, timeFin);            
        }

        public static bool UpdateEstudioProveedor(int intEstudioId, int intProveedorId, int intClienteId, bool bitNecesitaEstudio, bool bitDeshabilitado, TimeSpan timeInicio, TimeSpan timeFin)
        {
            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
               new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();
            bool? UpdatedHorario = false;

            adapter.UpdateEstudioProveedor(intEstudioId, intProveedorId, intClienteId, bitNecesitaEstudio, bitDeshabilitado, timeInicio, timeFin, ref UpdatedHorario);

            return UpdatedHorario.Value;
        }

        public static void DeleteEstudioProveedor(int intEstudioId, int intProveedorId, int intClienteId)
        {
            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
               new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();
            adapter.DeleteEstudioProveedor(intEstudioId, intProveedorId, intClienteId);
        }

        public static ProveedorEstudios GetProveedorEstudiosById(int intEstudioId, int intProveedorId, int intClienteId)
        {
            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
                new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();

            ProveedoresEstudioDS.ProveedorEstudiosDataTable table =
                adapter.GetEstudioProveedorByEstudioId(intEstudioId, intProveedorId, intClienteId);

            ProveedorEstudios obj = null;

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {
                obj = new ProveedorEstudios()
                {
                    ClienteId = table[0].clienteId,
                    ProveedorMedicoId = table[0].proveedorMedicoId,
                    EstudioId = table[0].estudioId,
                    EstudioNombre = table[0].estudioNombre,
                    NecesitaCita = table[0].necesitaCita,
                    NecesitaCitaEstado = table[0].necesitaCitaEstado,
                    Deshabilitado = table[0].deshabilitado,
                    DeshabilitadoEstado = table[0].deshabilitadoEstado,
                    HoraInicio = table[0].horaInicio,
                    HoraFin = table[0].horaFin,
                    HoraInicioDisplay = table[0].horaInicio.ToString(),
                    HoraFinDisplay = table[0].horaFin.ToString()
                };
            }

            return obj;
        }

        public static List<ClientEstudioTimeRangeCollisionJSON> GetTimeCollisionsByProveedorEstudio(int intProveedorMedicoId,
            int intEstudioId, int intClienteId, TimeSpan HoraInicio, TimeSpan HoraFin, out int? intCollisions)
        {
            intCollisions = 0;

            ProveedoresEstudioDSTableAdapters.TimeRangeProveedoresTableAdapter adapter =
                new ProveedoresEstudioDSTableAdapters.TimeRangeProveedoresTableAdapter();

            ProveedoresEstudioDS.TimeRangeProveedoresDataTable table =
                adapter.VerifyTimeRangeProveedores(intProveedorMedicoId, intEstudioId, intClienteId, HoraInicio, HoraFin, ref intCollisions);

            List<ClientEstudioTimeRangeCollisionJSON> list = new List<ClientEstudioTimeRangeCollisionJSON>();

            if (table != null && table.Rows != null && table.Rows.Count > 0)
            {

                foreach (ProveedoresEstudioDS.TimeRangeProveedoresRow row in table)
                {
                    ClientEstudioTimeRangeCollisionJSON obj = new ClientEstudioTimeRangeCollisionJSON()
                    {
                        ClienteId = row.ClienteId,
                        ClienteNombre = row.ClienteNombre,
                        EstudioId = row.estudioId,
                        EstudioNombre = row.estudioNombre
                    };

                    list.Add(obj);
                }
            }

            return list;
        }

        public static int VerifyProveedorEstudio(string varCiudadId, int intEstudioId, int intClienteId)
        {
            int? intProveedorId = 0;

            ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter adapter =
                new ProveedoresEstudioDSTableAdapters.ProveedorEstudiosTableAdapter();

            adapter.VerifyEstudioProveedorInsert(varCiudadId, intEstudioId, intClienteId, ref intProveedorId);

            return intProveedorId.Value;
        }
    }
}