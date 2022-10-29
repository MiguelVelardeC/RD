using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Data;

/// <summary>
/// Descripción breve de CitaEstudioBLL
/// </summary>
/// 

namespace Artexacta.App.CitaEstudio.BLL
{
    public class CitaEstudioBLL
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        public CitaEstudioBLL()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        // Mostrar Citas
        private static CitaEstudioC FillRecordC(CitaEstudioDS.usp_CitasBycodIDRow row)
        {
            CitaEstudioC objcita = new
                CitaEstudioC(row.citaDesgravamenid, row.estado, row.fechaCreacion,
                    row.fechaHoraCita, row.aprobado,row.observacionLabo,row.nom );

            return objcita;
        }
        // Mostrar Estudios de la Cita
       
        public static List<CitaEstudioC> getCitasListar( int citaDesgravamenId)
        {
            //DataTable dt = new DataTable();
            List<CitaEstudioC> theList = new List<CitaEstudioC>();
            if (citaDesgravamenId < 0)
                throw new ArgumentException("Cita cannot be <= 0.");

            CitaEstudioC TheCitaEstudioC = null;
            try
            {
                if (citaDesgravamenId != 0)
                {
                    CitaEstudioDSTableAdapters.usp_CitasBycodIDTableAdapter theadapter = new CitaEstudioDSTableAdapters.usp_CitasBycodIDTableAdapter();
                    CitaEstudioDS.usp_CitasBycodIDDataTable thetable = theadapter.Get_usp_CitasBycodID(citaDesgravamenId);
                    if (thetable != null && thetable.Rows.Count > 0)
                    {
                        foreach (CitaEstudioDS.usp_CitasBycodIDRow row in thetable.Rows)
                        {
                            //DataRow rowd = dt.NewRow();
                            TheCitaEstudioC = FillRecordC(row);
                            theList.Add(TheCitaEstudioC);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Citas", ex);
                throw ex;
            }
            return theList;

        }
        public static void UpdateFecha(int citaDesgravamenId, DateTime feccrea,DateTime feccita,string usuario)
        {

            CitaEstudioDSTableAdapters.usp_CitasBycodIDTableAdapter adapter =
                new CitaEstudioDSTableAdapters.usp_CitasBycodIDTableAdapter();

            adapter.UpdateFechaCita(citaDesgravamenId,feccrea, feccita, usuario);
            
        }

        /// >>>>>>>>>>>>>>>>>>> CITA ESTUDIO
       
        private static CitaEstudio FillRecord(CitaEstudioDS.CitasyEstudioRow row)
        {
            CitaEstudio objcita = new
                CitaEstudio(row.citaDesgravamenId, row.estudioId, row.proveedorMedicoId, row.nom_estudio, row.fechaRealizado,
            row.nom_proveedor, row.realizado, row.necesitoCita);

            return objcita;
        }


        public static List<CitaEstudio> getCitasEstudioListar(int citaDesgravamenId)
        {
            //DataTable dt = new DataTable();
            List<CitaEstudio> theList = new List<CitaEstudio>();
            if (citaDesgravamenId < 0)
                throw new ArgumentException("Cita cannot be <= 0.");

            CitaEstudio TheCitaEstudio = null;
            try
            {
                if (citaDesgravamenId != 0)
                {
                    CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter theadapter = new CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter();
                    CitaEstudioDS.CitasyEstudioDataTable thetable = theadapter.Get_usp_CitasEstudioBycodID(citaDesgravamenId);
                    if (thetable != null && thetable.Rows.Count > 0)
                    {
                        foreach (CitaEstudioDS.CitasyEstudioRow row in thetable.Rows)
                        {
                           
                            TheCitaEstudio = FillRecord(row);
                            theList.Add(TheCitaEstudio);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("An error was ocurred while geting list Citas y sus Estudios", ex);
                throw ex;
            }
            return theList;

        }

        public static void DeleteEstudioCita(int citaDesgravamenId, int estudioid, string usuario)
        {

            CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter adapter =
                new CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter();

            adapter.deletecitaestudio(citaDesgravamenId, estudioid, usuario);

        }

        public static void UpdateEstudioMedico(int citaDesgravamenId, int estudioid, int proveedorid, 
                                            DateTime fecha,string usuario)
        {

            CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter adapter =
                new CitaEstudioDSTableAdapters.CitasyEstudioTableAdapter();

            adapter.UpdateEstudioMedico(citaDesgravamenId, estudioid, proveedorid, fecha,usuario);

        }
    }
}