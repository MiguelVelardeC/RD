using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using Artexacta.App.User.BLL;
using log4net;

namespace Artexacta.App.Siniestro.BLL
{
    public class SiniestroBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public static Artexacta.App.Utilities.Bitacora.Bitacora theBitacora = new Artexacta.App.Utilities.Bitacora.Bitacora();

        public SiniestroBLL () { }

        private static Siniestro FillRecord ( SiniestroDS.SiniestroRow row )
        {
            DateTime FechaMax = DateTime.MaxValue;
            Siniestro objSiniestro = new
                Siniestro(
                      row.SiniestroId
                    , row.ClienteId
                    , row.FechaSiniestro
                    , row.FechaDenuncia
                    , row.IsCausaNull() ? "" : row.Causa
                    , row.LugarDpto
                    , row.LugarProvincia
                    , row.IsZonaNull() ? "" : row.Zona
                    , row.IsSindicatoNull() ? "" : row.Sindicato
                    , row.OperacionId
                    , row.NumeroRoseta
                    , row.NumeroPoliza
                    , row.IsLugarVentaNull() ? "" : row.LugarVenta
                    , row.NombreTitular
                    , row.CITitular
                    , row.Placa
                    , row.Tipo
                    , row.IsNroChasisNull() ? "" : row.NroChasis
                    , row.IsNroMotorNull() ? "" : row.NroMotor
                    , row.Sector
                    , row.IsnombreInspectorNull() ? "" : row.nombreInspector
                );

            return objSiniestro;
        }
        private static SiniestroForList FillRecordForList ( SiniestroDS.SearchSiniestroForListRow row )
        {
            DateTime FechaMax = DateTime.MaxValue;
            SiniestroForList objSiniestro = new
                SiniestroForList(
                    row.SiniestroId
                    , row.FechaSiniestro
                    , row.FechaDenuncia
                    , row.TotalOcupantes
                    , row.CantidadHeridos
                    , row.CantidadFallecidos
                    , row.IsLugarNull() ? "" : row.Lugar
                    , row.IsZonaNull() ? "" : row.Zona
                    , row.IsSindicatoNull() ? "" : row.Sindicato
                    , row.NumeroRoseta
                    , row.NumeroPoliza
                    , row.OperacionId
                    , row.Placa
                    , row.TipoVehiculo
                    , row.Sector
                    , row.CITitular
                    , row.NombreTitular
                    , row.IsSiniestrosPreliquidacionNull() ? 0 : row.SiniestrosPreliquidacion
                    , row.IsSiniestrosPagadosNull() ? 0 : row.SiniestrosPagados
                    , row.EstadoSeguimiento
                    , row.NombreAuditor
                    , row.IsFileCountNull() ? 0 : row.FileCount
                    , row.RowNumber
                );

            return objSiniestro;
        }

