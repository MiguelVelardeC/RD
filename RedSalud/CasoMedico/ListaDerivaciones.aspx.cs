using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Desgravamen;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Desgravamen.BLL;
using System.IO;
using Artexacta.App.Utilities.Document;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.Documents;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Medico;
using Telerik.Web.UI;
using Artexacta.App.Caso.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Caso;
using Artexacta.App.Poliza.BLL;
using Artexacta.App.Especialidad.BLL;
using Artexacta.App.Poliza;
using Artexacta.App.Derivacion;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Ciudad;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using Artexacta.App.Security.BLL;

public partial class CasoMedico_ListaDerivaciones : Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static Bitacora theBitacora = new Bitacora();
    private List<string> userPermissions;

    /*(public bool UserAuthorizedAprobar
{
get
{
return UserAuthorizedAprobar;
}
set
{
UserAuthorizedAprobar = value;
}
}*/

    protected void Page_Load(object sender, EventArgs e)
    {
        SearchPA.Config = new DerivacionEspecialistaSearchConfig();

        SearchPA.OnSearch += SearchPA_OnSearch;
        userPermissions = SecurityBLL.GetUserPermissions();
        if (!IsPostBack)
        {

            LoadParameters();
            /*try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_ADMIN");
                UserAuthorizedAprobar = true;
            }
            catch (Exception q)
            {
                log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para aprobar usuario ", q);
                UserAuthorizedAprobar = false;
            }*/

            int userId = 0;
            try
            {
                userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
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

                if (medico != null)
                {
                    bool isUserAdmin = false;

                    try
                    {
                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_LISTAR_DERIVACIONES_ADMIN");
                        isUserAdmin = true;
                    }
                    catch (Exception q)
                    {
                        log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
                    }

                    if (isUserAdmin)
                    {
                        MedicoUsuarioId.Value = "0";
                        MedicoDesgravamenGridView.MasterTableView.GetColumn("MedicoNombre").Display = true;
                        MedicoDesgravamenGridView.MasterTableView.GetColumn("EspecialidadNombre").Display = true;
                    }
                    else
                    {
                        MedicoUsuarioId.Value = medico.MedicoId.ToString();
                        medicoDerivadoComboBox.Visible = false;
                        derivadoLabel.Visible = false;
                        EspecialidadDerivadoRadComboBox.Visible = false;
                        LabelEspecialidadDerivado.Visible = false;
                    }
                }
                else
                {
                    bool isUserAdmin = false;

                    try
                    {
                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_LISTAR_DERIVACIONES_ADMIN");
                        isUserAdmin = true;
                    }
                    catch (Exception q)
                    {
                        log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
                    }

                    if (isUserAdmin)
                    {
                        MedicoDesgravamenGridView.MasterTableView.GetColumn("MedicoNombre").Display = true;
                        MedicoDesgravamenGridView.MasterTableView.GetColumn("EspecialidadNombre").Display = true;
                    }
                    else
                    {
                        medicoDerivadoComboBox.Visible = false;
                        derivadoLabel.Visible = false;
                    }

                    MedicoUsuarioId.Value = "0";
                }

                bool isExportToExcelAllowed = false;

                try
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_EXPORTAR_DERIVACIONES");
                    isExportToExcelAllowed = true;
                }
                catch (Exception q)
                {
                    isExportToExcelAllowed = false;
                }

                btnExportExcel.Visible = isExportToExcelAllowed;

                SetupMedicoQuery();
            }
            if (userId < 0)
            {
                Response.Redirect("~/MainPage.aspx");
                return;
            }
            UserIdHiddenField.Value = userId.ToString();

            loadExtraCombo();
            SearchPA.Query = BuildQuery(); //"(" + searchConstant + ")"; 
            //SearchPA.OnSearch +=SearchPA_OnSearch;
        }
    }

    private void SetupMedicoQuery()
    {
        string parameter = "";
        if (MedicoUsuarioId.Value != "0")
        {
            parameter = @"@MEDICODERIVADOID " + MedicoUsuarioId.Value;
            SearchPA.Query = parameter;
        }
    }
    void SearchPA_OnSearch()
    {
        MedicoDesgravamenGridView.DataBind();
    }

    protected void Pager_PageChanged(int row)
    {
        //PropuestoAseguradoGridView.DataBind();
    }

    protected void MedicoDesgravamenDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Ocurrio un error al tratar de obtener la lista de Medicos Desgravamen", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener la lista de Medicos Desgravamen");
            e.ExceptionHandled = true;
        }
        int totalRows = 0;
        try
        {
            totalRows = Convert.ToInt32(e.OutputParameters["totalRows"]);
        }
        catch (Exception ex)
        {
            log.Error("Failed to get OuputParameter 'totalRows'", ex);
        }
        Pager.TotalRows = totalRows;
        if (totalRows == 0)
        {
            Pager.Visible = false;
            return;
        }
        if (Pager.CurrentRow > Pager.TotalRows)
        {
            Pager.CurrentRow = 0;
        }
        Pager.Visible = true;
        Pager.BuildPagination();
    }

    protected void MedicoDesgravamenGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerPA")
        {

            int PacienteId = 0;
            string rawArgument = e.CommandArgument.ToString();
            if (rawArgument != null && !string.IsNullOrEmpty(rawArgument))
            {
                string[] arrArguments = rawArgument.Split('|');

                if (arrArguments.Length > 1)
                {
                    if (int.TryParse(arrArguments[1], out PacienteId))
                    {
                        int derivacionId = 0;
                        int.TryParse(arrArguments[0], out derivacionId);
                        int CasoIdOriginal = 0;
                        DerivacionEspecialista derivacion = null;
                        if (derivacionId > 0)
                        {
                            Session["DerivacionId"] = derivacionId;
                            derivacion = DerivacionBLL.getDerivacionByDerivacionId_NEW(derivacionId);
                            CasoIdOriginal = derivacion.CasoId;
                        }

                        int PolizaId = GetCorrectPolizaId(PacienteId, CasoIdOriginal);
                        if (PolizaId == 0)
                        {
                            SystemMessages.DisplaySystemErrorMessage("El paciente tiene una póliza vencida.");
                            return;
                        }
                        string motivoConsultaId = GetMotivoConsultaId(derivacionId);

                        if (derivacion != null)
                        {
                            int CasoId = (derivacion.CasoIdCreado > 0) ? derivacion.CasoIdCreado : InsertNewCasoDirty(PolizaId, PacienteId, motivoConsultaId);
                            if (CasoId <= 0)
                            {
                                SystemMessages.DisplaySystemErrorMessage("Error al Crear el Caso.");
                                return;
                            }

                            if (derivacion != null && CasoId > 0)
                            {
                                if (derivacion.CasoIdCreado <= 0)
                                {
                                    derivacion.CasoIdCreado = CasoId;
                                    DerivacionBLL.UpdateDerivacionNEW(derivacion);
                                    DerivacionBLL.MarkCasoAsCasoDerivacion(CasoId);
                                    //Session["Mode"] = "Edit";
                                }
                                else
                                {
                                    bool isAutorizadoEditar = false;
                                    try
                                    {
                                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_EDITAR_TODOS");
                                        isAutorizadoEditar = true;
                                    }
                                    catch (Exception q)
                                    {
                                        log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para editar derivaciones", q);
                                    }

                                    if (isAutorizadoEditar)
                                    {
                                        //Session["Mode"] = "Edit";
                                        Session["isApproved"] = "True";
                                    }
                                }

                                Session["CitaId"] = 1;
                                Session["CasoId"] = CasoId;
                                Session["RECONSULTA"] = null;
                                Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx");
                            }
                        }
                        else
                        {
                            SystemMessages.DisplaySystemErrorMessage("Error al obtener datos de la derivacion");
                        }
                        return;
                    }
                }
                else
                {
                    SystemMessages.DisplaySystemErrorMessage("Error de parametros mal formados");
                }
            }
            return;
        }

        if (e.CommandName == "Delete")
        {

            int DerivacionId = 0;
            try
            {
                string id = e.CommandArgument.ToString();
                DerivacionId = Convert.ToInt32(id);
            }
            catch
            {
                log.Error("No pudo convertir " + e.CommandArgument.ToString() + " a id");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                DerivacionEspecialista esp = DerivacionBLL.getDerivacionByDerivacionId_NEW(DerivacionId);

                int CasoCreado = esp.CasoIdCreado;

                if (CasoCreado > 0)
                {
                    SystemMessages.DisplaySystemErrorMessage("No se puede eliminar una derivacion que tiene un Caso Creado");
                }
                else
                {
                    DerivacionBLL.DeleteDerivacion(DerivacionId);
                    SystemMessages.DisplaySystemMessage("Se ha eliminado el caso correctamente");
                }

            }
            catch (Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("La derivacion no pudo ser eliminada");
            }
            MedicoDesgravamenGridView.DataBind();
            return;
        }
    }

    private int GetCorrectPolizaId(int pacienteId, int casoId)
    {
        try
        {
            Caso caso = CasoBLL.GetCasoByCasoId(casoId);

            if (caso != null)
            {
                int polizaId = caso.PolizaId;

                Poliza poliza = (caso != null) ? PolizaBLL.GetPolizaByPolizaId(polizaId) : null;

                if (poliza != null && (poliza.FechaFin > DateTime.UtcNow))
                {
                    return poliza.PolizaId;
                }
                else
                {
                    List<Poliza> polizas = PolizaBLL.GetPolizaByPacienteId(pacienteId);

                    if (polizas != null && polizas.Count > 0)
                    {
                        foreach (Poliza item in polizas)
                        {
                            if (item.FechaFin > DateTime.UtcNow)
                            {
                                return item.PolizaId;
                            }
                        }
                    }

                }

            }
        }
        catch (Exception eq)
        {
            log.Error("An error ocurred while getting Poliza Details", eq);
        }

        return 0;
    }

    private string GetMotivoConsultaId(int derivacionId)
    {
        string motivoConsultaId = "ACCID";

        Artexacta.App.Especialidad.Especialidad esp = null;
        try
        {
            DerivacionEspecialista derivacion = DerivacionBLL.getDerivacionByDerivacionId_NEW(derivacionId);
            //int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);
            if (derivacion != null && derivacion.MedicoId > 0)
            {
                Medico medico = MedicoBLL.getMedicoByMedicoId(derivacion.MedicoId);
                if (medico != null)
                {
                    esp = EspecialidadBLL.GetEspecialidadById(medico.EspecialidadId);
                }
            }
        }
        catch (Exception q)
        {
            log.Warn("No puede obtener especiadliad para userId " + HttpContext.Current.User.Identity.Name, q);
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener la Especialidad.");
        }
        if (esp != null && esp.Nombre.StartsWith("ODONTOLOGÍA"))
        {
            motivoConsultaId = "ODONTO";
        }

        return motivoConsultaId;
    }

    protected int InsertNewCasoDirty(int PolizaId, int PacienteId, string MoticoConsultaId)
    {
        int CasoId = 0;
        try
        {
            string UserName = HttpContext.Current.User.Identity.Name;
            Artexacta.App.User.User objUser = UserBLL.GetUserByUsername(UserName);
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



    protected void enlaceRapidoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(SearchPA.Query))
        {
            SearchPA.Query = ""; //"(" + searchConstant + ")";                
        }
        else
        {
            string q = SearchPA.Query;
            int yaFiltrado = q.LastIndexOf(" AND (");
            if (yaFiltrado < 0)
                yaFiltrado = q.StartsWith("(") && q.EndsWith(")") ? 0 : -1;

            if (yaFiltrado >= 0)
            {
                q = q.Substring(0, yaFiltrado);
                SearchPA.Query = q;
            }
        }

        MedicoDesgravamenGridView.DataBind();
    }
    protected void MedicoDesgravamenGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            DerivacionEspecialistaSearchResult objItem = (DerivacionEspecialistaSearchResult)e.Item.DataItem;

            ImageButton VerPA = (ImageButton)e.Item.FindControl("VerPA");
            ImageButton DelPA = (ImageButton)e.Item.FindControl("DeletePA");

            if (!userPermissions.Contains("CASOS_ELIMINAR_DERIVACION_ESPECIALISTAS"))
            {
                DelPA.Visible = false;
            }
            else
            {
                DelPA.Visible = true;
            }

            if (objItem != null)
            {
                if (!string.IsNullOrEmpty(objItem.CasoCodigoDerivacion))
                {
                    e.Item.CssClass = "Atendida rgRow";
                }
            }

            //InfoPA.Visible = false;
        }
    }
    /*protected void DocumentosRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        FileInfo f = new FileInfo(e.CommandName);
        Response.Clear();
        string mimeType = FileUtilities.GetFileMIMEType(f.Extension);
        if (mimeType != null)
        {
            Response.ContentType = mimeType;
        }
        string path = e.CommandArgument.ToString();
        Response.AddHeader("Content-Disposition", "attachment;Filename=\"" + HttpUtility.UrlPathEncode(f.Name) + "\"");
        Response.AddHeader("Content-Length", new FileInfo(path).Length.ToString());
        Response.WriteFile(path);
        Response.Flush();
        Response.End();
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ProgramacionCitaLabo objItem = (ProgramacionCitaLabo)e.Item.DataItem;

            Repeater DocumentosRepeater = (Repeater)e.Item.FindControl("DocumentosRepeater");
            DocumentosRepeater.DataSource = objItem.LaboratorioFiles;
            DocumentosRepeater.DataBind();
        }
    }*/
    protected void MedicoDesgravamenGridView_DataBound(object sender, EventArgs e)
    {
        /* if (string.IsNullOrWhiteSpace(SearchPA.Query))
         {
             SearchPA.Query = ""; //"(" + searchConstant + ")";                
         }
         else
         {
             string q = SearchPA.Query;
             int yaFiltrado = q.LastIndexOf(" AND (");
             if (yaFiltrado < 0)
                 yaFiltrado = q.StartsWith("(") && q.EndsWith(")") ? 0 : -1;

             if (yaFiltrado >= 0)
             {
                 q = q.Substring(0, yaFiltrado);
                 SearchPA.Query = q;
             }
         }

         MedicoDesgravamenGridView.DataBind();*/

    }
    protected void boton_Click(object sender, EventArgs e)
    {
        SearchPA.Query = this.BuildQuery();
        MedicoDesgravamenGridView.DataBind();
    }

    private List<GenericComboContainer> loadCities()
    {
        //Check permissions to see what to load
        bool isUserAdmin = false;
        try
        {

            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("CASOS_LISTAR_DERIVACIONES_ADMIN");
            isUserAdmin = true;

            //////
        }
        catch (Exception q)
        {
            log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
            isUserAdmin = false;
        }
        List<Ciudad> list = new List<Ciudad>();
        List<GenericComboContainer> combos = new List<GenericComboContainer>();
        string medico = MedicoUsuarioId.Value;

        int medicoId = 0;
        int.TryParse(medico, out medicoId);


        try
        {
            string ciudadId = "";
            if (medicoId > 0)
            {
                Medico med = MedicoBLL.getMedicoByMedicoId(medicoId);
                if (med != null)
                {
                    int user = med.UserId;

                    Artexacta.App.User.User u = UserBLL.GetUserById(user);
                    if (u != null)
                    {
                        ciudadId = u.CiudadId;
                    }
                }
            }
            list = CiudadBLL.getCiudadList();




            if (list != null && list.Count > 0)
            {
                if (!string.IsNullOrEmpty(ciudadId) && !isUserAdmin)
                {
                    foreach (Ciudad c in list)
                    {
                        if (c.CiudadId == ciudadId)
                        {
                            combos.Add(new GenericComboContainer()
                            {
                                ContainerId = c.CiudadId,
                                ContainerName = c.Nombre
                            });
                        }
                    }
                }
                else
                {
                    foreach (Ciudad c in list)
                    {
                        combos.Add(new GenericComboContainer()
                        {
                            ContainerId = c.CiudadId,
                            ContainerName = c.Nombre
                        });
                    }
                }

            }
        }
        catch (Exception)
        {
        }



        return combos;
    }

    private void loadExtraCombo()
    {
        //Check permissions to see what to load
        bool isUserAdmin = false;
        try
        {

            userPermissions.Contains("CASOS_LISTAR_DERIVACIONES_ADMIN");
            isUserAdmin = true;


        }
        catch (Exception q)
        {
            log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para ver derivaciones como administrador", q);
            isUserAdmin = false;
        }
        List<GenericComboContainer> list = loadCities();//CiudadBLL.getCiudadList();
        //loadCities();


        string emptyId = "0";
        string emptyDescription = "TODOS";
        if (list != null)
        {
            list.Insert(0, new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        else
        {
            list = new List<GenericComboContainer>();
            list.Add(new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }

        ciudadComboBox.DataSource = list;
        ciudadComboBox.DataValueField = "ContainerId";
        ciudadComboBox.DataTextField = "ContainerName";
        ciudadComboBox.DataBind();



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
                clientes = DerivacionBLL.GetClientesByMedicoIdCombo(clienteId);
            }
        }
        catch (Exception eqq)
        {

        }
        if (clientes != null)
        {
            clientes.Insert(0, new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        else
        {
            clientes = new List<GenericComboContainer>();
            clientes.Add(new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        if (clientes != null)
        {
            clienteComboBox.DataSource = clientes;
            clienteComboBox.DataValueField = "ContainerId";
            clienteComboBox.DataTextField = "ContainerName";
            clienteComboBox.DataBind();
        }

    }


    private void LoadParameters()
    {
        string emptyId = "0";
        string emptyDescription = "TODOS";

        //load Medico Derivador (userId is selectedValue)

       /* List<GenericComboContainer> medicoDerivado = DerivacionBLL.GetMedicosEspecialista(emptyId);

        if (medicoDerivado != null)
        {
            medicoDerivado.Insert(0, new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        else
        {
            medicoDerivado = new List<GenericComboContainer>();

            medicoDerivado.Add(new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        medicoDerivadoComboBox.DataSource = medicoDerivado;
        medicoDerivadoComboBox.DataValueField = "ContainerId";
        medicoDerivadoComboBox.DataTextField = "ContainerName";
        medicoDerivadoComboBox.DataBind();*/

        List<GenericComboContainer> listDerivadores = DerivacionBLL.GetMedicoUserIdByCiudadCombo(emptyId);

        if (listDerivadores != null)
        {
            listDerivadores.Insert(0, new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        else
        {
            listDerivadores = new List<GenericComboContainer>();
            listDerivadores.Add(new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        medicoDerivadorComboBox.DataSource = listDerivadores;
        medicoDerivadorComboBox.DataValueField = "ContainerId";
        medicoDerivadorComboBox.DataTextField = "ContainerName";
        medicoDerivadorComboBox.DataBind();
    }


    private string BuildQuery()
    {
        string result = "";

        string ciudad = (ciudadComboBox.SelectedItem != null) ? ciudadComboBox.SelectedItem.Text : "";
        if (ciudadComboBox.SelectedIndex > 0 && ciudadComboBox.SelectedItem != null)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CIUDAD " + ciudad : result + @"@CIUDAD " + ciudad;
            result = result + " ";
        }

        string cliente = clienteComboBox.SelectedValue;

        if (clienteComboBox.SelectedIndex > 0)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CLIENTE " + cliente : @"@CLIENTE " + cliente;
        }

        string estado = estadoComboBox.SelectedValue;

        if (estadoComboBox.SelectedIndex > 0)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@ISATENDIDO " + estado : @"@ISATENDIDO " + estado;
        }

        /*
        string derivacion = derivacionIdText.Text;

        if (!string.IsNullOrEmpty(derivacion))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @"AND " + @"@DERIVACION " + derivacion : result + @"@DERIVACION " + derivacion;
            result = result + " ";
        }*/

        string medicoDerivador = medicoDerivadorComboBox.SelectedValue;//value is userId
        if (medicoDerivadorComboBox.SelectedIndex > 0)
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADORID " + medicoDerivador : result + @"@MEDICODERIVADORID " + medicoDerivador;
            result = result + " ";
        }

        if (medicoDerivadoComboBox.Visible)
        {
            string medicoDerivado = medicoDerivadoComboBox.SelectedValue;
            if (medicoDerivadoComboBox.SelectedIndex > 0)
            {
                result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@MEDICODERIVADOID " + medicoDerivado : result + @"@MEDICODERIVADOID " + medicoDerivado;
                result = result + " ";
            }
        }
        string codigoCasoInicial = codigoCasoIdText.Text;
        if (!string.IsNullOrEmpty(codigoCasoInicial))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CODIGOCASO " + codigoCasoInicial : result + @"@CODIGOCASO " + codigoCasoInicial;
            result = result + " ";
        }

        string codigoCasoDerivacion = codigoCasoDerivacionText.Text;
        if (!string.IsNullOrEmpty(codigoCasoDerivacion))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@CODIGOCASODERIVACION " + codigoCasoDerivacion : result + @"@CODIGOCASODERIVACION " + codigoCasoDerivacion;
            result = result + " ";
        }

        string pacienteNombre = pacienteNombreText.Text;
        if (!string.IsNullOrEmpty(pacienteNombre))
        {
            result = (!string.IsNullOrEmpty(result)) ? result + @" AND " + @"@PACIENTENOMBRE " + pacienteNombre : result + @"@PACIENTENOMBRE " + pacienteNombre;
            result = result + " ";
        }

        DateTime dtFechaInicial = (FechaInicio.SelectedDate != null) ? FechaInicio.SelectedDate.Value : DateTime.MinValue;
        DateTime dtFechaFinal = (FechaFin.SelectedDate != null) ? FechaFin.SelectedDate.Value : DateTime.MinValue;

        if (FechaInicio.SelectedDate != null && FechaFin.SelectedDate != null)
        {
            dtFechaInicial = dtFechaInicial.AddDays(-1);
            dtFechaFinal = dtFechaFinal.AddDays(+1);
        }

        string fechaInicial = (dtFechaInicial != DateTime.MinValue) ? dtFechaInicial.ToString("yyyy-MM-dd") : "";
        string fechaFinal = (dtFechaFinal != DateTime.MinValue) ? dtFechaFinal.ToString("yyyy-MM-dd") : "";
        if (!string.IsNullOrEmpty(fechaInicial) && !string.IsNullOrEmpty(fechaFinal))
        {
            result += (string.IsNullOrEmpty(result)) ? "" : " AND ";
            result += @" (@FECHACREACION > " + fechaInicial + " AND @FECHACREACION < " + fechaFinal + ")";
        }

        return result;
    }

    protected void DerivacionesRadGrid_ExcelExportCellFormatting(object sender, ExcelExportCellFormattingEventArgs e)
    {
        e.Cell.Text = e.Cell.Text.ToUpper();

        if (e.FormattedColumn.DataType == typeof(string))
        {
            e.Cell.Style["mso-number-format"] = @"\@";
        }
    }

    private void ExportarAExcel()
    {
        /*
        if (FechaInicio.SelectedDate == null || FechaFin.SelectedDate == null)
        {
            SystemMessages.DisplaySystemMessage("Debe colocar fecha inicio y fin");
            return;
        }*/

        try
        {
            int? totalRows = 0;

            SearchPA.Query = this.BuildQuery();

            string sqlQuery = SearchPA.Sql;

            int medicoId = 0;
            int firstRow = 0;

            int.TryParse(MedicoUsuarioId.Value, out medicoId);

            List<DerivacionEspecialistaSearchResult> listaDerivaciones =
                DerivacionBLL.GetDerivacionEspecialistaBySearch(sqlQuery, 1000000, firstRow, ref totalRows, medicoId);

            DataTable data = this.ConvertListToDataTable(listaDerivaciones);

            using (ExcelPackage pck = new ExcelPackage())
            {
                //Create the worksheet
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("EstudioxXFinanciera");

                /*DataTable datos = Artexacta.App.Desgravamen.BLL.ReporteDesgravamenBLL.GetReporteCantidadEstudiosPorFinanciera(
                    FechaInicio.SelectedDate.Value, FechaFin.SelectedDate.Value, cliente, financiera, estudio, tipoProducto, cobroFinanciera);
                */

                ws.Cells["A1"].LoadFromDataTable(data, true);
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                //ws.Cells["A1"].LoadFromDataTable(datos, true);
                ws.Cells[ws.Dimension.Address.ToString()].AutoFitColumns();



                //Format the header for column 1-6
                using (ExcelRange rng = ws.Cells["A1:J1"])
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
                Response.AddHeader("content-disposition", "attachment;  filename=DerivacionesReporte.xlsx");
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

    private DataTable ConvertListToDataTable(List<DerivacionEspecialistaSearchResult> list)
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
        data.Columns.Add("ESTADO");
        data.Columns.Add("CODIGO CASO INICIAL");
        data.Columns.Add("CODIGO CASO ESPECIALISTA");
        data.Columns.Add("PACIENTE");
        data.Columns.Add("DERIVADO POR");
        data.Columns.Add("ESPECIALISTA ASIGNADO");
        data.Columns.Add("ESPECIALIDAD");
        data.Columns.Add("CLIENTE");
        data.Columns.Add("CIUDAD DE DERIVACION");
        data.Columns.Add("FECHA DE DERIVACION");


        foreach (DerivacionEspecialistaSearchResult searchResult in list)
        {
            data.Rows.Add(searchResult.isAtendidoDisplay,
                            searchResult.CasoCodigoDerivado,
                            searchResult.CasoCodigoDerivacion,
                            searchResult.PacienteNombre,
                            searchResult.DerivadorNombre,
                            searchResult.MedicoNombre,
                            searchResult.EspecialidadNombre,
                            searchResult.ClienteNombre,
                            searchResult.CiudadDerivacionNombre,
                            searchResult.FechaCreacion);
        }


        return data;
    }
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        ExportarAExcel();
    }


    protected void EspecialidadDerivadoRadComboBox_OnClientSelectedIndexChanged(object o, EventArgs e)
    {
        int idEspecialidad = 0;
        if (EspecialidadDerivadoRadComboBox.SelectedValue.Length > 0)
            idEspecialidad = int.Parse(EspecialidadDerivadoRadComboBox.SelectedValue);

        LoadParametersDerivado(idEspecialidad);


    }
    private void LoadParametersDerivado(int EspecialidadId)
    {
        string emptyId = "0";
        string emptyDescription = "TODOS";

        //load Medico Derivador (userId is selectedValue)

        List<GenericComboContainer> medicoDerivado = DerivacionBLL.GetMedicosEspecialistaxCiuAndEsp(emptyId, EspecialidadId);

        if (medicoDerivado != null)
        {
            medicoDerivado.Insert(0, new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        else
        {
            medicoDerivado = new List<GenericComboContainer>();

            medicoDerivado.Add(new GenericComboContainer()
            {
                ContainerId = emptyId,
                ContainerName = emptyDescription
            });
        }
        medicoDerivadoComboBox.DataSource = medicoDerivado;
        medicoDerivadoComboBox.DataValueField = "ContainerId";
        medicoDerivadoComboBox.DataTextField = "ContainerName";
        medicoDerivadoComboBox.DataBind();


    }
    

}