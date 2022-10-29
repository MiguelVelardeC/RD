using Artexacta.App.Configuration;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Security.BLL;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.Utilities.VersionUtilities;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public static Bitacora theBitacora = new Bitacora();
    private List<string> userPermissions;

    protected void Page_Load(object sender, EventArgs e)
    {
        log.Debug("############# Master Page starting Page Load #############");
        BuildFeedbackIframeCode();

        if (!IsPostBack)
        {
            log.Debug("Page is not Postback");

            // Verify the integrity of the system
            VersionUtilities.verifySystemVersionIntegrity();

            log.Debug("Application version is ok");

            /* 
             * Perform security validations for the page
             */

            int userID = 0;

            if (LoginSecurity.IsUserAuthenticated())
            {
                try
                {
                    userID = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                }
                catch (Exception q)
                {
                    log.Error("Failed to get the ID of the current user", q);
                }
            }
            else
            {
                bool redirecToLogin = true;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("ExamenMedicoImprimir.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("Historial.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("HistorialPrint.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("SiniestroImprimir.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("CartaGarantiaImprimir.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("HistorialPrintDESG.aspx"))
                    redirecToLogin = false;
                if (Page.Request.CurrentExecutionFilePath.EndsWith("OrdenDeServicioImprimir.aspx"))
                    redirecToLogin = false;

                if (redirecToLogin)
                    Response.Redirect("~/Authentication/Login.aspx");
            }

            CurrentUserIDHiddenField.Value = userID.ToString();

            bool userIsAdministrator = LoginSecurity.IsUserAdministrator();
            bool currentPageRoleManagement = IsCurrentPageUserOrRoleManagement();
            userPermissions = SecurityBLL.GetUserPermissions();

            if (log.IsDebugEnabled)
            {
                if (userIsAdministrator)
                    log.Debug("Current user IS administrator");
                else
                    log.Debug("Current user IS NOT administrator");

                if (currentPageRoleManagement)
                    log.Debug("Current page IS Role Management");
                else
                    log.Debug("Current page IS NOT Role Management");
            }

            if (userIsAdministrator && currentPageRoleManagement)
            {
                // If the current page is the Role Management or the User Management pages then
                // the administrator user is sufficient for access to those pages.
                // Do nothing. We don't perform any further security checks.
                log.Debug("Current user is Admin and page is Role Management.  No further security checks required");
            }
            else
            {
                log.Debug("Determining if user has access to page");

                // Verify that the user has sufficient access permissions for the page.
                if (!IsUserAuthorizedPage())
                {
                    // Transfer the user to a page that tells him that he is not authorized to 
                    // see that page.
                    Response.Redirect("~/Authentication/NotAuthorized.aspx");
                }
            }

            lblUseName.Text = HttpContext.Current.User.Identity.Name;

            LoadMainMenuScript();
            ConstructMenu(userID);
        }
        else
        {
            // Verify that the user has been authenticated.
            LoginSecurity.EnsureUserAuthentication();
        }

        //BuildFeedbackIframeCode();
        //LoadCalendarItems(CurrentUserIDHiddenField.Value);

        log.Debug("############# Master Page ending Page Load #############");
    }

    private void ConstructMenu(int userID)
    {
        if (userID <= 0)
            return;

        List<Artexacta.App.Menu.Menu> theMenu;
        List<Artexacta.App.Menu.Menu> theVisibleMenu;
        theMenu = Artexacta.App.Menu.MenuBLL.MenuBLL.ReadMenuFromXMLConfiguration();

        List<string> theClases = new List<string>();

        // We have to construct the set of "menu classes" for the user.  These will determine what
        // menus the user has access to.

        if (!LoginSecurity.IsUserAuthenticated())
        {
            Response.Redirect("~/Authentication/Login.aspx");
        }

        if (!userPermissions.Contains("SOLO_VISTA"))
        {
            theClases.Add("CHANGEPASS");

            if (userPermissions.Contains("MANAGE_SECURITY"))
                theClases.Add("SECURITY");

            if (userPermissions.Contains("BITACORA_VER"))
                theClases.Add("BITACORA");


            if (userPermissions.Contains("ADMIN_CLASIFICADORES"))
                theClases.Add("CLASIFICADOR");

            if (userPermissions.Contains("MANAGE_NOTIFICATION"))
                theClases.Add("MANTENIMIENTO");

            if (userPermissions.Contains("REPORTS"))
            {
                theClases.Add("REPORTES");
                theClases.Add("VERSION");
            }

            if (userPermissions.Contains("ADMIN_PACIENTES"))
                theClases.Add("PACIENTE");

            if (userPermissions.Contains("IMPORT_PACIENTES"))
                theClases.Add("PACIENTE_IMPORT");

            if (userPermissions.Contains("SEE_POL"))
            {
                theClases.Add("SEECASOS");
            }
            //agregando los medicos odontologos
            if (userPermissions.Contains("MANAGE_CASOS"))
            {

                string RolAdmin = System.Web.Configuration.WebConfigurationManager.AppSettings["RolAdmin"];
                Artexacta.App.Validacion.BLL.ValidacionBLL obj = new Artexacta.App.Validacion.BLL.ValidacionBLL();
                if (obj.VerificarRol(RolAdmin))
                {
                    theClases.Add("CASOMEDICOLISTA");
                    theClases.Add("CASOODONTOLOGIALISTA");

                }
                else
                {
                    if (Artexacta.App.Medico.BLL.MedicoBLL.getMedicoByUserId(userID) != null)
                    {
                        string EspecialidadOdontologo = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadOdontologo"];
                        theClases.Add("CALENDARIO");
                        if (Artexacta.App.Medico.BLL.MedicoBLL.getMedicoByUserId(userID).EspecialidadId == Artexacta.App.Especialidad.BLL.EspecialidadBLL.GetEspecialidadxNombre(EspecialidadOdontologo).EspecialidadId)
                        {
                            theClases.Add("CASOS");
                            theClases.Add("CASOODONTOLOGIALISTA");
                        }
                        else
                        {
                            string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
                            int EspecialidadId = Artexacta.App.Especialidad.BLL.EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
                            if (Artexacta.App.Medico.BLL.MedicoBLL.getMedicoByUserId(userID).EspecialidadId == EspecialidadId)
                            {
                                theClases.Add("CASOMEDICOLISTA");
                            }


                        }
                    }



                }
            }

            if (userPermissions.Contains("MANAGE_ENFERMERIA"))
            {
                theClases.Add("ENFERMERIA");
            }
            if (userPermissions.Contains("CASO_EMERGENCIA"))
            {
                theClases.Add("EMERGENCIA");
            }

            if (userPermissions.Contains("CALLCENTER"))
                theClases.Add("CALLCENTER");

            if (userPermissions.Contains("IMPORT_NACIONAL_VIDA"))
                theClases.Add("NACIONALVIDA");

            if (userPermissions.Contains("EXPORT_FARMACIA_CHAVEZ"))
                theClases.Add("EXPORT_FARMACIA_CHAVEZ");

            if (userPermissions.Contains("MANAGE_GASTOS_CASOS"))
                theClases.Add("CASOMEDICOGASTOS");

            //agregando el CoPagoLista
            if (userPermissions.Contains("CASOS_LISTAR_COPAGOS"))
            {
                //if (Artexacta.App.Medico.BLL.MedicoBLL.getMedicoByUserId(userID) != null)
                //{
                   
                //        string EspecialidadMG = System.Web.Configuration.WebConfigurationManager.AppSettings["EspecialidadMG"];
                //        int EspecialidadId = Artexacta.App.Especialidad.BLL.EspecialidadBLL.GetEspecialidadxNombre(EspecialidadMG).EspecialidadId;
                //        if (Artexacta.App.Medico.BLL.MedicoBLL.getMedicoByUserId(userID).EspecialidadId == EspecialidadId)
                //        {
                //            theClases.Add("CASOSCOPAGOS");
                //        }

                    
                //}
                //else
                //{

                    theClases.Add("CASOSCOPAGOS");
                //}
              
            }
            ///////////////////////////////////////////
            if (userPermissions.Contains("MANAGE_MEDICS"))
                theClases.Add("MEDIC");

            if (userPermissions.Contains("MANAGE_PROVEEDOR"))
                theClases.Add("PROVEEDOR");

            if (userPermissions.Contains("APPROVAL_CASOS"))
                theClases.Add("APPROVAL_CASOS");

            if (userPermissions.Contains("MANAGE_CONSOLIDACION"))
                theClases.Add("CONSOLIDACION");

            if (userPermissions.Contains("MANAGE_SOAT"))
                theClases.Add("SOAT");

            if (userPermissions.Contains("MANAGE_SOAT_DASHBOARD"))
                theClases.Add("SOAT_ADMIN");

            if (userPermissions.Contains("ADMIN_SOAT_PAGOS") || userPermissions.Contains("INSERT_SOAT_PAGOS"))
                theClases.Add("SOAT_PAGOS");
            if (userPermissions.Contains("ADMIN_SOAT_PAGOS"))
                theClases.Add("SOAT_PRELIQUIDACION");

            if (userPermissions.Contains("CASOS_LISTAR_DERIVACIONES") || userPermissions.Contains("CASOS_LISTAR_DERIVACIONES_ADMIN"))
                theClases.Add("DERIVACIONES_ESPECIALISTA");

            bool manageDesgravamen = userPermissions.Contains("MANAGE_DESGRAVAMEN");
            bool dashboardDesgravamen = userPermissions.Contains("DESGRAVAMEN_DASHBOARD");
            bool estudiosDesgravamen = userPermissions.Contains("MANAGE_LABDESG");
            bool examenMedicoDesgravamen = userPermissions.Contains("DESGRAVAMEN_EXAMEN_MEDICO");
            bool viewCasosEjecutivos = userPermissions.Contains("DESGRAVAMEN_VIEW_CASOS");

            if (userPermissions.Contains("DESGRAVAMEN_ADMIN"))
                theClases.Add("ADMIN_DOCTORS_PROVIDERS");
            if (manageDesgravamen || dashboardDesgravamen || estudiosDesgravamen || examenMedicoDesgravamen)
                theClases.Add("DESGRAVAMEN");

            if (viewCasosEjecutivos)
            {
                theClases.Add("DESGRAVAMEN_VIEW_CASOS_EJECUTIVOS");
            }

            if (manageDesgravamen /*|| examenMedicoDesgravamen*/)
                theClases.Add("DESGRAVAMEN_ADMIN");

            if (dashboardDesgravamen)
            {
                theClases.Add("DESGRAVAMEN_DASHBOARD");
                theClases.Add("DESGRAVAMEN_MEDICO");
            }

            if (estudiosDesgravamen)
                theClases.Add("DESGRAVAMEN_ESTUDIOS");

            if (userPermissions.Contains("DESGRAVAMEN_ELIMINAR_CITA"))
                theClases.Add("DESGRAVAMEN_ELIMINAR");

            if (examenMedicoDesgravamen && !dashboardDesgravamen)
            {
                MedicoDesgravamen objMed = null;
                try
                {
                    objMed = MedicoDesgravamenBLL.GetMedicoDesgravamenByUserId(userID);
                }
                catch (Exception q)
                {
                    log.Warn("El objeto medico es nulo para user " + userID.ToString(), q);
                    objMed = null;
                }
                if (objMed != null)
                    theClases.Add("DESGRAVAMEN_MEDICO");
            }

            bool visorBitacora = userPermissions.Contains("BITACORA_VER");
            if (visorBitacora)
                theClases.Add("BITACORA");
        }
        theVisibleMenu = Artexacta.App.Menu.MenuBLL.MenuBLL.RecursiveConstructionOfVisibleMenus(theMenu, theClases);
        string visibleXML = Artexacta.App.Menu.MenuBLL.MenuBLL.GetMenuXML(theVisibleMenu, 0);
        MainRadMenu.LoadXml(visibleXML);
    }

    /// <summary>
    /// In the HTML header places the script tag and the script loading
    /// </summary>
    private void LoadMainMenuScript()
    {
        StringBuilder scriptText = new StringBuilder("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/jquery-1.11.1.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/jquery.scrollTo.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/jquery.caretPosition.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/jquery-ui-1.10.4.custom.min.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/mainMenu.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<script src=\"");
        scriptText.Append(ResolveClientUrl("~/Scripts/jquery.popup.js"));
        scriptText.Append("\" type=\"text/javascript\"></script>\n");

        scriptText.Append("<link rel=\"icon\" href=\"");
        scriptText.Append(ResolveUrl("~/favicon.ico"));
        scriptText.Append("\" type=\"image/vnd.microsoft.icon\"></script>\n");

        JqueryAndMainMenuScript.Text = scriptText.ToString();
    }

    private bool IsUserAuthorizedPage()
    {
        string currentPage = Page.Request.AppRelativeCurrentExecutionFilePath;

        //Solo ver Casos Medicos*
        string[] soloVista = new string[]{
            "~/CasoMedico/CasoMedicoLista.aspx",
             "~/CasoMedico/CasoMedicoSoloVista.aspx",
          //   "~/CoPagos/CopagoDetail.aspx"
             ////////////////////////////////////////////////////////////////////////////////////
        };



        bool boolView = userPermissions.Contains("SOLO_VISTA");
        if (boolView && currentPage.Equals("~/MainPage.aspx"))
        {
            Response.Redirect("~/CasoMedico/CasoMedicoLista.aspx", true);
        }
        for (int i = 0; i < soloVista.Length; i++)
        {
            if (currentPage.Equals(soloVista[i]) && boolView)
                return true;
        }

        // The following is a list of all the pages that are open to 
        // authenticated users.  These users do not need specific permissions
        // to access the page. 
        string[] openPages = {
             "~/MainPage.aspx",
             "~/Security/ChangePassword.aspx",
             "~/Authentication/UserIsLocked.aspx",
             "~/Authentication/UserIsUnlocked.aspx",
             "~/Authentication/UserNotApproved.aspx",
             "~/About/VersionInformation.aspx",
             "~/Test/TestWSAlianza.aspx",
             "~/Test/TestSeleccionHora.aspx",
             "~/Test/TestPdf.aspx",
             "~/Test/TestEnlaceDesg.aspx",
             "~/Export/ExportParaTerceros.aspx",
             "~/Import/ImportParaTerceros.aspx",
             "~/Calendario/CitasDoctores.aspx",
             "~/Desgravamen/ExamenMedicoImprimir.aspx",
             "~/CasoMedico/Historial.aspx",
             "~/CasoMedico/HistorialPrint.aspx",
             "~/SOAT/SiniestroImprimir.aspx",
             "~/SOAT/CartaGarantiaImprimir.aspx",
             "~/CasoMedico/HistorialPrintDESG.aspx",
          //    "~/CoPagos/CopagoLista.aspx",
            "~/CoPagos/CoPagoDetail.aspx",
            "~/Siniestralidad/ReporteSiniestralidadDetail.aspx",
            "~/Siniestralidad/ReporteSiniestralidadClienteDetail.aspx",
            "~/Desgravamen/OrdenDeServicioImprimir.aspx"
            ///////////////////////////////////////////////
        };

        for (int i = 0; i < openPages.Length; i++)
        {
            if (currentPage.Equals(openPages[i]))
                return true;
        }

        // SECURITY pages
        string[] securityPages = new string[] {
             "~/Security/AssignRoles.aspx",
             "~/Security/AssignRolesByUser.aspx",
             "~/Security/DefinePermissionsByRol.aspx",
             "~/Security/DefinePermissionsByUser.aspx",
             "~/Security/NewRole.aspx",
             "~/Security/UserList.aspx",
             "~/Security/CreateUser.aspx",
             "~/Security/EditUser.aspx",
             "~/Security/UserIsLocked.aspx",
             "~/Security/UserIsUnlocked.aspx",
             "~/Security/UserServiceList.aspx",
             "~/Security/UserService.aspx",
        };

        for (int i = 0; i < securityPages.Length; i++)
        {
            if (currentPage.Equals(securityPages[i]) &&
                userPermissions.Contains("MANAGE_SECURITY"))
                return true;
        }

        #region Clasificadores
        // CLASIFICADORES pages
        string[] clasificadoresPages = new string[]{
              "~/Clasificadores/Enfermedad/EnfermedadList.aspx",
             "~/Clasificadores/Enfermedad/EnfermedadDetails.aspx",

              "~/Clasificadores/TipoEnfermedad/TipoEnfermedadList.aspx",
             "~/Clasificadores/TipoEnfermedad/TipoEnfermedadDetails.aspx",

             "~/Clasificadores/TipoEstudio/TipoEstudioList.aspx",
             "~/Clasificadores/TipoEstudio/TipoEstudioDetails.aspx",

             "~/Clasificadores/Especialidad/EspecialidadList.aspx",
             "~/Clasificadores/Especialidad/EspecialidadDetails.aspx",

             "~/Clasificadores/Protocolo/ProtocoloList.aspx",
             "~/Clasificadores/Protocolo/ProtocoloDetails.aspx",

             "~/Clasificadores/Presentacion/PresentacionList.aspx",
             "~/Clasificadores/Presentacion/PresentacionDetails.aspx",

             "~/Clasificadores/Ciudad/CiudadList.aspx",
             "~/Clasificadores/Ciudad/CiudadDetails.aspx",

              "~/Clasificadores/Medicamento/MedicamentoList.aspx",
             "~/Clasificadores/Medicamento/MedicamentoDetails.aspx"
        };

        for (int i = 0; i < clasificadoresPages.Length; i++)
        {
            if (currentPage.Equals(clasificadoresPages[i]) &&
                userPermissions.Contains("ADMIN_CLASIFICADORES"))
                return true;
        }
        #endregion

        // Notificacion
        string[] NotificacionPages = new string[]{
              "~/Mantenimiento/Notificacion/ConfigurarNotificacion.aspx",
              "~/Mantenimiento/Notificacion/Notificacion.aspx",
              "~/Mantenimiento/Citas/CambioFechas.aspx",
              "~/Mantenimiento/Citas/EstudiosMedicos.aspx"
        };

        for (int i = 0; i < NotificacionPages.Length; i++)
        {
            if (currentPage.Equals(NotificacionPages[i]) &&
                userPermissions.Contains("MANAGE_NOTIFICATION"))
                return true;
        }


        // Medico pages
        string[] medicoPages = new string[]{
             "~/Medico/MedicoList.aspx",
             "~/Medico/MedicoDetails.aspx"
        };

        for (int i = 0; i < medicoPages.Length; i++)
        {
            if (currentPage.Equals(medicoPages[i]) &&
                userPermissions.Contains("MANAGE_MEDICS"))
                return true;
        }
        // Pagos pages
        string[] pagosPages = new string[]{
             "~/SOAT/PagoGastosList.aspx"
        };

        for (int i = 0; i < pagosPages.Length; i++)
        {
            if (currentPage.Equals(pagosPages[i]) &&
                (userPermissions.Contains("ADMIN_SOAT_PAGOS") ||
                userPermissions.Contains("INSERT_SOAT_PAGOS")))
                return true;
        }

        // Proveedor pages
        string[] proveedorPages = new string[]{
             "~/Proveedor/ProveedorList.aspx",
             "~/Proveedor/ProveedorDetails.aspx",
             "~/Proveedor/ProveedorUserList.aspx",
             "~/Proveedor/ProveedorUser.aspx"
        };

        for (int i = 0; i < proveedorPages.Length; i++)
        {
            if (currentPage.Equals(proveedorPages[i]) &&
                userPermissions.Contains("MANAGE_PROVEEDOR"))
                return true;
        }

        #region Report
        // REPORTES pages
        string[] reportesPages = new string[]{
            "~/Reportes/ReporteAsegurado.aspx",
            "~/Reportes/ReporteConsolidado.aspx",
            "~/Reportes/ReporteCasoMedico.aspx",
            "~/Reportes/ReporteCasoMedicoCompleto.aspx",
            "~/Reportes/ReporteLaboratorioPorAtencion.aspx",
            "~/Reportes/ReporteFarmacia.aspx",
            "~/Reportes/ReporteEnfermeria.aspx",
            "~/Reportes/ReporteEmergencia.aspx",
            "~/Reportes/ReporteOdontologia.aspx",
            "~/CasoMedico/CasoMedicoDashboard.aspx",
            "~/Siniestralidad/ReporteSiniestralidad.aspx",
            "~/Siniestralidad/ReporteSiniestralidadCliente.aspx",
            "~/Reportes/ReporteConsolidadoCitas.aspx",
            "~/Reportes/ReporteAtencionesPorMes.aspx",
            "~/Reportes/ReporteDistribucionAtencionPlaza.aspx",
            "~/Reportes/ReporteDiagnosticoPresuntivo.aspx",
            "~/Reportes/ReporteCantidadLlamadasHora.aspx",
            "~/Reportes/ReporteDistribucionAtencionMedico.aspx",
            "~/Reportes/ReporteUsuariosMayorUso.aspx",
            "~/Reportes/ReporteCalificacionesObtenidasMedico.aspx",
            "~/Reportes/ReporteMedicamentosMasRecetados.aspx",
            "~/Reportes/ReporteRecetasExtendidas.aspx",

        };

        for (int i = 0; i < reportesPages.Length; i++)
        {
            if (currentPage.Equals(reportesPages[i]) &&
                userPermissions.Contains("REPORTS"))
                return true;
        }
        #endregion

        #region Pacientes
        // Pacientes pages
        string[] pacientePages = new string[]{
             "~/Paciente/PacienteList.aspx",
             "~/Paciente/PacienteDetails.aspx",
             "~/Paciente/PolizaList.aspx",
             "~/CitasVideoLLamadas/CitasVideoLLamadasList.aspx",
             "~/CitasVideoLLamadas/CitaVideoLLamadaDetails.aspx",
             "~/Paciente/PolizaDetails.aspx"
        };

        for (int i = 0; i < pacientePages.Length; i++)
        {
            if (currentPage.Equals(pacientePages[i]) &&
                userPermissions.Contains("ADMIN_PACIENTES"))
                return true;
        }

        // Import Pacientes pages
        string[] pacienteImportPages = new string[]{
             "~/Paciente/ImportarPaciente.aspx"
        };

        for (int i = 0; i < pacienteImportPages.Length; i++)
        {
            if (currentPage.Equals(pacienteImportPages[i]) &&
                userPermissions.Contains("IMPORT_PACIENTES"))
                return true;
        }
        #endregion

        //Administrar Casos Medicos*
        string[] casosPages = new string[]{
            "~/CasoMedico/CasoMedicoLista.aspx",
             "~/CasoMedico/CasoMedicoDetalle.aspx",
             "~/CasoMedico/CasoMedicoSoloVista.aspx",
             "~/CasoMedico/CasoOdontologico.aspx",
             "~/CasoMedico/CasoMedicoRegistro.aspx",
             "~/Calendario/MiCalendario.aspx",
             "~/CasoMedico/Historial.aspx"
             //"~/CasoMedico/CasoMedicoOdontologiaLista.aspx"
             ///////////////////////////////////////////////////////////
        };
        /* Esto era arriba antes 
             "~/CasoMedico/CasoMedicoLista.aspx",
            "~/CoPagos/CopagoLista.aspx",
             "~/CasoMedico/CasoMedicoDetalle.aspx",
             "~/CasoMedico/CasoMedicoSoloVista.aspx",
             "~/CasoMedico/CasoOdontologico.aspx",
             "~/CasoMedico/CasoMedicoRegistro.aspx",
             "~/Calendario/MiCalendario.aspx",
             "~/CasoMedico/Historial.aspx"
             ///////////////////////////////////////////////////////////
        };
       
        */
 
        bool boolManegeCasos = userPermissions.Contains("MANAGE_CASOS");
        for (int i = 0; i < casosPages.Length; i++)
        {
            if (currentPage.Equals(casosPages[i]) && boolManegeCasos)
                return true;
           
        }
        string[] CoPagosPages = new string[]{
            "~/CoPagos/CopagoLista.aspx"
             //"~/CasoMedico/CasoMedicoOdontologiaLista.aspx"
             ///////////////////////////////////////////////////////////
        };
       
        bool boolManegeCasosres = userPermissions.Contains("CASOS_LISTAR_COPAGOS");
        for (int i = 0; i < CoPagosPages.Length; i++)
        {
            if (currentPage.Equals(CoPagosPages[i]) && boolManegeCasosres)
                return true;
        }



            //Ver Casos Medicos*
            string[] seeCasosPages = new string[]{
            "~/CasoMedico/CasoMedicoRegistro.aspx"
        };
        bool boolSeeCasos = userPermissions.Contains("SEE_POL");
        for (int i = 0; i < seeCasosPages.Length; i++)
        {
            if (currentPage.Equals(seeCasosPages[i]) && boolSeeCasos)
                return true;
        }

        if (currentPage.Equals("~/Calendario/CitasDoctores") &&
            userPermissions.Contains("CALLCENTER"))
            return true;

        //Administrar Casos Medicos por enfermeria*
        string[] EnfermeriaPages = new string[]{
            "~/CasoMedico/CasoMedicoLista.aspx",
             "~/CasoMedico/CasoMedicoRegistro.aspx",
             "~/CasoMedico/Historial.aspx",
             "~/CasoMedico/Enfermeria.aspx",
             "~/CasoMedico/EnfermeriaList.aspx"
        };
        bool boolMANAGE_ENFERMERIA = userPermissions.Contains("MANAGE_ENFERMERIA");
        for (int i = 0; i < EnfermeriaPages.Length; i++)
        {
            if (currentPage.Equals(EnfermeriaPages[i]) && boolMANAGE_ENFERMERIA)
                return true;
        }


        //Administrar Gastos Medicos
        string[] gastosPages = new string[]{
            "~/CasoMedico/CasoMedicoLista.aspx",
             "~/Gasto/GastoDetalle.aspx"
        };

        for (int i = 0; i < gastosPages.Length; i++)
        {
            if (currentPage.Equals(gastosPages[i]) &&
                userPermissions.Contains("MANAGE_GASTOS_CASOS"))
                return true;
        }

        //Aprovar Detalle de Casos Medicos
        string[] aprobarCasosPages = new string[]{
             "~/CasoMedico/ListaCasoPorAprobar.aspx"
        };

        for (int i = 0; i < aprobarCasosPages.Length; i++)
        {
            if (currentPage.Equals(aprobarCasosPages[i]) &&
                userPermissions.Contains("APPROVAL_CASOS"))
                return true;
        }

        //**
        //Administrar consolidacion
        string[] ConsolidacionPages = new string[]{
             "~/Consolidacion/AgregarConsolidacion.aspx",
             "~/Consolidacion/Consolidados.aspx"
        };

        for (int i = 0; i < ConsolidacionPages.Length; i++)
        {
            if (currentPage.Equals(ConsolidacionPages[i]) &&
                userPermissions.Contains("MANAGE_CONSOLIDACION"))
                return true;
        }

        //Administrar RedMedica
        string[] RedMedicaPages = new string[]{
             "~/RedMedica/RedMedicaList.aspx",
             "~/RedMedica/RedMedicaDetails.aspx"
        };

        for (int i = 0; i < RedMedicaPages.Length; i++)
        {
            if (currentPage.Equals(RedMedicaPages[i]) &&
                userPermissions.Contains("MANAGE_REDMEDICA"))
                return true;
        }

        //Administrar Aseguradora
        string[] AseguradoraPages = new string[]{
             "~/Cliente/ClienteList.aspx",
             "~/Cliente/ClienteDetails.aspx"
        };

        for (int i = 0; i < AseguradoraPages.Length; i++)
        {
            if (currentPage.Equals(AseguradoraPages[i]) &&
                userPermissions.Contains("MANAGE_ASEGURADORA"))
                return true;
        }

        //Registrar Caso Medico de Emergencia por un proveedor Hospital
        string[] CasoMedicoEmergenciaPages = new string[]{
             "~/CasoMedico/CasoMedicoRegistro.aspx",
             "~/CasoMedico/Emergencia.aspx",
             "~/CasoMedico/CasoMedicoLista.aspx"

        };

        for (int i = 0; i < CasoMedicoEmergenciaPages.Length; i++)
        {
            if (currentPage.Equals(CasoMedicoEmergenciaPages[i]) &&
                userPermissions.Contains("CASO_EMERGENCIA"))
                return true;
        }

        //Ingresar Siniestros SOAT
        string[] SOATPages = new string[]{
             "~/SOAT/SOATWizard.aspx",
             "~/SOAT/SOATList.aspx",
             "~/SOAT/SiniestroImprimir.aspx",
             "~/SOAT/CartaGarantiaImprimir.aspx",
             "~/SOAT/CartaGarantiaEdit.aspx"
        };

        for (int i = 0; i < SOATPages.Length; i++)
        {
            if (currentPage.Equals(SOATPages[i])
                && userPermissions.Contains("MANAGE_SOAT"))
                return true;
        }

        if (currentPage.Equals("~/SOAT/SOATDashboard.aspx") &&
            userPermissions.Contains("MANAGE_SOAT_DASHBOARD"))
            return true;

        if (currentPage.Equals("~/SOAT/PreliquidacionList.aspx") &&
            userPermissions.Contains("ADMIN_SOAT_PAGOS"))
            return true;

        //Eliminar Caso Medico si no tiene gastos consolidados
        if (currentPage.Equals("~/CasoMedico/CasoMedicoLista.aspx") &&
                userPermissions.Contains("DELETE_CASO"))
            return true;

        #region Desgravamen

        // --- MOdulo DESGRAVAMEN        

        //Dashboard Desgravamen

        if (currentPage.Equals("~/Desgravamen/ReportesDesgravamen.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_DASHBOARD"))
            return true;
        if (currentPage.Equals("~/Desgravamen/ReporteEstudioXPA.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_ESTUDIOXPA"))
            return true;
        if (currentPage.Equals("~/Desgravamen/ReporteEstudioXPANew.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_ESTUDIOXPANEW")) 
            return true;

        if (currentPage.Equals("~/Desgravamen/ReporteEstudiosEstados.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_ESTUDIOXPA"))
            return true;

        if (currentPage.Equals("~/Desgravamen/RecuperacionCitasEliminadas.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_ELIMINAR_CITA"))
            return true;
        string[] DESG_Admin = new string[]{
            "~/Desgravamen/MedicoDesgravamenLista.aspx",
            "~/Desgravamen/MedicoDesgravamenDetalle.aspx",
            "~/Desgravamen/ProveedorDesgravamenLista.aspx",
            "~/Desgravamen/ProveedorDesgravamenDetalle.aspx",
            "~/Desgravamen/ProveedorEstudiosDetalle.aspx"

        };
        for (int i = 0; i < DESG_Admin.Length; i++)
        {
            if (currentPage.Equals(DESG_Admin[i]) &&
                userPermissions.Contains("DESGRAVAMEN_ADMIN"))
                return true;
        }
        //Administracion Desgravamen
        string[] administracionDesgravamenPages = new string[]{
            "~/Desgravamen/PropuestoAsegurado.aspx",
            "~/Desgravamen/PropuestoAseguradoLista.aspx",
            "~/Desgravamen/PropuestoAseguradoCita.aspx",
            "~/Desgravamen/SeleccionHoraLibre.aspx",
            "~/Desgravamen/OrdenDeServicioImprimir.aspx",
            "~/Desgravamen/ExamenMedico.aspx",
            "~/Desgravamen/ExamenMedicoImprimir.aspx",
            "~/CasoMedico/CasoMedicoDetalle.aspx",
            "~/CasoMedico/Historial.aspx"
        };

        for (int i = 0; i < administracionDesgravamenPages.Length; i++)
        {
            if (currentPage.Equals(administracionDesgravamenPages[i]) &&
                userPermissions.Contains("MANAGE_DESGRAVAMEN"))
                return true;
        }

        //Laboratorios para Propuestos Asegurados
        string[] estudiosPAPages = new string[]{
            "~/Desgravamen/LaboratorioPropuestoAsegurado.aspx",
            "~/Desgravamen/DetalleLaboratoriosPropuestoAsegurado.aspx",
            "~/Desgravamen/OrdenDeServicioImprimir.aspx"
        };

        for (int i = 0; i < estudiosPAPages.Length; i++)
        {
            if (currentPage.Equals(estudiosPAPages[i]) &&
                (userPermissions.Contains("MANAGE_LABDESG") ||
                 userPermissions.Contains("DESGRAVAMEN_TODOSLABORATORIOS")))
                return true;
        }

        //Laboratorios para Propuestos Asegurados
        string[] examenMedicoPages = new string[]{
            "~/Desgravamen/ExamenMedico.aspx",
            "~/Desgravamen/ExamenMedicoImprimir.aspx",
            "~/Desgravamen/AgendaMedico.aspx",
            "~/CasoMedico/CasoMedicoDetalle.aspx",
            "~/CasoMedico/Historial.aspx",
            "~/Desgravamen/PropuestoAseguradoLista.aspx",
            "~/Desgravamen/OrdenDeServicioImprimir.aspx"
        };
        for (int i = 0; i < examenMedicoPages.Length; i++)
        {
            if (currentPage.Equals(examenMedicoPages[i]) &&
                userPermissions.Contains("DESGRAVAMEN_EXAMEN_MEDICO"))
                return true;
        }

        #endregion

        if (currentPage.Equals("~/CasoMedico/CasoMedicoLista.aspx") &&
                userPermissions.Contains("DELETE_CASO"))
            return true;

        if (currentPage.Equals("~/Bitacora/ListaEventosBitacora.aspx") &&
                userPermissions.Contains("BITACORA_VER"))
            return true;

        if (currentPage.Equals("~/Desgravamen/PropuestoAseguradoListaEjecutivos.aspx") &&
                userPermissions.Contains("DESGRAVAMEN_VIEW_CASOS"))
            return true;

        if (currentPage.Equals("~/CasoMedico/ListaDerivaciones.aspx") &&
                (userPermissions.Contains("CASOS_LISTAR_DERIVACIONES") ||
                userPermissions.Contains("CASOS_LISTAR_DERIVACIONES_ADMIN")))
            return true;


        // Nothing else worked.  The user should not be allowed to access the page.
        return false;
    }

    private bool IsCurrentPageUserOrRoleManagement()
    {
        string currentPage = Page.Request.CurrentExecutionFilePath;
        if (!String.IsNullOrEmpty(currentPage))
        {
            if (currentPage.EndsWith("Security/DefinePermissionsByRole.aspx") ||
                currentPage.EndsWith("Security/DefinePermissionsByUser.aspx"))
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        string loggedOutUser = HttpContext.Current.User.Identity.Name;
        Session["LoggedOutUser"] = loggedOutUser;
    }

    protected void LoginStatus1_LoggedOut(object sender, EventArgs e)
    {
        theBitacora.RecordTrace(Bitacora.TraceType.UserLogout, Session["LoggedOutUser"].ToString(), "Seguridad", CurrentUserIDHiddenField.Value, "Fin de sesión de usuario");
    }

    private void BuildFeedbackIframeCode()
    {
        bool sotEnabled = false;
        try
        {
            sotEnabled = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["SOTEnabled"]);
        }
        catch (Exception q)
        {
            log.Warn("Either SotEnabled is not defined in the web.config or has an invalid value.", q);
            sotEnabled = false;
        }
        if (sotEnabled)
        {
            string code = "<p>No feedback for SOT</p>";

            FeedbackIframeCode.Text = code;
            return;
        }


        string usernameLogged = HttpContext.Current.User.Identity.Name;
        User objUser = null;
        try
        {
            objUser = UserBLL.GetUserByUsername(usernameLogged);
        }
        catch
        {
            log.Info("Could not load user, dealing with an anonymous user here.");
            objUser = null;
        }

        if (objUser == null) return;

        try
        {
            string username = objUser.Username;
            string email = objUser.Email;
            string application = Configuration.GetApplicationName();
            string applicationKey = Configuration.GetApplicationKey();
            string applicationVersion = VersionUtilities.getApplicationVersionstring();

            string lang = "ES";
            string page = Request.Url.OriginalString;
            string context = "";

            string feedbackUrl = Configuration.GetFeedbackUrl() + "?" +
                (string.IsNullOrEmpty(application) ? "" : "appName=" + application + "&") +
                (string.IsNullOrEmpty(applicationVersion) ? "" : "appVersion=" + applicationVersion + "&") +
                (string.IsNullOrEmpty(applicationKey) ? "" : "appKey=" + applicationKey + "&") +
                (string.IsNullOrEmpty(username) ? "" : "username=" + username + "&") +
                (string.IsNullOrEmpty(email) ? "" : "email=" + email + "&") +
                (string.IsNullOrEmpty(page) ? "" : "page=" + page + "&") +
                (string.IsNullOrEmpty(lang) ? "" : "lang=" + lang + "&") +
                (string.IsNullOrEmpty(context) ? "" : "context=" + context);

            if (feedbackUrl.EndsWith("&"))
                feedbackUrl = feedbackUrl.Substring(0, feedbackUrl.Length - 1);

            string code = "<iframe src=\"" + feedbackUrl + "\" scrolling=\"auto\" " +
                "style=\"width:600px; height:400px; border:solid 1px #fff \"></iframe>";

            FeedbackIframeCode.Text = code;
        }
        catch (Exception err)
        {
            log.Error("Error forming the iframe code for the feedback call", err);
        }
    }
}
