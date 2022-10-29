using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Artexacta.App.User;
using Artexacta.App.User.BLL;
using Artexacta.App.Consolidacion.BLL;
using Telerik.Web.UI;
using Artexacta.App.GastoDetalle.BLL;

public partial class Consolidacion_AgregarConsolidacion : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CiudadHF.Value = UserBLL.GetCuidadIdByUsername(HttpContext.Current.User.Identity.Name);
            this.FechaFin.MaxDate = DateTime.Now;
        }
    }

    protected void ProveedorODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de los Proveedores.");
            log.Error("Function ProveedorODS_Selected on page Consolidacion.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }


    protected void SearchLB_Click(object sender, EventArgs e)
    {
        this.GastoD.Visible = true;
        this.GastoD.DataBind();
    }

    protected void GastoDetalleODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener los detalles de Gastos por Proveedor.");
            log.Error("Function GastoDetalleODS_Selected on page Consolidacion.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void SaveConsolidacionLB_Click(object sender, EventArgs e)
    {
        //validar si es que existe una fila seleccionada, de lo contrario mostrar un mensaje de q no se a seleccionado 
        try
        {
            if (RowsSelected())
            {
                SaveConsolidacion();
                GastoD.DataBind();
            }
            else
                SystemMessages.DisplaySystemErrorMessage("No ha seleccionado ninguna fila de la tabla Gastos por Consolidar.");
        }
        catch (Exception ex)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al intentar realizar la consolidación.");
            log.Error("Function SaveConsolidacionLB_Click on page Consolidacion.aspx", ex);
        }
    }
    protected bool RowsSelected()
    {
        bool boolRowSelected = false;
        foreach (GridDataItem item in GastoD.MasterTableView.Items)
        {
            RadioButton AceptadoRb = (RadioButton)item.FindControl("AceptadoRb");
            bool Aceptado = AceptadoRb.Checked;

            RadioButton RechazadoRb = (RadioButton)item.FindControl("RechazadoRb");
            bool Rechazado = RechazadoRb.Checked;
            if (Aceptado || Rechazado)
            {
                boolRowSelected = true;
                break;
            }
        }
        return boolRowSelected;
    }

    protected void SaveConsolidacion()
    {
        int ConsolidacionId = 0;
        int ProveedorId = Convert.ToInt32(ProveedorDDL.SelectedValue);
        DateTime FechaHasta = (DateTime)FechaFin.SelectedDate;//teoricamente obtiene con horas 00, veirificar
        int UserId = UserBLL.GetUserIdByUsername(HttpContext.Current.User.Identity.Name);

        ConsolidacionId = ConsolidacionBLL.InsertConsolidacion(ProveedorId, FechaHasta, 0, UserId, DateTime.Now, GastoD.MasterTableView);
        if (ConsolidacionId <= 0)
        {
            SystemMessages.DisplaySystemErrorMessage("Error no se pudo realizar la consolidación");
            return;
        }

        //int TotalSeleccionados = 0;
        int Correctos = 0;
        int Incorrectos = 0;

        //modificar los gastosDetalle con el idConsolidacion y Aceptado/Rechazado
        foreach (GridDataItem item in GastoD.MasterTableView.Items)
        {
            HiddenField GastoDetalleIdHF = (HiddenField)item.FindControl("GastoDetalleIdHF");

            RadioButton AceptadoRb = (RadioButton)item.FindControl("AceptadoRb");
            bool Aceptado = AceptadoRb.Checked;

            RadioButton RechazadoRb = (RadioButton)item.FindControl("RechazadoRb");
            bool Rechazado = RechazadoRb.Checked;
            if (Aceptado || Rechazado)
            {
                int GastoDetalleId = Convert.ToInt32(GastoDetalleIdHF.Value);
                if (GastoDetalleBLL.ConslidarGastoDetalle(GastoDetalleId, ConsolidacionId, Aceptado, Rechazado))
                    Correctos++;
                else
                    Incorrectos++;
            }
        }

        if (Correctos == 0)
            SystemMessages.DisplaySystemErrorMessage("Error, No se pudo realizar la consolidación de los gastos detalles seleccionados.");
        else if (Incorrectos == 0)
            SystemMessages.DisplaySystemMessage("Gastos Consolidados correctamente: " + Correctos.ToString());
        else
            SystemMessages.DisplaySystemErrorMessage("Error, Consolidados: " + Correctos.ToString() + ", No Consolidados: " + Incorrectos.ToString());

    }
}