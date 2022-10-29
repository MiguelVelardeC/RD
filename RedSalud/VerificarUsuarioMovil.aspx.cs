using Artexacta.App.Documents;
using Artexacta.App.Documents.BLL;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.Document;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VerificarUsuarioMovil : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string pacId = Request.QueryString["tgysdf2p234"];
            string codigoUsuario = Encriptacion.DescifradoTexto(pacId);
            bool verificado = PacienteBLL.VerificarUsuarioMovil(Convert.ToInt32(codigoUsuario), true);
            if (verificado)
            {
                lblMensaje.Text = "Ha completado el registro del usuario móvil exitosamente, ahora puede iniciar sesión en la aplicación móvil...";
                pnlRegistroExitoso.Visible = true;
                pnlLoSentimos.Visible = false;
            }
            else
            {
                lblBadMensaje.Text = "No se ha podido verificar su usuario, intente nuevamente haciendo clic en el enlace que se envió a su correo...";
                pnlRegistroExitoso.Visible = false;
                pnlLoSentimos.Visible = true;
            }
        }
        catch (Exception ex)
        {
            pnlRegistroExitoso.Visible = false;
            pnlLoSentimos.Visible = true;
            lblBadMensaje.Text = "Algo inesperado ocurrió, por favor vuelva a intentarlo más tarde...";
        }
        
    }
}