using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de CitaRecetaDetalle
/// </summary>
public class CitaRecetaDetalle
{
    public int detId { get; set; }
    public string Medicamento { get; set; }
    public string Grupo { get; set; }
    public string SubGrupo { get; set; }
    public string Presentacion { get; set; }
    public string Concentracion { get; set; }
    public string Cantidad { get; set; }
    public string InstruccionesUso { get; set; }

    public CitaRecetaDetalle()
    {
    }
}