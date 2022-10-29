using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Gasto.BLL
{
    /// <summary>
    /// Summary description for GastoBLL
    /// </summary>
    public class GastoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public GastoBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int InsertNewGastoDerivacion(DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int DerivacionId)
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (DerivacionId <= 0)
                throw new ArgumentException("DerivacionId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpDerivacion(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, DerivacionId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Derivacion", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertNewGastoEmergencia(DateTime FechaCreacion, DateTime FechaGasto
        , decimal Monto, int NroFacturaRecibo, string TipoDocumento
        , int FileId, decimal RetencionImpuesto, int EmergenciaId)
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (EmergenciaId <= 0)
                throw new ArgumentException("EmergenciaId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpEmergencia(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, EmergenciaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Emergencia", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertNewGastoEstudio(DateTime FechaCreacion, DateTime FechaGasto
                ,decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int EstudioId)
        {
            if (Monto<=0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto<=0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (EstudioId <= 0)
                throw new ArgumentException("EstudioId cannot be less than or equal to zero.");

            int? GastoId=0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpEstudio(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, EstudioId);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Estudio", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertNewGastoInternacion(DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int InternacionId)
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (InternacionId <= 0)
                throw new ArgumentException("InternacionId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpInternacion(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, InternacionId);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Internacion", ex);
                throw;
            }
            return (int)GastoId;
        }
        public static int InsertNewGastoOdontologia ( DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int OdontologiaId )
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (OdontologiaId <= 0)
                throw new ArgumentException("OdontologiaId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpOdontologia(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, OdontologiaId);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Odontologia", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertNewGastoReceta(DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int RecetaId)
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (RecetaId <= 0)
                throw new ArgumentException("RecetaId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpReceta(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, RecetaId);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Receta", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertNewGastoMedicamento ( DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto, int MedicamentoId )
        {
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be less than or equal to zero.");

            int? GastoId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoAndGDetAndUpMedicamento(ref GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto, MedicamentoId);

            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new Gasto Receta", ex);
                throw;
            }
            return (int)GastoId;
        }

        public static int InsertGastoDetalle(int GastoId, DateTime FechaCreacion, DateTime FechaGasto
                , decimal Monto, int NroFacturaRecibo, string TipoDocumento
                , int FileId, decimal RetencionImpuesto)
        {
            if (GastoId <= 0)
                throw new ArgumentException("GastoId cannot be less than or equal to zero.");
            if (Monto <= 0)
                throw new ArgumentException("Monto cannot be less than or equal to zero.");
            if (NroFacturaRecibo <= 0)
                throw new ArgumentException("NroFacturaRecibo cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoDocumento))
                throw new ArgumentException("TipoDocumento cannot be null or empty");
            if (RetencionImpuesto <= 0)
                throw new ArgumentException("RetencionImpuesto cannot be less than or equal to zero.");

            int? GastoDetalleId = 0;
            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.InsertGastoDetalle(ref GastoDetalleId, GastoId, FechaCreacion, FechaGasto, Monto
                    , NroFacturaRecibo, TipoDocumento, FileId, RetencionImpuesto);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting new GastoDetalle", ex);
                throw;
            }
            return (int)GastoDetalleId;
        }

        public static void BlockUnlock ( int GastoId, bool block )
        {
            if (GastoId <= 0)
                throw new ArgumentException("GastoId cannot be less than or equal to zero.");

            try
            {
                GastoDSTableAdapters.GastoTableAdapter theAdapter = new GastoDSTableAdapters.GastoTableAdapter();
                theAdapter.BlockUnlock(GastoId, block);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Blocking / Unlocking Gasto", ex);
                throw;
            }
        }
    }
}