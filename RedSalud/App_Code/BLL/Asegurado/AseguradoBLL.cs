using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.Asegurado.BLL
{
    /// <summary>
    /// Summary description for CiudadBLL
    /// </summary>
    public class AseguradoBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        
        private static Asegurado FillRecord ( AseguradoDS.AseguradosRow row )
        {
            Asegurado objCiudad = new Asegurado(
                row.AseguradoId
                ,row.Aseguradora
                , row.IsAseguradoNull() ? "" : row.Asegurado
                ,row.NumeroPoliza
                ,row.MontoTotal
                );
            return objCiudad;
        }

        public static Asegurado GetAseguradoById ( int AseguradoraId, string CodigoAsegurado, string NumeroPoliza )
        {
            if (AseguradoraId <= 0)
                throw new ArgumentException("AseguradoraId no puede ser <= 0.");
            if (string.IsNullOrWhiteSpace(CodigoAsegurado))
                throw new ArgumentException("CodigoAsegurado no puede ser nulo o vacio.");
            if (string.IsNullOrWhiteSpace(NumeroPoliza))
                throw new ArgumentException("NumeroPoliza no puede ser nulo o vacio.");

            Asegurado TheAsegurado = null;
            try
            {
                AseguradoDSTableAdapters.AseguradosTableAdapter theAdapter = new AseguradoDSTableAdapters.AseguradosTableAdapter();
                AseguradoDS.AseguradosDataTable theTable = theAdapter.GetAseguradoById(AseguradoraId, CodigoAsegurado, NumeroPoliza);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    AseguradoDS.AseguradosRow row = theTable[0];
                    TheAsegurado = FillRecord(row);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Asegurado data", ex);
                throw;
            }
            return TheAsegurado;
        }

        public static List<Asegurado> GetAllAsegurado()
        {
            List<Asegurado> asegurados = new List<Asegurado>();
            try
            {
                AseguradoDSTableAdapters.AseguradosTableAdapter theAdapter = new AseguradoDSTableAdapters.AseguradosTableAdapter();
                AseguradoDS.AseguradosDataTable theTable = theAdapter.GetAseguradoAll();
                foreach (AseguradoDS.AseguradosRow row in theTable.Rows)
                {
                    asegurados.Add(FillRecord(row));
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Asegurados list", ex);
                throw;
            }
            return asegurados;
        }
   }
}