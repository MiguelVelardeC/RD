using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.Estudio.BLL;
using Artexacta.App.Internacion.BLL;
using log4net;

namespace Artexacta.App.Caso.CasoForAprobation.BLL
{
    /// <summary>
    /// Summary description for CasoForAprobationBLL
    /// </summary>
    public class CasoForAprobationBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public CasoForAprobationBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static CasoForAprobation FillRecord(CasoForAprobationDS.CasoForAprobationRow row)
        {
            CasoForAprobation objCasoForAprobation = new CasoForAprobation(
                row.CasoId
                , row.Id
                , row.TipoEstudio
                , row.NombreProveedor
                , row.Observacion
                , Configuration.Configuration.ConvertToClientTimeZone(row.FechaCreacion)
                , row.TABLE
                , row.IsFileCountNull() ? 0 : row.FileCount);
            return objCasoForAprobation;
        }

        public static List<CasoForAprobation> GetCasoListForAprobation ( int CasoId )
        {
            return GetCasoListForAprobation(CasoId, false);
        }

        public static List<CasoForAprobation> GetCasoListForAprobation ( int CasoId, bool IsFileVisible )
        {
            if (CasoId < 0)
                throw new ArgumentException("CasoId Cannot be minor zero.");

            List<CasoForAprobation> theList = new List<CasoForAprobation>();
            CasoForAprobation objCasoForAprobation = null;
            try
            {
                CasoForAprobationDSTableAdapters.CasoForAprobationTableAdapter theAdapter = new CasoForAprobationDSTableAdapters.CasoForAprobationTableAdapter();
                CasoForAprobationDS.CasoForAprobationDataTable theTable =theAdapter.GetCasoListForAprobation(CasoId, IsFileVisible);

                if (theTable != null && theTable.Rows.Count > 0)
                { 
                    foreach(CasoForAprobationDS.CasoForAprobationRow row in theTable.Rows)
                    {
                        objCasoForAprobation = FillRecord(row);
                        theList.Add(objCasoForAprobation);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list CasoForAprobation", ex);
                throw;
            }
            return theList;
        }

        public static bool AproveCaso(int Id, int AprobacionUserId, DateTime FechaAprobacion, string TableName)
        {
            if (Id <= 0)
                throw new ArgumentException("Id cannot be less than or equal to zero.");
            if (AprobacionUserId <= 0)
                throw new ArgumentException("AprobacionUserId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(TableName))
                throw new ArgumentException("TableName cannot be null or empty.");

            bool Aprove = false;
            try
            {
                switch (TableName)
                {
                    case "tbl_Derivacion":
                        Aprove= DerivacionBLL.AproveDerivacion(Id,AprobacionUserId,FechaAprobacion);
                        break;
                    case "tbl_Estudio":
                        Aprove = EstudioBLL.AproveEstudio(Id, AprobacionUserId, FechaAprobacion);
                        break;
                    case "tbl_Internacion":
                        Aprove = InternacionBLL.AproveInternacion(Id, AprobacionUserId, FechaAprobacion);
                        break;
                    default:
                        //enviar mensaje de que el nombre de la tabla no es correcto
                        throw new ArgumentException("TableName not valid");
                }
                //if (TableName.Equals("tbl_Derivacion"))
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while aprove caso medico", ex);
                throw;
            }
            return Aprove;
        }

        public static List<CasoForAprobation> GetCasoListAprobated ( int CasoId )
        {
            return GetCasoListAprobated(CasoId, false);
        }

        public static List<CasoForAprobation> GetCasoListAprobated ( int CasoId, bool IsFileVisible )
        {
            if (CasoId < 0)
                throw new ArgumentException("CasoId Cannot be minor zero.");

            List<CasoForAprobation> theList = new List<CasoForAprobation>();
            CasoForAprobation objCasoForAprobation = null;
            try
            {
                CasoForAprobationDSTableAdapters.CasoForAprobationTableAdapter theAdapter = new CasoForAprobationDSTableAdapters.CasoForAprobationTableAdapter();
                CasoForAprobationDS.CasoForAprobationDataTable theTable = theAdapter.GetCasoListAprobatedByCasoId(CasoId, IsFileVisible);

                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CasoForAprobationDS.CasoForAprobationRow row in theTable.Rows)
                    {
                        objCasoForAprobation = FillRecord(row);
                        theList.Add(objCasoForAprobation);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list CasoForAprobation", ex);
                throw;
            }
            return theList;
        }

    }
}