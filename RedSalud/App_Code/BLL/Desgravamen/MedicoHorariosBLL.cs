using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for MedicoHorariosBLL
    /// </summary>
    public class MedicoHorariosBLL
    {
        public MedicoHorariosBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<MedicoHorarios> GetMedicoHorarios(int medicoDesgravamenId, int clienteId)
        {
            /*MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter adapter =
                new MedicoDesgravamenDSTableAdapters.MedicosDesgravamenTableAdapter();*/

            MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter adapter = new MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter();
            MedicoHorariosDS.MedicoHorariosDataTable table = adapter.GetMedicoHorarios(medicoDesgravamenId, clienteId);


            List<MedicoHorarios> list = new List<MedicoHorarios>();

            foreach (MedicoHorariosDS.MedicoHorariosRow row in table)
            {
                MedicoHorarios obj = new MedicoHorarios();
                obj.MedicoHorariosId = row.medicoHorariosId;
                obj.ClienteId = row.clienteId;
                obj.ClienteNombre = row.nombreCliente;
                obj.HoraInicio = row.horaInicio;
                obj.HoraFin = row.horaFin;
                list.Add(obj);
            }

            return list;
        }

        public static int InsertMedicoHorarios(MedicoHorarios obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("object MedicoDesgravamen cannot be null");
            }

            if (obj.ClienteId == null || obj.ClienteId <= 0)
            {
                throw new ArgumentException("Cliente cannot be null or empty");
            }

            if (obj.HoraInicio == null)
            {
                throw new ArgumentException("Hora Inicio cannot be null or empty");
            }

            if (obj.HoraFin == null)
            {
                throw new ArgumentException("Hora Fin cannot be null or empty");
            }

            if (obj.MedicoDesgravamenId == null || obj.MedicoDesgravamenId <= 0)
            {
                throw new ArgumentException("MedicoDesgravamenId cannot be null or empty");
            }
            
            int? medicoHorariosId = 0;

            MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter adapter =
                new MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter();
            //adapter.InsertMedicoHorarios(obj.MedicoDesgravamenId,obj.ClienteId,
            /*
            PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter adapter = new PropuestoAseguradoDSTableAdapters.PropuestoAseguradoTableAdapter();
            adapter.InsertPropuestoAsegurado(ref propuestoAseguradoId, obj.NombreCompleto, obj.CarnetIdentidad, obj.FechaNacimiento, obj.TelefonoCelular, obj.Genero);*/

            adapter.InsertMedicoHorarios(obj.MedicoDesgravamenId, obj.ClienteId, obj.HoraInicio, obj.HoraFin, ref medicoHorariosId);

            /*if (medicoHorariosId == null || medicoHorariosId.Value <= 0)
            {
                throw new Exception("Identity for Primary Key was not be generated");
            }*/

            return medicoHorariosId.Value;
        }
        
        public static void DeleteMedicoHorarios(int medicoHorariosId)
        {
            if (medicoHorariosId <= 0)                            
                throw new ArgumentException("MedicoHorariosId cannot be equals or less than zero");
            

            MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter adapter =
                new MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter();

            int? result = 0;

            adapter.DeleteMedicoHorarios(medicoHorariosId, ref result);

            if (result.Value > 1)
            {
                throw new Exception("More than 1 row was eliminated with medicoHorariosId " + medicoHorariosId);
            }
            else if (result.Value == 0)
            {
                throw new Exception("No Rows were Eliminated with medicoHorariosId " + medicoHorariosId);
            }
        }

        public static MedicoHorarios GetMedicoHorariosById(int medicoHorariosId)
        {
            if (medicoHorariosId <= 0)
                throw new ArgumentException("medicoHorariosId cannot be less than or equal to zero.");

            MedicoHorarios objMedicoHorarios = null;
            try
            {
                MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter theAdapter =
                new MedicoHorariosDSTableAdapters.MedicoHorariosTableAdapter();
                MedicoHorariosDS.MedicoHorariosDataTable theTable = theAdapter.GetMedicoHorariosById(medicoHorariosId);
                if (theTable != null && theTable.Rows.Count > 0)
                {
                    MedicoHorariosDS.MedicoHorariosRow Row = theTable[0];
                    objMedicoHorarios = new MedicoHorarios()
                    {
                        MedicoHorariosId = Row.medicoHorariosId,
                        MedicoDesgravamenId = Row.medicoDesgravamenId,
                        ClienteId = Row.clienteId,
                        ClienteNombre = Row.nombreCliente,
                        HoraInicio = Row.horaInicio,
                        HoraFin = Row.horaFin
                    };

                    return objMedicoHorarios;
                }
            }
            catch (Exception ex)
            {
                //log.Error("An error was ocurred while getting list objMedicoHorarios by Id", ex);
                throw ex;
            }
            return objMedicoHorarios;
        }
    }
}