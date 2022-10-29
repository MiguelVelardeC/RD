using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.MedicamentoLINAME.BLL
{
    public class MedicamentoLINAMEBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public MedicamentoLINAMEBLL () { }

        private static MedicamentoLINAME FillRecord ( MedicamentoLINAMEDS.MedicamentoLINAMERow row )
        {
            MedicamentoLINAME objMedicamento = new MedicamentoLINAME(
                row.MedicamentoLINAMEId
                ,row.MedicamentoGrupoId
                ,row.MedicamentoSubgrupoId
                ,row.Nombre
                ,row.NombreGrupo
                ,row.NombreSubgrupo);

            return objMedicamento;
        }

        public static int InsertMedicamento(int CasoId, int MedicamentoCLAId, string TipoMedicamentoId
            , string Indicaciones, DateTime FechaCreacion)
        {
            if(CasoId<=0)
                throw new ArgumentException("CasoId cannot be less than or equal to zero.");
            if (MedicamentoCLAId<=0)
                throw new ArgumentException("MedicamentoCLAId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TipoMedicamentoId))
                throw new ArgumentException("TipoMedicamentoId cannot be null or empty.");
            if (string.IsNullOrEmpty(Indicaciones))
                throw new ArgumentException("Indicaciones cannot be null or empty.");

            int? DetalleId = 0;
            
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();

                theAdapter.Insert(ref DetalleId, CasoId, MedicamentoCLAId
                    ,TipoMedicamentoId,Indicaciones,FechaCreacion);

                if (DetalleId == null || DetalleId <= 0)
                {
                    throw new Exception("SQL insertó el registro exitosamente pero retornó un status null <= 0");
                }
                return (int)DetalleId;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while inserting Caso", q);
                throw;
            }
        }

        public static MedicamentoLINAME GetMedicamentoLINAMEById ( int MedicamentoId )
        {
            if (MedicamentoId <= 0)
                throw new ArgumentException("MedicamentoId cannot be less than or equal to zero.");

            try
            {
                MedicamentoLINAMEDSTableAdapters.MedicamentoLINAMETableAdapter theAdapter = new MedicamentoLINAMEDSTableAdapters.MedicamentoLINAMETableAdapter();
                MedicamentoLINAMEDS.MedicamentoLINAMEDataTable theTable = theAdapter.GetMedicamentoLINAMEByID(MedicamentoId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    return FillRecord(theTable[0]);
                }
                return null;
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting MedicamentoLINAME", q);
                throw;
            }
        }

        public static int SearchMedicamentoLINAME ( ref List<MedicamentoLINAME> theList, int pageSize, int firstRow, string search )
        {
            try
            {
                int? _totalRows = 0;
                MedicamentoLINAMEDSTableAdapters.MedicamentoLINAMETableAdapter theAdapter = new MedicamentoLINAMEDSTableAdapters.MedicamentoLINAMETableAdapter();
                MedicamentoLINAMEDS.MedicamentoLINAMEDataTable theTable = theAdapter.SearchMedicamentosLINAME(search, pageSize, firstRow, ref _totalRows);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (MedicamentoLINAMEDS.MedicamentoLINAMERow row in theTable.Rows)
                    {
                        MedicamentoLINAME objMedicamento = FillRecord(row);
                        theList.Add(objMedicamento);
                    }
                    return (int)_totalRows;
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting MedicamentoDetails", q);
                throw;
            }
            return 0;
        }

        public static bool DeleteMedicamento(int DetalleId)
        {
            if (DetalleId <= 0)
                throw new ArgumentException("DetalleId the Medicamento cannot be less than or equal to zero.");
            try
            {
                MedicamentosDSTableAdapters.MedicamentoTableAdapter theAdapter = new MedicamentosDSTableAdapters.MedicamentoTableAdapter();
                theAdapter.Delete(DetalleId);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting Medicamento", ex);
                throw;
            }
        }
    }
}