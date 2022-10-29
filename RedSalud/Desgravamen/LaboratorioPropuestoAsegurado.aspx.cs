using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Desgravamen;
using System.Text;
using System.IO;
using Artexacta.App.Utilities.Document;
using Artexacta.App.LoginSecurity;
using Telerik.Web.UI;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;

public partial class Desgravamen_LaboratorioPropuestoAsegurado : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int UserId
    {
        get
        {
            int userId = 0;
            try
            {
                userId = Convert.ToInt32(UserIdHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'UserIdHiddenField.Value' to int value", ex);
            }
            return userId;
        }
        set
        {
            UserIdHiddenField.Value = value.ToString();
        }
    }

    public bool IsAdminPanelActive { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        SearchLabo.Config = new PropuestoAseguradoLaboSearchConfig();
        SearchLabo.OnSearch += SearchLabo_OnSearch;        

        if (!IsPostBack)
        {
            if (!LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_ESTUDIOXPA"))
            {
                ReporteLink.Visible = false;
            }

            if (Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_TODOSLABORATORIOS"))
            {
                UserId = 0;
                LaboratorioNombreLabel.Text = "Aquí la lista de todos los clientes con laboratorios";
                //load all required filters here for admins
                AdminPanel.Visible = true;
                IsAdminPanelActive = true;
                clientesComboBox.Visible = false;
                loadProveedoresCombo();
                SearchLabo.Visible = false;
            }
            else
            {

                SearchLabo.Visible = true;
                AdminPanel.Visible = false;
                IsAdminPanelActive = false;
                clientesComboBox.Visible = true;
                UserId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
                List<ProveedorDesgravamen>  objPM = null;
                try
                {
                    objPM = ProveedorMedicoBLL.GetProveedorMedicoByUserId(UserId);
                    if (objPM == null || objPM.Count <= 0)
                        UserId = -1;
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        foreach (ProveedorDesgravamen obj in objPM)
                        {
                            sb.Append(", ").Append(obj.ProveedorNombre);
                        }
                        LaboratorioNombreLabel.Text = "Aquí la lista de los clientes para " + sb.ToString(2, sb.Length - 2);
                    }
                }
                catch (Exception)
                {
                    SystemMessages.DisplaySystemErrorMessage("Ocurrio error al traer proveedor de desgravamen");
                    UserId = -1;
                } 
                
            }
            LoadClientesToCombo();

            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_TODOSLABORATORIOS");
                PropuestoAseguradoGridView.MasterTableView.GetColumn("NombreProveedor").Display = true;
                //NombreMedico
            }
            catch (Exception q)
            {
                log.Warn("El usuario " + User.Identity.Name + " no tiene permisos para administrar estudios, se cargara la pagina sin columnas de administracion ", q);

            }
        }
    }

    void SearchLabo_OnSearch()
    {
        PropuestoAseguradoGridView.DataBind();
    }

    protected void Pager_PageChanged(int row)
    {
        ;
    }

    protected void PropuestoAseguradoDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Ocurrio un error al tratar de obtener la lista de Activos", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("No se pudo cargar la lista de Propuestos Asegurados");
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
        Pager.Visible = true;
        Pager.BuildPagination();
    }
    protected void PropuestoAseguradoGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerEstudios")
        {
              //e.Item;
            //string value =
            GridDataItem dataItem = e.Item as GridDataItem;
            string value = dataItem["EstudioId"].Text;
            Session["EstudioId"] = value;
            Session["CitaDesgravamenId"] = e.CommandArgument;
            Response.Redirect("~/Desgravamen/DetalleLaboratoriosPropuestoAsegurado.aspx");
        }
    }
    protected void PropuestoAseguradoGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            PropuestoAseguradoEstudio objItem = (PropuestoAseguradoEstudio)e.Item.DataItem;

            Image InfoPA = (Image)e.Item.FindControl("InfoPA");

            DateTime masUnDia = DateTime.Now.AddDays(-1);

            if (objItem.FechaAtencion == DateTime.MinValue || objItem.FechaAtencion.Year < 2000)
            //if (objItem.EstadoLabo.ToLowerInvariant() == CitaMedica.EstadoCita.Citada.ToString().ToLowerInvariant() && 
            //    objItem.FechaAtencionLabo == DateTime.MinValue)
            {
                if (objItem.FechaCita > masUnDia)
                {
                    InfoPA.ImageUrl = "../Images/Neutral/verify_disable.png";
                    InfoPA.ToolTip = "La cita agendada y todavía no atendida";
                }
                else
                {
                    InfoPA.ImageUrl = "../Images/Neutral/alert.gif";
                    InfoPA.ToolTip = "La cita fue agendada hace más de 24 horas, y NO FUE Atendida";
                }

                return;
            }

            //if (objItem.EstadoLabo.ToLowerInvariant() == CitaMedica.EstadoCita.Atendida.ToString().ToLowerInvariant())
            if (objItem.FechaAtencion > DateTime.MinValue)
            {
                InfoPA.ImageUrl = "../Images/Neutral/complete.gif";
                InfoPA.ToolTip = "El laboratorio ya fue atendido";

                return;
            }

            InfoPA.Visible = false;
        }
    }
    protected void DocumentosRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
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
    protected void clientesComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //load clientes desgravamen
        updateClienteIdHidden();
        PropuestoAseguradoGridView.Rebind();
    }

    private void LoadClientesToCombo()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        list.Insert(0, new RedCliente()
        {
            ClienteId = 0,
            NombreJuridico = "Todos"
        });
        

        if (AdminPanel.Visible)
        {
            clientesComboBoxAdmin.DataSource = list;
            clientesComboBoxAdmin.DataValueField = "ClienteId";
            clientesComboBoxAdmin.DataTextField = "NombreJuridico";
            clientesComboBoxAdmin.DataBind();
        }
        else
        {
            clientesComboBox.DataSource = list;
            clientesComboBox.DataValueField = "ClienteId";
            clientesComboBox.DataTextField = "NombreJuridico";
            clientesComboBox.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string s = BuildQuery();

        SearchLabo.Query = s;

        updateClienteIdHidden();
        PropuestoAseguradoGridView.Rebind();
    }

    private void updateClienteIdHidden()
    {
        string selectedValue = (AdminPanel.Visible)?clientesComboBoxAdmin.SelectedValue : clientesComboBox.SelectedValue;

        try
        {
            int intSelectedValue = Convert.ToInt32(selectedValue);
            ClienteIdHiddenField.Value = Convert.ToString(intSelectedValue);
        }
        catch (Exception q)
        {
            log.Error("There was an error converting selectedValue to int selectedValue: " + selectedValue, q);
            throw q;
        }
    }
    private void loadProveedoresCombo()
    {
        List<ProveedorDesgravamen> listaProveedores = ProveedorMedicoBLL.GetProveedorMedico();
        listaProveedores.Insert(0, new ProveedorDesgravamen()
        {
            ProveedorMedicoId = 0,
            ProveedorNombre = "Todos"
        });
        ProveedoresComboBox.DataSource = listaProveedores;
        ProveedoresComboBox.DataValueField = "ProveedorMedicoId";
        ProveedoresComboBox.DataTextField = "ProveedorNombre";
        ProveedoresComboBox.DataBind();
    }
    private string BuildQuery()
    {
        string parameterBuilder = "";

        if (ProveedoresComboBox.SelectedValue != "0")
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @CODIGOPROVEEDOR " + ProveedoresComboBox.SelectedValue;
        }

       if (!string.IsNullOrEmpty(CitaId.Text))
        {
            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @CITAID " + CitaId.Text;
        }

        if (!string.IsNullOrEmpty(nombrePropuestoAsegurado.Text))
        {

            parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
            parameterBuilder += @" @NOMBRE " + nombrePropuestoAsegurado.Text;
        }

        if (FechaInicioCita.SelectedDate != null && FechaFinCita.SelectedDate != null
            && FechaInicioCita.SelectedDate != DateTime.MinValue && FechaFinCita.SelectedDate != DateTime.MinValue)
        {
            DateTime dateInicial = FechaInicioCita.SelectedDate.Value;
            DateTime dateFinal = FechaFinCita.SelectedDate.Value;

            dateInicial = dateInicial.AddDays(-1);
            dateFinal = dateFinal.AddDays(1);

            string dtInicial = dateInicial.ToString("yyyy-MM-dd");
            string dtFinal = dateFinal.ToString("yyyy-MM-dd");
            if (!string.IsNullOrEmpty(dtInicial) && !string.IsNullOrEmpty(dtFinal))
            {
                parameterBuilder += (string.IsNullOrEmpty(parameterBuilder)) ? "" : " AND ";
                parameterBuilder += @" (@FECHACITA > " + dtInicial + " AND @FECHACITA < " + dtFinal + ")";
            }


        }


        return parameterBuilder;
    }
}