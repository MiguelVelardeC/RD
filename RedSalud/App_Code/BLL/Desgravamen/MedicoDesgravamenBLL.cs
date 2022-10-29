using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for MedicoDesgravamenBLL
    /// </summary>
    public class MedicoDesgravamenBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public MedicoDesgravamenBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static MedicoDesgravamen FillRecord(MedicoDesgravamenDS.MedicoDesgravamenRow row)
        {
            MedicoDesgravamen objMedico = new MedicoDesgravamen();

            objMedico.Direccion = row.direccion;
            objMedico.MedicoDesgravamenId = row.medicoDesgravamenId;
            objMedico.ProveedorMedicoId = row.proveedorMedicoId;
            objMedico.Nombre = row.nombre;
            objMedico.UserId = row.userId;

            return objMedico;
        }

        public static MedicoDesgravamen GetMedicoDesgravamenById(int medicoId)
        {
            if (medicoId <= 0)
                throw new ArgumentException("medicoId cannot be less than or equal to zero.");
            
            MedicoDesgravamen objMedico = null;
            try
            {
                MedicoDesgravamenDSTableAdapters.MedicoDesgravamenTableAdapter theAdapter =
                new MedicoDesgravamenDSTableAdapters.MedicoDesgravamenTableAdapter();
                MedicoDesgravamenDS.MedicoDesgravamenDataTable theTable = theAdapter.GetMedicoDesgravamenById(medicoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objMedico = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list objMedico by Id", ex);
                throw;
            }
            return objMedico;
        }

        public static MedicoDesgravamen GetMedicoDesgravamenByUserId(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("userId cannot be less than or equal to zero.");

            MedicoDesgravamen objMedico = null;
            try
            {
                MedicoDesgravamenDSTableAdapters.MedicoDesgravamenTableAdapter theAdapter =
                new MedicoDesgravamenDSTableAdapters.MedicoDesgravamenTableAdapter();
                MedicoDesgravamenDS.MedicoDesgravamenDataTable theTable = theAdapter.GetMedicoDesgravamenByUserId(userId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    objMedico = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list objMedico by user Id", ex);
                throw;
            }
            return objMedico;
        }

        public static List<MedicoDesgravamenSearchResult> GetMedicoDesgravamenBySearch(string varSql, int pageSize, int firstRow, ref int? totalRows)
        {
            MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter adapter = 
                new MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter();

            MedicoDesgravamenDS.MedicosDesgravamenDataTable table =                 
                adapter.GetMedicoDesgravamenBySearch(varSql, pageSize, firstRow, ref totalRows);

            List<MedicoDesgravamenSearchResult> list = new List<MedicoDesgravamenSearchResult>();

            foreach (MedicoDesgravamenDS.MedicosDesgravamenRow row in table)
            {
                MedicoDesgravamenSearchResult obj = new MedicoDesgravamenSearchResult();
                obj.MedicoDesgravamenId = row.medicoDesgravamenId;
                obj.NombreMedico = row.nombreMedico;
                obj.UserId = row.userId;
                obj.Username = row.username;
                obj.ProveedorMedicoId = row.proveedorMedicoId;
                obj.NombreProveedor = row.nombreProveedor;
                obj.Direccion = row.direccion;
                list.Add(obj);
            }

            return list;
        }

        public static int InsertMedicoDesgravamen(MedicoDesgravamen obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object MedicoDesgravamen cannot be null");
            }

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                throw new ArgumentException("Nombre cannot be null or empty");
            }

            if (string.IsNullOrEmpty(obj.Direccion))
            {
                throw new ArgumentException("Direccion cannot be null or empty");
            }

            if (obj.ProveedorMedicoId == null || obj.ProveedorMedicoId <= 0)
            {
                throw new ArgumentException("Proveedor cannot be null or empty");
            }

            if (obj.UserId == null || obj.UserId <= 0)
            {
                throw new ArgumentException("User cannot be null or empty");
            }

            int? medicoDesgravamenId = 0;

            MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter adapter = 
                new MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter();
            adapter.InsertMedicoDesgravamen(ref medicoDesgravamenId, obj.Nombre, obj.ProveedorMedicoId, obj.Direccion, obj.UserId);
            /*
            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.InsertPropuestoAsegurado(ref propuestoAseguradoId, obj.NombreCompleto, obj.CarnetIdentidad, obj.FechaNacimiento, obj.TelefonoCelular, obj.Genero);*/

            if (medicoDesgravamenId == null || medicoDesgravamenId.Value <= 0)
            {
                throw new Exception("Identity for Primary Key was not be generated");
            }

            return medicoDesgravamenId.Value;
        }

        public static void UpdateMedicoDesgravamen(MedicoDesgravamen obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object MedicoDesgravamen cannot be null");
            }

            if (obj.MedicoDesgravamenId <= 0)
            {
                throw new ArgumentException("MedicoDesgravamenId cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(obj.Nombre))
            {
                throw new ArgumentException("Nombre cannot be null or empty");
            }

            if (obj.ProveedorMedicoId <= 0)
            {
                throw new ArgumentException("ProveedorMedicoId cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(obj.Direccion))
            {
                throw new ArgumentException("Direccion cannot be null or empty");
            }

            if (obj.UserId <= 0)
            {
                throw new ArgumentException("UserId cannot be equals or less than zero");
            }

            MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter adapter = 
                new MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter();

            adapter.UpdateMedicoDesgravamen(obj.MedicoDesgravamenId, obj.Nombre, obj.ProveedorMedicoId, obj.Direccion, obj.UserId);
        }

        public static void DeleteMedicoDesgravamen(int MedicoId)
        {
            if (MedicoId <= 0)
            {
                throw new ArgumentException("MedicoDesgravamenId cannot be equals or less than zero");
            }

            MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter adapter =
                new MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter();

            adapter.DeleteMedicoDesgravamen(MedicoId);
        }

        public static List<ComboContainer> GetMedicoDesgravamenCombo()
        {
            

            MedicoDesgravamenDSTableAdapters.MedicoDesgravamenComboTableAdapter adapter =
               new MedicoDesgravamenDSTableAdapters.MedicoDesgravamenComboTableAdapter();

            MedicoDesgravamenDS.MedicoDesgravamenComboDataTable table =
                adapter.GetMedicoDesgravamenCombo();

            List<ComboContainer> list = new List<ComboContainer>();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (MedicoDesgravamenDS.MedicoDesgravamenComboRow row in table)
                {
                    ComboContainer obj = new ComboContainer()
                    {
                        ContainerId = row.medicoDesgravamenId.ToString(),
                        ContainerName = row.nombreMedico
                    };
                    list.Add(obj);
                }
            }
            return list;
        }
    }
}