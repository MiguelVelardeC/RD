using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for PropuestoAseguradoBLL
    /// </summary>
    public class PropuestoAseguradoBLL
    {
        public PropuestoAseguradoBLL()
        {
            ;
        }

        public static int InsertPropuestoAsegurado(PropuestoAsegurado obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object PropuestoAsegurado cannot be null");
            }

            if (string.IsNullOrEmpty(obj.NombreCompleto))
            {
                throw new ArgumentException("NombreCompleto cannot be null or empty");
            }

            if (string.IsNullOrEmpty(obj.CarnetIdentidad))
            {
                throw new ArgumentException("CarnetIdentidad cannot be null or empty");
            }

            int? propuestoAseguradoId = 0;
            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.InsertPropuestoAsegurado(ref propuestoAseguradoId, obj.NombreCompleto, obj.CarnetIdentidad, obj.FechaNacimiento, obj.TelefonoCelular, obj.Genero, obj.ClienteId);

            if (propuestoAseguradoId == null || propuestoAseguradoId.Value <= 0)
            {
                throw new Exception("Identity for Primary Key was not be generated");
            }

            return propuestoAseguradoId.Value;
        }

        public static void UpdatePropuestoAsegurado(PropuestoAsegurado obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object PropuestoAsegurado cannot be null");
            }

            if (obj.PropuestoAseguradoId <= 0)
            {
                throw new ArgumentException("PropuestoAseguradoId cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(obj.NombreCompleto))
            {
                throw new ArgumentException("NombreCompleto cannot be null or empty");
            }

            if (string.IsNullOrEmpty(obj.CarnetIdentidad))
            {
                throw new ArgumentException("CarnetIdentidad cannot be null or empty");
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.UpdatePropuestoAsegurado(obj.PropuestoAseguradoId, obj.NombreCompleto, obj.CarnetIdentidad, obj.FechaNacimiento, obj.TelefonoCelular, obj.Genero, obj.ClienteId);

        }


        public static PropuestoAsegurado GetPropuestoAseguradoId(int propuestoAseguradoId)
        {
            if (propuestoAseguradoId <= 0)
            {
                throw new ArgumentException("propuestoAseguradoId cannot be equals or less than zero");
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            PropuestoAseguradoDS.PropuestoAseguradoDataTable table = adapter.GetPropuestoAseguradoById(propuestoAseguradoId);

            PropuestoAseguradoDS.PropuestoAseguradoRow row = table[0];
            PropuestoAsegurado obj = new PropuestoAsegurado()
            {
                PropuestoAseguradoId = row.propuestoAseguradoId,
                NombreCompleto = row.nombreCompleto,
                CarnetIdentidad = row.carnetIdentidad,
                FechaNacimiento = row.fechaNacimiento,
                TelefonoCelular = row.telefonoCelular,
                FotoId = row.IsfotoIdNull() ? 0 : row.fotoId,
                Genero = row.IsgeneroNull() ? "" : row.genero,
                ClienteId = row.clienteId
            };

            return obj;
        }

        public static PropuestoAsegurado GetPropuestoAseguradoByCI(string ci, int clienteId)
        {
            if (string.IsNullOrWhiteSpace(ci))
            {
                throw new ArgumentException("Carnet no puede estar vacío");
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            PropuestoAseguradoDS.PropuestoAseguradoDataTable table = adapter.GetPropuestoAseguradoByCI(ci, clienteId);
            
            
            if (table != null && table.Rows.Count > 0)
            {
                PropuestoAseguradoDS.PropuestoAseguradoRow row = table[0];
                PropuestoAsegurado obj = new PropuestoAsegurado()
                {
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    NombreCompleto = row.nombreCompleto,
                    CarnetIdentidad = row.carnetIdentidad,
                    FechaNacimiento = row.fechaNacimiento,
                    TelefonoCelular = row.telefonoCelular,
                    FotoId = row.IsfotoIdNull() ? 0 : row.fotoId,
                    Genero = row.IsgeneroNull() ? "" : row.genero,
                    ClienteId = row.clienteId
                };
                return obj;
            }

            return null;
            
        }

        public static void UpdateFotoId(int propuestoAseguradoId, int fotoId)
        {
            if (propuestoAseguradoId <= 0)
            {
                throw new ArgumentException("propuestoAseguradoId cannot be equals or less than zero");
            }

            if (fotoId < 0)
            {
                throw new ArgumentException("fotoId cannot be less than zero");
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.UpdateFotoId(propuestoAseguradoId, fotoId);
        }

        /// <summary>
        /// Este metodo recie el id del medico que atendera la cit.a Sin embargo, es posibel que se cooridne una cita solamente 
        /// para el laboratorio, en cuyo caso, el id del medico va a 0 y se debe colocar el id del proveedorMedico
        /// </summary>
        /// <param name="citaDesgravamenId"></param>
        /// <param name="medicoDesgravamenId"></param>
        /// <param name="proveedorMedicoId"></param>
        /// <param name="fechaHoraCita"></param>        

        public static void SaveProgramacionCita(int citaDesgravamenId, int medicoDesgravamenId, int proveedorMedicoId,
            DateTime fechaHoraCita, int estudioId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("citaDesgravamenId cannot be equals or less than zero");
            }

            if (medicoDesgravamenId < 0)
            {
                throw new ArgumentException("medicoDesgravamenId cannot be less than zero");
            }

            if (medicoDesgravamenId == 0 && proveedorMedicoId == 0 && fechaHoraCita == DateTime.MinValue)
            {
                fechaHoraCita = DateTime.Now;
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.SaveProgramacionCita(citaDesgravamenId, medicoDesgravamenId, proveedorMedicoId, estudioId, fechaHoraCita);
        }


        public static List<PropuestoAseguradoSearchResult> GetPropuestoAseguradoBySearch(string whereSql, int intUsuarioId, int intClienteId, bool eliminado,
            int pageSize, int firstRow, ref int? totalRows)
        {
            bool Authorization = false;
            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_PALIST");
                Authorization = true;
            }
            catch (Exception)
            {
                Authorization = false;
                //throw;
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoSearchTableAdapter adapter = 
                new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoSearchTableAdapter();
            PropuestoAseguradoDS.PropuestoAseguradoSearchDataTable table =
                adapter.GetPropuestoAseguradoBySearch(whereSql, intUsuarioId, eliminado, pageSize, firstRow, ref totalRows, intClienteId, Authorization);


            List<PropuestoAseguradoSearchResult> list = new List<PropuestoAseguradoSearchResult>();

            foreach (PropuestoAseguradoDS.PropuestoAseguradoSearchRow row in table)
            {
                PropuestoAseguradoSearchResult obj = new PropuestoAseguradoSearchResult()
                {
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    CitaDesgravamenId = row.IscitaDesgravamenIdNull() ? 0 : row.citaDesgravamenId,
                    Nombre = row.nombreCompleto,
                    PropuestoAseguradoNroDocumento = row.carnetIdentidad,
                    TelefonoCelular = row.telefonoCelular,
                    TipoProducto = row.IstipoProductoNull() ? "" : row.tipoProducto,
                    FinancieraId = row.financieraId,
                    Financiera = row.IsfinancieraNull() ? "" : row.financiera,
                    FechaCreacionProgramacionCita = row.IsfechaCreacionNull() ? DateTime.MinValue : row.fechaCreacion,
                    FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita,
                    CiudadId = row.IsciudadIdNull() ? "" : row.ciudadId,
                    NombreCiudad = row.IsciudadNull() ? "" : row.ciudad,
                    TieneExamenMedico = row.IstieneExamenMedicoNull() ? 0 : row.tieneExamenMedico,
                    NecesitaLaboratorio = row.IsnecesitaLaboratorioNull() ? false : row.necesitaLaboratorio,
                    NecesitaExamenMedico = row.IsnecesitaExamenMedicoNull() ? false : row.necesitaExamenMedico,
                    CobroFinanciera = row.IscobroFinancieraNull() ? false : row.cobroFinanciera,
                    Aprobado = row.IsaprobadoNull() ? false : row.aprobado,
                    AprobadoLabo = row.IsaprobadoLaboNull() ? false : row.aprobadoLabo,
                    NombreMedico = row.IsnombreMedicoNull() ? "" : row.nombreMedico,
                    UsuarioRegistro = row.IsusuarioRegistroNull() ? "" : row.usuarioRegistro,
                    ClienteId = row.clienteId,
                    NombreJuridico = row.IsNombreJuridicoNull() ? "" : row.NombreJuridico
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<ProgramacionCita> GetProgramacionCitaByMedicoFecha(int medicoId, DateTime fecha, bool ignoreDate, string propuestoAsegurado)
        {
            PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter();
            PropuestoAseguradoDS.ProgramacionCitaDataTable table = adapter.GetProgramacionCitaByMedicoFecha(medicoId, fecha, propuestoAsegurado, ignoreDate);

            List<ProgramacionCita> results = new List<ProgramacionCita>();

            foreach (PropuestoAseguradoDS.ProgramacionCitaRow row in table.Rows)
            {
                ProgramacionCita obj = new ProgramacionCita()
                {
                    CitaDesgravamen = row.citaDesgravamenId,
                    Estado = row.estado,
                    MedicoId = row.medicoDesgravamenId,
                    NombreMedico = row.nombreMedico,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    NombreProveedor = row.nombreProveedor,
                    FechaCreacion = row.fechaCreacion,
                    FechaHoraCita = row.fechaHoraCita,
                    FechaUltimaModificacion = row.fechaUltimaModificacion,
                    Pasada = row.IspasadaNull() ? 0 : row.pasada,
                    ClienteId = row.clienteId,
                    ClienteNombre = row.clienteNombre,
                    FinancieraId = row.financieraId,
                    FinancieraNombre = row.financieraNombre,
                    EjecutivoId = row.ejecutivoId,
                    EjecutivoNombre = row.ejecutivoNombre
                };
                results.Add(obj);
            }
            return results;
        }

        public static ProgramacionCita GetProgramacionCita(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("citaDesgravamenId cannot be equals or less than zero");
            }
            PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.ProgramacionCitaTableAdapter();
            PropuestoAseguradoDS.ProgramacionCitaDataTable table = adapter.GetProgramacionCita(citaDesgravamenId);

            if (table.Rows.Count == 0)
                return null;

            PropuestoAseguradoDS.ProgramacionCitaRow row = table[0];
            ProgramacionCita obj = new ProgramacionCita()
            {
                Aprobado = row.aprobado,
                CitaDesgravamen = row.citaDesgravamenId,
                Estado = row.estado,
                MedicoId = row.medicoDesgravamenId,
                NombreMedico = row.nombreMedico,
                ProveedorMedicoId = row.proveedorMedicoId,
                NombreProveedor = row.nombreProveedor,
                FechaCreacion = row.fechaCreacion,
                FechaHoraCita = row.fechaHoraCita, 
                FechaUltimaModificacion =row.fechaUltimaModificacion
            };
            return obj;
        }

        public static List<PropuestoAseguradoSearchResult> GetPropuestoAseguradoParametersBySearch(string whereSql, int intUsuarioId, int intClienteId, bool eliminado,
            DateTime dtFechaInicioCita, DateTime dtFechaFinCita, int intFinancieraId, int intEjecutivoId, string varCiudad, string varTipoProducto, int intCitaDesgravamenId, string varPropuestoAsegurado, int pageSize, int firstRow, ref int? totalRows)
        {
            bool Authorization = false;
            bool RangoFechas = false;
            bool bitEjecutivoSearch = false;

            if (intEjecutivoId > 0)
            {
                bitEjecutivoSearch = true;
            }

            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_PALIST");
                Authorization = true;
                if (dtFechaInicioCita != null && dtFechaFinCita != null 
                    && dtFechaInicioCita != DateTime.MinValue && dtFechaFinCita != DateTime.MinValue)
                {
                    RangoFechas = true;
                }
            }
            catch (Exception)
            {
                Authorization = false;
                RangoFechas = false;
                //throw;
            }

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoParametersSearchTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoParametersSearchTableAdapter();
            PropuestoAseguradoDS.PropuestoAseguradoParametersSearchDataTable table;
            if (RangoFechas)
            {
                table = adapter.GetPropuestoAseguradoParametersBySearch(whereSql, intUsuarioId, eliminado, pageSize, firstRow, ref totalRows, 
                    intClienteId, dtFechaInicioCita, dtFechaFinCita, intFinancieraId, bitEjecutivoSearch, intEjecutivoId, varCiudad, 
                    varTipoProducto, intCitaDesgravamenId, varPropuestoAsegurado, Authorization);
            }
            else
            {
                table = adapter.GetPropuestoAseguradoParametersBySearch(whereSql, intUsuarioId, eliminado, pageSize, firstRow, ref totalRows, 
                    intClienteId, null, null, intFinancieraId, bitEjecutivoSearch, intEjecutivoId, varCiudad, varTipoProducto, 
                    intCitaDesgravamenId, varPropuestoAsegurado, Authorization);
            }
            

            List<PropuestoAseguradoSearchResult> list = new List<PropuestoAseguradoSearchResult>();

            foreach (PropuestoAseguradoDS.PropuestoAseguradoParametersSearchRow row in table)
            {
                PropuestoAseguradoSearchResult obj = new PropuestoAseguradoSearchResult()
                {
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    CitaDesgravamenId = row.IscitaDesgravamenIdNull() ? 0 : row.citaDesgravamenId,
                    Nombre = row.nombreCompleto,
                    TelefonoCelular = row.telefonoCelular,
                    TipoProducto = row.IstipoProductoNull() ? "" : row.tipoProducto,
                    Financiera = row.IsfinancieraNull() ? "" : row.financiera,
                    FechaCreacionProgramacionCita = row.IsfechaCreacionNull() ? DateTime.MinValue : row.fechaCreacion,
                    FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita,
                    CiudadId = row.IsciudadIdNull() ? "" : row.ciudadId,
                    NombreCiudad = row.IsciudadNull() ? "" : row.ciudad,
                    TieneExamenMedico = row.IstieneExamenMedicoNull() ? 0 : row.tieneExamenMedico,
                    NecesitaLaboratorio = row.IsnecesitaLaboratorioNull() ? false : row.necesitaLaboratorio,
                    NecesitaExamenMedico = row.IsnecesitaExamenMedicoNull() ? false : row.necesitaExamenMedico,
                    Aprobado = row.IsaprobadoNull() ? false : row.aprobado,
                    AprobadoLabo = row.IsaprobadoLaboNull() ? false : row.aprobadoLabo,
                    NombreMedico = row.IsnombreMedicoNull() ? "" : row.nombreMedico,
                    UsuarioRegistro = row.IsusuarioRegistroNull() ? "" : row.usuarioRegistro,
                    ClienteId = row.clienteId
                };
                list.Add(obj);
            }
            return list;
        }

        public static List<PropuestoAseguradoSearchResult> GetPropuestoAseguradoBySearchALL(string whereSql, int intUsuarioId, int intClienteId, bool eliminado,
            int pageSize, int firstRow, ref int? totalRows)
        {
            bool Authorization = true;

            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoSearchTableAdapter adapter =
                new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoSearchTableAdapter();
            PropuestoAseguradoDS.PropuestoAseguradoSearchDataTable table =
                adapter.GetPropuestoAseguradoBySearch(whereSql, intUsuarioId, eliminado, pageSize, firstRow, ref totalRows, intClienteId, Authorization);


            List<PropuestoAseguradoSearchResult> list = new List<PropuestoAseguradoSearchResult>();

            foreach (PropuestoAseguradoDS.PropuestoAseguradoSearchRow row in table)
            {
                PropuestoAseguradoSearchResult obj = new PropuestoAseguradoSearchResult()
                {
                    PropuestoAseguradoId = row.propuestoAseguradoId,
                    CitaDesgravamenId = row.IscitaDesgravamenIdNull() ? 0 : row.citaDesgravamenId,
                    Nombre = row.nombreCompleto,
                    PropuestoAseguradoNroDocumento = row.carnetIdentidad,
                    TelefonoCelular = row.telefonoCelular,
                    TipoProducto = row.IstipoProductoNull() ? "" : row.tipoProducto,
                    FinancieraId = row.financieraId,
                    Financiera = row.IsfinancieraNull() ? "" : row.financiera,
                    FechaCreacionProgramacionCita = row.IsfechaCreacionNull() ? DateTime.MinValue : row.fechaCreacion,
                    FechaHoraCita = row.IsfechaHoraCitaNull() ? DateTime.MinValue : row.fechaHoraCita,
                    CiudadId = row.IsciudadIdNull() ? "" : row.ciudadId,
                    NombreCiudad = row.IsciudadNull() ? "" : row.ciudad,
                    TieneExamenMedico = row.IstieneExamenMedicoNull() ? 0 : row.tieneExamenMedico,
                    NecesitaLaboratorio = row.IsnecesitaLaboratorioNull() ? false : row.necesitaLaboratorio,
                    NecesitaExamenMedico = row.IsnecesitaExamenMedicoNull() ? false : row.necesitaExamenMedico,
                    Aprobado = row.IsaprobadoNull() ? false : row.aprobado,
                    AprobadoLabo = row.IsaprobadoLaboNull() ? false : row.aprobadoLabo,
                    NombreMedico = row.IsnombreMedicoNull() ? "" : row.nombreMedico,
                    UsuarioRegistro = row.IsusuarioRegistroNull() ? "" : row.usuarioRegistro,
                    ClienteId = row.clienteId
                };
                list.Add(obj);
            }
            return list;
        }
    }
}