using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User.BLL;
using log4net;
using Artexacta.App.Utilities.SystemMessages;

namespace Artexacta.App.RedClientePrestaciones.BLL
{
    /// <summary>
    /// Summary description for RedClientePrestacionesBLL
    /// </summary>
    public class RedClientePrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedClientePrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedClientePrestaciones FillRecord(RedClientePrestacionesDS.RedClientePrestacionesRow row)
        {
            RedClientePrestaciones theNewRecord = new RedClientePrestaciones(
                row.preId
                , row.prestacion
                , row.preTipoPrestacion
                , row.ClienteId
                , row.IsprePrecioNull() ? 0 : row.prePrecio
                , row.IspreCoPagoMontoNull() ? 0 : row.preCoPagoMonto
                , row.IspreCoPagoPorcentajeNull() ? 0 : row.preCoPagoPorcentaje
                , row.IspreCantidadConsultasAnoNull() ? 0 : row.preCantidadConsultasAno
                , row.IspreDiasCarenciaNull() ? 0 : row.preDiasCarencia
                , row.IspreMontoTopeNull() ? 0 : row.preMontoTope);

            return theNewRecord;
        }

        public static List<RedClientePrestaciones> GetAllClientePrestaciones(int ClienteId)
        {
            List<RedClientePrestaciones> theList = new List<RedClientePrestaciones>();
            RedClientePrestaciones theRedClientePrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter();
                RedClientePrestacionesDS.RedClientePrestacionesDataTable theTable = theAdapter.GetAllClientePrestaciones(ClienteId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedClientePrestacionesRow row in theTable.Rows)
                    {
                        theRedClientePrestaciones = FillRecord(row);
                        theList.Add(theRedClientePrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedClientePrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static List<RedClientePrestaciones> GetAllClientePrestacionesXClienteId(int ClienteId)
        {
           
            List<RedClientePrestaciones> theList = new List<RedClientePrestaciones>();
            List<RedClientePrestaciones> theListResults = new List<RedClientePrestaciones>();
            theList = GetAllClientePrestaciones(ClienteId);
            for (int i=0;i<theList.Count;i++)
            {
                if (theList[i].ClienteId==ClienteId)
                {
                    RedClientePrestaciones theRedClientePrestaciones = null;
                    theRedClientePrestaciones = theList[i];
                    theListResults.Add(theRedClientePrestaciones);
                }
            }
            return theListResults;
        }

        public static int UpdateClientePrestaciones(int preId, string preTipoPrestacion,
            int ClienteId, decimal? prePrecio, decimal? preCoPagoMonto, decimal? preCoPagoPorcentaje,
            int? preCantidadConsultasAno, int? preDiasCarencia, decimal? preMontoTope)
        {
            int? newPreId = preId;
            if (string.IsNullOrEmpty(preTipoPrestacion))
                throw new ArgumentException("TipoPrestacion cannot be null or empty.");
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");

          

            try
            {
                if (preId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter();
                    theAdapter.InsertClientePrestaciones(ref newPreId, preTipoPrestacion,
                            ClienteId, prePrecio.Value, preCoPagoMonto, preCoPagoPorcentaje,
                            preCantidadConsultasAno, preDiasCarencia, preMontoTope);
                } else
                {
                    RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter();
                    theAdapter.UpdateClientePrestaciones(preId, preTipoPrestacion,
                            ClienteId, prePrecio, preCoPagoMonto, preCoPagoPorcentaje,
                            preCantidadConsultasAno, preDiasCarencia, preMontoTope);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating ClientePrestaciones", ex);
                throw;
            }
            return (newPreId ?? 0);
        }

        public static bool DeleteRedClientePrestaciones(int preId)
        {
            if (preId <= 0)
                throw new ArgumentException("preId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedClientePrestacionesTableAdapter();
                theAdapter.DeleteClientePrestaciones(preId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting RedClientePrestaciones", ex);
                throw;
            }
        }
    }

    public class RedEspecialidadPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedEspecialidadPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedEspecialidadPrestaciones FillRecord(RedClientePrestacionesDS.RedEspecialidadesPrestacionesRow row)
        {
            RedEspecialidadPrestaciones theNewRecord = new RedEspecialidadPrestaciones(
                row.detId
                , row.EspecialidadId
                , row.Especialidad
                , row.ClienteId
                , row.CantidadVideoLLamadas
                , row.FrecuenciaVideoLLamadas
                , row.DescripcionCantFrecuencia);

            return theNewRecord;
        }

        public static List<RedEspecialidadPrestaciones> GetAllEspecialidadPrestaciones(int ClienteId)
        {
            List<RedEspecialidadPrestaciones> theList = new List<RedEspecialidadPrestaciones>();
            RedEspecialidadPrestaciones theRedEspecialidadPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedEspecialidadesPrestacionesDataTable theTable = theAdapter.GetAllEspecialidadesPrestaciones(ClienteId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedEspecialidadesPrestacionesRow row in theTable.Rows)
                    {
                        theRedEspecialidadPrestaciones = FillRecord(row);
                        theList.Add(theRedEspecialidadPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedEspecialidadPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static List<RedEspecialidadPrestaciones> GetEspecialidadPrestacionesNotSaved(int ClienteId, bool NotSaved)
        {
            List<RedEspecialidadPrestaciones> theList = new List<RedEspecialidadPrestaciones>();
            RedEspecialidadPrestaciones theRedEspecialidadPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedEspecialidadesPrestacionesDataTable theTable = theAdapter.GetEspecialidadesPrestacionesNotSaved(ClienteId, NotSaved);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedEspecialidadesPrestacionesRow row in theTable.Rows)
                    {
                        theRedEspecialidadPrestaciones = FillRecord(row);
                        theList.Add(theRedEspecialidadPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedEspecialidadPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static int UpdateEspecialidadPrestaciones(int detId, int EspecialidadId,
            int ClienteId, string Frecuencia, int cantidad)
        {
            int? newDetId = detId;
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                    theAdapter.InsertEspecialidadesPrestaciones(ref newDetId, EspecialidadId,
                            ClienteId,Frecuencia,cantidad);
                }
                else
                {
                    RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                    theAdapter.UpdateEspecialidadesPrestaciones(detId, EspecialidadId,
                            ClienteId,Frecuencia,cantidad);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating EspecialidadesPrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteRedEspecialidadPrestaciones(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                theAdapter.DeleteEspecialidadesPrestaciones(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting EspecialidadPrestaciones", ex);
                throw;
            }
        }

        public static List<RedEspecialidadPrestaciones> GetAllEspecialidadPrestacionesxCliente(int ClienteId)
        {
            List<RedEspecialidadPrestaciones> theList = new List<RedEspecialidadPrestaciones>();
            List<RedEspecialidadPrestaciones> theListResult = new List<RedEspecialidadPrestaciones>();
            RedEspecialidadPrestaciones theRedEspecialidadPrestaciones = null;
            
            try
            {
                RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedEspecialidadesPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedEspecialidadesPrestacionesDataTable theTable = theAdapter.GetAllEspecialidadesPrestaciones(ClienteId);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedEspecialidadesPrestacionesRow row in theTable.Rows)
                    {
                        theRedEspecialidadPrestaciones = FillRecord(row);
                        theList.Add(theRedEspecialidadPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedEspecialidadPrestaciones", ex);
                throw;
            }
            for (int i=0;i<theList.Count();i++)
            {
                if (theList[i].ClienteId!=0)
                {
                    theListResult.Add(theList[i]);
                }
            }
            return theListResult;
        }
    }

    public class RedGruposLabPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedGruposLabPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedGruposLabPrestaciones FillRecord(RedClientePrestacionesDS.RedGruposLabPrestacionesRow row)
        {
            RedGruposLabPrestaciones theNewRecord = new RedGruposLabPrestaciones(
                row.detId
                , row.EstudioId
                , row.Estudio);

            return theNewRecord;
        }

        public static List<RedGruposLabPrestaciones> GetGruposLabPrestacionesNotSaved(int ClienteId, bool NotSaved)
        {
            List<RedGruposLabPrestaciones> theList = new List<RedGruposLabPrestaciones>();
            RedGruposLabPrestaciones theRedGruposLabPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedGruposLabPrestacionesDataTable theTable = theAdapter.GetGruposLabPrestacionesNotSaved(ClienteId, NotSaved);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedGruposLabPrestacionesRow row in theTable.Rows)
                    {
                        theRedGruposLabPrestaciones = FillRecord(row);
                        theList.Add(theRedGruposLabPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedGruposLabPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static int UpdateGruposLabPrestaciones(int detId, int EstudioId,
            int ClienteId)
        {
            int? newDetId = detId;
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter();
                    theAdapter.InsertGruposLabPrestaciones(ref newDetId, EstudioId,
                            ClienteId);
                }
                else
                {
                    RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter();
                    theAdapter.UpdateGruposLabPrestaciones(detId, EstudioId,
                            ClienteId);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating GruposLabPrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteRedGruposLabPrestaciones(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedGruposLabPrestacionesTableAdapter();
                theAdapter.DeleteGruposLabPrestaciones(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting GruposLabPrestaciones", ex);
                throw;
            }
        }
    }

    public class RedImagenPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedImagenPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedImagenPrestaciones FillRecord(RedClientePrestacionesDS.RedImagenPrestacionesRow row)
        {
            RedImagenPrestaciones theNewRecord = new RedImagenPrestaciones(
                row.detId
                , row.EstudioId
                , row.Estudio);

            return theNewRecord;
        }

        public static List<RedImagenPrestaciones> GetImagenPrestacionesNotSaved(int ClienteId, bool NotSaved)
        {
            List<RedImagenPrestaciones> theList = new List<RedImagenPrestaciones>();
            RedImagenPrestaciones theRedImagenPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedImagenPrestacionesDataTable theTable = theAdapter.GetImagenPrestacionesNotSaved(ClienteId, NotSaved);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedImagenPrestacionesRow row in theTable.Rows)
                    {
                        theRedImagenPrestaciones = FillRecord(row);
                        theList.Add(theRedImagenPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedImagenPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static int UpdateImagenPrestaciones(int detId, int EstudioId,
            int ClienteId)
        {
            int? newDetId = detId;
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter();
                    theAdapter.InsertImagenPrestaciones(ref newDetId, EstudioId,
                            ClienteId);
                }
                else
                {
                    RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter();
                    theAdapter.UpdateImagenPrestaciones(detId, EstudioId,
                            ClienteId);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating ImagenPrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteImagenPrestaciones(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedImagenPrestacionesTableAdapter();
                theAdapter.DeleteImagenPrestaciones(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting ImagenPrestaciones", ex);
                throw;
            }
        }
    }

    public class RedCirugiasPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedCirugiasPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedCirugiasPrestaciones FillRecord(RedClientePrestacionesDS.RedCirugiasPrestacionesRow row)
        {
            RedCirugiasPrestaciones theNewRecord = new RedCirugiasPrestaciones(
                row.detId
                , row.CodigoArancelarioId
                , row.CodigoArancelario
                , row.detMontoTope
                , row.detCantidadTope);

            return theNewRecord;
        }

        public static List<RedCirugiasPrestaciones> GetCirugiasPrestacionesNotSaved(int ClienteId, bool NotSaved)
        {
            List<RedCirugiasPrestaciones> theList = new List<RedCirugiasPrestaciones>();
            RedCirugiasPrestaciones theRedCirugiasPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedCirugiasPrestacionesDataTable theTable = theAdapter.GetCirugiasPrestacionesNotSaved(ClienteId, NotSaved);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedCirugiasPrestacionesRow row in theTable.Rows)
                    {
                        theRedCirugiasPrestaciones = FillRecord(row);
                        theList.Add(theRedCirugiasPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedCirugiasPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static int UpdateCirugiasPrestaciones(int detId, string CodigoArancelarioId,
            int ClienteId, decimal detMontoTope, int detCantidadTope)
        {
            int? newDetId = detId;
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");
            //if (detMontoTope <= 0)
            //    throw new ArgumentException("detMontoTope cannot be null or empty.");
            //if (detCantidadTope <= 0)
            //    throw new ArgumentException("detCantidadTope cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter();
                    theAdapter.InsertCirugiasPrestaciones(ref newDetId, CodigoArancelarioId,
                            ClienteId, detMontoTope, detCantidadTope);
                }
                else
                {
                    RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter();
                    theAdapter.UpdateCirugiasPrestaciones(detId, CodigoArancelarioId,
                            ClienteId, detMontoTope, detCantidadTope);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating CirugiasPrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteCirugiasPrestaciones(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedCirugiasPrestacionesTableAdapter();
                theAdapter.DeleteCirugiasPrestaciones(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting CirugiasPrestaciones", ex);
                throw;
            }
        }
    }
    public class RedOdontoPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedOdontoPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedOdontoPrestaciones FillRecord(RedClientePrestacionesDS.RedOdontoPrestacionesRow row)
        {
            RedOdontoPrestaciones theNewRecord = new RedOdontoPrestaciones(
                row.detId
                , row.PrestacionOdontologicaId
                , row.PrestacionOdontologica
                , row.detCantidadConsultasAno);

            return theNewRecord;
        }

        public static List<RedOdontoPrestaciones> GetOdontoPrestacionesNotSaved(int ClienteId, bool NotSaved)
        {
            List<RedOdontoPrestaciones> theList = new List<RedOdontoPrestaciones>();
            RedOdontoPrestaciones theRedOdontoPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedOdontoPrestacionesDataTable theTable = theAdapter.GetOdontoPrestacionesNotSaved(ClienteId, NotSaved);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedOdontoPrestacionesRow row in theTable.Rows)
                    {
                        theRedOdontoPrestaciones = FillRecord(row);
                        theList.Add(theRedOdontoPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedOdontoPrestaciones", ex);
                throw;
            }
            return theList;
        }

        public static int UpdateOdontoPrestaciones(int detId, int PrestacionOdontologicaId,
            int ClienteId, int detCantidadConsultasAno)
        {
            int? newDetId = detId;
            if (ClienteId <= 0)
                throw new ArgumentException("ClienteId cannot be null or empty.");
            //if (detCantidadConsultasAno <= 0)
            //    throw new ArgumentException("detCantidadConsultasAno cannot be null or empty.");

            try
            {
                if (detId <= 0)
                {
                    RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter();
                    theAdapter.InsertOdontoPrestaciones(ref newDetId, PrestacionOdontologicaId,
                            ClienteId, detCantidadConsultasAno);
                }
                else
                {
                    RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter();
                    theAdapter.UpdateOdontoPrestaciones(detId, PrestacionOdontologicaId,
                            ClienteId, detCantidadConsultasAno);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Inserting/Updating OdontoPrestaciones", ex);
                throw;
            }
            return (newDetId ?? 0);
        }

        public static bool DeleteOdontoPrestaciones(int detId)
        {
            if (detId <= 0)
                throw new ArgumentException("detId cannot be less than or equal to zero.");
            try
            {
                RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedOdontoPrestacionesTableAdapter();
                theAdapter.DeleteOdontoPrestaciones(detId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while Deleting OdontoPrestaciones", ex);
                throw;
            }
        }
    }

    public class RedTipoPrestacionesBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public RedTipoPrestacionesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static RedTipoPrestaciones FillRecord(RedClientePrestacionesDS.RedTipoPrestacionesRow row)
        {
            RedTipoPrestaciones theNewRecord = new RedTipoPrestaciones(
                row.id
                , row.prestacion);

            return theNewRecord;
        }

        public static List<RedTipoPrestaciones> GetAllTipoPrestaciones()
        {
            List<RedTipoPrestaciones> theList = new List<RedTipoPrestaciones>();
            RedTipoPrestaciones theRedTipoPrestaciones = null;
            try
            {
                RedClientePrestacionesDSTableAdapters.RedTipoPrestacionesTableAdapter theAdapter = new RedClientePrestacionesDSTableAdapters.RedTipoPrestacionesTableAdapter();
                RedClientePrestacionesDS.RedTipoPrestacionesDataTable theTable = theAdapter.GetAllTipoPrestaciones();

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (RedClientePrestacionesDS.RedTipoPrestacionesRow row in theTable.Rows)
                    {
                        theRedTipoPrestaciones = FillRecord(row);
                        theList.Add(theRedTipoPrestaciones);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list RedTipoPrestaciones", ex);
                throw;
            }
            return theList;
        }
        
    }
}