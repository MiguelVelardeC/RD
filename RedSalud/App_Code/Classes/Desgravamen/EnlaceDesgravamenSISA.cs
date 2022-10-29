using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EnlaceDesgravamenSISA
/// </summary>
public class EnlaceDesgravamenSISA
{
    public int CitaDesgravamenId { get; set; }
    public int PropuestoAseguradoId { get; set; }
    public string CarnetIdentidad { get; set; }
    public int PacienteId { get; set; }
    public string CodigoAsegurado { get; set; }
    public string NumeroPoliza { get; set; }
    public int PolizaId { get; set; }
    public int CasoId { get; set; }
    public string CodigoCaso { get; set; }
    public bool Dirty { get; set; }
    public int ClienteDesgravamenId { get; set; }

	public EnlaceDesgravamenSISA()
	{
        ;
	}
}