using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_UserService : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["UsuarioServicioID"] == null)
        {
            this.divGrid.Visible = false;
            return;
        }
        if (Request["UsuarioServicioCliente"] != null)
        {
            int id = 0;
            int.TryParse(Request["UsuarioServicioCliente"].ToString(), out id);
            using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
            {
                var cliente = db.tbl_UsuarioServicioCliente.Where(x => x.UsuarioServicioCliente == id).FirstOrDefault();
                if (cliente != null)
                {
                    db.tbl_UsuarioServicioCliente.Remove(cliente);
                    db.SaveChanges();
                }
                Response.Redirect(string.Format("~/Security/UserService.aspx?UsuarioServicioID={0}", Request["UsuarioServicioID"].ToString()));
            }
        }
        if (!IsPostBack)
        {
            int id = 0;
            int.TryParse(Request["UsuarioServicioID"].ToString(), out id);
            this.txtUsername.ReadOnly = true;
            using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
            {
                var user = db.tbl_UsuarioServicio.Where(x => x.UsuarioServicioID == id).FirstOrDefault();
                this.txtUsername.Text = user.Usuario;
                this.txtPassword.Text = user.Contrasena;
                this.chkHabilitado.Checked = user.Habilitado;

                var clientes = db.tbl_UsuarioServicioCliente.Where(x=>x.UsuarioServicioID == id).Select(x=> new { x.UsuarioServicioCliente, x.UsuarioServicioID,x.ClienteID,x.tbl_RED_Cliente.CodigoCliente, x.tbl_RED_Cliente.NombreJuridico })
                    .OrderBy(x=>x.NombreJuridico).ToList();
                this.ClientesGridView.DataSource = clientes;
                this.ClientesGridView.DataBind();

                var clientesCod = db.tbl_RED_Cliente.OrderBy(x=>x.NombreJuridico).ToList();
                this.lstCodigoClientes.DataSource = clientesCod;
                this.lstCodigoClientes.DataValueField = "ClienteId";
                this.lstCodigoClientes.DataTextField = "NombreJuridico";
                this.lstCodigoClientes.DataBind();
            }
        }
    }

    protected void InsertButton_Click(object sender, EventArgs e)
    {
        if (Request["UsuarioServicioID"] != null)
        {
            int id = 0;
            int.TryParse(Request["UsuarioServicioID"].ToString(), out id);

            using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
            {
                var user = db.tbl_UsuarioServicio.Where(x => x.UsuarioServicioID == id).FirstOrDefault();
                user.Contrasena = this.txtPassword.Text;
                user.Habilitado = this.chkHabilitado.Checked;
                db.SaveChanges();
            }
        }
        else
        {
            using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
            {
                var user = new Cognos.Negocio.tbl_UsuarioServicio()
                {
                    Usuario = this.txtUsername.Text,
                    Contrasena = this.txtPassword.Text,
                    Habilitado = this.chkHabilitado.Checked,
                    Email = "",
                };
                db.tbl_UsuarioServicio.Add(user);
                db.SaveChanges();
                Response.Redirect(string.Format("~/Security/UserService.aspx?UsuarioServicioID={0}",user.UsuarioServicioID));
            }
        }
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/UserServiceList.aspx");
    }


    protected void btnInsertar_Click(object sender, EventArgs e)
    {
        if (this.lstCodigoClientes.SelectedIndex < 0 || this.lstCodigoClientes.SelectedValue == string.Empty)
        {
            return;
        }
        int clienteID = 0;
        int.TryParse(this.lstCodigoClientes.SelectedItem.Value, out clienteID);
        int id = 0;
        int.TryParse(Request["UsuarioServicioID"].ToString(), out id);

        using (Cognos.Negocio.Negocio db = new Cognos.Negocio.Negocio())
        {
            var clienteExiste = db.tbl_UsuarioServicioCliente.Where(x => x.ClienteID == clienteID && x.UsuarioServicioID == id).FirstOrDefault();
            if (clienteExiste != null)
            {

                return;
            }
            var cliente = new Cognos.Negocio.tbl_UsuarioServicioCliente()
            {
                ClienteID = clienteID,
                UsuarioServicioID = id,
            };
            db.tbl_UsuarioServicioCliente.Add(cliente);
            db.SaveChanges();
            Response.Redirect(string.Format("~/Security/UserService.aspx?UsuarioServicioID={0}", id));
        }
    }
}