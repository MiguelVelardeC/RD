using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.CitaEstudio.BLL;
using log4net;
public partial class Mantenimiento_Citas_CambioFechas : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Obtener la última Fecha del mes anterior
            DateTime fec = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            lblFecha.Text=fec.ToShortDateString();
            //txtcitaid.Focus();
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string cod = txtcitaid.Text;
        try
        {
            if (string.IsNullOrEmpty(cod)) throw new ArgumentException("No se ingreso el codigo de cita");
            int num = Int32.Parse(cod);

            CitasDataSource.SelectParameters.Clear();
            CitasDataSource.SelectParameters.Add("citaDesgravamenId", cod);
            CitasDataSource.DataBind();


        }
        catch (Exception q)

        {
            log.Error("An error was ocurred while buscaba la cita", q);
            throw q;
        }

       
    }

    protected void CitasDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Citas");
            log.Error("Ocurrio un error al Obtener la lista de Citas", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void CitasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "UpdateRecord")
        {
            int citaDesgravamenId = Convert.ToInt32(e.CommandArgument);

            string fecha =lblFecha.Text;
            string fechacreacion=CitasGridView.Rows[0].Cells[2].Text;
            string fechaCita = CitasGridView.Rows[0].Cells[3].Text;

            fechacreacion = fecha + ' ' + fechacreacion.Substring(11, 8);
            fechaCita = fecha + ' '+fechaCita.Substring(11, 8);

            DateTime feccrea = Convert.ToDateTime(fechacreacion);
            DateTime feccita = Convert.ToDateTime(fechaCita);
            string usuario =  User.Identity.Name;
            
            CitaEstudioBLL.UpdateFecha(citaDesgravamenId, feccrea, feccita,usuario);
            txtcitaid.Text = "";

            SystemMessages.DisplaySystemMessage("Se ha actualizado la fecha de la cita " + citaDesgravamenId);
            CitasGridView.DataBind();
            return;
            
        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        string num = "0";

        //CitaEstudioBLL.getCitasListar(num);
        CitasDataSource.SelectParameters.Clear();
        CitasDataSource.SelectParameters.Add("citaDesgravamenId", num );
        CitasDataSource.DataBind();

        txtcitaid.Text = "";
        //txtcitaid.Focus();

    }
}