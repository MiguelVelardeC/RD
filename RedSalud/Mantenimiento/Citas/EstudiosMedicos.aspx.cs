using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Utilities.SystemMessages;
using Artexacta.App.CitaEstudio.BLL;
using log4net;
public partial class Mantenimiento_Citas_EstudiosMedicos : System.Web.UI.Page
{

    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string cod = txtcitaid.Text;
        try
        {
            if (string.IsNullOrEmpty(cod)) throw new ArgumentException("No se ingreso el codigo de cita");
            int num = Int32.Parse(cod);

            imgeliminar.Visible = true;
            imgupdate.Visible = true;

            CitaEstudioDataSource.SelectParameters.Clear();
            CitaEstudioDataSource.SelectParameters.Add("citaDesgravamenId", cod);
            CitaEstudioDataSource.DataBind();
            lblFecha.Visible = true;
            DateTime fec = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
            lblFecha.Text = fec.ToShortDateString();


        }
        catch (Exception q)

        {
            log.Error("An error was ocurred while buscaba la cita", q);
            throw q;
        }

    }

    protected void CitaEstudioDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Ocurrio un error al Obtener la lista de Citas y sus Estudios");
            log.Error("Ocurrio un error al Obtener la lista de Citas", e.Exception);
            e.ExceptionHandled = true;
        }
    }

    protected void CitaEstudioGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void CitaEstudioGridView_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void chkestudio_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkEstudiova = (CheckBox)sender;
        GridViewRow row = (GridViewRow)ChkEstudiova.NamingContainer;
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkallva = (CheckBox)CitaEstudioGridView.HeaderRow.FindControl("chkall");
        foreach(GridViewRow row in CitaEstudioGridView.Rows)
        {
            CheckBox chkrow = (CheckBox)row.FindControl("chkestudio");
            if (chkallva.Checked == true) { chkrow.Checked = true;}
            else { chkrow.Checked = false;}

        }
    }

    protected void btnlimpiar_Click(object sender, EventArgs e)
    {
        string num = "0";

        //CitaEstudioBLL.getCitasListar(num);
        CitaEstudioDataSource.SelectParameters.Clear();
        CitaEstudioDataSource.SelectParameters.Add("citaDesgravamenId", num);
        CitaEstudioDataSource.DataBind();
        imgeliminar.Visible = false;
        imgupdate.Visible = false;
        lblFecha.Visible = false;
        lblFecha.Text = "";
        txtcitaid.Text = "";
        //txtcitaid.Focus();
    }

    protected void imgeliminar_Click(object sender, ImageClickEventArgs e)
    {
        int si = 0;

        for (int i = 0; i < CitaEstudioGridView.Rows.Count; i++) 
        {
            string citaid = CitaEstudioGridView.Rows[i].Cells[1].Text;

            CheckBox chkdelete = (CheckBox)CitaEstudioGridView.Rows[i].Cells[0].FindControl("chkestudio");

            if (chkdelete.Checked)
            {
                Label lblci = (Label)CitaEstudioGridView.Rows[i].Cells[0].FindControl("lblcitaid");
                Label lblest = (Label)CitaEstudioGridView.Rows[i].Cells[0].FindControl("lblestudioid");

                string estudio = CitaEstudioGridView.Rows[i].Cells[1].Text;

                int idCITA = Convert.ToInt32(lblci.Text);
                int idESTUDIO = Convert.ToInt32(lblest.Text);
                string usuario = User.Identity.Name;

                CitaEstudioBLL.DeleteEstudioCita(idCITA, idESTUDIO, usuario);
               

                SystemMessages.DisplaySystemMessage("Se ha eliminado el estudio " + estudio + " de la cita " + lblci.Text);

                si += i + 1;

            }
        
        }
        if (si == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "alert('Debe seleccionar al menos un estudio.');", true);

        }
        else
        {

            CitaEstudioGridView.DataBind();
        }
    }

    protected void imgupdate_Click(object sender, ImageClickEventArgs e)
    {
        int si= 0;

        for (int i = 0; i < CitaEstudioGridView.Rows.Count; i++)
        {
            CheckBox chkdelete = (CheckBox)CitaEstudioGridView.Rows[i].Cells[0].FindControl("chkestudio");

            if (chkdelete.Checked)
            {
                string citaid = CitaEstudioGridView.Rows[i].Cells[1].Text;

                DateTime fec = Convert.ToDateTime(lblFecha.Text);

                Label lblci = (Label)CitaEstudioGridView.Rows[i].Cells[0].FindControl("lblcitaid");
                Label lblest = (Label)CitaEstudioGridView.Rows[i].Cells[0].FindControl("lblestudioid");
                Label lblprovee = (Label)CitaEstudioGridView.Rows[i].Cells[0].FindControl("lblproveedorid");

                string estudio = CitaEstudioGridView.Rows[i].Cells[1].Text;

                int idCITA = Convert.ToInt32(lblci.Text);
                int idESTUDIO = Convert.ToInt32(lblest.Text);
                int idprovee = Convert.ToInt32(lblprovee.Text);
                string usuario = User.Identity.Name;

                CitaEstudioBLL.UpdateEstudioMedico(idCITA, idESTUDIO, idprovee, fec, usuario);
                //Falta validar si hay almenos un checkbox seleccionado

                SystemMessages.DisplaySystemMessage("Se ha actualizado las fechas del estudio " + estudio + " de la cita " + lblci.Text);

                si += i+1;
            }
        }

        if (si == 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Alerta", "alert('Debe seleccionar al menos un estudio.');",true);

        }
        else
        {

            CitaEstudioGridView.DataBind();
        }
    }
}