        public static Siniestro GetSiniestroByID ( int SiniestroId )
        {
            if (SiniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            Siniestro TheSiniestro = null;
            try
            {
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();
                SiniestroDS.SiniestroDataTable theTable = theAdapter.GetSiniestroByID(SiniestroId);
                
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheSiniestro = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Siniestro data", ex);
                throw;
            }
            return TheSiniestro;
        }

        public static void InsertSiniestro ( ref Siniestro siniestro, Seguimiento.Seguimiento seguimiento )
        {
            siniestro.SiniestroId = InsertSiniestro(siniestro.ClienteId, siniestro.FechaSiniestro, siniestro.FechaDenuncia, siniestro.TipoCausa, siniestro.Causa,
                siniestro.LugarDpto, siniestro.LugarProvincia, siniestro.Zona, siniestro.Sindicato, siniestro.OperacionId, siniestro.NumeroRoseta,
                siniestro.NumeroPoliza, siniestro.LugarVenta, siniestro.NombreTitular, siniestro.CITitular, siniestro.Placa,
                siniestro.Tipo, siniestro.NroChasis, siniestro.NroMotor, siniestro.Sector, seguimiento.Acuerdo, seguimiento.Rechazado, seguimiento.Observaciones, siniestro.NombreInspector);
        }
        public static int InsertSiniestro ( int ClienteId, DateTime fechaSiniestro, DateTime fechaDenuncia, string tipoCausa, string causa, string lugarDpto,
            string lugarProvincia, string zona, string sindicato, string operacionId, string numeroRoseta, string numeroPoliza,
            string lugarVenta, string nombreTitular, string ciTitular, string placa, string tipo, string NroChasis, string NroMotor, string sector,
            bool Acuerdo, bool Rechazado, string Observaciones, string NombreInspector)
        {
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be <= 0.");
            if (string.IsNullOrEmpty(operacionId))
                throw new ArgumentException("operacionId cannot null or empty.");
            if (string.IsNullOrEmpty(lugarDpto))
                throw new ArgumentException("lugarDpto cannot null or empty.");
            if (string.IsNullOrEmpty(lugarProvincia))
                throw new ArgumentException("lugarProvincia cannot null or empty.");
            if (string.IsNullOrEmpty(numeroRoseta))
                throw new ArgumentException("numeroRoseta cannot null or empty.");
            if (string.IsNullOrEmpty(numeroPoliza))
                throw new ArgumentException("numeroRoseta cannot null or empty.");
            if (string.IsNullOrEmpty(placa))
                throw new ArgumentException("placa cannot null or empty.");
            int userId = 0;
            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                if (userId <= 0)
                    throw new ArgumentException("UserId cannot be <= 0.");
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            try
            {
                int? SiniestroId = 0;
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();

                theAdapter.Insert(ref SiniestroId, ClienteId, userId, fechaSiniestro, fechaDenuncia, tipoCausa + causa, lugarDpto, lugarProvincia, zona, sindicato,
                    operacionId, numeroRoseta, numeroPoliza, lugarVenta, nombreTitular, ciTitular, placa, tipo, NroChasis,NroMotor,NombreInspector, sector,
                    Acuerdo, Rechazado, Observaciones);

                if (SiniestroId <= 0)
                {
                    throw new ArgumentException("the insert SP return SiniestroId less than or equal to zero.");
                }
                string msgBitacora = "Insertado Siniestro con datos: operacionId:" + operacionId + ", lugarDpto:" + lugarDpto + ", fechaSiniestro:" + fechaSiniestro + ", fechaDenuncia:" + fechaDenuncia;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.InsertCasoMedicoSOAT, HttpContext.Current.User.Identity.Name, "SiniestroSOAT", SiniestroId.ToString(), msgBitacora);
                return (int)SiniestroId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting Siniestro", ex);
                throw new ArgumentException("An error was ocurred while inserting Siniestro.", ex);
            }
        }

