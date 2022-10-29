using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for FinancieraBLL
    /// </summary>
    public class FinancieraBLL
    {
        public FinancieraBLL()
        {

        }

        public static List<Financiera> GetFinancieras(int clienteId, string varCiudad)
        {
            
            List<Financiera> list = new List<Financiera>();
            string parameterCiudad = "0";
            /*
            if (string.IsNullOrEmpty(varCiudad) || varCiudad == "0")
                parameterCiudad = "0";
            else
                parameterCiudad = varCiudad;
            */

            PropuestoAseguradoDSTableAdapters.FinancieraTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.FinancieraTableAdapter();
            PropuestoAseguradoDS.FinancieraDataTable table = adapter.GetFinancieras(clienteId, parameterCiudad);

            foreach (PropuestoAseguradoDS.FinancieraRow row in table)
            {
                Financiera obj = new Financiera()
                {
                    FinancieraId = row.financieraId,
                    Nombre = row.nombre,
                    Nit = row.nit,
                    CentralCiudadId = row.centralCiudadId,
                    ClienteId = row.clienteId
                };
                list.Add(obj);
            }            
            return list;
        }
    }
}