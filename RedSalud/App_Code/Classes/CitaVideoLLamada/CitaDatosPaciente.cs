using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.Apoc.Render.Pdf;

/// <summary>
/// Descripción breve de CitaDatosPaciente
/// </summary>
public class CitaDatosPaciente
{
    public int PacienteId { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Genero { get; set; }
    public string EstadoCivil { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string LugarTrabajo { get; set; }
    public string TelefonoTrabajo { get; set; }
    public int NroHijos { get; set; }
    public string CarnetIdentidad { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public int FotoId { get; set; }

    public CitaDatosPaciente()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
}