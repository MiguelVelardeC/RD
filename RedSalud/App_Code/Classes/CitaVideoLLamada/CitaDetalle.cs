using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CitaDetalle
/// </summary>
public class CitaDetalle
{
    public int citId { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string Medico { get; set; }
    public string Especialidad { get; set; }
    public string Motivo { get; set; }
    public DateTime FechaCita { get; set; }
    public string Estado { get; set; }
    public string Recomendaciones { get; set; }
    public string citObservaciones { get; set; }
    public string Enfermedad1 { get; set; }
    public string Enfermedad2 { get; set; }
    public string Enfermedad3 { get; set; }


    public string Calificacion { get; set; }
    public string Comentario { get; set; }
    public int PolizaId { get; set; }
    public int AseguradoId { get; set; }
    public CitaDetalle()
    {
    }

}