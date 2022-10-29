using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using log4net;
using System.Data.SqlClient;
using Artexacta.App.Utilities.Import;
using System.Data.SqlTypes;

namespace Artexacta.App.Paciente.BLL
{
    /// <summary>
    /// Summary description for PacienteBLL
    /// </summary>
    public class PacienteBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public static Artexacta.App.Utilities.Bitacora.Bitacora theBitacora = new Artexacta.App.Utilities.Bitacora.Bitacora();

        public PacienteBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static Paciente FillRecord(PacienteDS.PacienteRow row)
        {
            Paciente objPaciente = new Paciente(
                row.PacienteId
                ,row.Nombre
                //,row.Apellido
                ,row.FechaNacimiento
                ,row.IsCarnetIdentidadNull()? "": row.CarnetIdentidad
                ,row.Direccion
                ,row.Telefono
                ,row.IsLugarTrabajoNull()? "":row.LugarTrabajo
                ,row.IsTelefonoTrabajoNull()? "":row.TelefonoTrabajo
                ,row.EstadoCivil
                ,row.NroHijo
                ,row.IsAntecedentesNull()? "":row.Antecedentes
                ,row.IsAntecedentesAlergicosNull()? "":row.AntecedentesAlergicos
                ,row.IsAntecedentesGinecoobstetricosNull()? "":row.AntecedentesGinecoobstetricos
                ,row.IsEmailNull()? "":row.Email
                ,row.Genero
                ,row.Celular
                ,row.UsuarioMovil
                ,row.UsuarioVerificado
                );
            if (!row.IsFotoIdNull())
            {
                objPaciente.FotoId = row.FotoId;
            }

            return objPaciente;
        }
        private static Paciente FillRecord(PacienteDS.PacienteGetAllPacienteSearchRow row)
        {
            Paciente objPaciente = new Paciente(
                row.PacienteId
                , row.Nombre
                , row.FechaNacimiento
                , row.IsCarnetIdentidadNull() ? "" : row.CarnetIdentidad
                , row.Direccion
                , row.Telefono
                , row.IsLugarTrabajoNull() ? "" : row.LugarTrabajo
                , row.IsTelefonoTrabajoNull() ? "" : row.TelefonoTrabajo
                , row.EstadoCivil
                , row.NroHijo
                , row.IsAntecedentesNull() ? "" : row.Antecedentes
                , row.IsAntecedentesAlergicosNull() ? "" : row.AntecedentesAlergicos
                , row.IsAntecedentesGinecoobstetricosNull() ? "" : row.AntecedentesGinecoobstetricos
                , row.IsEmailNull() ? "" : row.Email
                , row.Genero
                , ""
                , ""
                , false
                );
            

            return objPaciente;
        }

        public static Paciente GetPacienteByPacienteId(int PacienteId)
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            Paciente ThePaciente = null;
            try
            {
                PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
                PacienteDS.PacienteDataTable theTable = theAdapter.GetPacienteByPacienteId(PacienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    PacienteDS.PacienteRow row = theTable[0];
                    ThePaciente = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Paciente data", ex);
                throw;
            }
            return ThePaciente;
        }

