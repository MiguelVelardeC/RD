using Artexacta.App.User.BLL;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.User;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Configuration;
using Artexacta.App.Utilities.SystemMessages;
using Telerik.Web.UI;

public partial class Security_CreateUser : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            // El usuario no debería estar logged in al crearlo
            CreateUserWizard1.LoginCreatedUser = false;
            ProcessSessionParameters();
        }

        //Obtener el control de mensajes
        Label theMessageLabel = (Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("MessageLabel");

        if (theMessageLabel != null)
        {
            theMessageLabel.Text = "";
        }
    }

    protected void ProcessSessionParameters ()
    {
        int MedicoCreate = 0;
        if (Session["MEDICO_CREATE"] != null && !string.IsNullOrEmpty(Session["MEDICO_CREATE"].ToString()))
        {
            try
            {
                MedicoCreate = Convert.ToInt32(Session["MEDICO_CREATE"]);
                CreateUserWizard1.CancelDestinationPageUrl = "~/Medico/MedicoList";
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session MedicoCreate:" + Session["MEDICO_CREATE"]);
            }
        }
        MedicoCreateHF.Value = MedicoCreate.ToString();
        Session["MEDICO_CREATE"] = null;
    }

    protected void CreateUserWizard1_CreatingUser(object sender, LoginCancelEventArgs e)
    {
        if (!Page.IsValid)
            return;

        //Obtener el control de mensajes
        Label theMessageLabel = (Label)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("MessageLabel");

        if (theMessageLabel == null)
        {
            log.Error("Error al obtener el control para mostrar mensajes.");
            return;
        }
        theMessageLabel.Text = "";

        //Si el nombre de usuario existe y aun tiene el codigo de verificacion intentamos eliminar
        //el registro de usuario

        MembershipUser theUser = Membership.GetUser(CreateUserWizard1.UserName);
        if (theUser != null)
        {
            //Si esta aprobado no se continua
            if (theUser.IsApproved)
            {
                theMessageLabel.Text = "Nombre de usuario existente.";
                e.Cancel = true;
                return;
            }
            else
            {
                try
                {
                    Membership.DeleteUser(CreateUserWizard1.UserName);

                    User theClassUser = UserBLL.GetUserByUsername(CreateUserWizard1.UserName);

                    if (theClassUser != null)
                        UserBLL.DeleteUserRecord(theClassUser.UserId);
                }
                catch (Exception q)
                {
                    theMessageLabel.Text = "Error al eliminar un usuario no verificado.";
                    log.Error("Error al eliminar un usuario no verificado." + q.Message);
                    e.Cancel = true;
                }
            }
        }
    }

    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        if (!Page.IsValid)
            return;

        // Se creó el usuario y hay que generar el código:
        string userName = CreateUserWizard1.UserName;
        string fullName = "";
        int userId = 0;

        try
        {
            // Store the confirmation string in the user's additional info for later verification
            string CellPhone = "";
            string PhoneNumber = "";
            int PhoneArea = 0;
            int PhoneCode = 0;
            string Address = "";
            string Email = "";
            string CiudadId = "";

            TextBox theAddress = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("AddressTextBox");
            TextBox thePhoneNumber = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("NumeroTextBox");
            TextBox thePhoneArea = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("CiudadAreaTextBox");
            TextBox thePhoneCode = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("PaisAreaTextBox");
            TextBox theCellPhone = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("CellPhoneTextBox");
            TextBox theEmail = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email");
            TextBox theFullName = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("FullNameTextBox");
            RadComboBox CiudadDDL = (RadComboBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("CiudadDDL");

            if (theCellPhone != null && theAddress != null
                && thePhoneNumber != null && theEmail != null
                && thePhoneArea != null && thePhoneCode != null
                && theFullName != null && CiudadDDL!=null)
            {
                fullName = theFullName.Text;
                PhoneNumber = thePhoneNumber.Text;
                if (!string.IsNullOrEmpty(thePhoneArea.Text))
                    PhoneArea = Convert.ToInt32(thePhoneArea.Text);

                if (!string.IsNullOrEmpty(thePhoneCode.Text))
                    PhoneCode = Convert.ToInt32(thePhoneCode.Text);

                CellPhone = theCellPhone.Text;
                Address = theAddress.Text;
                Email = theEmail.Text;
                CiudadId = CiudadDDL.SelectedValue;
            }

            userId = UserBLL.InsertUserRecord(CreateUserWizard1.UserName, fullName, CellPhone,
                Address, PhoneNumber, PhoneArea, PhoneCode, Email,CiudadId);
        }
        catch (Exception q)
        {
            Session["ErrorMessage"] = "Error al crear el usuario. Por favor contactese con el administrador del sistema o intente nuevamente. " + q.Message;
            Response.Redirect("~/FatalError.aspx");
        }
        if (MedicoCreateHF.Value == "1")
        {
            Session["MedicoId"] = null;
            Session["UserName"] = fullName;
            Session["UserID"] = userId.ToString();
            Response.Redirect("~/Medico/MedicoDetails.aspx");
        }
    }

    protected void CreateUserWizard1_SendingMail(object sender, MailMessageEventArgs e)
    {
        try
        {
            string confirmURL = HttpContext.Current.Request.Url.Scheme + "://" +
                HttpContext.Current.Request.Url.Authority +
                HttpContext.Current.Request.ApplicationPath + "/MainPage.aspx";

            e.Message.From = new System.Net.Mail.MailAddress(
                Configuration.GetReturnEmailAddress(), Configuration.GetReturnEmailName());
            e.Message.Body = e.Message.Body.Replace("<%UserName%>", CreateUserWizard1.UserName);
            e.Message.Body = e.Message.Body.Replace("<%Contraseña%>", CreateUserWizard1.Password);
            e.Message.Body = e.Message.Body.Replace("<%Link%>",
                "<a href=\"" + confirmURL + "\">" + confirmURL + "</a>");
            e.Message.Subject = Configuration.GetCreationEmailSubject();
            e.Message.IsBodyHtml = true;
        }
        catch (Exception q)
        {
            log.Error("Failed to construct a confirmation email for the creation of an account for user " + CreateUserWizard1.UserName, q);
            e.Cancel = true;
        }
    }

    protected void CreateUserWizard1_CancelUser(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(MedicoCreateHF.Value))
        {
            Response.Redirect("~/Security/UserList.aspx");
        }
        else
        {
            Response.Redirect("~/Medico/MedicoList.aspx");
        }
    }

    protected void ExistsEmailCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
    {
        args.IsValid = false;

        TextBox emailTB = (TextBox)CreateUserWizard1.CreateUserStep.ContentTemplateContainer.FindControl("Email");

        string userName = Membership.GetUserNameByEmail(emailTB.Text.Trim());

        if (!String.IsNullOrEmpty(userName))
        {
            // Verify that the user is not valid
            MembershipUser muser = Membership.GetUser(userName);
            if (muser == null)
            {
                args.IsValid = true;
            }
        }
        else
        {
            args.IsValid = true;
        }
    }

    protected void CiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Ciudades.");
            log.Error("Function CiudadODS_Selected on page CreateUser.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CreateUserWizard1_SendMailError(object sender, SendMailErrorEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al enviar el email.");
            log.Error("Function CreateUserWizard1_SendMailError on page CreateUser.aspx", e.Exception);
            e.Handled = true;
        }
    }
}