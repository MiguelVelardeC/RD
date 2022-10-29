using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Cita.BLL
{
    /// <summary>
    /// Summary description for CitaBLL
    /// </summary>
    public class CitaBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");


        public CitaBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        private static Cita FillRecord(CitaDS.CitasRow row)
        {
            Cita objCita = new Cita();
            objCita.CitaId = row.CitaId;
            objCita.Description = row.Description;
            objCita.EndTime = row.EndTime;
            objCita.MedicoId = row.IsMedicoIdNull() ? 0 : row.MedicoId;
            objCita.PacienteId = row.IsPacienteIdNull() ? 0 : row.PacienteId;
            objCita.ProveedorId = row.IsProveedorIdNull() ? 0 : row.ProveedorId;
            objCita.StartTime = row.StartTime;
            objCita.Subject = row.Subject;

            return objCita;
        }

        public static Cita GetCitaByCitaId(int CitaId)
        {
            if (CitaId <= 0)
                throw new ArgumentException("CitaId no puede ser menor o igual a cero.");

            Cita TheCita = null;
            try
            {
                CitaDSTableAdapters.CitasTableAdapter theAdapter = 
                    new CitaDSTableAdapters.CitasTableAdapter();
                CitaDS.CitasDataTable theTable = theAdapter.GetCitaById(CitaId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    TheCita = FillRecord(theTable[0]);
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Cita data", ex);
                throw;
            }
            return TheCita;
        }

        public static List<Cita> GetCitaByMedicoProveedorId(int medicoId, int proveedorId, 
            DateTime viewStartTime, DateTime viewEndTime)
        {
            if (medicoId != 0 && proveedorId != 0)
            {
                throw new ArgumentException("El medicoId y el proveedor no pueden ser ambos diferentes de 0");
            }

            List<Cita> TheCita = new List<Cita>();
            try
            {
                CitaDSTableAdapters.CitasTableAdapter theAdapter =
                    new CitaDSTableAdapters.CitasTableAdapter();
                CitaDS.CitasDataTable theTable = theAdapter.GetCitaByMedicoProveedorId(medicoId, proveedorId, viewStartTime, viewEndTime);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    foreach (CitaDS.CitasRow row in theTable.Rows)
                    {
                        TheCita.Add(FillRecord(row));
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting Cita data", ex);
                throw;
            }
            return TheCita;
        }


        public static void UpdateCita(Cita objCita) 
        {
            if (objCita == null)
                throw new ArgumentException("Cita cannot be null.");
            if (objCita.CitaId <= 0)
                throw new ArgumentException("CitaId debe ser positivo");

            try
            {
                CitaDSTableAdapters.CitasTableAdapter TheAdapter = new CitaDSTableAdapters.CitasTableAdapter();
                TheAdapter.Update(objCita.CitaId, objCita.StartTime, objCita.EndTime, objCita.Subject, objCita.Description);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Cita", q);
                throw;
            }
        }

        public static void InsertCita(Cita objCita)
        {
            if (objCita == null)
                throw new ArgumentException("Cita cannot be null.");
            if (objCita.CitaId > 0)
                throw new ArgumentException("CitaId debe ser 0 o negativo");

            if (objCita.MedicoId <= 0 && objCita.ProveedorId <= 0)
            {
                throw new ArgumentException("LA cita debe ser para un medico o un proveedor medico");
            }

            try
            {
                int? citaId = 0;
                CitaDSTableAdapters.CitasTableAdapter TheAdapter = new CitaDSTableAdapters.CitasTableAdapter();
                TheAdapter.Insert(ref citaId, 
                    objCita.MedicoId == 0 ? null : (int?)objCita.MedicoId, 
                    objCita.ProveedorId == 0 ? null : (int?)objCita.ProveedorId, 
                    objCita.StartTime, objCita.EndTime, objCita.Subject, objCita.Description, 
                    objCita.PacienteId == 0 ? null : (int?)objCita.PacienteId);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while Updating Cita", q);
                throw;
            }
        }

        public static void DeleteCita(int citaId)
        {
            if (citaId <= 0)
                throw new ArgumentException("CitaId debe ser positivo");

            try
            {
                CitaDSTableAdapters.CitasTableAdapter TheAdapter = new CitaDSTableAdapters.CitasTableAdapter();
                TheAdapter.Delete(citaId);
            }
            catch (Exception q)
            {
                log.Error("An error was ocurred while deleting Cita", q);
                throw;
            }
        }
    }
}