        public static List<Paciente> getPacienteList()
        {
            List<Paciente> theList = new List<Paciente>();
            Paciente thePaciente = null;
            try
            {
                PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
                PacienteDS.PacienteDataTable theTable = theAdapter.GetAllPaciente();
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PacienteDS.PacienteRow row in theTable.Rows)
                    {
                        thePaciente = FillRecord(row);
                        theList.Add(thePaciente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Paciente", ex);
                throw;
            }
            return theList;
        }

        public static List<Paciente> SearchPaciente(string Search)
        {
            if (string.IsNullOrEmpty(Search))
                Search = "";

            List<Paciente> theList = new List<Paciente>();
            Paciente thePaciente = null;
            try
            {
                PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
                PacienteDS.PacienteDataTable theTable = theAdapter.SearchPaciente(Search);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PacienteDS.PacienteRow row in theTable.Rows)
                    {
                        thePaciente = FillRecord(row);
                        theList.Add(thePaciente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Paciente by search", ex);
                throw;
            }
            return theList;
        }
        public static List<Paciente> GetAllPacienteSearch(string Search,int MedicoId,int ClienteId)
        {
            if (string.IsNullOrEmpty(Search))
                Search = "";
            List<Paciente> theList = new List<Paciente>();
            Paciente thePaciente = null;
            try
            {
               
                PacienteDSTableAdapters.PacienteGetAllPacienteSearchTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteGetAllPacienteSearchTableAdapter();
                PacienteDS.PacienteGetAllPacienteSearchDataTable theTable = theAdapter.GeTAllPacienteSearch(Search,MedicoId,ClienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PacienteDS.PacienteGetAllPacienteSearchRow row in theTable.Rows)
                    {
                        thePaciente = FillRecord(row);
                        theList.Add(thePaciente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Paciente by search", ex);
                throw;
            }
            return theList;
        }
        public static int SearchPaciente(ref List<Paciente> _cache, int ClienteId, string Search, int pageSize, int firstItem)
        {
            return SearchPaciente(ref _cache, ClienteId, Search, pageSize, firstItem, false);
        }
        public static int SearchPaciente ( ref List<Paciente> _cache, int ClienteId, string Search, int pageSize, int firstItem, bool addAseguradoId)
        {
            if (string.IsNullOrEmpty(Search))
                Search = "";

            int? totalNumberOfRows = 0;
            try
            {
                PacienteDSTableAdapters.SearchPacientePagTableAdapter 
                    theAdapter = new PacienteDSTableAdapters.SearchPacientePagTableAdapter();
                PacienteDS.SearchPacientePagDataTable theTable = theAdapter.SearchPacientePag(Search, ClienteId, pageSize, firstItem, ref totalNumberOfRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (PacienteDS.SearchPacientePagRow row in theTable.Rows)
                    {
                        Paciente thePaciente = new Paciente();
                        thePaciente.PacienteId = row.PacienteId;
                        thePaciente.Nombre = row.Nombre;
                        thePaciente.CarnetIdentidad = row.CarnetIdentidad;
                        thePaciente.CodigoAsegurado = row.Codigo;
                        if (addAseguradoId) thePaciente.AseguradoId = row.AseguradoId;
                        _cache.Add(thePaciente);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Paciente by search", ex);
                throw;
            }
            return (int)totalNumberOfRows;
        }

        public static bool UpdatePacienteBasic(Paciente ObjPaciente)
        {
            if(ObjPaciente.PacienteId<=0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(ObjPaciente.Direccion))
                throw new ArgumentException("Direccion cannot be null or empty.");
            if(string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.EstadoCivil))
                throw new ArgumentException("EstadoCivil cannot be null or empty.");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            try
            {
                theAdapter.UpdatePacienteBasicById(ObjPaciente.PacienteId, ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Direccion, ObjPaciente.Telefono, ObjPaciente.LugarTrabajo
                    , ObjPaciente.TelefonoTrabajo, ObjPaciente.EstadoCivil, ObjPaciente.NroHijos, ObjPaciente.Genero);
                string msgBitacora = "Paciente " + ObjPaciente.Nombre;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.ActualizarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Paciente Basic data", ex);
                throw;
            }
        }
        public static bool VerificarUsuarioMovil(int pacId,bool verificado) {
            try
            {
                PacienteDSTableAdapters.rstPacienteMovilTableAdapter theAdapter = new PacienteDSTableAdapters.rstPacienteMovilTableAdapter();
                theAdapter.VerificarUsuarioMovil(verificado, pacId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error has ocurred while Updating Paciente Movil Basic data", ex);
                throw;
            }
            
        }
        //Method updates Fecha De Nacimiento, just a fork of updateBasic
        public static bool UpdatePacienteBasicByFechaNacimiento(Paciente ObjPaciente)
        {
            if (ObjPaciente.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(ObjPaciente.Direccion))
                throw new ArgumentException("Direccion cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.EstadoCivil))
                throw new ArgumentException("EstadoCivil cannot be null or empty.");
            if (ObjPaciente.FechaNacimiento <= DateTime.MinValue)
                throw new ArgumentException("FechaNacimiento cannot have MinValue");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            try
            {
                theAdapter.UpdatePacienteBasicFechaNacimientoById(ObjPaciente.PacienteId, ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Direccion, ObjPaciente.Telefono, ObjPaciente.LugarTrabajo
                    , ObjPaciente.TelefonoTrabajo, ObjPaciente.EstadoCivil, ObjPaciente.NroHijos, ObjPaciente.Genero, ObjPaciente.FechaNacimiento);
                string msgBitacora = "Paciente " + ObjPaciente.Nombre;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.ActualizarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Paciente Basic data", ex);
                throw;
            }
        }


        public static void UpdateFotoId(int PacienteId, int FotoId)
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            try
            {
                theAdapter.UpdateFotoId(PacienteId, FotoId);
                string msgBitacora = "Paciente " + PacienteId;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.ActualizarPacienteFoto, HttpContext.Current.User.Identity.Name, "PACIENTE", FotoId.ToString(), msgBitacora);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Paciente FotoId", ex);
                throw;
            }
        }

        public static bool UpdatePacienteComplete(Paciente ObjPaciente)
        {
            if (ObjPaciente.PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(ObjPaciente.Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            /*if (string.IsNullOrEmpty(ObjPaciente.Apellido))
                throw new ArgumentException("Apellido cannot be null or empty.");*/
            if (string.IsNullOrEmpty(ObjPaciente.Direccion))
                throw new ArgumentException("Direccion cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.EstadoCivil))
                throw new ArgumentException("EstadoCivil cannot be null or empty.");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            try
            {
                theAdapter.UpdatePacienteByPacienteId(ObjPaciente.PacienteId
                    ,ObjPaciente.Nombre,ObjPaciente.FechaNacimiento
                    , ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Direccion, ObjPaciente.Telefono, ObjPaciente.LugarTrabajo
                    , ObjPaciente.TelefonoTrabajo, ObjPaciente.EstadoCivil, ObjPaciente.NroHijos
                    , ObjPaciente.Antecedentes, ObjPaciente.AntecedentesAlergicos
                    , ObjPaciente.AntecedentesGinecoobstetricos, ObjPaciente.Email, ObjPaciente.Genero, ObjPaciente.Celular);
                string msgBitacora = "Paciente " + ObjPaciente.Nombre + " nacido el " + ObjPaciente.FechaNacimiento + " de Genero " + ObjPaciente.GeneroForDisplay;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.ActualizarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Paciente data", ex);
                throw;
            }
        }

        public static int InsertPaciente(Paciente ObjPaciente)
        {
            if (string.IsNullOrEmpty(ObjPaciente.Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            /*if (string.IsNullOrEmpty(ObjPaciente.Apellido))
                throw new ArgumentException("Apellido cannot be null or empty.");*/
            if (string.IsNullOrEmpty(ObjPaciente.Direccion))
                throw new ArgumentException("Direccion cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.EstadoCivil))
                throw new ArgumentException("EstadoCivil cannot be null or empty.");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            int? PacienteId = 0;
            try
            {
                theAdapter.InsertPaciente(ref PacienteId
                    , ObjPaciente.Nombre, ObjPaciente.FechaNacimiento
                    , ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Direccion, ObjPaciente.Telefono, ObjPaciente.LugarTrabajo
                    , ObjPaciente.TelefonoTrabajo, ObjPaciente.EstadoCivil, ObjPaciente.NroHijos
                    , ObjPaciente.Antecedentes, ObjPaciente.AntecedentesAlergicos
                    , ObjPaciente.AntecedentesGinecoobstetricos, ObjPaciente.Email, ObjPaciente.Genero, ObjPaciente.Celular);
                string msgBitacora = "Paciente " + ObjPaciente.Nombre + " nacido el " + ObjPaciente.FechaNacimiento + " de Genero " + ObjPaciente.GeneroForDisplay;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return (int)PacienteId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Paciente data", ex);
                throw;
            }
        }

        /// <summary>
        /// Deveulve el id de un paciente que lo crea o lo obtiene. Solamente busca por nombre y que haya sido
        /// metido con NA en Direccion y Telefono.
        /// Esto se ha hecho exclusivamente par ael tema de actualizar los datos desde Alianza
        /// </summary>
        /// <param name="ObjPaciente"></param>
        /// <returns></returns>
        public static int InsertOrGetPaciente(Paciente ObjPaciente)
        {
            if (string.IsNullOrEmpty(ObjPaciente.Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            /*if (string.IsNullOrEmpty(ObjPaciente.Apellido))
                throw new ArgumentException("Apellido cannot be null or empty.");*/
            if (string.IsNullOrEmpty(ObjPaciente.Direccion))
                throw new ArgumentException("Direccion cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.EstadoCivil))
                throw new ArgumentException("EstadoCivil cannot be null or empty.");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            int? PacienteId = 0;
            try
            {
                theAdapter.InsertOrGetPaciente(ref PacienteId
                    , ObjPaciente.Nombre, ObjPaciente.FechaNacimiento
                    , ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Direccion, ObjPaciente.Telefono, ObjPaciente.LugarTrabajo
                    , ObjPaciente.TelefonoTrabajo, ObjPaciente.EstadoCivil, ObjPaciente.NroHijos
                    , ObjPaciente.Antecedentes, ObjPaciente.AntecedentesAlergicos
                    , ObjPaciente.AntecedentesGinecoobstetricos, ObjPaciente.Email, ObjPaciente.Genero);
                string msgBitacora = "[InsertOrGetPaciente]Paciente " + ObjPaciente.Nombre + " nacido el " + ObjPaciente.FechaNacimiento + " de Genero " + ObjPaciente.GeneroForDisplay;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return (int)PacienteId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Paciente data", ex);
                throw;
            }
        }

        /// <summary>
        /// Exclusivamente utilizado cunado el paciente es de tipo desgravamen. El CI viene con el codigo de cliente
        /// de desgravamen en el SP.
        /// </summary>
        /// <param name="ObjPaciente"></param>
        /// <returns></returns>
        public static int InsertOrGetPacienteByCI(Paciente ObjPaciente, string ciudadId)
        {
            if (string.IsNullOrEmpty(ObjPaciente.Nombre))
                throw new ArgumentException("Nombre cannot be null or empty.");
            if (string.IsNullOrEmpty(ObjPaciente.Telefono))
                throw new ArgumentException("Telefono cannot be null or empty.");
            if (ObjPaciente.NroHijos < 0)
                ObjPaciente.NroHijos = 0;// or error?

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            int? PacienteId = 0;
            try
            {
                theAdapter.DESGInsertOrGetPacienteByCI(ref PacienteId
                    , ObjPaciente.Nombre, ObjPaciente.FechaNacimiento
                    , ObjPaciente.CarnetIdentidad
                    , ObjPaciente.Telefono, ciudadId, ObjPaciente.Genero);
                string msgBitacora = "[InsertOrGetPacienteByCI]Paciente " + ObjPaciente.Nombre + " nacido el " + ObjPaciente.FechaNacimiento + " de Genero " + ObjPaciente.GeneroForDisplay;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", ObjPaciente.CarnetIdentidad, msgBitacora);

                return (int)PacienteId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Paciente data", ex);
                throw;
            }
        }

        public static bool DeletePaciente(int PacienteId)
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero."); 
            
            try
            {
                PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
                theAdapter.DeletePacienteById(PacienteId);
                string msgBitacora = "Eliminado Paciente " + PacienteId;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.EliminarPaciente, HttpContext.Current.User.Identity.Name, "PACIENTE", PacienteId.ToString(), msgBitacora);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Paciente", ex);
                throw;
            }
        }

        public static bool isCritico ( int PacienteId )
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            try
            {
                return (bool)theAdapter.IsCritico(PacienteId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Updating Paciente Basic data", ex);
                throw;
            }
        }

        public static string ValidateImportarPaciente(DataTable theDataTable, int ClienteID)
        {
            return ValidateImportarPaciente(ref theDataTable, ClienteID, false);
        }

        public static string ValidateImportarPaciente ( DataTable theDataTable, int ClienteID, bool EraseClientPolizaState )
        {
            return ValidateImportarPaciente(ref theDataTable, ClienteID, EraseClientPolizaState, 0);
        }

        public static string ValidateImportarPaciente ( DataTable theDataTable, int ClienteID, bool EraseClientPolizaState, int firstRowNumber )
        {
            return ValidateImportarPaciente(ref theDataTable, ClienteID, EraseClientPolizaState, firstRowNumber);
        }

        public static string ValidateImportarPaciente ( ref DataTable theDataTable, int ClienteID, bool EraseClientPolizaState )
        {
            return ValidateImportarPaciente(ref theDataTable, ClienteID, EraseClientPolizaState, 0); 
        }

        public static string ValidateImportarPaciente ( ref DataTable theDataTable, int ClienteID, bool EraseClientPolizaState, int firstRowNumber )
        {
            if (ClienteID < 0)
                throw new ArgumentException("ClienteID cannot be less than or equal to zero.");
            if (theDataTable == null)
                throw new ArgumentException("DataTable object cannot be null");

            PacienteDSTableAdapters.ValidarImportacionPacienteTableAdapter theAdapter = new PacienteDSTableAdapters.ValidarImportacionPacienteTableAdapter();

            PacienteDS.ValidarImportacionPacienteDataTable theTable = theAdapter.ValidarImportacionPaciente(theDataTable, ClienteID, EraseClientPolizaState);
            string ErrorList = "";
            List<string> errores = new List<string>();
            foreach (PacienteDS.ValidarImportacionPacienteRow row in theTable.Rows)
            {
                string numeroPoliza = row.IsNumeroPolizaNull() ? "" : row.NumeroPoliza;
                ErrorList += "Error la fila " + ( firstRowNumber + row.RowNumber) + ": " +
            "<div style='margin-left: 20px;'>" + (row.ErrorType == 1 ? "El Asegurado " + row.CodigoAsegurado + ", que intenta actualizar no tiene el mismo nombre que el de Base de datos."
            : (row.ErrorType == 2 ? "El Numero de Póliza " + numeroPoliza + " no pertenece al Asegurado con Código " + row.CodigoAsegurado + "."
            : (row.ErrorType == 3 ? "La póliza " + numeroPoliza + " no se puede insertar porque ya existe una póliza activa para ese Asegurado con Código " + row.CodigoAsegurado + "."
            : "Asegurados repetidos Código de Asegurado = " + row.CodigoAsegurado + ", solo se permite una póliza por asegurado."))) + "</div><br />";
                errores.Add(row.CodigoAsegurado);
            }
            List<DataRow> rowToDelete = new List<DataRow>();
            foreach (DataRow row in theDataTable.Rows)
            {
                try
                {
                    if (errores.Contains(row["CodigoAsegurado"]))
                        rowToDelete.Add(row);
                }catch(Exception e){
                    log.Error(e);
                }
            }
            foreach (DataRow row in rowToDelete)
            {
                theDataTable.Rows.Remove(row);
            }

            return ErrorList;
        }

        public static void ImportarPaciente ( DataTable theDataTable, int ClienteID, string Lugar, bool EraseClientPolizaState, ref string errorList )
        {
            int aux = 0;
            ImportarPaciente(theDataTable, ClienteID, Lugar, EraseClientPolizaState, true, ref errorList, ref aux, ref aux, ref aux, ref aux);
        }

        public static void ImportarPaciente(DataTable theDataTable, int ClienteID, string Lugar,
            bool EraseClientPolizaState, bool update, ref string errorList, ref int ActualizadosActivo,
            ref int ActualizadosInactivo, ref int Insertados, ref int Errores )
        {
            if (ClienteID < 0)
                throw new ArgumentException("ClienteID cannot be less than or equal to zero.");
            if (theDataTable == null)
                throw new ArgumentException("DataTable object cannot be null");
            if (string.IsNullOrWhiteSpace(Lugar))
                Lugar = "SCZ";

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();

            int? nbActualizadosAActivo = 0;
            int? nbActualizadosAInactivo = 0;
            int? nbInsertados = 0;
            int? nbErrores = 0;
            theAdapter.ImportarPaciente(theDataTable, ClienteID, Lugar, EraseClientPolizaState, update, 
                ref nbActualizadosAActivo, ref nbActualizadosAInactivo, ref nbInsertados, ref nbErrores);

            ActualizadosActivo = (int)nbActualizadosAActivo;
            ActualizadosInactivo = (int)nbActualizadosAInactivo;
            Insertados = (int)nbInsertados;
            Errores = (int)nbErrores;
        }

        public static void InactivarPolizas(int ClienteID)
        {
            if (ClienteID < 0)
                throw new ArgumentException("ClienteID cannot be less than or equal to zero.");

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
            theAdapter.cmdTimeout = 600;
            theAdapter.InactivarPolizas(ClienteID);
        }
        public static void ImportarPacienteV2(DataTable theDataTable, int ClienteID, string Lugar, ref string rowError)
        {
            bool Insertado = false;
            ImportarPacienteV2(theDataTable, ClienteID, Lugar, ref rowError, ref Insertado);
        }

        public static void ImportarPacienteV2(DataTable theDataTable, int ClienteID, string Lugar, ref string rowError, ref bool Insertado)
        {
            if (ClienteID < 0)
                throw new ArgumentException("ClienteID cannot be less than or equal to zero.");
            if (theDataTable == null)
                throw new ArgumentException("DataTable object cannot be null");
            if (string.IsNullOrWhiteSpace(Lugar))
                Lugar = "SCZ";

            PacienteDSTableAdapters.PacienteTableAdapter theAdapter = new PacienteDSTableAdapters.PacienteTableAdapter();
            DataRow row = theDataTable.Rows[0];
            try
            {
                int nroHijos = 0;
                try
                {
                    nroHijos = Convert.ToInt32("0" + row[ImportElement.NroHijo]);
                }
                catch { }
                bool? boolInsertado = false;
                theAdapter.ImportarPacienteV2(ClienteID, Lugar,
                    row[ImportElement.Nombre].ToString(),
                    row[ImportElement.FechaNacimiento].Equals("") ? SqlDateTime.MinValue.Value : Convert.ToDateTime(row[ImportElement.FechaNacimiento]),
                    Convert.ToBoolean(row[ImportElement.Genero]),
                    row[ImportElement.Relacion].ToString(),
                    row[ImportElement.CarnetIdentidad].ToString(),
                    row[ImportElement.Direccion].ToString(),
                    row[ImportElement.Telefono].ToString(),
                    row[ImportElement.LugarTrabajo].ToString(),
                    row[ImportElement.TelefonoTrabajo].ToString(),
                    row[ImportElement.EstadoCivil].ToString(),
                    nroHijos,
                    row[ImportElement.Antecedentes].ToString(),
                    row[ImportElement.AntecedentesAlergicos].ToString(),
                    row[ImportElement.AntecedentesGinecoobstetricos].ToString(),
                    row[ImportElement.Email].ToString(),
                    row[ImportElement.CodigoAsegurado].ToString(),
                    row[ImportElement.NumeroPoliza].ToString(),
                    Convert.ToDateTime(row[ImportElement.FechaInicioPoliza]),
                    Convert.ToDateTime(row[ImportElement.FechaFinPoliza]),
                    Convert.ToInt32(row[ImportElement.MontoTotal]),
                    Convert.ToInt32(row[ImportElement.MontoFarmacia]),
                    row[ImportElement.Cobertura].ToString(),
                    row[ImportElement.NombrePlan].ToString(),
                    row[ImportElement.EstadoPoliza].ToString(),
                    ref boolInsertado);
                Insertado = boolInsertado.Value;
            }
            catch(Exception q){
                log.Error("Error al Importar Paciente", q);
                rowError = "<br />Error al Importar Paciente: " +
                    row[ImportElement.Nombre] + " [" + row[ImportElement.CodigoAsegurado] + "]";
            }
        }
    }
}