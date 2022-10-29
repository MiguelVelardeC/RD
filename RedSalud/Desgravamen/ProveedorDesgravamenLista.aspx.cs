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

public partial class Desgravamen_ProveedorDesgravamenLista : Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private static Bitacora theBitacora = new Bitacora();
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
        SearchPA.Config = new ProveedorDesgravamenSearchConfig();
        SearchPA.OnSearch += SearchPA_OnSearch;

        if (!IsPostBack)
        {
            SearchPA.Query = "";//"(" + searchConstant + ")";             

            int userId = 0;
            try
            {
                userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
                if (userId == 0)
                {
                    userId = -1;
                    throw new Exception("No se pudo encontrar el id del usuario");
                }
                
            }
            catch (Exception q)
            {
                if (userId < 0) {
                    SystemMessages.DisplaySystemErrorMessage("No se pudo obtener el id del usuario " + User.Identity.Name);    
                }
                // si es un usuario valido se queda con el id de usuario encontrado
            }

            if (userId < 0)
            {
                Response.Redirect("~/MainPage.aspx");
                return;
            }
            UserIdHiddenField.Value = userId.ToString();
            loadCiudadesCombo();
        }
    }

    void SearchPA_OnSearch()
    {
        ProveedorDesgravamenGridView.DataBind();
    }

    protected void Pager_PageChanged(int row)
    {
        //PropuestoAseguradoGridView.DataBind();
    }

    protected void ProveedorDesgravamenDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
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

    protected void ProveedorDesgravamenGridView_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "VerPA")
        {
            Session["ProveedorMedicoId"] = e.CommandArgument;
            Response.Redirect("~/Desgravamen/ProveedorDesgravamenDetalle.aspx");
            return;
        }
        
        if (e.CommandName == "Delete")
        {
            
            int ProveedorDesgravamenId = 0;
            try
            {
                string id = e.CommandArgument.ToString();
                ProveedorDesgravamenId = Convert.ToInt32(id);                
            }
            catch {
                log.Error("No pudo convertir " + e.CommandArgument.ToString() + " a id");
                SystemMessages.DisplaySystemErrorMessage("No se llamó a eliminar de manera correcta, intente de nuevo");
                return;
            }

            try
            {
                int oldId = ProveedorDesgravamenId;
                ProveedorMedicoBLL.DeleteProveedorMedico(ProveedorDesgravamenId);
                log.Info("El Proveedor " + oldId + " ha sido eliminado por el usuario " + User.Identity.Name);
                theBitacora.RecordTrace(Bitacora.TraceType.DESGEliminarProveedor, User.Identity.Name, "Desgravamen", ProveedorDesgravamenId.ToString(), "El Id es el ProveedorId");
                SystemMessages.DisplaySystemMessage("El Proveedor Desgravamen ha sido eliminado");
                
            }
            catch(Exception q)
            {
                log.Error("No pudo ser eliminado", q);
                SystemMessages.DisplaySystemErrorMessage("El medico no pudo ser eliminado");
            }
            ProveedorDesgravamenGridView.DataBind();
            return;
        }
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

        ProveedorDesgravamenGridView.DataBind();
    }
    protected void ProveedorDesgravamenGridView_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem ||
            e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
        {
            ProveedorDesgravamen objItem = (ProveedorDesgravamen)e.Item.DataItem;

            ImageButton VerPA = (ImageButton)e.Item.FindControl("VerPA");
            ImageButton DelPA = (ImageButton)e.Item.FindControl("DeletePA");

            if (!Artexacta.App.LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("DESGRAVAMEN_ADMIN"))
            {
                VerPA.Visible = false;
                DelPA.Visible = false;
            }
            else
            {
                VerPA.Visible = true;
                DelPA.Visible = true;
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
    protected void ProveedorDesgravamenGridView_DataBound(object sender, EventArgs e)
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

    private void loadCiudadesCombo()
    {
        List<ComboContainer> list = CiudadesDesgravamenBLL.GetCiudadesDesgravamenCombo();
        list.Insert(0, new ComboContainer()
        {
            ContainerId = "0",
            ContainerName = "TODOS"
        });
        ciudadComboBox.DataSource = list;
        ciudadComboBox.DataValueField = "ContainerId";
        ciudadComboBox.DataTextField = "ContainerName";
        ciudadComboBox.DataBind();
    }

    private string BuildQuery()
    {
        string queryBuilder = "";

        if (!string.IsNullOrEmpty(ProveedorNombreTextBox.Text))
        {
            if (!string.IsNullOrEmpty(queryBuilder))
            {
                queryBuilder += " AND ";
            }
            queryBuilder += @"@PROVEEDOR "+ProveedorNombreTextBox.Text;
        }

        if (!string.IsNullOrEmpty(ProveedorUsuarioTextBox.Text))
        {
            if (!string.IsNullOrEmpty(queryBuilder))
            {
                queryBuilder += " AND ";
            }
            queryBuilder += @"@USUARIO " + ProveedorUsuarioTextBox.Text;
        }

        if (!string.IsNullOrEmpty(ciudadComboBox.SelectedValue) && ciudadComboBox.SelectedValue != "0")
        {
            if (!string.IsNullOrEmpty(queryBuilder))
            {
                queryBuilder += " AND ";
            }
            queryBuilder += @"@CIUDAD " + ciudadComboBox.SelectedItem.Text;
        }
        return queryBuilder;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchPA.Query = BuildQuery();
        ProveedorDesgravamenGridView.Rebind();
    }
}