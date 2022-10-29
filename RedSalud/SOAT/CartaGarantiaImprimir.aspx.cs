using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Security.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.User;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Siniestro.BLL;
using Artexacta.App.Siniestro;
using Artexacta.App.Seguimiento;
using Artexacta.App.Seguimiento.BLL;

public partial class SOAT_CartaGarantiaImprimir : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    public int CitaDesgravamenId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(CitaDesgravamenIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                CitaDesgravamenIdHiddenField.Value = "0";
            else
                CitaDesgravamenIdHiddenField.Value = value.ToString();
        }
    }

    public int SiniestroID
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(SiniestroIdHiddenfield.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                SiniestroIdHiddenfield.Value = "0";
            else
                SiniestroIdHiddenfield.Value = value.ToString();
        }
    }

    public int PropuestoAseguradoId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(PropuestoAseguradoIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                PropuestoAseguradoIdHiddenField.Value = "0";
            else
                PropuestoAseguradoIdHiddenField.Value = value.ToString();
        }
    }

    public int ProveedorMedicoId
    {
        get
        {
            try
            {
                int theValue = Convert.ToInt32(ProveedorMedicoIdHiddenField.Value);
                return theValue;
            }
            catch
            {
                return 0;
            }
        }
        set
        {
            if (value <= 0)
                ProveedorMedicoIdHiddenField.Value = "0";
            else
                ProveedorMedicoIdHiddenField.Value = value.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ProcessSessionParameters();
                //CargarDirecciones();

                //CitaIdLabel.Text = CitaDesgravamenId.ToString();

                CargarDatosSiniestro(SiniestroID);
                
                //FacturarPAPanel.Visible = false;

                
            }
            catch (Exception q)
            {
                log.Error("No se puede mostrar la pagina", q);

                SystemMessages.DisplaySystemErrorMessage("Parámetros para llamar a la página son incorrectos");
                Response.Redirect("~/MainPage.aspx");
                return;
            }
        }
    }

   

    private void CargarDatosSiniestro(int SiniestroId)
    {
        try
        {
            Siniestro objSiniestro = SiniestroBLL.GetSiniestroByID(SiniestroID);

            if (objSiniestro != null)
            {
                ordenServicioCliente.InnerText = "";//cl.NombreJuridico;
                NroSiniestro.Text = objSiniestro.OperacionId;
                FechaSiniestro.Text = objSiniestro.FechaSiniestro.ToString("dd/MM/yyyy");
                FechaDenuncia.Text = objSiniestro.FechaDenuncia.ToString("dd/MM/yyyy");
                CausaSiniestro.Text = objSiniestro.Causa;
                LugarSiniestro.Text = objSiniestro.LugarDpto;
                Area.Text = objSiniestro.Zona;
                Sindicato.Text = objSiniestro.Sindicato;
                NroRoseta.Text = objSiniestro.NumeroRoseta;
                Placa.Text = objSiniestro.Placa;
                LugarVentaPoliza.Text = objSiniestro.LugarVenta;
                TitularPoliza.Text = objSiniestro.NombreTitular;
                CI_NIT_Poliza.Text = objSiniestro.CITitular;
                TipoVehiculo.Text = objSiniestro.Tipo;
                NroChasis.Text = objSiniestro.Cilindrada;
                SectorVehiculo.Text = objSiniestro.Sector;
                Seguimiento seguimiento = SeguimientoBLL.GetSeguimientoByID(SiniestroID);
                Observaciones.Text = (seguimiento != null)? seguimiento.Observaciones: "";

                SiniestroStat stat = SiniestroBLL.GetSiniestroStatbySiniestroId(SiniestroID);

                if (stat != null)
                {
                    CantFallecidos.Text = stat.CantFallecidos.ToString();
                    CantHeridos.Text = stat.CantHeridos.ToString();
                    CantIlesos.Text = stat.CantIlesos.ToString();
                }


            }
        }
        catch (Exception eq)
        {
            SystemMessages.DisplaySystemErrorMessage("There was an error loading Siniestro");
            log.Error("there was an error loading siniestroId = " + SiniestroID, eq);
        }
    }

    private void ProcessSessionParameters()
    {

        int cdid = 0;
        string paginaBack = "SOATList.aspx";
        try
        {
            cdid = Convert.ToInt32(Request.QueryString["siniestro"]);
            if (cdid <= 0)
                throw new ArgumentException("No puede ser el id menor o igual a 0");

            if (Request.QueryString["PAGINABACK"] != null && !string.IsNullOrWhiteSpace(Request.QueryString["PAGINABACK"].ToString()))
                paginaBack = Session["PAGINABACK"].ToString();

            SiniestroID = cdid;
            Session["siniestro"] = null;
            Session["PAGINABACK"] = null;

            PaginaBackHiddenField.Value = paginaBack;
        }
        catch (Exception q)
        {
            log.Warn("IDentificador del SiniestroId", q);
            SystemMessages.DisplaySystemWarningMessage("Se llamó la página sin el identificador del siniestro");
            Response.Redirect("~/MainPage.aspx");
            return;
        }

        int userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
        List<ProveedorDesgravamen> objProv = ProveedorMedicoBLL.GetProveedorMedicoByUserId(userId);
        if (objProv != null && objProv.Count == 1)
        {
            ProveedorMedicoId = objProv[0].ProveedorMedicoId;
        }
        else
        {
            ProveedorMedicoId = 0;
        }
    }

    protected void cmdVolver_Click(object sender, EventArgs e)
    {
        //Session["SiniestroId"] = CitaDesgravamenId;

        Response.Redirect(PaginaBackHiddenField.Value);
    }
    protected void EstudiosDataSource_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }

    protected void AccidentadoODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Accidentados.");
            log.Error("Function AccidentadoODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}