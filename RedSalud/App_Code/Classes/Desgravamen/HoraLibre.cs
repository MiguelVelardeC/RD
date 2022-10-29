using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HoraLibre
/// </summary>
public class HoraLibre
{
    public DateTime Horario { get; set; }
    public int MedicoId { get; set; }
    public string NombreMedico { get; set; }
    public int ProveedorMedicoId { get; set; }
    public string NombreLugar { get; set; }
    public string CiudadId { get; set; }
    public int ClienteId { get; set; }
    public string HorarioForDisplay
    {
        get
        {
            return string.Format("{0:dd/MMM/yyyy HH:mm}", Horario);
        }
    }
    public string DiaFecha
    {
        get
        {
            return Horario.Day.ToString();
        }
    }
    public string ID
    {
        get
        {
            return Horario.Ticks.ToString() + "," + (MedicoId > 0 ? MedicoId : ProveedorMedicoId);
        }
    }

    public string NombreProveedor
    {
        get
        {
            return NombreMedico == "" ? NombreLugar : NombreMedico;
        }
    }
    

	public HoraLibre(DateTime horario, int medicoId, string nombreMedico, int lugarId, string nombreLugar, string ciudadId, int clienteId)
	{
        Horario = horario;
        MedicoId = medicoId;
        NombreMedico = nombreMedico;
        ProveedorMedicoId = lugarId;
        NombreLugar = nombreLugar;
        CiudadId = ciudadId;
        ClienteId = clienteId;
	}
}