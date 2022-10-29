using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using MedicoDSTableAdapters;

namespace Artexacta.App.Medico.BLL
{
    /// <summary>
    /// Summary description for MedicoBLL
    /// </summary>
    public class MedicoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public MedicoBLL()
        {
        }

        private static Medico FillRecord(MedicoDS.MedicoRow row)
        {
            Medico objMedico = new Medico(
                row.MedicoId
                , row.UserId
                , row.Name
                , row.EspecialidadId
                , row.Especialidad
                , row.Sedes
                , row.ColegioMedico
                , row.Estado
                , row.Observacion
                , row.isExternal
                , row.isCallCenter
                , row.FechaActualizacion
                , row.PermiteVideoLlamada
                , row.FotoId
                );

            return objMedico;
        }
        private static Medico FillRecord(MedicoDS.Medico_TodosRow row)
        {
            Medico objMedico = new Medico(
                row.MedicoId
                , row.UserId
                , row.fullname
                , 0
                , ""
                , ""
                , ""
                , ""
                , ""
                , false
                , false
                , new DateTime()
                , false
                , 0
                );

            return objMedico;
        }
        private static MedicoHorario FillRecord(MedicoDS.MedicoHorarioRow row)
        {
            MedicoHorario objMedico = new MedicoHorario(row.horId, row.Dia, row.horaInicio, row.horaFin);
            return objMedico;
        }
        //Edwin suyo
        private static Medico FillRecord(MedicoDS.GetAllMedicoxCiudadRow row)
        {
            Medico objMedico = new Medico(
            row.userId
            ,row.MedicoId
            ,row.fullname
            ,row.Nombre
            ,row.ciudadId
            );

            return objMedico;
        }
        public static int GetALLMedicoxCiudadYEspecialidad( List<Medico> theMedico, string CiudadId,int EspecialidadId)
        {
            int? totalRows = 0;
            try
            {
                
                MedicoDSTableAdapters.GetAllMedicoxCiudadTableAdapter theAdapter = new MedicoDSTableAdapters.GetAllMedicoxCiudadTableAdapter();
                MedicoDS.GetAllMedicoxCiudadDataTable theTable = theAdapter.GetAllMedicoxCiudad(CiudadId,EspecialidadId);
                foreach (MedicoDS.GetAllMedicoxCiudadRow row in theTable)
                {
                    theMedico.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serchin Medicos", ex);
                throw;
            }
            return theMedico.Count;
        }
        public static int GetALLMedico(List<Medico> theMedico, string CiudadId,int ClienteId,int EspecialidadId)
        {
            int? totalRows = 0;
            try
            {

                MedicoDSTableAdapters.GetAllMedicoxCiudadTableAdapter theAdapter = new MedicoDSTableAdapters.GetAllMedicoxCiudadTableAdapter();
                MedicoDS.GetAllMedicoxCiudadDataTable theTable = theAdapter.GetAllMedico(CiudadId, EspecialidadId,ClienteId);
                foreach (MedicoDS.GetAllMedicoxCiudadRow row in theTable)
                {
                    theMedico.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serchin Medicos", ex);
                throw;
            }
            return theMedico.Count;
        }
        public static List<MedicoHorario> GetMedicoHorario(int MedicoId)
        {
            List<MedicoHorario> theMedico = new List<MedicoHorario>();
            try
            {
                MedicoDSTableAdapters.MedicoHorarioTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoHorarioTableAdapter();
                MedicoDS.MedicoHorarioDataTable theTable = theAdapter.GetMedicoHorario(MedicoId);

                foreach (MedicoDS.MedicoHorarioRow row in theTable)
                {
                    theMedico.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serchin Medicos", ex);
                throw;
            }
            return theMedico;
        }
        public static void DeleteMedicoHorario(int horId)
        {
            if (horId <= 0)
                throw new ArgumentException("horId cannot be less than or equal to zero.");
            if (horId <= 0)
                throw new ArgumentException("horId cannot be less than or equal to zero.");
            try
            {
                MedicoDSTableAdapters.MedicoHorarioTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoHorarioTableAdapter();
                theAdapter.MedicoDeleteHorario(horId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting horario médico", ex);
                throw;
            }
        }
        public static void InsertMedicoHorario(int MedicoId, int dia, DateTime horaInicio, DateTime horaFin)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            
            try
            {
                MedicoDSTableAdapters.MedicoHorarioTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoHorarioTableAdapter();
                theAdapter.MedicoAddHorario(MedicoId, dia, horaInicio, horaFin);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting MedicoCliente", ex);
                throw;
            }
        }
        //--------------------------------------
        private static MedicoCliente FillRecordMedicoCliente(MedicoDS.MedicoClienteRow row)
        {
            MedicoCliente objMedicoCliente = new MedicoCliente(
                row.MedicoId
                , row.ClienteId
                , row.UserId
                , row.Name
                , row.NombreJuridico
                , row.Nit.ToString()
                , row.CodigoCliente);

            return objMedicoCliente;
        }

        public static int SearchMedico(ref List<Medico> theMedico, string where, int firstRow, int pagesize)
        {
            int? totalRows = 0;
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetMedicoBySearch(where, pagesize, firstRow, ref totalRows);
                foreach (MedicoDS.MedicoRow row in theTable)
                {
                    theMedico.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serchin Medicos", ex);
                throw;
            }
            return (int)totalRows;
        }

        public static Medico getMedicoByMedicoId(int MedicoId)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            Medico theMedico = null;
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetMedicoByID(MedicoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theMedico = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medico by MedicoId", ex);
                throw;
            }
            return theMedico;
        }
        public int  BuscarUserIdDeMedico(int MedicoId)
        {
            int userId = 0;
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            Medico theMedico = null;
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetMedicoByID(MedicoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theMedico = FillRecord(theTable[0]);
                    userId = theMedico.UserId;
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medico by MedicoId", ex);
                throw;
            }
            return userId;
        }
        public int BuscarProveedorIdDeMedico(int MedicoId)
        {
            int userId = 0;
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            Medico theMedico = null;
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetMedicoByID(MedicoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theMedico = FillRecord(theTable[0]);
                    userId = theMedico.UserId;
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medico by MedicoId", ex);
                throw;
            }
            return userId;
        }
        public static Medico getMedicoByUserId(int UserId)
        {
            if (UserId <= 0)
                throw new ArgumentException("UserId cannot be less than or equal to zero.");
            Medico theMedico = null;
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetMedicoByUserId(UserId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    theMedico = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medico by MedicoId", ex);
                throw;
            }
            return theMedico;
        }

        public static int InsertMedico(Medico medico)
        {
            if (medico.UserId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (medico.EspecialidadId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            int? MedicoId = 0;

            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                theAdapter.Insert(ref MedicoId, medico.EspecialidadId, medico.Sedes, medico.ColegioMedico,
                    medico.UserId, medico.Estado, medico.Observacion, medico.IsExternal, medico.IsCallCenter);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Insert new Medico", ex);
                throw;
            }
            return (MedicoId).Value;
        }

        public static bool UpdateMedico(Medico medico)
        {
            if (medico.UserId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (medico.EspecialidadId <= 0)
                throw new ArgumentException("ProveedorId cannot be less than or equal to zero.");

            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                theAdapter.Update(medico.MedicoId, medico.EspecialidadId, medico.Sedes, medico.ColegioMedico,
                    medico.UserId, medico.Estado, medico.Observacion, medico.IsExternal, medico.IsCallCenter,medico.PermiteVideoLLamada);

                ProveedorUserDSTableAdapters.ProveedorUpdateNewTableAdapter Adapter = new ProveedorUserDSTableAdapters.ProveedorUpdateNewTableAdapter();
                Adapter.UpdateProveedorUserMedicoNew(medico.MedicoId,medico.EspecialidadId,medico.Sedes,medico.ColegioMedico,medico.UserId,medico.Estado,medico.Observacion);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Medico", ex);
                throw;
            }
        }

        public static void UpdateFotoId(int MedicoId, int FotoId)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");

            MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();

            try
            {
                theAdapter.MedicoUpdateFotoId(MedicoId, FotoId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating MedicoId FotoId", ex);
                throw;
            }
        }

        public static bool DeleteMedico(Medico objMedico)
        {
            return DeleteMedico(objMedico.MedicoId);
        }
        public static bool DeleteMedico(int MedicoId)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                theAdapter.Delete(MedicoId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Medico", ex);
                throw;
            }
        }

        public static List<MedicoCliente> getMedicoClienteByMedicoId(int MedicoId)
        {
            List<MedicoCliente> theList = new List<MedicoCliente>();
            try
            {
                MedicoDSTableAdapters.MedicoClienteTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoClienteTableAdapter();
                MedicoDS.MedicoClienteDataTable theTable = theAdapter.GetMedicoClienteByMedicoID(MedicoId);
                foreach (MedicoDS.MedicoClienteRow row in theTable)
                {
                    theList.Add(FillRecordMedicoCliente(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting MedicoCliente list", ex);
                throw;
            }
            return theList;
        }
        public static List<Medico> getAllMedicos()
        {
            List<Medico> resultados = new List<Medico>();
            try
            {
                MedicoDSTableAdapters.MedicoTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoTableAdapter();
                MedicoDS.MedicoDataTable theTable = theAdapter.GetAllMedicos();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicoDS.MedicoRow row in theTable.Rows)
                    {
                        Medico theMedico = FillRecord(row);
                        resultados.Add(theMedico);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Medico", ex);
                throw;
            }
            return resultados;
        }
        public static List<Medico> TodosMedicos() {
            try
            {
                List<Medico> resultados = new List<Medico>();
                MedicoDSTableAdapters.MedicoTodosTableAdapter theAdapter = new MedicoTodosTableAdapter();
                MedicoDS.Medico_TodosDataTable theTable = theAdapter.GetData();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicoDS.Medico_TodosRow row in theTable.Rows)
                    {
                        Medico theMedico = FillRecord(row);
                        resultados.Add(theMedico);
                    }
                }
                return resultados;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void DeleteMedicoCliente(int MedicoId, int ClienteId)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            try
            {
                MedicoDSTableAdapters.MedicoClienteTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoClienteTableAdapter();
                theAdapter.Delete(MedicoId, ClienteId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting MedicoCliente", ex);
                throw;
            }
        }
        public static void InsertMedicoCliente(int MedicoId, int ClienteId)
        {
            if (MedicoId <= 0)
                throw new ArgumentException("MedicoId cannot be less than or equal to zero.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be less than or equal to zero.");
            try
            {
                MedicoDSTableAdapters.MedicoClienteTableAdapter theAdapter = new MedicoDSTableAdapters.MedicoClienteTableAdapter();
                theAdapter.Insert(MedicoId, ClienteId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting MedicoCliente", ex);
                throw;
            }
        }

        public static List<Medico> getEspecialistasAutocomplete(string ciudadId, int clienteId, int especialidadId, int firstRow, int pageSize, string Filter, ref int totalRows)
        {
            List<Medico> theList = new List<Medico>();

            if (clienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than zero.");
            if (string.IsNullOrEmpty(ciudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                MedicoEspecialistasTableAdapter adapter = new MedicoEspecialistasTableAdapter();
                int? total = 0;
                MedicoDS.MedicoEspecialistasDataTable table = adapter.GetEspecialistas(ciudadId, clienteId, especialidadId, firstRow, pageSize, Filter, ref total);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (MedicoDS.MedicoEspecialistasRow row in table.Rows)
                    {
                        Medico m = new Medico()
                        {
                            MedicoId = row.MedicoId,
                            Nombre = row.MedicoNombre,
                            EspecialidadId = row.EspecialidadId,
                            Especialidad = row.EspecialidadNombre,
                            Sedes = row.Sedes,
                            ColegioMedico = row.ColegioMedico,
                            Observacion = row.Observacion,
                            IsExternal = row.IsExternal,
                            Direccion = row.Direccion,
                            Telefono = row.Telefono,
                            Celular = row.Celular
                        };
                        theList.Add(m);
                    }
                }
                totalRows = total.Value;
                /*
                ProveedorDSTableAdapters.ProveedorTableAdapter theAdapter = new ProveedorDSTableAdapters.ProveedorTableAdapter();
                int? total = 0;
                ProveedorDS.ProveedorDataTable theTable = theAdapter.Get_AUTOCOMPLETE_SearchProveedor(TipoProveedorId, CiudadId
                    , redMedicaIdPaciente, firstRow, pageSize, Filter, ref total);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (ProveedorDS.ProveedorRow row in theTable.Rows)
                    {
                        Proveedor theProveedor = FillRecord(row);
                        theList.Add(theProveedor);
                    }
                }
                totalRows = (int)total;
                 */
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

        public static List<Medico> getEspecialistasProveedorAutocomplete(string ciudadId, int clienteId, int especialidadId, int firstRow, int pageSize, string Filter, ref int totalRows)
        {
            List<Medico> theList = new List<Medico>();

            if (clienteId < 0)
                throw new ArgumentException("ClienteId cannot be less than zero.");
            if (string.IsNullOrEmpty(ciudadId))
                throw new ArgumentException("CiudadId cannot be null or empty.");

            try
            {
                MedicoEspecialistasTableAdapter adapter = new MedicoEspecialistasTableAdapter();
                int? total = 0;
                MedicoDS.MedicoEspecialistasDataTable table = adapter.GetEspecialistasProveedor(ciudadId, clienteId, especialidadId, firstRow, pageSize, Filter, ref total);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (MedicoDS.MedicoEspecialistasRow row in table.Rows)
                    {
                        Medico m = new Medico()
                        {
                            MedicoId = row.MedicoId,
                            Nombre = row.MedicoNombre,
                            EspecialidadId = row.EspecialidadId,
                            Especialidad = row.EspecialidadNombre,
                            Sedes = row.Sedes,
                            ColegioMedico = row.ColegioMedico,
                            Observacion = row.Observacion,
                            IsExternal = row.IsExternal,
                            Direccion = row.Direccion,
                            Telefono = row.Telefono,
                            Celular = row.Celular
                        };
                        theList.Add(m);
                    }
                }
                totalRows = total.Value;
               
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

        public static List<Medico> getEspecialistasAutocompleteNew( int especialidadId)
        {
            List<Medico> theList = new List<Medico>();

            if (especialidadId < 0)
                throw new ArgumentException("especialidadId cannot be less than zero.");
           

            try     
            {
                MedicoEspecialistasTableAdapter adapter = new MedicoEspecialistasTableAdapter();
                int? total = 0;
                MedicoDS.MedicoEspecialistasDataTable table = adapter.GetEspecialistasNew(null, especialidadId,null);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (MedicoDS.MedicoEspecialistasRow row in table.Rows)
                    {
                        Medico m = new Medico()
                        {
                            MedicoId = row.MedicoId,
                            Nombre = row.MedicoNombre,
                            EspecialidadId = row.EspecialidadId,
                            Especialidad = row.EspecialidadNombre,
                            Sedes = row.Sedes,
                            ColegioMedico = row.ColegioMedico,
                            Observacion = row.Observacion,
                            IsExternal = row.IsExternal,
                            Direccion = row.Direccion,
                            Telefono = row.Telefono,
                            Celular = row.Celular
                        };
                        theList.Add(m);
                    }
                }
               
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }



        public static List<Medico> getEspecialistasxMedicoId(int MedicoId)
        {
            List<Medico> theList = new List<Medico>();

            if (MedicoId < 0)
                throw new ArgumentException("especialidadId cannot be less than zero.");


            try
            {
                MedicoEspecialistasTableAdapter adapter = new MedicoEspecialistasTableAdapter();
                int? total = 0;
                MedicoDS.MedicoEspecialistasDataTable table = adapter.GetEspecialistasNew(null, null, MedicoId);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (MedicoDS.MedicoEspecialistasRow row in table.Rows)
                    {
                        Medico m = new Medico()
                        {
                            MedicoId = row.MedicoId,
                            Nombre = row.MedicoNombre,
                            EspecialidadId = row.EspecialidadId,
                            Especialidad = row.EspecialidadNombre,
                            Sedes = row.Sedes,
                            ColegioMedico = row.ColegioMedico,
                            Observacion = row.Observacion,
                            IsExternal = row.IsExternal,
                            Direccion = row.Direccion,
                            Telefono = row.Telefono,
                            Celular = row.Celular  
                        };
                        theList.Add(m);
                    }
                }

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Proveedor", ex);
                throw;
            }
            return theList;
        }

    }
}