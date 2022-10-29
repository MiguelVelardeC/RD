using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Artexacta.App.Accidentado.BLL
{
    public class AccidentadoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public AccidentadoBLL() {}

        private static Accidentado FillRecord(AccidentadoDS.AccidentadoRow row)
        {
            DateTime FechaMax = DateTime.MaxValue;
            Accidentado objAccidentado = new
                Accidentado(
                    row.AccidentadoId
                    ,row.Nombre
                    ,row.CarnetIdentidad
                    ,row.Genero
                    ,row.FechaNacimiento
                    ,row.EstadoCivil
                    ,row.IsLicenciaConducirNull() ? '-' : (row.LicenciaConducir ? 'S' : 'N')
                    ,row.Conductor
                    ,row.Tipo
                    ,row.Estado
                );           

            if (!row.IsSiniestrosPreliquidacionNull())
            {
                objAccidentado.SiniestroPreliquidacion = row.SiniestrosPreliquidacion;
            }

            if (!row.IsSiniestrosPagadosNull())
            {
                objAccidentado.SiniestroPagado = row.SiniestrosPagados;
            }

            if (!row.IsFileCountNull())
            {
                objAccidentado.FileCount = row.FileCount;
            }

            if (!row.IsSiniestroIdNull())
            {
                int siniestroId = 0;
                int.TryParse(row.SiniestroId, out siniestroId);
                objAccidentado.SiniestroId = siniestroId;
            }

            if (!row.IsReservaNull())
            {
                objAccidentado.Reserva = row.Reserva;
            }

            objAccidentado.IsIncapacidadTotal = row.IsIncapacidadTotal;

            return objAccidentado;
        }

        public static List<Accidentado> SearchAccidentado ( string search, int SiniestroId )
        {
            if (SiniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            List<Accidentado> theList = new List<Accidentado>();
            Accidentado TheAccidentado = null;
            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                AccidentadoDS.AccidentadoDataTable theTable = theAdapter.SearchAccidentadoBySiniestroId(search, SiniestroId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (AccidentadoDS.AccidentadoRow row in theTable.Rows)
                    {
                        TheAccidentado = FillRecord(row);
                        theList.Add(TheAccidentado);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serching Accidentado data By SiniestroId", ex);
                throw;
            }
            return theList;
        }

        public static List<Accidentado> GetAccidentadosBySiniestroId(string search, int siniestroId)
        {
            if (siniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            List<Accidentado> theList = new List<Accidentado>();
            Accidentado TheAccidentado = null;
            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                AccidentadoDS.AccidentadoDataTable theTable = theAdapter.GetAccidentadosBySiniestroId(siniestroId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (AccidentadoDS.AccidentadoRow row in theTable.Rows)
                    {
                        TheAccidentado = FillRecord(row);
                        theList.Add(TheAccidentado);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while serching Accidentado data By SiniestroId", ex);
                throw;
            }
            return theList;
        }

        public static DataTable GetGastosByAccidentadoPivotTipo(int SiniestroId)
        {
            DataTable theTable = new DataTable();
            using (SqlConnection theConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["RedSaludDBConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_SOAT_GetGastosByAccidentadoPivotTipo", theConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@intSiniestroId", SqlDbType.Int).Value = SiniestroId;
                    theConnection.Open();

                    using (SqlDataAdapter theAdapter = new SqlDataAdapter())
                    {
                        theAdapter.SelectCommand = cmd;

                        theAdapter.Fill(theTable);
                    }
                }
            }
            return theTable;
        }


        public static Accidentado GetAccidentadoByID ( int AccidentadoID, int SiniestroId )
        {
            if (AccidentadoID < 0)
                throw new ArgumentException("AccidentadoID cannot be less than or equal to zero.");
            if (SiniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            Accidentado TheAccidentado = null;
            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                AccidentadoDS.AccidentadoDataTable theTable = theAdapter.GetAccidentadoById(AccidentadoID, SiniestroId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheAccidentado = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Accidentado data", ex);
                throw;
            }
            return TheAccidentado;
        }

        public static Accidentado GetConductor ( int SiniestroID )
        {
            if (SiniestroID < 0)
                throw new ArgumentException("SiniestroID cannot be less than or equal to zero.");

            Accidentado TheAccidentado = null;
            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                AccidentadoDS.AccidentadoDataTable theTable = theAdapter.GetConductor(SiniestroID);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheAccidentado = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Accidentado data", ex);
                throw;
            }
            return TheAccidentado;
        }

        public static void SaveAccidentado ( ref Accidentado accidentado, int SiniestroId )
        {
            int _AccidentadoID = accidentado.AccidentadoId;
            SaveAccidentado(ref _AccidentadoID, SiniestroId, accidentado.Nombre, accidentado.CarnetIdentidad
                , accidentado.Genero, accidentado.FechaNacimiento, accidentado.EstadoCivil, accidentado.LicenciaConducir
                , accidentado.Tipo, accidentado.Estado, accidentado.IsIncapacidadTotal);
            accidentado.AccidentadoId = _AccidentadoID;
        }
        public static void SaveAccidentado ( ref int AccidentadoID, int SiniestroId, string nombre, string carnetIdentidad,
            bool genero, DateTime fechaNacimiento, string estadoCivil, string licenciaConducir, string Tipo, bool estado, bool isIncapacidadTotal )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("nombre cannot null or empty.");

            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                int? _AccidentadoID = AccidentadoID;
                theAdapter.SaveAccidentado(ref _AccidentadoID, SiniestroId, nombre, carnetIdentidad, genero,
                        fechaNacimiento, estadoCivil, licenciaConducir, Tipo, estado, isIncapacidadTotal);

                AccidentadoID = (int)_AccidentadoID;
                if (AccidentadoID <= 0)
                    throw new ArgumentException("The Accidentado was inserted but AccidentadoID return less than or equal to zero.");
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Accidentado", ex);
                throw;
            }
        }
        public static void DeleteAccidentado ( int AccidentadoID )
        {
            if (AccidentadoID <= 0)
                throw new ArgumentException("AccidentadoID cannot be less than or equal to zero.");

            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                theAdapter.DeleteAccidentado(AccidentadoID);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Accidentado", ex);
                throw;
            }
        }
        public static bool IsAccidentadoSave ( int SiniestroID, int AccidentadoID )
        {
            if (SiniestroID <= 0)
                return false;
            if (AccidentadoID < 0)
                AccidentadoID = 0;
            try
            {
                AccidentadoDSTableAdapters.AccidentadoTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoTableAdapter();
                return (bool)theAdapter.IsAccidentadoSave(SiniestroID, AccidentadoID);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while checking if Accidentado is saved", ex);
                throw;
            }
        }

        public static List<DocumentFile> getFileList ( int AccidentadoId )
        {

            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                AccidentadoDSTableAdapters.AccidentadoFileTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoFileTableAdapter();
                AccidentadoDS.AccidentadoFileDataTable theTable = theAdapter.GetFile(AccidentadoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (AccidentadoDS.AccidentadoFileRow row in theTable.Rows)
                    {
                        theList.Add(DocumentFile.CreateNewTypedDocumentFileObject(
                            row.fileID,
                            row.dateUploaded,
                            row.fileSize,
                            row.fileName,
                            row.extension,
                            row.fileStoragePath
                            ));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Accidentado file list", ex);
                throw;
            }
            return theList;
        }
        public static void DeleteFile ( int FileId )
        {
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                AccidentadoDSTableAdapters.AccidentadoFileTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Accidentado File", ex);
                throw;
            }
        }
        public static void InsertFile ( int AccidentadoId, int FileId )
        {
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                AccidentadoDSTableAdapters.AccidentadoFileTableAdapter theAdapter = new AccidentadoDSTableAdapters.AccidentadoFileTableAdapter();
                theAdapter.InsertFile(FileId, AccidentadoId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Accidentado File", ex);
                throw;
            }
        }
    }
}