        public static void UpdateSiniestro ( Siniestro siniestro, Seguimiento.Seguimiento seguimiento )
        {
            UpdateSiniestro(siniestro.SiniestroId, siniestro.ClienteId, siniestro.FechaSiniestro, siniestro.FechaDenuncia, siniestro.TipoCausa, siniestro.Causa,
                siniestro.LugarDpto, siniestro.LugarProvincia, siniestro.Zona, siniestro.Sindicato, siniestro.OperacionId, siniestro.NumeroRoseta,
                siniestro.NumeroPoliza, siniestro.LugarVenta, siniestro.NombreTitular, siniestro.CITitular, siniestro.Placa,
                siniestro.Tipo, siniestro.NroChasis, siniestro.NroMotor, siniestro.Sector, seguimiento.Acuerdo, seguimiento.Rechazado, seguimiento.Observaciones, siniestro.NombreInspector);
        }
        public static void UpdateSiniestro ( int SiniestroId, int ClienteId, DateTime fechaSiniestro, DateTime fechaDenuncia, string tipoCausa, string causa,
            string lugarDpto, string lugarProvincia, string zona, string sindicato, string operacionId, string numeroRoseta, string numeroPoliza,
            string lugarVenta, string nombreTitular, string ciTitular, string placa, string tipo, string NroChasis, string NroMotor, string sector,
            bool Acuerdo, bool Rechazado, string Observaciones, string NombreInspector )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be <= 0.");
            if (string.IsNullOrEmpty(operacionId))
                throw new ArgumentException("operacionId cannot null or empty.");
            if (string.IsNullOrEmpty(lugarDpto))
                throw new ArgumentException("lugarDpto cannot null or empty.");
            if (string.IsNullOrEmpty(lugarProvincia))
                throw new ArgumentException("lugarProvincia cannot null or empty.");
            if (string.IsNullOrEmpty(numeroRoseta))
                throw new ArgumentException("numeroRoseta cannot null or empty.");
            if (string.IsNullOrEmpty(numeroPoliza))
                throw new ArgumentException("numeroRoseta cannot null or empty.");
            if (string.IsNullOrEmpty(placa))
                throw new ArgumentException("placa cannot null or empty.");
            try
            {
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();
                theAdapter.Update(SiniestroId, ClienteId, fechaSiniestro, fechaDenuncia, tipoCausa + causa, lugarDpto, lugarProvincia, zona, sindicato,
                    operacionId, numeroRoseta, numeroPoliza, nombreTitular, lugarVenta, ciTitular, placa, tipo, NroChasis, NroMotor, NombreInspector, sector,
                    Acuerdo, Rechazado, Observaciones);
                string msgBitacora = "Actualizando Siniestro con datos: operacionId:" + operacionId + ", lugarDpto:" + lugarDpto + ", fechaSiniestro:" + fechaSiniestro + ", fechaDenuncia:" + fechaDenuncia;
                theBitacora.RecordTrace(Artexacta.App.Utilities.Bitacora.Bitacora.TraceType.UpdateCasoMedicoSOAT, HttpContext.Current.User.Identity.Name, "SiniestroSOAT", SiniestroId.ToString(), msgBitacora);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating Siniestro", ex);
                throw;
            }
        }

        public static void DeleteSiniestro ( int SiniestroId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            int userId = 0;

            try
            {
                userId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error getting the user Id.", e);
            }

            try
            {
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();
                theAdapter.Delete(SiniestroId, userId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting Siniestro", ex);
                throw;
            }
        }

        public static bool HaveConductor ( int SiniestroId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");

            try
            {
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();
                return (bool)theAdapter.HaveConductor(SiniestroId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while getting if Siniestro Have Conductor", ex);
                throw;
            }
        }

        public static int SearchSiniestroForList ( List<SiniestroForList> _cache, ref int totalAccidentados, ref decimal totalPreliquidado, 
            ref decimal totalPagado, int pagesize, int firstPage, string where, int clienteId, int userId, string orderBy )
        {
            List<SiniestroForList> TheSiniestroList = new List<SiniestroForList>();
            try
            {
                SiniestroDSTableAdapters.SearchSiniestroForListTableAdapter theAdapter = new SiniestroDSTableAdapters.SearchSiniestroForListTableAdapter();
                int? totalRows = 0;
                int? refTotalAccidentados = 0;
                decimal? refTotalPreliquidado = 0;
                decimal? refTotalPagado = 0;
                theAdapter.cmdTimeout = 600;
                SiniestroDS.SearchSiniestroForListDataTable theTable = theAdapter.SearchSiniestroForList(pagesize, firstPage, ref totalRows,
                    ref refTotalAccidentados, ref refTotalPreliquidado, ref refTotalPagado, where, userId, clienteId, orderBy);

                foreach (SiniestroDS.SearchSiniestroForListRow row in theTable.Rows)
                {
                    _cache.Add(FillRecordForList(row));
                }
                totalAccidentados = (refTotalAccidentados == null ? 0 : (int)refTotalAccidentados);
                totalPreliquidado = (refTotalPreliquidado == null ? 0 : (decimal)refTotalPreliquidado);
                totalPagado = (refTotalPagado == null ? 0 : (decimal)refTotalPagado);
                return (int)totalRows;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while searching Siniestro data for list", ex);
                throw ex;
            }
        }

        public static List<SiniestroForExport> GetSiniestroForExport(string where, int clienteId, int userId, string orderBy)
        {
            List<SiniestroForExport> TheSiniestroList = new List<SiniestroForExport>();
            try
            {
                SiniestroDSTableAdapters.ExportToExcelTableAdapter theAdapter = new SiniestroDSTableAdapters.ExportToExcelTableAdapter();
                theAdapter.cmdTimeout = 600;
                SiniestroDS.ExportToExcelDataTable theTable = theAdapter.GetExportToExcel(where, clienteId, userId, orderBy);
                int i = 1;
                foreach (SiniestroDS.ExportToExcelRow row in theTable.Rows)
                {   
                    TheSiniestroList.Add(new SiniestroForExport(i, row.Nombre, row.Genero, row.FechaNacimiento, row.EstadoCivil, row.CarnetIdentidad,
                        row.EsConductor,row.LicenciaConducir, row.TipoAccidentado, row.EstadoAccidentado, row.OperacionId, row.Creador, row.NumeroRoseta, row.NumeroPoliza,
                        row.NombreTitular, row.CITitular, row.FechaSiniestro, row.FechaDenuncia, row.TotalOcupantes, row.CantidadHeridos,
                        row.CantidadFallecidos, row.LugarDpto, row.LugarProvincia, row.Zona, row.Sindicato, row.Tipo, row.Cilindrada, row.NroChasis, row.NroMotor, row.Sector, row.Placa,
                        row.NombreES, row.IsFechaVisitaNull() ? SqlDateTime.MinValue.Value : row.FechaVisita, row.Grado, row.DiagnosticoPreliminar, row.MontoGestion,
                        row.ReservaEstimada, row.Hospitalarios, row.Cirugia, row.Ambulancias, row.Laboratorios, row.Farmacias, row.GastosHospitalarios, row.GastosCirugia, row.GastosAmbulancias, row.GastosLaboratorios, row.GastosFarmacias,
                        row.GastosHonorarios, row.GastosAmbulatorio, row.GastosOsteosintesis, row.GastosReembolso, row.Honorarios, row.Ambulatorio,
                        row.Osteosintesis, row.GastosReembolso, row.Estado,
                        row.Acuerdo, row.Rechazado, row.Observaciones, row.TieneAdjuntos));
                    i++;
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while searching Siniestro data for list", ex);
                throw;
            }
            return TheSiniestroList;
        }

        public static bool ValidateSiniestro ( DateTime FechaSiniestro, string OperacionId, string LugarDpto )
        {
            try
            {
                SiniestroDSTableAdapters.SiniestroTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroTableAdapter();
                return (bool)theAdapter.ValidateSiniestro(FechaSiniestro, OperacionId, LugarDpto);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while validating Siniestro data for insert", ex);
                throw;
            }
        }

        public static List<DocumentFile> getFileList ( int SiniestroId )
        {

            List<DocumentFile> theList = new List<DocumentFile>();
            try
            {
                SiniestroDSTableAdapters.SiniestroFileTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroFileTableAdapter();
                SiniestroDS.SiniestroFileDataTable theTable = theAdapter.GetFile(SiniestroId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (SiniestroDS.SiniestroFileRow row in theTable.Rows)
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
                log.Error("An error was ocurred while geting Siniestro file list", ex);
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
                SiniestroDSTableAdapters.SiniestroFileTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroFileTableAdapter();
                theAdapter.Delete(FileId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Siniestro File", ex);
                throw;
            }
        }
        public static void InsertFile ( int SiniestroId, int FileId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (FileId <= 0)
                throw new ArgumentException("FileId cannot be less than or equal to zero.");

            try
            {
                SiniestroDSTableAdapters.SiniestroFileTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroFileTableAdapter();
                theAdapter.InsertFile(FileId, SiniestroId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting Siniestro File", ex);
                throw;
            }
        }

        public static List<DocumentFile> GetSiniestroFiles(int siniestroId  )
        {
            /*
            if (siniestroId <= 0)
            {
                //throw new ArgumentException("siniestroId cannot be equals or less than zero");
            }*/

            SiniestroFilesDSTableAdapters.SiniestroFilesTableAdapter adapter = new SiniestroFilesDSTableAdapters.SiniestroFilesTableAdapter();
            SiniestroFilesDS.SiniestroFilesDataTable table = adapter.GetFilesBySiniestroId(siniestroId);

            List<DocumentFile> list = new List<DocumentFile>();

            if (siniestroId > 0)
            {
                foreach (var row in table)
                {
                    DocumentFile obj = DocumentFile.CreateNewTypedDocumentFileObject(
                        row.fileId,
                        row.dateUploaded,
                        row.fileSize,
                        row.fileName,
                        row.extension,
                        row.fileStoragePath);
                    list.Add(obj);
                }
            }
            return list;
        }

        public static SiniestroStat GetSiniestroStatbySiniestroId(int SiniestroId)
        {

            SiniestroStat stat = new SiniestroStat();
            try
            {
                
                SiniestroDSTableAdapters.SiniestroStatTableAdapter theAdapter = new SiniestroDSTableAdapters.SiniestroStatTableAdapter();
                //return (bool)theAdapter.ValidateSiniestro(FechaSiniestro, OperacionId, LugarDpto);
                SiniestroDS.SiniestroStatDataTable table = theAdapter.GetSiniestroStat(SiniestroId);

                if (table != null && table.Count > 0)
                {
                    stat.SiniestroId = table[0].SiniestroId;
                    stat.CantHeridos = table[0].CantidadHeridos;
                    stat.CantIlesos = table[0].CantidadIlesos;
                    stat.CantFallecidos = table[0].CantidadFallecidos;
                }

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while validating Siniestro data for insert", ex);
                throw;
            }
            return stat;
        }
    }
}