using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Plan.BLL
{
    /// <summary>
    /// Summary description for PlanBLL
    /// </summary>
    public class PlanBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public PlanBLL ()
        {
        }

        private static Plan FillRecord(PlanDS.PlanRow row)
        {
            Plan objPlan = new Plan(
                row.Nombre
                ,row.TipoEstudioId
                ,row.Cantidad
                ,row.CantidadActual);

            return objPlan;
        }

        public static List<Plan> getPlanUseForAsegurado ( int AseguradoId )
        {
            if (AseguradoId <= 0)
                throw new ArgumentException("AseguradoId cannot be less than or equal to zero.");
            List<Plan> listPlan = new List<Plan>();
            try
            {
                PlanDSTableAdapters.PlanTableAdapter theAdapter = new PlanDSTableAdapters.PlanTableAdapter();
                PlanDS.PlanDataTable theTable = theAdapter.GetPlanUseForAsegurado(AseguradoId);
                foreach (PlanDS.PlanRow row in theTable.Rows)
                {
                    listPlan.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Plan by AseguradoId", ex);
                throw;
            }
            return listPlan;
        }
    }
}