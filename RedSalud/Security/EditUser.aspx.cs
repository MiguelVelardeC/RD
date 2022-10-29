using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.UserClienteSOAT.BLL;
using Telerik.Web.UI;
using Artexacta.App.Security.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Configuration;

public partial class Security_EditUser : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        FotoFileUpload.FilesLoaded += FotoFileUpload_FilesLoaded;
        FotoFileUpload.MaxFileInputCount = 1;

        if (!IsPostBack)
        {
            ProcessSessionParameteres();
            if (UsernameHiddenField.Value != null && UsernameHiddenField.Value.Length > 0)
            {
                GetUserDetails(UsernameHiddenField.Value.ToString());
            }
            else
            {
                Response.Redirect("~/Security/UserList.aspx");
            }

            loadDigitalSignatureDimensions();

            //FirmaDigitalDimensions
            //DESGExecutiveRoleName

        }
    }

    private void loadDigitalSignatureDimensions()
    {
        int[] rawDATA = Configuration.GetDigitalSignatureDimension();
        FotoSignatureUrl.Width = rawDATA[0];
        FotoSignatureUrl.Height = rawDATA[1];
    }


    public int FotoId
    {
        get
        {
            int fotoId = 0;
            try
            {
                fotoId = Convert.ToInt32(FotoSignatureIDHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return fotoId;
        }
        set
        {
            if (value < 0)
                FotoSignatureIDHiddenField.Value = "0";
            else
                FotoSignatureIDHiddenField.Value = value.ToString();
        }
    }

    private void ProcessSessionParameteres()
    {
        if (Session["USERID"] != null)
        {
            string initUserName = "";
            initUserName = (string)Session["USERID"];
            if (!string.IsNullOrEmpty(initUserName))
            {
                UsernameHiddenField.Value = initUserName;
            }
        }
        Session["USERID"] = null;
    }

    protected void GetUserDetails(string UserName)
    {

        if (String.IsNullOrEmpty(UserName))
        {
            log.Error("Se recibió un usuario vació o nulo.");
            SystemMessages.DisplaySystemMessage("Se recibió un usuario vació o nulo.");
        }

        UsernameLabel.Text = "[ " + UserName + " ]";
        MembershipUser MemUser = null;

        try
        {
            MemUser = Membership.GetUser(UserName);
        }
        catch (Exception q)
        {
            log.Error("Error al intentar obtener detalles del usuario " + UserName, q);
            SystemMessages.DisplaySystemMessage("Error al intentar obtener detalles del usuario: " + UserName + ".");
        }

        if (MemUser == null)
        {
            log.Error("No se pudo encontrar al usuario " + UserName + " en la lista de cuentas ASP.NET.");

            Session["ErrorMessage"] = "No se encontró el usuario indicado.";
            Response.Redirect("~/FatalError.aspx");
        }

        try
        {
            EmailTextBox.Text = MemUser.Email.ToString();
            EmailHiddenField.Value = EmailTextBox.Text;

            User theUser = UserBLL.GetUserByUsername(UserName);
            if (theUser != null)
            {
                NameTextBox.Text = theUser.FullName;
                AddressTextBox.Text = theUser.Address;
                CellPhoneTextBox.Text = theUser.CellPhone;
                CiudadAreaTextBox.Text = theUser.PhoneArea.ToString();
                NumeroTextBox.Text = theUser.PhoneNumber;
                PaisAreaTextBox.Text = theUser.PhoneCode.ToString();
                UserIdHiddenField.Value = theUser.UserId.ToString();
                this.CiudadDDL.SelectedValue = theUser.CiudadId;

                string rol_ejecutivo = Artexacta.App.Configuration.Configuration.GetDESGExecutiveRoleName();

                bool isInRole = SecurityBLL.UserInRole(UserName, rol_ejecutivo);
                ClienteDesgravamenAsociado.Visible = isInRole;

                if (ClienteDesgravamenAsociado.Visible)
                {
                    LoadClientesToCombo();
                    int clientId = ClienteUsuarioBLL.GetClienteByUsuarioId(theUser.UserId);
                    clientesComboBox.SelectedValue = OldClienteIdHiddenField.Value = (clientId > 0) ? clientId.ToString() : "0";
                }

                //Se verfica si tiene el rol de importador
                bool isImport = SecurityBLL.UserInRole(UserName, "ImportarNacionalVida");
                //Si no tiene el rol se verifica si tiene el permiso
                if (!isImport)
                {
                    isImport = SecurityBLL.IsUserAllowedToPerformPermission(UserName, "IMPORT_NACIONAL_VIDA");
                }
                ClienteUsuarioAsociado.Visible = isImport;

                if (ClienteUsuarioAsociado.Visible)
                {
                    LoadClientesUsuarioToCombo();
                    var cliUsr = Artexacta.App.ClienteUsuario.BLL.ClienteUsuarioBLL.GetClienteUsuarioByUserId(theUser.UserId);
                    clientesUsuarioComboBox.SelectedValue =
                        OldClienteUsuarioIdHiddenField.Value = 
                        (cliUsr.ClienteId > 0) ? cliUsr.ClienteId.ToString() : "0";
                }

                FileIdHiddenField.Value = theUser.SignatureFileId.ToString();
                int[] imageDimensions = Configuration.GetDigitalSignatureDimension();
                if (theUser.SignatureFileId > 0)
                {
                    FotoSignatureUrl.ImageUrl = "~/ImageResize.aspx?ID=" + theUser.SignatureFileId.ToString() + "&W=" + imageDimensions[0] + "&H=" + imageDimensions[1];
                    FotoSignatureUrl.Visible = true;
                }
            
            }
        }
        catch (Exception q)
        {
            log.Error("Cannot get user data", q);
            SystemMessages.DisplaySystemMessage("Error al obtener los datos del usuario.");
        }
    }
    private void LoadClientesToCombo()
    {
        List<RedCliente> list = RedClienteBLL.GetClientesDesgravamen();
        List<RedCliente> modifiedList = new List<RedCliente>();
        modifiedList.Add(new RedCliente()
        {
            ClienteId = 0,
            NombreJuridico = "Ninguno"
        });
        foreach (RedCliente cliente in list)
        {
            modifiedList.Add(cliente);
        }

        clientesComboBox.DataSource = modifiedList;
        clientesComboBox.DataValueField = "ClienteId";
        clientesComboBox.DataTextField = "NombreJuridico";
        clientesComboBox.DataBind();
    }
    private void LoadClientesUsuarioToCombo()
    {
        //List<RedCliente> list = RedClienteBLL.getAllRedClienteList();

        //List<RedCliente> modifiedList = new List<RedCliente>();
        //modifiedList.Add(new RedCliente()
        //{
        //    ClienteId = 0,
        //    NombreJuridico = "Ninguno"
        //});
        //foreach (RedCliente cliente in list)
        //{
        //    modifiedList.Add(cliente);
        //}

        var clientes = Artexacta.App.Derivacion.BLL.DerivacionBLL.GetClientesByMedicoIdCombo(0);
        clientes.Insert(0, new Artexacta.App.GenericComboContainer.GenericComboContainer()
        {
            ContainerId = "0",
            ContainerName = "NINGUNO"
        });

        //clientesUsuarioComboBox.DataSource = modifiedList;
        clientesUsuarioComboBox.DataSource = clientes;
        //clientesUsuarioComboBox.DataValueField = "ClienteId";
        //clientesUsuarioComboBox.DataTextField = "NombreJuridico";
        clientesUsuarioComboBox.DataValueField = "ContainerId";
        clientesUsuarioComboBox.DataTextField = "ContainerName";
        clientesUsuarioComboBox.DataBind();
    }




    protected void SaveButton_Click(object sender, EventArgs e)
    {
        bool sucess = false;
        string userName = UsernameHiddenField.Value;

        if (!Page.IsValid)
            return;

        try
        {
            if (EmailTextBox.Text != EmailHiddenField.Value)
            {
                string user = Membership.GetUserNameByEmail(EmailTextBox.Text);

                if (!String.IsNullOrEmpty(user) && !user.Equals(userName))
                {
                    SystemMessages.DisplaySystemErrorMessage("El correo electrónico ya está registrado para otro usuario.");
                    return;
                }

                MembershipUser theUser = null;
                theUser = Membership.GetUser(userName);

                if (theUser != null)
                {
                    theUser.Email = EmailTextBox.Text;
                    Membership.UpdateUser(theUser);
                }
                else
                {
                    SystemMessages.DisplaySystemMessage("No se pudo obtener información [ASPDB] del usuario:" + userName + ".");
                    return;
                }
            }

            sucess = true;
        }
        catch (Exception q)
        {
            log.Error("Cannot update user email in database", q);
            SystemMessages.DisplaySystemMessage("No se pudo modificar el correo electrónico del Usuario.");
        }

        if (sucess)
        {
            try
            {
                if (!UserBLL.UpdateUserRecord(Convert.ToInt32(UserIdHiddenField.Value),
                        userName,
                        NameTextBox.Text,
                        CellPhoneTextBox.Text,
                        AddressTextBox.Text,
                        NumeroTextBox.Text,
                        Convert.ToInt32(CiudadAreaTextBox.Text),
                        Convert.ToInt32(PaisAreaTextBox.Text),
                        EmailTextBox.Text,
                        CiudadDDL.SelectedValue))
                {
                    SystemMessages.DisplaySystemMessage("No se pudo modificar el Usuario " + userName + ".");
                }
                else
                {
                    SystemMessages.DisplaySystemMessage("Se modificó los datos del usuario satisfactoriamente.");
                }

                if (OldClienteIdHiddenField.Value != clientesComboBox.SelectedValue)
                {
                    
                        int intClienteId = Convert.ToInt32(clientesComboBox.SelectedValue);
                        int intOldClienteId = (!string.IsNullOrEmpty(OldClienteIdHiddenField.Value)) ? Convert.ToInt32(OldClienteIdHiddenField.Value) : 0;
                        int intUserId = Convert.ToInt32(UserIdHiddenField.Value);
                    if (clientesComboBox.SelectedValue != "0" && OldClienteIdHiddenField.Value != "0")
                    {
                        ClienteUsuarioBLL.SaveClienteUsuario(intUserId, intClienteId, intOldClienteId);
                        SystemMessages.DisplaySystemMessage("Se ha asignado el usuario ejecutivo exitosamente");
                    }
                    else if (clientesComboBox.SelectedValue == "0" && OldClienteIdHiddenField.Value != "0")
                    {
                        ClienteUsuarioBLL.DeleteClienteUsuario(intUserId, intOldClienteId);
                        SystemMessages.DisplaySystemMessage("Se ha eliminado la asignacion exitosamente");
                    }
                }

                if (OldClienteUsuarioIdHiddenField.Value != clientesUsuarioComboBox.SelectedValue)
                {

                    int intClienteId = Convert.ToInt32(clientesUsuarioComboBox.SelectedValue);
                    int intUserId = Convert.ToInt32(UserIdHiddenField.Value);
                    //Se elimina todas las relaciones
                    Artexacta.App.ClienteUsuario.BLL.ClienteUsuarioBLL.DeleteClienteUsuario(intUserId);
                    if (clientesUsuarioComboBox.SelectedValue != "0" && OldClienteUsuarioIdHiddenField.Value != "0")
                    {
                        var cliUsr = new Artexacta.App.ClienteUsuario.ClienteUsuario
                        {
                            ClienteId = intClienteId,
                            UserId = intUserId
                        };
                        Artexacta.App.ClienteUsuario.BLL.ClienteUsuarioBLL.InsertClienteUsuario(cliUsr);
                        SystemMessages.DisplaySystemMessage("Se ha asignado el cliente para el usuario de importación exitosamente");
                    }
                    else if (clientesComboBox.SelectedValue == "0" && OldClienteIdHiddenField.Value != "0")
                    {
                        SystemMessages.DisplaySystemMessage("Se ha eliminado la asignacion exitosamente");
                    }
                }

            }
            catch (Exception q)
            {
                log.Error("Cannot update user information to database", q);
                SystemMessages.DisplaySystemMessage("No se pudo modificar el Usuario.");
            }
        }
        else
        {
            SystemMessages.DisplaySystemMessage("No se pudo modificar el Usuario " + userName + ".");
        }

        Response.Redirect("~/Security/UserList.aspx");
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Security/UserList.aspx");
    }

    protected void CiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Ciudades.");
            log.Error("Function CiudadODS_Selected on page EditUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Ciudades.");
            log.Error("Function ClienteODS_Selected on page EditUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void UserClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Ciudades.");
            log.Error("Function ClienteODS_Selected on page EditUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void NewUserClienteSOATLB_Click(object sender, EventArgs e)
    {
        try
        {
            int UserId = Convert.ToInt32(UserIdHiddenField.Value);
            int ClienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
            foreach (GridDataItem item in UserClienteRadGrid.Items)
            {
                if (item.GetDataKeyValue("ClienteId").Equals(ClienteId))
                {
                    SystemMessages.DisplaySystemWarningMessage("El Cliente ya esta asociado.");
                    return;
                }
            }
            UserClienteSOATBLL.InsertSiniestro(UserId, ClienteId);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al asociar con el Cliente.");
            log.Error("Function NewUserClienteSOATLB_Click on page EditUser.aspx", q);
        }
        UserClienteRadGrid.DataBind();
    }
    protected void DeleteImageButton_Command(object sender, CommandEventArgs e)
    {
        try
        {
            int UserId = Convert.ToInt32(UserIdHiddenField.Value);
            int ClienteId = Convert.ToInt32(e.CommandArgument);
            UserClienteSOATBLL.DeleteUserClienteSOAT(UserId, ClienteId);
        }
        catch (Exception q)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al quitar el Cliente.");
            log.Error("Function DeleteImageButton_Command on page EditUser.aspx", q);
        }
        UserClienteRadGrid.DataBind();
    }

    public void FotoFileUpload_FilesLoaded(object sender, Artexacta.App.Documents.FileUpload.FilesLoadedArgs e)
    {
        if (e.FilesLoaded != null && e.FilesLoaded.Count > 0)
        {
            FotoId = e.FilesLoaded[0].ID;
            FotoSignatureUrl.Visible = true;
            int[] imageDimensions = Configuration.GetDigitalSignatureDimension();

            FotoSignatureUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W="+imageDimensions[0]+"&H="+imageDimensions[1];
            FileIdHiddenField.Value = FotoId.ToString();
            int currentUserId = 0;
            int.TryParse(UserIdHiddenField.Value, out currentUserId);

            bool updateResult = UserBLL.UpdateSignatureFileId(FotoId, currentUserId);

            if (updateResult)
            {
                SystemMessages.DisplaySystemMessage("Se ha actualizado la firma digital exitosamente");
            }
            else
            {
                SystemMessages.DisplaySystemErrorMessage("Hubo un error al actualizar la firma digital");

            }
        }
    }
}