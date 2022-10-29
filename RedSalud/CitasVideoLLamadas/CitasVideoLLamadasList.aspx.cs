using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Derivacion.BLL;
using Artexacta.App.GenericComboContainer;
using Artexacta.App.Medico;
using Artexacta.App.Medico.BLL;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;

public partial class CitasVideoLLamadasList : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            LoadParameters();
        }
    }

    //protected void SearchLB_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        this.CitasVideoLLamadaRadGrid.Visible = true;
    //        CitasVideoLLamadaRadGrid.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
        
    //}

    protected void PacienteRadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                int PacienteId = Convert.ToInt32(e.CommandArgument);
                Session["PacienteId"] = PacienteId.ToString();

                Response.Redirect("PacienteDetails.aspx");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PacienteID in PacienteRadGrid_ItemCommand on page PacienteList.aspx", ex);
            }
        }
        if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                GridDataItem item = (GridDataItem)e.Item;
                int PacienteId = Convert.ToInt32(item.GetDataKeyValue("PacienteId").ToString());

                PacienteBLL.DeletePaciente(PacienteId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el paciente seleccionado");
                //PacienteRadGrid.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PacienteID in PacienteRadGrid_ItemCommand on page PacienteList.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el paciente, verifique q");
            }
        }
    }
    
    protected void PacienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Pacientes.");
            log.Error("Function PacienteODS_Selected on page PacienteList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CiudadDDL_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        
    }
    protected void CiudadODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de las Ciudades.");
            log.Error("Function CiudadODS_Selected on page ProveedorList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void CitasVideoLLamadaODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Pacientes.");
            log.Error("Function PacienteODS_Selected on page PacienteList.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    private void LoadParameters()
    {
        string emptyId = "0";
        string emptyDescription = "TODOS";

        //List<GenericComboContainer> listDerivadores = DerivacionBLL.GetMedicoUserIdByCiudadCombo(emptyId);
        var medicos = MedicoBLL.TodosMedicos();
        List<GenericComboContainer> listDerivadores = medicos.Select(_ => new GenericComboContainer { 
                                                                                                    ContainerId = _.MedicoId.ToString(),
                                                                                                    ContainerName = _.Nombre
                                                                                                    }).ToList();
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
        medicoId.DataSource = listDerivadores;
        medicoId.DataValueField = "ContainerId";
        medicoId.DataTextField = "ContainerName";
        medicoId.DataBind();
    }
    protected void ClienteGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VerDetalle")
        {
            Session["citId"] = e.CommandArgument.ToString();
            Response.Redirect("~/CitasVideoLLamadas/CitaVideoLLamadaDetails.aspx");

        }
    }
}