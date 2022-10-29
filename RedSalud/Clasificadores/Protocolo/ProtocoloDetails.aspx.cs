using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Protocolo;
using Artexacta.App.Protocolo.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;

public partial class Clasificadores_Protocolo_ProtocoloDetails : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ProcessSessionParameters();
        }
    }

    private void ProcessSessionParameters()
    {
        if (Session["ProtocoloId"] != null && !string.IsNullOrEmpty(Session["ProtocoloId"].ToString()))
        {
            int protocoloId = 0;
            try
            {
                protocoloId = Convert.ToInt32(Session["ProtocoloId"].ToString());
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el ProtocoloId de Session['ProtocoloId']", ex);
            }
            ProtocoloHiddenLabel.Text = protocoloId.ToString();
            if (protocoloId > 0)
                LoadFormData(protocoloId);
            Session["ProtocoloId"] = null;
            return;
        }
        ProtocoloHiddenLabel.Text = "0";
    }

    private void LoadFormData(int protocoloId)
    {
        TitleLabel.Text = "Editar Protocolo";
        try
        {
            Protocolo obj = ProtocoloBLL.GetProtocoloById(protocoloId);
            NombreTextBox.Text = obj.NombreEnfermedad;
            DescipcionTextBox.Text = obj.TextoProtocolo;
            TipoEnfermedadSelectedHiddenLabel.Text = obj.TipoEnfermedadId.ToString();
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al obtener los datos del Protocolo Seleccionado", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener los datos del Protocolo Seleccionado");
        }
    }


    protected void TipoEnfermedadDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Ocurrio un error al obtener la lista de Tipos de Enfermedad", e.Exception);
            e.ExceptionHandled = true;
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al obtener la lista de Tipos de Enfermedad");
        }
    }

    protected void TipoEnfermedadDropDownList_DataBound(object sender, EventArgs e)
    {
        ListItem item = new ListItem("Seleccione un Tipo de Enfermedad...", "");
        TipoEnfermedadDropDownList.Items.Insert(0, item);

        item = TipoEnfermedadDropDownList.Items.FindByValue(TipoEnfermedadSelectedHiddenLabel.Text);
        if (item != null)
            item.Selected = true;
    }

    protected void SaveButton_Click(object sender, EventArgs e)
    {
        try
        {
            int protocoloId = Convert.ToInt32(ProtocoloHiddenLabel.Text);
            string nombre = NombreTextBox.Text;
            string textoProtocolo = DescipcionTextBox.Text;
            int tipoEnefermedadId = Convert.ToInt32(TipoEnfermedadDropDownList.SelectedValue);

            Protocolo obj = new Protocolo(protocoloId, nombre, tipoEnefermedadId, textoProtocolo);

            if (protocoloId == 0)
            {
                ProtocoloBLL.InsertProtocolo(obj);
                SystemMessages.DisplaySystemMessage("El nuevo Protocolo se guardó correctamente");
            }
            else
            {
                ProtocoloBLL.UpdateProtocolo(obj);
                SystemMessages.DisplaySystemMessage("El Protocolo se guardó correctamente");
            }
        }
        catch (Exception ex)
        {
            log.Error("Ocurrio un error al guardar el Protocolo", ex);
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al guardar el Protocolo");
            return;
        }
        Response.Redirect("~/Clasificadores/Protocolo/ProtocoloList.aspx", true);
    }
}