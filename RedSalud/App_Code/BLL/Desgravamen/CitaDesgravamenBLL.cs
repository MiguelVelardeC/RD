using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for CitaDesgravamenBLL
    /// </summary>
    public class CitaDesgravamenBLL
    {
        public CitaDesgravamenBLL()
        {
            
        }

        public static CitaDesgravamen GetCitaDesgravamenById(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("citaDesgravamenId cannot be equals or less than zero");
            }

            CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter = new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            CitaDesgravamenDS.CitaDesgravamenDataTable table = adapter.GetCitaDesgravamenById(citaDesgravamenId);

            if (table == null || table.Rows.Count != 1)
            {
                throw new KeyNotFoundException("CitaDesgravamen cannot be found");
            }

            CitaDesgravamenDS.CitaDesgravamenRow row = table[0];

            CitaDesgravamen obj = new CitaDesgravamen()
            {
                CitaDesgravamenId = row.citaDesgravamenId,
                PropuestoAseguradoId = row.propuestoAseguradoId,
                FinancieraId = row.IsfinancieraIdNull() ? 0 : row.financieraId,
                CiudadId = row.ciudadId,
                CobroFinanciera = row.cobroFinanciera,
                Referencia = row.referencia,
                NecesitaExamen = row.necesitaExamen,
                NecesitaLaboratorio = row.necesitaLaboratorio,
                TipoProducto = row.tipoProducto,
                EjecutivoId = row.IsejecutivoIdNull() ? 0 : row.ejecutivoId,
                ClienteId = row.clienteId
            };

            return obj;
        }

        public static int InsertCitaDesgravamen(int propuestoAseguradoId, string tipoProd, int financieraId, string ciudadId,
            bool necesitaExamen, bool necesitaLaboratorio, bool cobroFinanciera, string referencia, int ejecutivoId, List<int> estudioIds, int clienteId)
        {
            if (propuestoAseguradoId <= 0)
            {
                throw new ArgumentException("propuestoAseguradoId cannot be equals or less than zero");
            }

            if (financieraId < 0)
            {
                throw new ArgumentException("financieraId cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(ciudadId))
            {
                throw new ArgumentException("ciudadId cannot be equals or less than zero");
            }

            if (ejecutivoId <= 0)
            {
                throw new ArgumentException("El ejecutivoId no puede ser menor o igual a 0");
            }

            DataTable tblEstudios = new DataTable();
            tblEstudios.Columns.Add("objectId", typeof(int));

            foreach (int estudioId in estudioIds)
            {
                DataRow row = tblEstudios.NewRow();
                row["objectId"] = estudioId;
                tblEstudios.Rows.Add(row);
            }
            int? citaDesgravamenId = 0;
            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.InsertCitaDesgravamen(
                propuestoAseguradoId, tipoProd, 
                financieraId, ciudadId, 
                necesitaExamen, necesitaLaboratorio, 
                cobroFinanciera, referencia, ejecutivoId, 
                tblEstudios, ref citaDesgravamenId, clienteId);

            if (citaDesgravamenId == null || citaDesgravamenId.Value <= 0)
            {
                throw new Exception("Identity for Primary Key was not be generated");
            }

            return citaDesgravamenId.Value;
        }

        public static void UpdateCitaDesgravamen(int citaDesgravamenId, int propuestoAseguradoId, string tipoProd, int financieraId, string ciudadId,
            bool necesitaExamen, bool necesitaLaboratorio, bool cobroFinanciera, string referencia, List<int> estudioIds, int clienteId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("citaDesgravamenId cannot be equals or less than zero");
            }

            if (propuestoAseguradoId <= 0)
            {
                throw new ArgumentException("propuestoAseguradoId cannot be equals or less than zero");
            }
            if (financieraId < 0)
            {
                throw new ArgumentException("financieraId cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(ciudadId))
            {
                throw new ArgumentException("ciudadId cannot be equals or less than zero");
            }

            DataTable tblEstudios = new DataTable();
            tblEstudios.Columns.Add("objectId", typeof(int));

            foreach (int estudioId in estudioIds)
            {
                DataRow row = tblEstudios.NewRow();
                row["objectId"] = estudioId;
                tblEstudios.Rows.Add(row);
            }
            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.UpdateCitaDesgravamen(propuestoAseguradoId, tipoProd, financieraId, ciudadId, necesitaExamen, necesitaLaboratorio, cobroFinanciera, referencia, tblEstudios, citaDesgravamenId, clienteId);

        }

        public static void DeleteCitaDesgravamen(int citaDesgravamenId, int propuestoAseguradoId)
        {
            if (citaDesgravamenId < 0)
            {
                throw new ArgumentException("citaDesgravamenId cannot be less than zero");
            }
            if (propuestoAseguradoId < 0)
            {
                throw new ArgumentException("propuestoAseguradoId cannot be less than zero");
            }
            if (citaDesgravamenId == 0 && propuestoAseguradoId == 0)
            {
                throw new ArgumentException("No pueden estar los dos a 0 citaDesgravamenId y propuestoAseguradoId");
            }


            CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter =
                new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            adapter.DeleteCitaDesgravamen(citaDesgravamenId, propuestoAseguradoId);
        }

        public static List<PropuestoAseguradoEstudio> GetListaPropuestoAseguradoEstudioBySearch(int userId, string whereSql,
            int pageSize, int firstRow, int clienteId, ref int? totalRows)
        {
            CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter adapter =
                new CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter();



            CitaDesgravamenDS.PropuestoAseguradoParaEstudioDataTable table = adapter.GetPropuestoAseguradoParaEstudio(userId, whereSql, pageSize, firstRow, clienteId, ref totalRows);

            
            List<PropuestoAseguradoEstudio> list = new List<PropuestoAseguradoEstudio>();
            foreach (var row in table)
            {
                PropuestoAseguradoEstudio obj = new PropuestoAseguradoEstudio()
                {
                    CitaDesgravamenId = row.citaDesgravamenId,
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    NombreCompleto = row.nombreCompleto,
                    FechaNacimiento = row.fechaNacimiento,
                    CarnetIdentidad = row.carnetIdentidad,
                    CobroFinanciera = row.cobroFinanciera,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    NombreProveedor = row.nombreProveedor,
                    FechaCita = row.fechaCita,
                    FechaAtencion = row.fechaAtencion,
                    Aprobado = row.aprobado,
                    Observacion = row.observacion,
                    EstudioId = row.estudioId,
                    EstudioNombre = row.estudioNombre,
                    ClienteId = row.clienteId,
                    ClienteNombre = row.clienteNombre
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<PropuestoAseguradoEstudio> GetListaPropuestoAseguradoEstudioByCitaId(int citaDesgravamenId)
        {
            CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter adapter =
                new CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter();
            CitaDesgravamenDS.PropuestoAseguradoParaEstudioDataTable table = adapter.GetPropuestoAseguradoParaEstudioByCitaId(citaDesgravamenId);

            List<PropuestoAseguradoEstudio> list = new List<PropuestoAseguradoEstudio>();
            foreach (var row in table)
            {
                PropuestoAseguradoEstudio obj = new PropuestoAseguradoEstudio()
                {
                    CitaDesgravamenId = row.citaDesgravamenId,
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    NombreCompleto = row.nombreCompleto,
                    FechaNacimiento = row.fechaNacimiento,
                    CarnetIdentidad = row.carnetIdentidad,
                    CobroFinanciera = row.cobroFinanciera,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    NombreProveedor = row.nombreProveedor,
                    FechaCita = row.fechaCita,
                    FechaAtencion = row.fechaAtencion,
                    Aprobado = row.aprobado,
                    Observacion = row.observacion
                };
                list.Add(obj);
            }
            return list;
        }

        public static void MarcarAprobado(int citaDesgravamenId)
        {
            PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter();

            adapter.MarcarAprobado(citaDesgravamenId, true);
        }

        public static void MarcarNoAprobado(int citaDesgravamenId)
        {
            PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter();

            adapter.MarcarAprobado(citaDesgravamenId, false);
        }

        public static void UpdateCitaLabo(DateTime timeInBolivia, int citaDesgravamenId, int proveedorMedicoId)
        {
            CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter adapter =
                new CitaDesgravamenDSTableAdapters.PropuestoAseguradoParaEstudioTableAdapter();
            adapter.UpdateAtencionLabo(citaDesgravamenId, proveedorMedicoId, timeInBolivia);
        }

        public static void UndeleteCitaDesgravamen(int citaDesgravamenID)
        {
            if (citaDesgravamenID <= 0)
                throw new ArgumentException("No se puede llamar undelete para una cita 0 o menor que 0");

            CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter =
                new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            adapter.UnDeleteCitaDesgravamen(citaDesgravamenID);
        }
    }
}