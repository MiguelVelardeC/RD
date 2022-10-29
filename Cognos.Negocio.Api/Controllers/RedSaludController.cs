using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cognos.Negocio;

namespace Cognos.Negocio.Api.Controllers
{
    [Authorize]
    public class RedSaludController : CognosApiController
    {
        [HttpPost]
        [Route("Api/RedSalud/InsertarAsegurado")]
        public IHttpActionResult InsertarAsegurado(Asegurado asegurado)
        {
            if (asegurado == null)
            {
                return ResponseVal("Asegurado no puede ser null");
            }
            if (string.IsNullOrEmpty(asegurado.CodigoCliente))
            {
                return ResponseVal("Codigo de Cliente no puede ser blanco");
            }
            if (string.IsNullOrEmpty(asegurado.NumeroPoliza))
            {
                return ResponseVal("Numero de póliza no puede ser blanco");
            }
            if (asegurado.NumeroPoliza.Length > 20)
            {
                return ResponseVal("Numero de póliza no debe ser más de 20 carácteres");
            }
            if (string.IsNullOrEmpty(asegurado.NombreAsegurado))
            {
                return ResponseVal("Nombre de Asegurado no puede ser blanco");
            }
            if (asegurado.NombreAsegurado.Length > 200)
            {
                return ResponseVal("Nombre de Asegurado no debe ser más de 200 carácteres");
            }
            if (string.IsNullOrEmpty(asegurado.CI))
            {
                return ResponseVal("Carnet de Identidad no puede ser blanco");
            }
            if (asegurado.CI.Length > 20)
            {
                return ResponseVal("CI no debe ser más de 20 carácteres");
            }
            if (string.IsNullOrEmpty(asegurado.NombrePlan))
            {
                return ResponseVal("Nombre del Plan no puede ser blanco");
            }
            if (asegurado.NombrePlan.Length > 100)
            {
                return ResponseVal("Nombre del Plan no debe ser más de 20 carácteres");
            }
            //string[] ciudades = new string[] { "ALT", "CBB", "COB", "LPZ", "MON", "ORU", "PTS", "SCR", "STC", "TRI", "TRJ" };
            string[] ciudades = new string[] { "SCZ", "CBBA", "LPZ" };
            if (string.IsNullOrEmpty(asegurado.Ciudad) || !ciudades.Contains(asegurado.Ciudad.ToUpper()))
            {
                //asegurado.Ciudad = "STC";
                asegurado.Ciudad = "SCZ";
            }
            if (asegurado.FechaInicio == DateTime.MinValue || asegurado.FechaFin == DateTime.MinValue
                || asegurado.FechaInicio >= asegurado.FechaFin)
            {
                return ResponseVal("La Fecha de Inicio y Fecha Fin no son validas");
            }
            if (string.IsNullOrEmpty(asegurado.RelacionDT))
            {
                asegurado.RelacionDT = "TITULAR";
            }
            else
            {
                var relacion = asegurado.RelacionDT.Trim().ToUpper();
                switch (relacion)
                {
                    case "T":
                        asegurado.RelacionDT = "TITULAR";
                        break;

                    case "D":
                        asegurado.RelacionDT = "DEPENDIENTE";
                        break;

                    default:
                        asegurado.RelacionDT = "TITULAR";
                        break;
                }
            }
            DateTime minFecha = new DateTime(1901, 01, 01);
            if (!asegurado.FechaNacimiento.HasValue || asegurado.FechaNacimiento.Value <= minFecha)
            {
                asegurado.FechaNacimiento = minFecha;
            }
            if (!asegurado.Genero.HasValue)
            {
                asegurado.Genero = true;
            }

            string msg = "";
            bool inserto = Library.Polizas.Insertar(asegurado.CodigoCliente, asegurado.NombreAsegurado, asegurado.FechaNacimiento ?? minFecha,
        asegurado.Genero ?? true, asegurado.RelacionDT, asegurado.CI, asegurado.Ciudad, asegurado.NumeroPoliza, asegurado.FechaInicio,
        asegurado.FechaFin, asegurado.NombrePlan, out msg, Usuario);
            if (inserto)
            {
                return ResponseOk("Asegurado ingresado correctamente");
            }
            else
            {
                return ResponseVal($"Asegurado no se pudo insertar {msg}");
            }
        }

        [HttpPut]
        [Route("Api/RedSalud/ActualizarEstadoPoliza")]
        public IHttpActionResult ActualizarEstadoPoliza(Poliza poliza)
        {
            if (poliza == null)
            {
                return ResponseVal("Poliza no puede ser null");
            }
            if (string.IsNullOrEmpty(poliza.CodigoCliente))
            {
                return ResponseVal("Codigo de Cliente no puede ser blanco");
            }
            if (string.IsNullOrEmpty(poliza.NumeroPoliza))
            {
                return ResponseVal("Numero de poliza no puede ser blanco");
            }
            if (string.IsNullOrEmpty(poliza.Estado) || !(poliza.Estado.Trim() == "A" || poliza.Estado.Trim() == "I"))
            {
                return ResponseVal($"Estado de Poliza invalido, los valores validos son A o I");
            }
            string msg = "";
            bool actualizo = Library.Polizas.Actualizar(poliza.CodigoCliente, poliza.NumeroPoliza, poliza.Estado, poliza.FechaInicio, poliza.FechaFin, out msg, Usuario);
            if (actualizo)
            {
                return ResponseOk("Estado de Poliza actualizado correctamente");
            }
            else
            {
                return ResponseVal($"Poliza no se pudo actualizar {msg}");
            }
        }
    }

    public class Asegurado
    {
        public string CodigoCliente { get; set; }
        public string NombreAsegurado { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool? Genero { get; set; }
        public string RelacionDT { get; set; }
        public string CI { get; set; }
        public string Ciudad { get; set; }
        public string NumeroPoliza { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string NombrePlan { get; set; }
    }
    public class Poliza
    {
        public string CodigoCliente { get; set; }
        public string NumeroPoliza { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
