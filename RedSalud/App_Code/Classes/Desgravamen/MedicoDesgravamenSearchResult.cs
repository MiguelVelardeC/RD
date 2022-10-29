using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Documents;
using log4net;

namespace Artexacta.App.Desgravamen
{
/// <summary>
/// Summary description for MedicoDesgravamenSearchResult
/// </summary>
public class MedicoDesgravamenSearchResult
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int MedicoDesgravamenId { get; set; }
    public string NombreMedico { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public int ProveedorMedicoId { get; set; }
    public string NombreProveedor { get; set; }
    public string Direccion { get; set; }

    public MedicoDesgravamenSearchResult()
    {

    }
    
}
}