using Artexacta.App.Caso;
using Artexacta.App.Caso.BLL;
using Artexacta.App.Ciudad;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Configuration;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.Security.BLL;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Artexacta.App.Validacion.BLL;
using Artexacta.App.RedCliente.BLL;

public partial class CasoMedico_CasoMedicoRegistro : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private int DataGridPageSize = 20;
    private Artexacta.App.Especialidad.Especialidad esp = null;
    private int ProveedorId;
    private decimal MontoMinimoEnPoliza;
    private decimal PorcentajeSiniestralidadAlerta;
    private List<string> userPermissions;
    private ValidacionBLL ObjValidacion = new ValidacionBLL();
    public int UserId
    {
        get
        {
            return Convert.ToInt32(UserIdHiddenField.Value);
        }
        set
        {
            UserIdHiddenField.Value = value.ToString();
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        VerficarPermisos();
        SearchCasoMedico.OnSearch +=
              new UserControls_SearchUserControl_SearchControl.OnSearchDelegate(searchCtl_CasoMedico_OnSearch);
        SearchCasoMedico.Config = new PolizaSearch();
        PorcentajeSiniestralidadAlerta = Configuration.GetPorcentajeSiniestralidadAlerta();


        int userId = 0;
        try
        {
            userId = UserBLL.GetUserIdByUsername(User.Identity.Name);
            if (userId == 0)
            {
                userId = -1;
                throw new Exception("No se pudo encontrar el id del usuario");
            }
            /*if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_EXAMEN_MEDICO") ||
                Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_VER_TODOSCASOS"))
                userId = 0;*/
        }
        catch (Exception q)
        {
            if (userId < 0)
            {
                SystemMessages.DisplaySystemErrorMessage("No se pudo obtener el id del usuario " + User.Identity.Name);
            }
            // si es un usuario valido se queda con el id de usuario encontrado
        }

        //MedicoUsuarioId hiddenfield Id
        Medico medico = null;
        if (userId > 0)
        {
            try
            {
                medico = MedicoBLL.getMedicoByUserId(userId);
            }
            catch (Exception)
            {
                log.Info("No medico is assigned to that UserId" + userId);
                SystemMessages.DisplaySystemWarningMessage("No tiene un medico asignado a su usuario");
            }


            bool isUserAdmin = false;

            if (userPermissions.Contains("VER_TODOS_LOS_CLIENTES_CASOMEDICO"))
            {
                isUserAdmin = true;
            }
            else
            {
                isUserAdmin = false;
            }

            if (isUserAdmin)
            {
                MedicoUsuarioId.Value = "0";
            }
            else
            {
                if (medico != null)
                {
                    MedicoUsuarioId.Value = medico.MedicoId.ToString();
                }

                else
                {
                    MedicoUsuarioId.Value = "-1";
                }
            }
        }
        if (!IsPostBack)
        {
            loadExtraCombos();
            //BindGrid();
            return;
        }


    }
    private void loadExtraCombos()
    {

        //Check permissions to see what to load
        bool isUserAdmin = false;
        try
        {

            if (userPermissions.Contains("VER_TODOS_LOS_CLIENTES_CASOMEDICO"))
            {
                isUserAdmin = true;
            }
            else
            {
                isUserAdmin = false;
            }
            //////
        }
        catch (Exception q)
        {
            log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
            isUserAdmin = false;
        }
        List<GenericComboContainer> clientes = null;
        try
        {
            if (isUserAdmin)
            {
                clientes = DerivacionBLL.GetClientesByMedicoIdCombo(0);
            }
            else
            {
                int clienteId = 0;
                int.TryParse(MedicoUsuarioId.Value, out clienteId);
                if (clienteId >= 0)
                    clientes = DerivacionBLL.GetClientesByMedicoIdCombo(clienteId);
                if(clienteId == -1 &&
                    Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("IMPORT_NACIONAL_VIDA"))
                {
                    //Se obtiene el id del usuario
                    var usuario = UserBLL.GetUserByUsername(HttpContext.Current.User.Identity.Name);
                    if (usuario != null)
                    {
                        //Se obtiene el id del cliente
                        var cliUsr = Artexacta.App.ClienteUsuario.BLL.ClienteUsuarioBLL.GetClienteUsuarioByUserId(usuario.UserId);
                        if (cliUsr != null)
                        {
                            var cliente = RedClienteBLL.GetRedClienteByClienteId(cliUsr.ClienteId);
                            var comboCliente = new GenericComboContainer
                            {
                                ContainerId = cliente.ClienteId.ToString(),
                                ContainerName = cliente.NombreJuridico,
                            };
                            if (clientes != null)
                            {
                                if(!clientes.Exists(x => x.ContainerId == cliente.ClienteId.ToString()))
                                {
                                    clientes.Add(comboCliente);
                                }
                            }
                            else
                            {
                                clientes = new List<GenericComboContainer>();
                                clientes.Add(comboCliente);
                            }
                        }
                    }                    
                }
            }
        }
        catch (Exception eqq)
        {

        }
        if (isUserAdmin)
        {
            if (clientes != null)
            {
                clientes.Insert(0, new GenericComboContainer()
                {
                    ContainerId = "0",
                    ContainerName = "TODOS"
                });
            }
            else
            {
                clientes = new List<GenericComboContainer>();
                clientes.Add(new GenericComboContainer()
                {
                    ContainerId = "0",
                    ContainerName = "TODOS"
                });
            }
        }

        if (clientes != null)
        {
            ClienteDDL.DataSource = clientes;
            ClienteDDL.DataValueField = "ContainerId";
            ClienteDDL.DataTextField = "ContainerName";
            ClienteDDL.DataBind();
        }
    }

    public void searchCtl_CasoMedico_OnSearch()
    {
        string sql = SearchCasoMedico.Sql;
        log.Debug("Parameter whereSql: " + sql);
        ActivePageHF.Value = "0";
        BindGrid();
    }

    private void VerficarPermisos()
    {
        userPermissions = SecurityBLL.GetUserPermissions();
        bool MANAGE_CASOS = userPermissions.Contains("MANAGE_CASOS");
        bool CASO_EMERGENCIA = userPermissions.Contains("CASO_EMERGENCIA");
        bool MANAGE_ENFERMERIA = userPermissions.Contains("MANAGE_ENFERMERIA");
        bool SEE_POL = userPermissions.Contains("SEE_POL");
        if (Session["MODE"] != null && !string.IsNullOrEmpty(Session["MODE"].ToString()))
        {
            string s = Session["MODE"].ToString();

            switch (Session["MODE"].ToString())
            {
                case "ENFERMERIA":
                    MotivoRegistroHF.Value = "ENFERMERIA";
                    PacienteRadGrid.MasterTableView.GetColumn("CrearCasoMedico").Visible = false;
                    PacienteRadGrid.MasterTableView.GetColumn("Reconsulta").Visible = MANAGE_CASOS;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEnfermeria").Visible = MANAGE_ENFERMERIA;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEmergencia").Visible = false;
                    TitleLabel.Text = "Registro de casos de enfermería";
                    break;
                case "EMERGENCIA":
                    MotivoRegistroHF.Value = "EMERGENCIA";
                    PacienteRadGrid.MasterTableView.GetColumn("CrearCasoMedico").Visible = false;
                    PacienteRadGrid.MasterTableView.GetColumn("Reconsulta").Visible = MANAGE_CASOS;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEnfermeria").Visible = false;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEmergencia").Visible = CASO_EMERGENCIA;
                    TitleLabel.Text = "Registro de casos de emergencias";
                    break;
                case "ODONTOLOGIA":
                    MotivoRegistroHF.Value = "ODONTOLOGIA";
                    PacienteRadGrid.MasterTableView.GetColumn("CrearCasoMedico").Visible = MANAGE_CASOS;
                    PacienteRadGrid.MasterTableView.GetColumn("Reconsulta").Visible = MANAGE_CASOS;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEnfermeria").Visible = false;
                    PacienteRadGrid.MasterTableView.GetColumn("CrearEmergencia").Visible = false;
                    TitleLabel.Text = "Registro de casos Odontologo";
                    break;
                default:
                    ModeHF.Value = " ";
                    break;
            }
        }

    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page CasoMedicoRegistro.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PacienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemMessage("Error al obtener la lista de Pacientes.");
            log.Error("Function PacienteODS_Selected on page CasoMedicoRegistro.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
        //para mandar por session cuando se seleccione un paciente
        this.ClienteIdHF.Value = ClienteDDL.SelectedValue.ToString();
    }
    protected void PacienteRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        int _clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
        string Respuesta = "ERROR";
        string StrCommmand = e.CommandName.ToString();
        if (StrCommmand.Equals("Select") || StrCommmand.Equals("CrearEmergencia") || StrCommmand.Equals("CrearEnfermeria"))
        {
            string MotivoConsultaId = "ACCID"; //ACCID/EMERG/ENFER/ODONTO
            try
            {
                HiddenField PacienteIdHF = (HiddenField)e.Item.FindControl("PacienteId");
                int PacienteId = Convert.ToInt32(PacienteIdHF.Value);
                int PolizaId = Convert.ToInt32(e.CommandArgument);

                Artexacta.App.Especialidad.Especialidad esp = null;
                try
                {
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    esp = EspecialidadBLL.GetEspecialidadByUserId(UserId);
                }
                catch (Exception q)
                {
                    log.Warn("No puede obtener especiadliad para userId " + HttpContext.Current.User.Identity.Name, q);
                    SystemMessages.DisplaySystemErrorMessage("Error al Obtener la Especialidad.");
                }
                if (esp != null && esp.Nombre.StartsWith("ODONTOLOGÍA"))
                {
                    MotivoConsultaId = "ODONTO";
                    string ValorODO = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorODO"];
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    string ConsultaOdontologica = System.Web.Configuration.WebConfigurationManager.AppSettings["ConsultaOdontologica"];
                    Respuesta = ValidacionBLL.ValidacionDePrestacion(PolizaId, _clienteId, ValorODO, ConsultaOdontologica);

                   
                }
                if (StrCommmand.Equals("CrearEmergencia"))
                {
                    MotivoConsultaId = "EMERG";
                    string ValorEM = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorEM"];
                    Respuesta = ValidacionBLL.ValidacionDePrestacion(PolizaId, _clienteId, ValorEM, "");
                  //  int ProveedorId = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name);
                    if (!string.IsNullOrEmpty(Respuesta))
                        SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");
                  /*  else if (ProveedorId <= 0)
                    {
                        SystemMessages.DisplaySystemErrorMessage("No tiene Asignado un Proveedor para agregar una Caso Medico de Emergencia.");
                       // return;
                    }*/
                }
                else if (StrCommmand.Equals("CrearEnfermeria"))
                {
                    MotivoConsultaId = "ENFER";
                    //Activar Esta Linea Si ya Realizaron el Metodos de Copagos Enfermeria
                    //Respuesta = ValidacionBLL.ValidacionDePrestacion(PolizaId, _clienteId, "EN", "");
                    //Mientras Tanto solo se Verificara el Estado de la Poliza
                    Respuesta = ValidacionBLL.VerificarEstadoDeLaPoliza(PolizaId);
                }
                int CasoId = 0;

                if (MotivoConsultaId.Contains("ACCID"))
                {
                    string ValorMG = System.Web.Configuration.WebConfigurationManager.AppSettings["ValorMG"];
                    Respuesta = ValidacionBLL.ValidacionDePrestacion(PolizaId, _clienteId, ValorMG, "");
                }

                if (string.IsNullOrEmpty(Respuesta))
                {
                    CasoId = InsertNewCasoDirty(PolizaId, PacienteId, MotivoConsultaId);
                }
                else
                {
                    SystemMessages.DisplaySystemErrorMessage("No Se Puede Realizar la Transaccion Por Motivo : " + Respuesta + " Para Este Servicio");
                    //   return;
                }

                if (CasoId <= 0)
                {
                    SystemMessages.DisplaySystemErrorMessage("Error al Crear el Caso.");
                  //  return;
                }
                Session["CitaId"] = 1;
                Session["CasoId"] = CasoId;
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al Obtener los id del paciente y de la póliza.");
                log.Error("Error al Obtener los id del paciente y de la póliza.", ex);
                //return;
            }
            Session["Mode"] = "Edit";
            if ((e.CommandName.Equals("Select") || e.CommandName.Equals("CrearEmergencia")) & string.IsNullOrEmpty(Respuesta))
            {
                Session["RECONSULTA"] = null;
                Response.Redirect("CasoMedicoDetalle.aspx");
            }
            else if (e.CommandName.Equals("CrearEmergencia") & string.IsNullOrEmpty(Respuesta))
            {
                Response.Redirect("Emergencia.aspx");
            }
            else if (e.CommandName.Equals("CrearEnfermeria") & string.IsNullOrEmpty(Respuesta))
            {
                Response.Redirect("Enfermeria.aspx");
            }
        }
        else if (StrCommmand.Equals("Reconsulta") & string.IsNullOrEmpty(Respuesta))
        {
            int CasoId = 0;
            try
            {
                CasoId = Convert.ToInt32(e.CommandArgument.ToString());
                Session["CasoId"] = CasoBLL.CreateReconsulta(CasoId).ToString();
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al obtener el id del Casos Medico Seleccionado.");
                log.Error("Function CasoRadGrid_ItemCommand on page CasoMedicoLista.aspx", ex);
                // return;
            }
            Session["RECONSULTA"] = "1";
            Response.Redirect("CasoMedicoDetalle.aspx");
        }
        BindGrid();
    }

    protected void PacienteRadGrid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (GridDataItem)e.Item;
            try
            {
                //Verificar si la polzia esta activa
                Poliza objPoliza = (Poliza)item.DataItem;
                if (objPoliza.Estado == "INACTIVO")
                {
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("CrearCasoMedico").OrderIndex].Text = "";
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("CrearEnfermeria").OrderIndex].Text = "";
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("Reconsulta").OrderIndex].Text = "";
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("CrearEmergencia").OrderIndex].Text = "";
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("Estado").OrderIndex].Style.Add("color", "#F00");
                    return;
                }
                //if (objPoliza.MontoTotal > -1)
                //{
                //    decimal MontoLibrePaciente = objPoliza.MontoTotal - objPoliza.GastoTotal;

                //    //si es menor al MontoMinimoEnPoliza no permitir crear caso
                //    if (MontoLibrePaciente < MontoMinimoEnPoliza)
                //    {
                //        item.CssClass = "MontoMinimoEnPolizaInferior";
                //        Image AlertaImg = (Image)item.FindControl("AlertaImg");
                //        if (AlertaImg != null)
                //        {
                //            AlertaImg.ImageUrl = "~/Images/Neutral/alert-red.gif";
                //            AlertaImg.ToolTip = "El paciente no cuenta con el monto mínimo suficiente para crear un nuevo Caso Medico";
                //            AlertaImg.Visible = true;
                //        }


                //        ImageButton DetailsImageButton = (ImageButton)item.FindControl("DetailsImageButton");
                //        if (DetailsImageButton != null)
                //        {
                //            DetailsImageButton.Enabled = false;
                //            DetailsImageButton.Visible = false;
                //        }

                //        ImageButton EmergenciaImageButton = (ImageButton)item.FindControl("EmergenciaImageButton");
                //        if (EmergenciaImageButton != null)
                //        {
                //            EmergenciaImageButton.Enabled = false;
                //            EmergenciaImageButton.Visible = false;
                //        }
                //    }
                //    else
                //    {
                //        decimal PorcentajeSiniestralidad = 0;
                //        if (objPoliza.MontoTotal != 0)
                //        {
                //            PorcentajeSiniestralidad = (objPoliza.GastoTotal / objPoliza.MontoTotal * 100);
                //        }


                //        if (PorcentajeSiniestralidad > PorcentajeSiniestralidadAlerta)
                //        {
                //            item.CssClass = "PorcentajeSiniestralidadAlerta";
                //            Image AlertaImg = (Image)item.FindControl("AlertaImg");
                //            if (AlertaImg != null)
                //                AlertaImg.Visible = true;
                //        }
                //        else
                //        {
                //            ImageButton DetailsImageButton = (ImageButton)item.FindControl("DetailsImageButton");
                //            if (MotivoRegistroHF.Value != "ODONTOLOGIA")
                //            {
                //                DetailsImageButton.Enabled = false;
                //                DetailsImageButton.Visible = false;
                //            }
                //        }

                //    }
                //}

                int CasoId = 0;
                string MotivoConsultaId = "";
                try
                {
                    CasoId = Convert.ToInt32(DataBinder.Eval(item.DataItem, "CasoId"));
                    if (CasoId > 0)
                    {
                        MotivoConsultaId = CasoBLL.GetMotivoConsultaId(CasoId);
                    }
                }
                catch (Exception ex)
                {
                    log.Error("Function PacienteRadGrid_ItemDataBound on page CasoMedicoRegistro.aspx", ex);
                }

                if (CasoId <= 0 || MotivoConsultaId != "ACCID")
                    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("Reconsulta").OrderIndex].Text = "";

                ProveedorId = UserBLL.GetProveedorIdTheUserName(HttpContext.Current.User.Identity.Name);

                //if (ProveedorId <= 0)
                //{
                //    item.Cells[PacienteRadGrid.Columns.FindByUniqueName("CrearEmergencia").OrderIndex].Text = "";
                //}

                Artexacta.App.Especialidad.Especialidad esp = null;
                try
                {
                    int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
                    esp = EspecialidadBLL.GetEspecialidadByUserId(UserId);
                }
                catch (Exception q)
                {
                    log.Warn("No puede obtener especiadliad para userId " + HttpContext.Current.User.Identity.Name, q);
                }
                if (esp != null && esp.Nombre.StartsWith("ODONTOLOGÍA"))
                {
                    ImageButton DetailsImageButton = (ImageButton)item.FindControl("DetailsImageButton");
                    DetailsImageButton.ToolTip = "Crear Caso de Odontología";
                }
            }
            catch (Exception ex)
            {
                SystemMessages.DisplaySystemErrorMessage("Error al verificar la alerta de siniestralidad del caso medico.");
                log.Error("Function PacienteRadGrid_ItemDataBound on page CasoMedicoRegistro.aspx", ex);
            }
            //GridDataItem da = e.Item as GridDataItem;
            //if (da["CasoCritico"].Text.Equals("True"))
            //{
            //    if (e.Item.ItemType == GridItemType.AlternatingItem)
            //        da.CssClass = "MontoMinimoEnPolizaInferiorAlt";
            //    if (e.Item.ItemType == GridItemType.Item)
            //        da.CssClass = "MontoMinimoEnPolizaInferior";
            //}

            //userPermissions = SecurityBLL.GetUserPermissions();
            //bool MANAGE_CASOS = userPermissions.Contains("MANAGE_CASOS");
            //bool CASO_EMERGENCIA = userPermissions.Contains("CASO_EMERGENCIA");
            //bool MANAGE_ENFERMERIA = userPermissions.Contains("MANAGE_ENFERMERIA");

            //((ImageButton)item.FindControl("DetailsImageButton")).Visible = MANAGE_CASOS;
            //((ImageButton)item.FindControl("ReconsultaImageButton")).Visible = MANAGE_CASOS;
            //((ImageButton)item.FindControl("EnfermeriaImageButton")).Visible = MANAGE_ENFERMERIA;
            //((ImageButton)item.FindControl("EmergenciaImageButton")).Visible = CASO_EMERGENCIA;
        }
    }

    protected int InsertNewCasoDirty(int PolizaId, int PacienteId, string MoticoConsultaId)
    {

        int CasoId = 0;
        try
        {

            string UserName = HttpContext.Current.User.Identity.Name;
            User objUser = UserBLL.GetUserByUsername(UserName);
            Caso objCaso = new Caso();
            /*
            int RedMedicaPaciente = 0;

            int clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
            List<RedMedica> ListRedMedica = RedMedicaBLL.getRedMedicaListByClienteId(clienteId);
            foreach (RedMedica ObjRedMedica in ListRedMedica)
            {
                RedMedicaPaciente = ObjRedMedica.RedMedicaId;
            }*/

            objCaso.Correlativo = 0;
            objCaso.CiudadId = objUser.CiudadId;
            objCaso.UserId = objUser.UserId;
            objCaso.PolizaId = PolizaId;
            objCaso.MotivoConsultaId = MoticoConsultaId;
            objCaso.Estado = "Abierto";//Abierto//Cerrado
            objCaso.Dirty = true;
            objCaso.PacienteId = PacienteId;

            CasoId = CasoBLL.InsertCasoRecordDirty(objCaso);
        }
        catch (Exception ex)
        {
            log.Error("Error to insert new CasoDirty", ex);
        }
        return CasoId;
    }
    #region Pager
    protected void PrimeroButton_Click(object sender, EventArgs e)
    {
        // Vamos a la primera página
        ActivePageHF.Value = "0";

        // Desplegamos los datos
        BindGrid();
    }

    protected void AnteriorRapidoButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a 5 paginas antes
        _activePage -= 5;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void AnteriorButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a la página anterior
        _activePage -= 1;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void SiguienteButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos la próxima página
        _activePage += 1;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void SiguienteRapidoButton_Click(object sender, EventArgs e)
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // Vamos a 5 paginas despues
        _activePage += 5;
        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = _activePage.ToString();

        // Desplegamos los datos
        BindGrid();
    }

    protected void UltimoButton_Click(object sender, EventArgs e)
    {
        int _totalFilas = Convert.ToInt32(TotalFilasHF.Value);
        decimal _activePage = (decimal)_totalFilas / (decimal)DataGridPageSize;

        // Grabar la página en la que queremos estar en el ViewState
        ActivePageHF.Value = ((int)_activePage + ((_activePage == (int)_activePage) ? -1 : 0)).ToString();

        // Desplegamos los datos
        BindGrid();
    }
    #endregion
    private void BindGrid()
    {
        // En esta variable almacenamos el total de filas en la base de datos.  No sólo las cargadas.
        // Por ejemplo, si en la base de datos hay 1,500,000.
        int _totalRows = 0;

        // Ponemos los productos leidos aquí. 
        List<Poliza> _cache = new List<Poliza>();

        int _activePage = Convert.ToInt32(ActivePageHF.Value);

        // No podemos ir antes de la página 0
        if (_activePage < 0)
            _activePage = 0;

        // Averiguamos desde que fila tenemos que cargar.
        int firstRow = _activePage * DataGridPageSize;

        if (firstRow < 0)
            firstRow = 0;

        // Obtener los datos de la BD
        try
        {
            if (string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
            {
                ClienteDDL.DataBind();
            }
            int _clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
            _totalRows = PolizaBLL.GetPolizaBySearchByPolizaOrAseguradoOrPaciente(_cache, _clienteId, DataGridPageSize, firstRow, SearchCasoMedico.Sql, OrderByHF.Value);

            foreach (GridColumn col in PacienteRadGrid.Columns)
            {
                col.HeaderStyle.CssClass = "";

                col.HeaderText = Regex.Replace(col.HeaderText, "\\s<.+>", "");
                if (col.UniqueName == OrderByHF.Value.Replace(" DESC", "").Replace(" ASC", ""))
                {
                    col.HeaderStyle.CssClass = "sortCol";
                    col.HeaderText = col.HeaderText + @" <img class=""sortImg"" style=""width:8px;border-width:0px;"" src=""" +
                        ResolveUrl("~/images/neutral/sort" + (OrderByHF.Value.EndsWith("ASC") ? "Up" : "Down") + ".png") +
                                       @""" title=""ASCENDIENTE"">";
                }
                else if (!col.UniqueName.StartsWith("TemplateColumn"))
                {
                    col.HeaderStyle.CssClass = "sortable";
                }
            }
        }
        catch (Exception q)
        {
            log.Error("No se puedo cargar los datos de la BD", q);
            SystemMessages.DisplaySystemErrorMessage("Error en la busqueda de Casos Médicos");
        }
        // Update the labels that tell us what we did
        LoadedFirstRecordLabel.Text = (firstRow + 1).ToString();
        LoadedNumRecordsLabel.Text = (firstRow + _cache.Count).ToString();
        TotalDBRecordsLabel.Text = _totalRows.ToString();

        // Nos aseguramos que no pasemos la última página 
        if (_activePage > (_totalRows / DataGridPageSize))
            _activePage = _totalRows / DataGridPageSize;

        // Grabar la página en la que estamos en el Viewstate
        ActivePageHF.Value = _activePage.ToString();

        // Registrar los índices de la primera y última fila cargada
        if (_cache.Count == 0)
        {
            PrimeraFilaCargadaHF.Value = "0";
            UltimaFilaCargadaHF.Value = "0";
        }
        else
        {
            PrimeraFilaCargadaHF.Value = (_cache[0].RowNumber - 1).ToString();
            UltimaFilaCargadaHF.Value = (_cache[_cache.Count - 1].RowNumber - 1).ToString();
        }

        // Registrar el total de filas leidas
        TotalFilasHF.Value = _totalRows.ToString();

        // Conectar el repeater con los datos cargados del Web
        PacienteRadGrid.DataSource = _cache;
        PacienteRadGrid.DataBind();

        // Actualizar el estado de habilitado los botones de navegación
        ChangeButtonStates();

        // Al final de esta rutina, los hidden fields PrimeraFilaCargadaHF, UltimaFilaCargadaHF,
        // ActivePageHF y TotalFilasHF tienen los valores de lo que realmente se cargó.
    }
    /// <summary>
    /// Apagamos o prendemos los botones de navegación del Grid2 dependiendo de donde
    /// estamos.
    /// </summary>
    private void ChangeButtonStates()
    {
        int _activePage = Convert.ToInt32(ActivePageHF.Value);
        int _totalFilas = Convert.ToInt32(TotalFilasHF.Value);

        PreevButton.Enabled = true;
        PreevFastButton.Enabled = true;
        NextButton.Enabled = true;
        NextFastButton.Enabled = true;
        FirstButton.Enabled = true;
        LastButton.Enabled = true;

        // Si esamos en la primera, apagar los botones de primera, anterior y anterior -5
        if (_activePage == 0)
        {
            PreevButton.Enabled = false;
            PreevFastButton.Enabled = false;
            FirstButton.Enabled = false;
        }

        // Si estamos en la última fila, apagar los botones de ultima, siguiente y siguiente + 5
        decimal decActivePage = (decimal)_totalFilas / (decimal)DataGridPageSize;
        decActivePage = ((int)decActivePage + ((decActivePage == (int)decActivePage) ? -1 : 0));
        if (_activePage == decActivePage)
        {
            NextButton.Enabled = false;
            NextFastButton.Enabled = false;
            LastButton.Enabled = false;
        }

        // Mostrar el de anterior -5 sólo si estamos pasada la 5
        if (_activePage < 5)
        {
            PreevFastButton.Enabled = false;
        }

        // Mostrar el de siguiente +5 sólo si faltan > 5 páginas para la última
        if (_activePage > _totalFilas / DataGridPageSize - 5)
        {
            NextFastButton.Enabled = false;
        }
    }
    //protected void ClienteDDL_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindGrid();
    //}
    protected void boton_Click(object sender, EventArgs e)
    {
        SearchCasoMedico.Query = this.BuildQuery();
        BindGrid();
    }

    private string BuildQuery()
    {
        string result = "";
        string pacienteNombre = pacienteNombreText.Text.Trim();
        if (!string.IsNullOrEmpty(pacienteNombre))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@NOMBREPACIENTE " + pacienteNombre : result + @"@NOMBREPACIENTE " + pacienteNombre;
            result = result + " ";
        }
        string CI = carnetPacienteText.Text.Trim();
        if (!string.IsNullOrEmpty(CI))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CARNETIDENTIDAD " + CI : result + @"@CARNETIDENTIDAD " + CI;
            result = result + " ";
        }
        string estado = estadoComboBox.SelectedValue;

        if (estadoComboBox.SelectedIndex > 0)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@ESTADO " + estado : @"@ESTADO " + estado;
        }
        string numeroPoliza = polizaText.Text.Trim();
        if (!string.IsNullOrEmpty(numeroPoliza))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@NumeroPoliza " + numeroPoliza : result + @"@NumeroPoliza " + numeroPoliza;
            result = result + " ";
        }
        //string ciudad = (ciudadComboBox.SelectedItem != null) ? ciudadComboBox.SelectedItem.Text : "";
        //if (ciudadComboBox.SelectedIndex > 0 && ciudadComboBox.SelectedItem != null)
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CIUDAD " + ciudad : result + @"@CIUDAD " + ciudad;
        //    result = result + " ";
        //}

        //string cliente = clienteComboBox.SelectedValue;

        //if (clienteComboBox.SelectedIndex > 0)
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CLIENTE " + cliente : @"@CLIENTE " + cliente;
        //}


        //string medicoDerivador = medicoDerivadorComboBox.SelectedValue;//value is userId
        //if (medicoDerivadorComboBox.SelectedIndex > 0)
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADORID " + medicoDerivador : result + @"@MEDICODERIVADORID " + medicoDerivador;
        //    result = result + " ";
        //}

        //if (medicoDerivadoComboBox.Visible)
        //{
        //    string medicoDerivado = medicoDerivadoComboBox.SelectedValue;
        //    if (medicoDerivadoComboBox.SelectedIndex > 0)
        //    {
        //        result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADOID " + medicoDerivado : result + @"@MEDICODERIVADOID " + medicoDerivado;
        //        result = result + " ";
        //    }
        //}
        //string codigoCasoInicial = codigoCasoIdText.Text;
        //if (!string.IsNullOrEmpty(codigoCasoInicial))
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CODIGOCASO " + codigoCasoInicial : result + @"@CODIGOCASO " + codigoCasoInicial;
        //    result = result + " ";
        //}

        //string codigoCasoDerivacion = codigoCasoDerivacionText.Text;
        //if (!string.IsNullOrEmpty(codigoCasoDerivacion))
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CODIGOCASODERIVACION " + codigoCasoDerivacion : result + @"@CODIGOCASODERIVACION " + codigoCasoDerivacion;
        //    result = result + " ";
        //}

        //string pacienteNombre = pacienteNombreText.Text;
        //if (!string.IsNullOrEmpty(pacienteNombre))
        //{
        //    result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@PACIENTENOMBRE " + pacienteNombre : result + @"@PACIENTENOMBRE " + pacienteNombre;
        //    result = result + " ";
        //}

        //DateTime dtFechaInicial = (FechaInicio.SelectedDate != null) ? FechaInicio.SelectedDate.Value : DateTime.MinValue;
        //DateTime dtFechaFinal = (FechaFin.SelectedDate != null) ? FechaFin.SelectedDate.Value : DateTime.MinValue;

        //if (FechaInicio.SelectedDate != null && FechaFin.SelectedDate != null)
        //{
        //    dtFechaInicial = dtFechaInicial.AddDays(-1);
        //    dtFechaFinal = dtFechaFinal.AddDays(+1);
        //}

        //string fechaInicial = (dtFechaInicial != DateTime.MinValue) ? dtFechaInicial.ToString("yyyy-MM-dd") : "";
        //string fechaFinal = (dtFechaFinal != DateTime.MinValue) ? dtFechaFinal.ToString("yyyy-MM-dd") : "";
        //if (!string.IsNullOrEmpty(fechaInicial) && !string.IsNullOrEmpty(fechaFinal))
        //{
        //    result += (string.IsNullOrEmpty(result)) ? "" : " AND ";
        //    result += @" (@FECHACREACION > " + fechaInicial + " AND @FECHACREACION < " + fechaFinal + ")";
        //}

        return result;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportarAExcel();
    }

    private DataTable ConvertListToDataTable(List<Poliza> list)
    {
        DataTable data = new DataTable();

        if (list == null)
            return null;

        if (list.Count <= 0)
            return null;

        //insert columns
        /*
         ESTADO, CODIGO CASO INICIAL, CODIGO CASO ESPECIALISTA, PACIENTE, DERIVADO POR	ESPECIALISTA ASIGNADO, 
         CLIENTE, CIUDAD DE DERIVACION, FECHA DE DERIVACION
        */
        data.Columns.Add("CÓDIGO ASEGURADO");
        data.Columns.Add("NOMBRE PACIENTE");
        data.Columns.Add("NÚMERO DE POLIZA");
        data.Columns.Add("ESTADO");
        data.Columns.Add("FECHA FIN");


        foreach (Poliza searchResult in list)
        {
            data.Rows.Add(searchResult.CodigoAsegurado,
                            searchResult.NombreCompletoPaciente,
                            searchResult.NumeroPoliza,
                            searchResult.Estado,
                            searchResult.FechaFinString);
        }


        return data;
    }
    private void ExportarAExcel()
    {


        try
        {

            SearchCasoMedico.Query = this.BuildQuery();

            string sqlQuery = SearchCasoMedico.Sql;

            int firstRow = 0;
            int _clienteId = Convert.ToInt32(ClienteDDL.SelectedValue);

            List<Poliza> _cache = new List<Poliza>();

            PolizaBLL.GetPolizaBySearchByPolizaOrAseguradoOrPaciente(_cache, _clienteId, 1000000, firstRow, sqlQuery, OrderByHF.Value);

            DataTable data = this.ConvertListToDataTable(_cache);

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("CasoMedicoRegistro");


                ws.Cells["A1"].LoadFromDataTable(data, true);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                //ws.Cells["A1"].LoadFromDataTable(datos, true);
                ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();



                //Format the header for column 1-6
                using (ExcelRange rng = ws.Cells["A1:E1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(153, 153, 153));
                }

                //Example how to Format Column 1 as numeric 
                using (ExcelRange col = ws.Cells[2, 2, 2 + data.Rows.Count - 1, 2])
                {
                    col.Style.Numberformat.Format = "@";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                /*
                string descFechas = FechaInicio.SelectedDate.Value.Month.ToString().PadLeft(2, '0') +
                    FechaInicio.SelectedDate.Value.Day.ToString().PadLeft(2, '0') + "_" +
                    FechaFin.SelectedDate.Value.Month.ToString().PadLeft(2, '0') +
                    FechaFin.SelectedDate.Value.Day.ToString().PadLeft(2, '0');
                */
                //Write it back to the client
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=CasoMedicoRegistro.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }
        catch (Exception q)
        {
            log.Error("Error writing response", q);
            SystemMessages.DisplaySystemErrorMessage("Ocurrió un error al obtener el archivo Excel con la información.");
        }
        finally
        {
            Response.Flush();
            Response.End();
        }
    }
}