using Artexacta.App.Ciudad.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Clasificadores_Ciudad_CiudadList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CiudadGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            try
            {
                string CiudadId = e.CommandArgument.ToString();
                //talvez alguna validacion de si alguien utiliza dicha ciudad, pero por ahora returna falso
                if (CiudadBLL.DeleteCiudad(CiudadId))
                    SystemMessages.DisplaySystemMessage("La Ciudad seleccionada se eliminó correctamente");
                else
                    SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la Ciudad seleccionada");
            }
            catch (Exception ex)
            {
                log.Error("no se pudo convertir la CiudadId desde e.CommandArgument", ex);
                SystemMessages.DisplaySystemErrorMessage("Ocurrio un eror al eliminar la Ciudad seleccionada");
            }
            CiudadGV.DataBind();
            return;
        }

        if (e.CommandName == "EditRecord")
        {
            Session["CiudadId"] = e.CommandArgument.ToString();
            Response.Redirect("~/Clasificadores/Ciudad/CiudadDetails.aspx");

        }
    }
    protected void CiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Ciudades.");
            log.Error("Ocurrio un error al Obtener la lista de Ciudades", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}