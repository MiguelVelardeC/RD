using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for HoraLibreBLL
    /// </summary>
    public class HoraLibreBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public HoraLibreBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static HoraLibre FillRecord(HoraLibreDS.HoraLibreMedicoRow row)
        {
            if (row.IshorarioNull())
                return null;
            HoraLibre objHoraLibre = new HoraLibre(row.horario, row.medicoDesgravamenID, row.nombreMedico, row.proveedorMedicoId, 
                row.nombreLugar, row.ciudadId, row.ClienteId);

            return objHoraLibre;
        }

        public static List<HoraLibre> GetHoraLibreMedico(string ciudadId, int clienteId)
        {
            if (string.IsNullOrEmpty(ciudadId))
                throw new ArgumentException("CiudadId no puede estar vacio");

            int nroDiasHaciaAdelante = Artexacta.App.Configuration.Configuration.GetDESGNumeroDiasHoraLibre();

            List<HoraLibre> theList = new List<HoraLibre>();
            HoraLibre objHoraLibre = null;
            try
            {
                HoraLibreDSTableAdapters.HoraLibreMedicoTableAdapter theAdapter = new HoraLibreDSTableAdapters.HoraLibreMedicoTableAdapter();
                HoraLibreDS.HoraLibreMedicoDataTable theTable = theAdapter.GetHoraLibreMedico(nroDiasHaciaAdelante, ciudadId, clienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (HoraLibreDS.HoraLibreMedicoRow row in theTable.Rows)
                    {
                        objHoraLibre = FillRecord(row);
                        theList.Add(objHoraLibre);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting hora libre", q);
                throw;
            }
            return theList;
        }        



        public static List<HoraLibre> GetHoraLibreProveedor(int proveedorMedicoId, int estudioId, int clienteId)
        {
            if (proveedorMedicoId <= 0)
                throw new ArgumentException("Proveedor Medico no puede ser menor que 0");

            int nroDiasHaciaAdelante = Artexacta.App.Configuration.Configuration.GetDESGNumeroDiasHoraLibre();

            List<HoraLibre> theList = new List<HoraLibre>();
            HoraLibre objHoraLibre = null;
            try
            {
                HoraLibreDSTableAdapters.HoraLibreMedicoTableAdapter theAdapter = new HoraLibreDSTableAdapters.HoraLibreMedicoTableAdapter();
                HoraLibreDS.HoraLibreMedicoDataTable theTable = theAdapter.GetHoraLibreProveedorMedico(nroDiasHaciaAdelante, proveedorMedicoId, estudioId, clienteId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (HoraLibreDS.HoraLibreMedicoRow row in theTable.Rows)
                    {
                        objHoraLibre = FillRecord(row);
                        theList.Add(objHoraLibre);
                    }
                }
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Getting hora libre", q);
                throw;
            }
            return theList;
        }
    }
}