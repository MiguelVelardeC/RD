using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ProveedorMedicoBLL
    /// </summary>
    public class ProveedorMedicoBLL
    {
        public ProveedorMedicoBLL()
        {
            
        }
        /** 
         * List of methods for object ProveedorMedico, 
         * since it isn't clear if there's any method that uses this functions i've left them alone
         * **/
        public static int InsertProveedorMedico(ProveedorDesgravamen obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object ProveedorMedico cannot be null");
            }

            if (string.IsNullOrEmpty(obj.ProveedorNombre))
            {
                throw new ArgumentException("Nombre cannot be null or empty");
            }

            if (string.IsNullOrEmpty(obj.CiudadId))
            {
                throw new ArgumentException("CiudadId cannot be null or empty");
            }

            int? ProveedorMedicoId = 0;
            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter = 
                new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            adapter.Insert(ref ProveedorMedicoId, obj.ProveedorNombre, obj.CiudadId, obj.UserId, obj.DuracionCita, obj.Principal);

            if (ProveedorMedicoId == null || ProveedorMedicoId.Value <= 0)
            {
                throw new Exception("Identity for Primary Key was not be generated");
            }

            return ProveedorMedicoId.Value;
        }

        public static void UpdateProveedorMedico(ProveedorDesgravamen obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object ProveedorMedico cannot be null");
            }

            if (string.IsNullOrEmpty(obj.ProveedorNombre))
            {
                throw new ArgumentException("Nombre cannot be null or empty");
            }

            if (string.IsNullOrEmpty(obj.CiudadId))
            {
                throw new ArgumentException("CiudadId cannot be null or empty");
            }

            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter = new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            adapter.Update(obj.ProveedorMedicoId, obj.ProveedorNombre, obj.CiudadId, obj.UserId, obj.DuracionCita, obj.Principal);

        }

        public static void DeleteProveedorMedico(int proveedorMedicoId)
        {
            if (proveedorMedicoId <=0)
            {
                throw new ArgumentException("proveedorMedicoId cannot be 0");
            }

            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter = 
                new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            adapter.Delete(proveedorMedicoId);

        }


        public static ProveedorDesgravamen GetProveedorMedicoId(int ProveedorMedicoId)
        {
            if (ProveedorMedicoId <= 0)
            {
                throw new ArgumentException("ProveedorMedicoId cannot be equals or less than zero");
            }

            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter = new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            ProveedorMedicoDS.ProveedorMedicoDataTable table = adapter.GetProveedorMedicoById(ProveedorMedicoId);

            if (table != null && table.Rows.Count > 0)
            {
                ProveedorMedicoDS.ProveedorMedicoRow row = table[0];
                ProveedorDesgravamen obj = new ProveedorDesgravamen()
                {
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    CiudadId = row.ciudadId,
                    CiudadNombre = row.ciudadNombre,
                    UserId = row.userId,
                    Username = row.username,
                    DuracionCita = row.duracionCita,
                    Principal = row.principal
                };

                return obj;
            }

            return new ProveedorDesgravamen();
        }


        public static List<ProveedorDesgravamen> GetProveedorMedicoByCiudad(string ciudadId)
        {
            if (string.IsNullOrWhiteSpace(ciudadId))
            {
                throw new ArgumentException("CiudadId cannot be null or empty");
            }
            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter =
                new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            ProveedorMedicoDS.ProveedorMedicoDataTable table = adapter.GetProveedorMedicoByCiudad(ciudadId);

            List<ProveedorDesgravamen> list = new List<ProveedorDesgravamen>();

            foreach (ProveedorMedicoDS.ProveedorMedicoRow row in table)
            {
                ProveedorDesgravamen obj = new ProveedorDesgravamen()
                {
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    CiudadId = row.ciudadId,
                    CiudadNombre = row.ciudadNombre,
                    UserId = row.userId,
                    Username = row.username,
                    DuracionCita = row.duracionCita,
                    Principal = row.principal
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<ProveedorDesgravamen> GetProveedorMedico()
        {
            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter =
                new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            ProveedorMedicoDS.ProveedorMedicoDataTable table = adapter.GetProveedorMedico();

            List<ProveedorDesgravamen> list = new List<ProveedorDesgravamen>();

            foreach (ProveedorMedicoDS.ProveedorMedicoRow row in table)
            {
                ProveedorDesgravamen obj = new ProveedorDesgravamen()
                {
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    CiudadId = row.ciudadId,
                    CiudadNombre = row.ciudadNombre,
                    UserId = row.userId,
                    Username = row.username,
                    DuracionCita = row.duracionCita,
                    Principal = row.principal
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<ProveedorDesgravamen> GetProveedorMedicoByUserId(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentException("user id cannot be lt 0");
            }
            ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter adapter =
                new ProveedorMedicoDSTableAdapters.ProveedorMedicoTableAdapter();
            ProveedorMedicoDS.ProveedorMedicoDataTable table = adapter.GetProveedorMedicoByUserId(userId);

            List<ProveedorDesgravamen> list = new List<ProveedorDesgravamen>();

            foreach (ProveedorMedicoDS.ProveedorMedicoRow row in table)
            {
                ProveedorDesgravamen obj = new ProveedorDesgravamen()
                {
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    CiudadId = row.ciudadId,
                    CiudadNombre = row.ciudadNombre,
                    UserId = row.userId,
                    Username = row.username,
                    DuracionCita = row.duracionCita,
                    Principal = row.principal
                };
                list.Add(obj);
            }
            return list;
        }
        /** 
         * End of List of methods for object ProveedorMedico
         * **/
        public static List<ProveedorDesgravamen> GetProveedorDesgravamenBySearch(string varSql, int pageSize, int firstRow, ref int? totalRows)
        {
            ProveedorMedicoDSTableAdapters.ProveedorDesgravamenTableAdapter adapter =
                new ProveedorMedicoDSTableAdapters.ProveedorDesgravamenTableAdapter();

            ProveedorMedicoDS.ProveedorDesgravamenDataTable table =
                adapter.GetProveedorDesgravamenBySearch(varSql, pageSize, firstRow, ref totalRows);

            List<ProveedorDesgravamen> list = new List<ProveedorDesgravamen>();

            foreach (ProveedorMedicoDS.ProveedorDesgravamenRow row in table)
            {
                ProveedorDesgravamen obj = new ProveedorDesgravamen()
                {
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    CiudadId = row.ciudadId,
                    CiudadNombre = row.ciudadNombre,
                    UserId = row.userId,
                    Username = row.username,
                    DuracionCita = row.duracionCita,
                    Principal = row.principal,
                    EstudiosHabilitados = row.estudiosHabilitados
                };
                list.Add(obj);
            }

            return list;
        }
    }
}