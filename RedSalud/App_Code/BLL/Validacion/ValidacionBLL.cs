using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.RedClientePrestaciones.BLL;
using Artexacta.App.RedClientePrestaciones;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Siniestralidad;
using Artexacta.App.Siniestralidad.BLL;
using Artexacta.App.ProveedorPrestaciones.BLL;
using Artexacta.App.ProveedorPrestaciones;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Security.BLL;
using System.Web.Security;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Proveedor.BLL;
using Artexacta.App.Proveedor;
using Artexacta.App.Medico;
using Artexacta.App.CLAPrestacionOdontologica.BLL;
using Artexacta.App.CLAPrestacionOdontologica;

/// <summary>
/// Summary description for ValidacionBLL
/// </summary>
/// 
namespace Artexacta.App.Validacion.BLL
{
    public class ValidacionBLL
    {
        public ValidacionBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public decimal BusquedaDeValorCoPagoMG(string TipodePago, int clienteId)
        {
            decimal CopagoValor = 0;
            List<RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion == "MG")
                {
                    if (ListaPrestaciones[i].CoPagoMonto > 0 & TipodePago == "Monto")
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                    else
                    {
                        if (ListaPrestaciones[i].CoPagoPorcentaje > 0 & TipodePago == "Porcentaje")
                        {
                            string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                            CopagoValor = decimal.Parse(ValorCoPago);
                            break;
                        }
                    }
                }

            }
            return CopagoValor;
        }
        public decimal BusquedaPrecioMG(int clienteId)
        {
            decimal CopagoValor = 0;
            List<RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion == "MG")
                {

                    string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                    CopagoValor = decimal.Parse(ValorCoPago);
                    break;

                }

            }
            return CopagoValor;
        }
        public bool VerficarPolizaVenicimiento(int polizaId)
        {
            List<PolizaValidation> ListaDeDatosPaciente = PolizaBLL.GetAllPolizaValidation(polizaId);
            if (ListaDeDatosPaciente[0].VigenciaPoliza)
                return true;
            else
                return false;
        }

        public bool VerificarSiElclienteCubreLaPoliza(int clienteId, string TipoPrestacion)
        {
            bool Servicio = false;
            List<RedClientePrestaciones.RedClientePrestaciones> GetAllClientePrestaciones = new List<App.RedClientePrestaciones.RedClientePrestaciones>();
            GetAllClientePrestaciones = RedClientePrestacionesBLL.GetAllClientePrestaciones(clienteId);
            for (int i = 0; i < GetAllClientePrestaciones.Count(); i++)
            {
                if (GetAllClientePrestaciones[i].TipoPrestacion.Contains(TipoPrestacion))
                {
                    if (GetAllClientePrestaciones[i].ClienteId != 0)
                    {
                        Servicio = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return Servicio;
        }
        /*tipo prestacion : Internación,Medicina General,Cirugía,Emergencia,Odontología,Especialidades,Laboratorios,Imagenología*/
        public bool VerificarCantidadYMontoTopeYMAcumuladoPrestacion(int ClienteId, int PolizaId, string TipoPrestacion)
        {
            bool Estado = false;
            List<SiniestralidadDetail> _cache = new List<SiniestralidadDetail>();
            int _totalRows = SiniestralidadBLL.GetReporteSiniestralidadDetailGetDate(_cache, ClienteId, PolizaId);
            if (_totalRows > 0)
            {
                bool MontoAcumuladoMG = false;
                bool ConsultaAcumuladoMG = false;
                bool MontoAcumuladoSuperadoMG = false;

                for (int i = 0; i < _cache.Count; i++)
                {
                    if (_cache[i].NombrePrestacion.Contains(TipoPrestacion))
                    {
                        if (_cache[i].MontoAcumulado < _cache[i].MontoTope)
                            MontoAcumuladoMG = true;
                        if ((_cache[i].ConsultasAcumuladas < _cache[i].ConsultasPorAnos) || _cache[i].ConsultasPorAnos == 0)
                            ConsultaAcumuladoMG = true;
                    }
                }
                if (_cache[_cache.Count - 1].MontoAcumulado < _cache[_cache.Count - 1].MontoTope)
                {
                    MontoAcumuladoSuperadoMG = true;
                }

                if (MontoAcumuladoMG & ConsultaAcumuladoMG & MontoAcumuladoSuperadoMG)
                {
                    Estado = true;
                }
            }
            return Estado;

        }

        public decimal BusquedaDeValorCoPago(string TipodePago, int clienteId, string TipoPrestacion)
        {
            decimal CopagoValor = 0;
            List<RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones = RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(clienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion.Contains(TipoPrestacion))
                {
                    if (TipodePago.Contains("Monto"))
                    {
                        string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoMonto * 100) / 100));
                        CopagoValor = decimal.Parse(ValorCoPago);
                        break;
                    }
                    else
                    {
                        if (TipodePago.Contains("Porcentaje"))
                        {
                            string ValorCoPago = string.Format("{0:n2}", (Math.Truncate(ListaPrestaciones[i].CoPagoPorcentaje * 100) / 100));
                            CopagoValor = decimal.Parse(ValorCoPago);
                            break;
                        }
                    }
                }

            }
            return CopagoValor;
        }
        public decimal BusquedaDePrecioPRestacionesOdontologicas(int proveedorId, int PrestacionesOdontologicasId)
        {
            decimal valor = 0;
            string ValorODO = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorODO"];
            List<RedProvLabImgCarDetallePrestaciones> Lista = RedProvLabImgCarDetallePrestacionesBLL.GetProvLabImgCarDetallePrestaciones(proveedorId, ValorODO, 0);
            for (int i = 0; i < Lista.Count; i++)
            {
                if (Lista[i].EstudioId == PrestacionesOdontologicasId)
                {
                    valor = Lista[i].detPrecio;
                }
            }
            return valor;
        }
        public static string ValidacionDePrestacion(int PolizaId, int ClienteId, string TipoPrestacion, string TipoCIoOD)
        {
            string MensajedeError = "";
            try
            {
                #region "Datos Cargados desde el Web Config"
                string NombreOdontologia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreOdontologia"];
                string NombreEspecialidad = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreEspecialidad"];
                string NombreMedicinaGeneral = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreMedicinaGeneral"];
                string NombreLaboratorios = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreLaboratorios"];
                string NombreImagenologia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreImagenologia"];
                string NombreCardiologia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreImagenologia"];
                string NombreFarmacia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreFarmacia"];
                string NombreCirugia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreCirugia"];
                string NombreInternacion = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreInternacion"];
                string NombreEmergencia = System.Web.Configuration.WebConfigurationManager.AppSettings["NombreEmergencia"];
                #endregion

                if (VerificarCliente(ClienteId, TipoPrestacion))
                {
                    List<SiniestralidadDetail> _cache = new List<SiniestralidadDetail>();
                    List<PolizaValidation> ListaDeDatosPaciente = PolizaBLL.GetAllPolizaValidation(PolizaId, "", 0, 0, 0, "", "");

                    int _totalRows = SiniestralidadBLL.GetReporteSiniestralidadDetail(_cache, ClienteId, PolizaId, "", "");
                    if ((TipoPrestacion.ToUpper() != "ALL".ToUpper()))
                    {
                        if (ListaDeDatosPaciente[0].VigenciaPoliza == false)
                        {
                            MensajedeError = "La Poliza Se Ha Vencido ";
                        }
                        else
                        {
                            if (ListaDeDatosPaciente[0].EstadoPoliza.Contains("INACTIVO"))
                            {
                                MensajedeError = " La Poliza Esta Inactiva ";
                            }
                            else
                            {
                                switch (TipoPrestacion)
                                {
                                    #region "Odontologia"
                                    case "OD":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreOdontologia.ToUpper())
                                            {
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                else
                                                {
                                                    int j = i + 1;
                                                    while (j < _cache.Count & _cache[j].NombrePrestacion.Contains("*"))
                                                    {
                                                        if (_cache[j].NombrePrestacion.Contains(TipoCIoOD))
                                                        {
                                                            if ((_cache[j].ConsultasAcumuladas >= _cache[j].ConsultasPorAnos) & _cache[j].ConsultasPorAnos!=0 )
                                                            {
                                                                MensajedeError = "Consultas Acumuladas ";
                                                            }

                                                        }
                                                        j = j + 1;
                                                    }
                                                }
                                                break;

                                            }
                                        }

                                        break;

                                    #endregion
                                    #region "Medico General"
                                    case "MG":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreMedicinaGeneral.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos!=0)
                                                {
                                                    MensajedeError = "Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }

                                        break;
                                    #endregion
                                    #region "Especialidad"
                                    case "ES":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreEspecialidad.ToUpper())
                                            {
                                               
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                else
                                                {
                                                    int j = i + 1;
                                                    int acumulacionconsultas = 0;
                                                    while (j < _cache.Count & _cache[j].NombrePrestacion.Contains("*"))
                                                    {

                                                        acumulacionconsultas = acumulacionconsultas + _cache[j].ConsultasPorAnos;
                                                         j =j + 1;
                                                    }


                                                    if ((acumulacionconsultas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                    {
                                                        MensajedeError = " Consultas Acumuladas ";
                                                    }
                                                    


                                                }
                                                break;
                                            }
                                           
                                        }
                                        break;
                                    #endregion
                                    #region "LABORATORIO"
                                    case "LA":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreLaboratorios.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = "Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "CARDIOLOGIA"
                                    case "CA":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreCardiologia.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = "Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "IMAGENOLOGIA"
                                    case "IM":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreImagenologia.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = "Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "FARMACIA"
                                    case "FA":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreFarmacia.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = "Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "CIRUGIA"
                                    case "CI":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreCirugia.ToUpper())
                                            {
                                                int j = i + 1;
                                                while (j < _cache.Count & _cache[j].NombrePrestacion.Contains("*"))
                                                {
                                                    if (_cache[j].NombrePrestacion.Contains(TipoCIoOD))
                                                    {
                                                        if ((_cache[j].ConsultasAcumuladas >= _cache[j].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                        {
                                                            MensajedeError = " Consultas Acumuladas ";
                                                        }
                                                        if (_cache[j].MontoAcumulado >= _cache[j].MontoTope)
                                                        {
                                                            MensajedeError = MensajedeError + " Monto Tope ";
                                                        }
                                                    }
                                                    j = j + 1;
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "INTERNACION"
                                    case "IN":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreInternacion.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = " Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    #endregion
                                    #region "EMERGENCIA"
                                    case "EM":

                                        for (int i = 0; i < _cache.Count; i++)
                                        {
                                            if (_cache[i].NombrePrestacion.ToUpper() == NombreEmergencia.ToUpper())
                                            {
                                                if ((_cache[i].ConsultasAcumuladas >= _cache[i].ConsultasPorAnos) & _cache[i].ConsultasPorAnos != 0)
                                                {
                                                    MensajedeError = " Consultas Acumuladas ";
                                                }
                                                if (_cache[i].MontoAcumulado >= _cache[i].MontoTope)
                                                {
                                                    MensajedeError = MensajedeError + " Monto Tope ";
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                        #endregion
                                }
                            }
                        }
                    }
                    else
                    {
                        #region "Verificar Monto Total x Monto Tope"
                        if (_cache[_cache.Count - 1].MontoAcumulado >= _cache[_cache.Count - 1].MontoTope)
                        {
                            MensajedeError = " Monto Tope Sobre Pasado ";
                        }
                        #endregion
                        #region "Verificar Estado de la Poliza"
                        else
                        {


                            if (ListaDeDatosPaciente[0].VigenciaPoliza == false)
                            {
                                MensajedeError = " La Poliza Se Ha Vencido ";
                            }
                            else
                            {
                                if (ListaDeDatosPaciente[0].EstadoPoliza.Contains("INACTIVO"))
                                {
                                    MensajedeError = " La Poliza Esta Inactivo ";
                                }
                            }

                        }
                        #endregion

                    }
                }
                else
                {
                    MensajedeError = " El Cliente de la Poliza No Ofrece ese Servicio ";
                }
            }
            catch (Exception e)
            {
                MensajedeError = " Error en la Base De Datos ";
            }
            return MensajedeError;
        }

        private static bool VerificarCliente(int ClienteId, string TipoDePrestacion)
        {

            List<Artexacta.App.RedClientePrestaciones.RedClientePrestaciones> ListaPrestaciones =
                Artexacta.App.RedClientePrestaciones.BLL.RedClientePrestacionesBLL.GetAllClientePrestacionesXClienteId(ClienteId);
            for (int i = 0; i < ListaPrestaciones.Count; i++)
            {
                if (ListaPrestaciones[i].TipoPrestacion.Contains(TipoDePrestacion))
                {


                    return true;


                }
            }
            return false;
        }

        public static string  VerificarEstadoDeLaPoliza(int PolizaId)
        {
            string MensajedeError = "";
            try
            {
            
                List<PolizaValidation> ListaDeDatosPaciente = PolizaBLL.GetAllPolizaValidation(PolizaId, "", 0, 0, 0, "", "");

                if (ListaDeDatosPaciente[0].VigenciaPoliza == false)
                {
                    MensajedeError = "La Poliza Se Ha Vencido ";
                }
                else
                {
                    if (ListaDeDatosPaciente[0].EstadoPoliza.Contains("INACTIVO"))
                    {
                        MensajedeError = " La Poliza Esta Inactivo ";
                    }
                }
            }
            catch (Exception e)
            {
                MensajedeError = " Error en la Base De Datos ";
            }
            return MensajedeError;
        }

         
        public bool VerificarRol(string Rol)
        {
            string UserName = HttpContext.Current.User.Identity.Name;
            Artexacta.App.User.User objUser = UserBLL.GetUserByUsername(UserName);
            string[] RolesForUser = null;
            MembershipUser theUser;
            RolesForUser = Roles.GetRolesForUser(objUser.Username);
            theUser = Membership.GetUser(objUser.Username);
            for (int i = 0; i < RolesForUser.Count(); i++)
            {
                if (RolesForUser[i].ToUpper()==Rol.ToUpper())
                {
                    return true;
                }
            }
            return false;
        }
        public bool VerificarPermisosEmergencia()
        {
            string PermisoInsertarEmergenciaPago = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoInsertarEmergenciaPago"];
            List<string> userPermissions = SecurityBLL.GetUserPermissions();
            if (userPermissions.Contains(PermisoInsertarEmergenciaPago))
                return true;
            else
                return false;
        }
        public bool VerificarPermisosInternacionYCirugia()
        {
            string PermisoInsertarInternacionPago = System.Web.Configuration.WebConfigurationManager.AppSettings["PermisoInsertarInternacionPago"];
            List<string> userPermissions = SecurityBLL.GetUserPermissions();
            if (userPermissions.Contains(PermisoInsertarInternacionPago))
                return true;
            else
                return false;


        }
        public bool VerificarPermisosCopagos()
        {
            string PermisoCoPago = System.Web.Configuration.WebConfigurationManager.AppSettings["VerificarPermisoCopago"];
            List<string> userPermissions = SecurityBLL.GetUserPermissions();
            if (userPermissions.Contains(PermisoCoPago))
                return true;
            else
                return false;
        }

        public static string VerificarServicioProveedorOdontologia(int clienteId,int userId,string Servicio)
        {
            string Respuesta = "";
            try
            {
                Medico.Medico medico = null;
                medico = MedicoBLL.getMedicoByUserId(userId);
                if (medico != null)
                {
                    Proveedor.Proveedor ObjProveedor = ProveedorMedicoBLL.GetProveedorPrecioByMedicoId(medico.MedicoId);
                    List<PrestacionOdontologica> list = PrestacionOdontologicaBLL.getPrestacionAllOdontologicaNew(clienteId, ObjProveedor.ProveedorId);
                    for (int i= 0; i<list.Count;i++)
                    {
                        if (list[i].Nombre.ToUpper()==Servicio.ToUpper())
                        {
                            Respuesta = "";
                            break;
                        }
                        else
                        {
                            Respuesta = "El Usuario proveedor no ofrece este servicio";
                        }
                    }
                    
                    return Respuesta;
                }
                else
                {
                    Respuesta = " El usuario no es un medico";
                    return Respuesta;
                }
        
            }
            catch(Exception ex)
            {
                Respuesta = " Error de base de datos al obtener el usuario Proveedor";
               
            }
            return Respuesta;
        }
        
    }
}