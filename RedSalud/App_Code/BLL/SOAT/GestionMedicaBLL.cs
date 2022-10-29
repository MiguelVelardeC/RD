using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace Artexacta.App.GestionMedica.BLL
{
    public class GestionMedicaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");

        public GestionMedicaBLL () { }


        private static GestionMedica FillRecord(GestionMedicaDS.GestionMedicaRow row)
        {
            DateTime FechaMax = DateTime.MaxValue;
            GestionMedica objGestionMedica = new
                GestionMedica(
                      row.SiniestroId
                    , row.AccidentadoId
                    , row.GestionMedicaId
                    , row.Nombre
                    , row.FechaVisita
                    , row.Grado
                    , row.DiagnosticoPreliminar
                );

            if (!row.IsProveedorIdNull())
            {
                objGestionMedica.ProveedorId = row.ProveedorId;
            }

            return objGestionMedica;
        }

        public static GestionMedica GetGestionMedicaByID ( int GestionMedicaId )
        {
            if (GestionMedicaId < 0)
                throw new ArgumentException("GestionMedicaId cannot be less than or equal to zero.");

            GestionMedica TheGestionMedica = null;
            try
            {
                GestionMedicaDSTableAdapters.GestionMedicaTableAdapter theAdapter = new GestionMedicaDSTableAdapters.GestionMedicaTableAdapter();
                GestionMedicaDS.GestionMedicaDataTable theTable = theAdapter.GetGestionMedicaById(GestionMedicaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheGestionMedica = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GestionMedica data", ex);
                throw;
            }
            return TheGestionMedica;
        }

        public static List<GestionMedica> GetAllGestionMedicaBySiniestroID ( int SiniestroId, int AccidentadoId )
        {
            if (SiniestroId < 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");

            List<GestionMedica> list = new List<GestionMedica>();
            try
            {
                GestionMedicaDSTableAdapters.GestionMedicaTableAdapter theAdapter = new GestionMedicaDSTableAdapters.GestionMedicaTableAdapter();
                GestionMedicaDS.GestionMedicaDataTable theTable = theAdapter.GetGestionMedicaByAccidentadoId(SiniestroId, AccidentadoId);
                foreach ( GestionMedicaDS.GestionMedicaRow row in theTable.Rows )
                {
                    GestionMedica TheGestionMedica = FillRecord(row);
                    list.Add(TheGestionMedica);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GestionMedica data", ex);
                throw;
            }
            return list;
        }

        public static List<string> GetTags (string search)
        {
            List<string> list = new List<string>();
            try
            {
                GestionMedicaDSTableAdapters.TagTableAdapter theAdapter = new GestionMedicaDSTableAdapters.TagTableAdapter();
                GestionMedicaDS.TagDataTable theTable = theAdapter.GetTags(search);
                foreach (GestionMedicaDS.TagRow row in theTable.Rows)
                {
                    list.Add(row.Nombre);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting GestionMedica Tags data", ex);
                throw;
            }
            return list;
        }

        public static void InsertGestionMedica ( ref GestionMedica GestionMedica )
        {
            GestionMedica.GestionMedicaId = InsertGestionMedica(GestionMedica.SiniestroId, GestionMedica.AccidentadoId, GestionMedica.Nombre
                , GestionMedica.FechaVisita, GestionMedica.Grado, GestionMedica.DiagnosticoPreliminar, 0, GestionMedica.ProveedorId);
        }

        public static void InsertGestionMedica ( ref GestionMedica GestionMedica, decimal reserva )
        {
            GestionMedica.GestionMedicaId = InsertGestionMedica(GestionMedica.SiniestroId, GestionMedica.AccidentadoId, GestionMedica.Nombre
                , GestionMedica.FechaVisita, GestionMedica.Grado, GestionMedica.DiagnosticoPreliminar, reserva, GestionMedica.ProveedorId);
        }
        public static int InsertGestionMedica ( int SiniestroId, int AccidentadoId, string Nombre, DateTime fechaVisita, 
                string grado, string DiagnosticoPreliminar, decimal reserva, int ProveedorId)
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId < 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                GestionMedicaDSTableAdapters.GestionMedicaTableAdapter theAdapter = new GestionMedicaDSTableAdapters.GestionMedicaTableAdapter();
                int? GestionMedicaId = 0;

                Accidentado.Accidentado acc = 
                    Accidentado.BLL.AccidentadoBLL.GetAccidentadoByID(AccidentadoId, SiniestroId);
                

                decimal montoGestion = reserva <= 0 ? 0 : Configuration.Configuration.GetMontoGestion();
                decimal montoGestionFallecido = Configuration.Configuration.GetMontoGestionFallecido();

                montoGestion = (!acc.Estado) ? montoGestionFallecido : montoGestion;
                theAdapter.Insert(SiniestroId, AccidentadoId, ref GestionMedicaId, Nombre, fechaVisita,
                    grado, DiagnosticoPreliminar, montoGestion, reserva, ProveedorId);
                return (int)GestionMedicaId;
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while inserting GestionMedica", ex);
                throw;
            }
        }

        public static void UpdateGestionMedica ( GestionMedica GestionMedica )
        {
            UpdateGestionMedica(GestionMedica.SiniestroId, GestionMedica.AccidentadoId, GestionMedica.GestionMedicaId, GestionMedica.Nombre
                , GestionMedica.FechaVisita, GestionMedica.Grado, GestionMedica.DiagnosticoPreliminar, GestionMedica.ProveedorId);
        }
        public static void UpdateGestionMedica ( int SiniestroId, int AccidentadoId, int GestionMedicaId, string Nombre, 
            DateTime fechaVisita, string grado, string DiagnosticoPreliminar, int ProveedorId )
        {
            if (SiniestroId <= 0)
                throw new ArgumentException("SiniestroId cannot be less than or equal to zero.");
            if (AccidentadoId <= 0)
                throw new ArgumentException("AccidentadoId cannot be less than or equal to zero.");
            if (GestionMedicaId <= 0)
                throw new ArgumentException("GestionMedicaId cannot be less than or equal to zero.");
            if (string.IsNullOrEmpty(Nombre))
                throw new ArgumentException("Nombre cannot null or empty.");

            try
            {
                GestionMedicaDSTableAdapters.GestionMedicaTableAdapter theAdapter = new GestionMedicaDSTableAdapters.GestionMedicaTableAdapter();
                theAdapter.Update(SiniestroId, AccidentadoId, GestionMedicaId, Nombre, fechaVisita, grado, DiagnosticoPreliminar, ProveedorId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while updating GestionMedica", ex);
                throw;
            }
        }
        public static void Delete ( int GestionMedicaId )
        {
            if (GestionMedicaId <= 0)
                throw new ArgumentException("GestionMedicaId cannot be less than or equal to zero.");

            try
            {
                GestionMedicaDSTableAdapters.GestionMedicaTableAdapter theAdapter = new GestionMedicaDSTableAdapters.GestionMedicaTableAdapter();
                theAdapter.Delete(GestionMedicaId);
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while deleting GestionMedica", ex);
                throw;
            }
        }

        public static char needUpdate ( GestionMedica gestionMedica )
        {
            if (gestionMedica.GestionMedicaId <= 0)
                return 'I';

            GestionMedica gestionMedicaBD = GetGestionMedicaByID(gestionMedica.GestionMedicaId);
            if (gestionMedicaBD == null)
                return 'I';

            bool update = false;
            update = (gestionMedica.Nombre != gestionMedicaBD.Nombre) || update;
            update = (gestionMedica.FechaVisita != gestionMedicaBD.FechaVisita) || update;
            update = (gestionMedica.Grado != gestionMedicaBD.Grado) || update;
            update = (gestionMedica.DiagnosticoPreliminar != gestionMedicaBD.DiagnosticoPreliminar) || update;

            return update ? 'U' : 'L';
        }
    }
}