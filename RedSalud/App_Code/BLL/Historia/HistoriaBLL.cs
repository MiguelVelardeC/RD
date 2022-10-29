using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Historia.BLL
{
    public class HistoriaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public HistoriaBLL() {}

        private static Historia FillRecord(HistoriaDS.HistoriaRow row)
        {
            Historia objHistoria = new Historia(
                row.HistoriaId
                , row.PacienteId
                , row.CasoId
                , row.MotivoConsulta
                , row.IsProtocoloIdNull() ? 0 : row.ProtocoloId
                //, row.Diagnostico
                , row.IsEnfermedadIdNull() ? "" : row.EnfermedadId
                , row.IsEnfermedadNull() ? "" : row.Enfermedad
                , row.DiagnosticoPresuntivo
                //, row.Tratamiento
                , row.IsPresionArterialNull() ? "" : row.PresionArterial
                , row.IsPulsoNull() ? "" : row.Pulso
                , row.IsTemperaturaNull() ? "" : row.Temperatura
                , row.IsFrecuenciaCardiacaNull() ? "" : row.FrecuenciaCardiaca
                ,row.IsExFisicoRegionalyDeSistemaNull()? "":row.ExFisicoRegionalyDeSistema
                );

            return objHistoria;
        }


        public static Historia GetHistoriaByHistoriaId(int HistoriaId)
        {
            if (HistoriaId <= 0)
                throw new ArgumentException("HistoriaId cannot be less than or equal to zero.");

            Historia TheHistoria = null;
            try
            {
                HistoriaDSTableAdapters.HistoriaTableAdapter theAdapter = new HistoriaDSTableAdapters.HistoriaTableAdapter();
                HistoriaDS.HistoriaDataTable theTable = theAdapter.GetHistoriaByHistoriaId(HistoriaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    HistoriaDS.HistoriaRow row = theTable[0];
                    TheHistoria = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Historia data By HistoriaId", ex);
                throw;
            }
            return TheHistoria;
        }

        public static List<Historia> getHistoriaListByCasoId(int CasoId)
        {
            if (CasoId <= 0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");

            List<Historia> theList = new List<Historia>();
            Historia theHistoria = null;
            try
            {
                HistoriaDSTableAdapters.HistoriaTableAdapter theAdapter = new HistoriaDSTableAdapters.HistoriaTableAdapter();
                HistoriaDS.HistoriaDataTable theTable = theAdapter.GetHistoriaByCasoId(CasoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (HistoriaDS.HistoriaRow row in theTable.Rows)
                    {
                        theHistoria = FillRecord(row);
                        theList.Add(theHistoria);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Historia by CasoId", ex);
                throw;
            }
            return theList;
        }

        public static List<Historia> getHistoriaListByPacienteId(int PacienteId)
        {
            if (PacienteId <= 0)
                throw new ArgumentException("PacienteId cannot be less than or equal to zero.");

            List<Historia> theList = new List<Historia>();
            Historia theHistoria = null;
            try
            {
                HistoriaDSTableAdapters.HistoriaTableAdapter theAdapter = new HistoriaDSTableAdapters.HistoriaTableAdapter();
                HistoriaDS.HistoriaDataTable theTable = theAdapter.GetHistoriaByPacienteId(PacienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (HistoriaDS.HistoriaRow row in theTable.Rows)
                    {
                        theHistoria = FillRecord(row);
                        theList.Add(theHistoria);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Historia By PacienteId", ex);
                throw;
            }
            return theList;
        }
    }
}