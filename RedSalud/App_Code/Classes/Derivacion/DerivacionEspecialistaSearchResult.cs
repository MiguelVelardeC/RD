using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DerivacionSearchResult
/// </summary>
public class DerivacionEspecialistaSearchResult
{
    public int DerivacionId { get; set; }
    public int CasoId { get; set; }
    public string CasoCodigoDerivado { get; set; }
    public int CasoIdDerivacion { get; set; }
    public string CasoCodigoDerivacion { get; set; }
    public int GastoId { get; set; }
    public string CiudadDerivacionId { get; set; }
    public string CiudadDerivacionNombre { get; set; }
    public int PacienteId { get; set; }
    public string PacienteNombre { get; set; }
    public int DerivadorUserId { get; set; }
    public string DerivadorNombre { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int MedicoId { get; set; }
    public string MedicoNombre { get; set; }
    public int DerivacionCasoId { get; set; }
    public string EspecialidadNombre { get; set; }
    public int ClienteId { get; set; }
    public string ClienteNombre { get; set; }
    public bool isAtendido { get; set; }
    public string isAtendidoDisplay { 
        get{
            return isAtendido? "ATENDIDO": "PENDIENTE";
        }
    }

	public DerivacionEspecialistaSearchResult()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}