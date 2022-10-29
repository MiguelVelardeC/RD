using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for ProgramacionCitaLaboBLL
    /// </summary>
    public class ProgramacionCitaLaboBLL
    {
        public ProgramacionCitaLaboBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<ProgramacionCitaLabo> GetProgramacionCitaLabo(int citaDesgravamenId)
        {
            ProgramacionCitaLaboDSTableAdapters.ProgramacionCitaLaboTableAdapter adapter =
                new ProgramacionCitaLaboDSTableAdapters.ProgramacionCitaLaboTableAdapter();
            ProgramacionCitaLaboDS.ProgramacionCitaLaboDataTable table = adapter.GetProveedorMedicoParaCitaDesgravamen(citaDesgravamenId);

            List<ProgramacionCitaLabo> list = new List<ProgramacionCitaLabo>();

            foreach (ProgramacionCitaLaboDS.ProgramacionCitaLaboRow row in table)
            {
                ProgramacionCitaLabo obj = new ProgramacionCitaLabo()
                {
                    CitaDesgravamenId = citaDesgravamenId,
                    EstudioId = row.estudioId,
                    EstudioNombre = row.estudioNombre,
                    ProveedorMedicoId = row.proveedorMedicoId,
                    ProveedorNombre = row.proveedorNombre,
                    NecesitaCita = row.necesitaCita > 0 ? true : false,
                    FechaCita = row.IsfechaCitaNull() ? DateTime.MinValue : row.fechaCita
                };
                list.Add(obj);
            }
            return list;
        }

        public static void Delete(int citaDesgravamenId)
        {
            if (citaDesgravamenId <= 0)
            {
                throw new ArgumentException("No puede eliminar las citas de los laboratorios de la cita desgravamenId " + citaDesgravamenId);
            }

            ProgramacionCitaLaboDSTableAdapters.ProgramacionCitaLaboTableAdapter adapter =
                new ProgramacionCitaLaboDSTableAdapters.ProgramacionCitaLaboTableAdapter();

            adapter.DeleteProgramacionCitaLabo(citaDesgravamenId);
        }
    }
}