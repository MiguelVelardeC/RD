using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Negocio.Library
{
    public class Polizas
    {
        public static bool Actualizar(string CodigoCliente, string NumeroPoliza, string Estado, DateTime? FechaInicio, DateTime? FechaFin
            , out string msg, string userName)
        {
            msg = "";
            CodigoCliente = CodigoCliente.Trim();
            NumeroPoliza = NumeroPoliza.Trim();
            using (Negocio context = new Negocio())
            {
                if (!EsValidoCodigoCliente(userName, CodigoCliente))
                {
                    msg = "El Código de Cliente no es válido.";
                    return false;
                }

                var polizas = (from pol in context.tbl_Poliza
                              join ase in context.tbl_Asegurado on pol.AseguradoId equals ase.AseguradoId
                              join cli in context.tbl_RED_Cliente on ase.ClienteId equals cli.ClienteId
                              where cli.CodigoCliente == CodigoCliente && pol.NumeroPoliza == NumeroPoliza
                              select pol
                              ).ToList();
                if (polizas == null || polizas.Count <=0)
                {
                    msg = $"Poliza con Numero {NumeroPoliza} no existe para el Cliente {CodigoCliente}";
                    return false;
                }
                if (FechaInicio.HasValue)
                {
                    foreach (tbl_Poliza poliza in polizas)
                    {
                        poliza.FechaInicio = FechaInicio;
                    }
                }
                if (FechaFin.HasValue)
                {
                    foreach (tbl_Poliza poliza in polizas)
                    {
                        poliza.FechaFin = FechaFin;
                    }
                }
                string nuevoEstado =  Estado.Trim() == "A" ? "ACTIVO" : "INACTIVO";
                foreach (tbl_Poliza poliza in polizas)
                {
                    tbl_PolizaEstado nestado = new tbl_PolizaEstado()
                    {
                        PolizaId = poliza.PolizaId,
                        Estado = nuevoEstado,
                        Fecha = DateTime.UtcNow
                    };
                    context.tbl_PolizaEstado.Add(nestado);
                }
                context.SaveChanges();
                return true;
            }
        }

        public static bool Insertar(string CodigoCliente,string NombreAsegurado,DateTime FechaNacimiento,
        bool Genero,string RelacionDT,string CI,string Ciudad,string NumeroPoliza,DateTime FechaInicio,
        DateTime FechaFin,string NombrePlan, out string msg, string userName)
        {
            msg = "";
            using (Negocio context = new Negocio())
            {
                if (!EsValidoCodigoCliente(userName, CodigoCliente))
                {
                    msg = "El Código de Cliente no es válido.";
                    return false;
                }

                msg = context.Database.SqlQuery<string>(
                    "EXEC usp_Paciente_ImportarPacienteServicio {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}"
                    , CodigoCliente,NumeroPoliza,NombreAsegurado,FechaNacimiento,Genero,RelacionDT,CI
                    ,FechaInicio,FechaFin,Ciudad,NombrePlan).FirstOrDefault();

                if (msg == "OK")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private static bool EsValidoCodigoCliente(string usuario, string codigoCliente)
        {
            using (var context = new Negocio())
            {
                var codCliente = (from us in context.tbl_UsuarioServicio
                                  join usc in context.tbl_UsuarioServicioCliente on us.UsuarioServicioID equals usc.UsuarioServicioID
                                  join cli in context.tbl_RED_Cliente on usc.ClienteID equals cli.ClienteId
                                  where us.Usuario == usuario
                                  && cli.CodigoCliente == codigoCliente
                                  select us).FirstOrDefault();

                if (codCliente != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
