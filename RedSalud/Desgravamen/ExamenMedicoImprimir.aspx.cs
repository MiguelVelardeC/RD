using Artexacta.App.Configuration;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.Utilities.SystemMessages;
using Cognos.Negocio;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Desgravamen_ExamenMedicoImprimir : SqlViewStatePage
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

    public string PaginaBack
    {
        get
        {
            return PaginaBackHiddenField.Value;
        }
        set
        {
            PaginaBackHiddenField.Value = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                int fullReport = 0;
                ProcessSessionParameters(ref fullReport);
                CitaMedica objCita = CitaMedicaBLL.GetCitaMedicaById(CitaDesgravamenId);
                MedicoDesgravamen objMedico = CargarME(objCita);
                ProveedorDesgravamen objProveedorMedico = CargarProveedor(objMedico);
                PropuestoAsegurado objPA = CargarPA(objCita);

                // Carga el reporte full solamente si es mayor que 0 
                CargarDatosCita(objCita, fullReport > 0);

                bool isFormConsent = false;
                CitaDesgravamen citaDesgravamen = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
                int clienteId = citaDesgravamen.ClienteId;

                int clienteFortaleza = Configuration.GetFortalezaDesgravamenId();

                if (clienteId == clienteFortaleza)
                {
                    isFormConsent = true;
                    //lblp19.Text = "19. Presión arterial (favor completar 3 tomas)";
                }                    

                formConsent.Visible = isFormConsent;


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

    private void CargarDatosCita(CitaMedica objCita, bool fullReport)
    {
        ConsultaCita objConsulta = ConsultaCitaBLL.GetConsultaCitaById(objCita.CitaDesgravamenId);
        if (objConsulta == null)
        {
            log.Info("No existe consulta todavia para citaDesgravamen id " + objCita.CitaDesgravamenId);
            return;
        }
        string clienteNombre = "";
        try
        {            
            CitaDesgravamen cd = CitaDesgravamenBLL.GetCitaDesgravamenById(objCita.CitaDesgravamenId);
            RedCliente cl = RedClienteBLL.GetRedClienteByClienteId(cd.ClienteId);
            clienteNombre = cl.NombreJuridico;
        }
        catch (Exception)
        {
            
        }

        clienteRevision.Text = clienteNombre;
        string revisionConsentimiento = System.Configuration.ConfigurationManager.AppSettings["DESGRevisionConsentimiento"];
        revisionConsentimiento = revisionConsentimiento.Replace("%CLIENTE%", clienteNombre);
        formConsent.Text = revisionConsentimiento;
        tituloRevision.Text = "Revisión Médica Nro " + objCita.CitaDesgravamenId.ToString();

        FechaCita.Text = objCita.FechaHoraCita.ToShortDateString();

        PAConsultaReciente.Text = objConsulta.FechaMotivoConsultaReciente;
        PAMedicoParticular.Text = objConsulta.NombreDireccionMP;
        PAProfesion.Text = objConsulta.OcupacionPA;
        PATratamiento.Text = objConsulta.TratamientoMedicacion;
        chkEstadoCivil.Text = objConsulta.EstadoCivilPA;

        txtEdadMadre.Text = objConsulta.EdadMadre.ToString();
        txtEdadMuerteMadre.Text = objConsulta.EdadMuerteMadre.ToString();
        txtEdadMuertePadre.Text = objConsulta.EdadMuertePadre.ToString();
        txtEdadPadre.Text = objConsulta.EdadPadre.ToString();
        txtEstadoMadre.Text = objConsulta.EstadoSaludMadre;
        txtEstadoPadre.Text = objConsulta.EstadoSaludPadre;

        txtEdadGeneroHnos.Text = objConsulta.EdadGeneroHermanos;
        txtEstadoHermanos.Text = objConsulta.EstadoSaludHermanos;
        nroHijosVivos.Text = objConsulta.NumeroVivos.ToString();
        nroHijosMuertos.Text = objConsulta.NumeroMuertos.ToString();

        p11Estatura.Text = (Convert.ToDouble(objConsulta.EstaturaCm) / 100.0).ToString("0.00");
        p11Peso.Text = objConsulta.PesoKg.ToString();

        if (fullReport)
        {
            txtConocePA.Text = objConsulta.TiempoConocePA;
            txtParientePA.Text = objConsulta.RelacionParentesco;

            txtPechoExpiracion.Text = objConsulta.PechoExpiracionCm.ToString();
            txtPechoInspiracion.Text = objConsulta.PechoInspiracionCm.ToString();
            txtAbdomen.Text = objConsulta.AbdomenCm.ToString();

            PAAparentaMasEdad.Text = objConsulta.AspectoEnfermizo ? "SI" : "NO";

            txtFrecuenciaDescanso.Text = objConsulta.PulsoFrecuenciaDescanso < 0 || objConsulta.PulsoFrecuenciaDescanso > 300 ?
                "-" : objConsulta.PulsoFrecuenciaDescanso.ToString();
            txtFrecuenciaEsfuerzo.Text = objConsulta.PulsoFrecuenciaEsfuerzo < 0 || objConsulta.PulsoFrecuenciaEsfuerzo > 300 ?
                "-" : objConsulta.PulsoFrecuenciaEsfuerzo.ToString();
            txtFrecuencia5Minutos.Text = objConsulta.PulsoFrecuencia5Minutos < 0 || objConsulta.PulsoFrecuencia5Minutos > 300 ?
                "-" : objConsulta.PulsoFrecuencia5Minutos.ToString();

            txtPresionArterial1.Text = objConsulta.PresionArterial1;
            txtPresionArterial2.Text = objConsulta.PresionArterial2;
            txtPresionArterial3.Text = objConsulta.PresionArterial3;

            txtIrregDescanso.Text = objConsulta.PulsoIrregularidadesDescanso.ToString();
            txtIrregEsfuerzo.Text = objConsulta.PulsoIrregularidadesEsfuerzo.ToString();
            txtIrreg5Minutos.Text = objConsulta.PulsoIrregularidades5Minutos.ToString();
            // Corazon
            PACorazonDisnea.Text = objConsulta.CorazonDisnea ? "Si" : "No";
            PACorazonDisnea.CssClass = objConsulta.CorazonDisnea ? "respuestaAfirmativa" : "respuestaPregunta";
            PACorazonEdema.Text = objConsulta.CorazonEdema ? "Si" : "No";
            PACorazonEdema.CssClass = objConsulta.CorazonEdema ? "respuestaAfirmativa" : "respuestaPregunta";
            PACorazonHipertrofia.Text = objConsulta.CorazonHipertrofia ? "Si" : "No";
            PACorazonHipertrofia.CssClass = objConsulta.CorazonHipertrofia ? "respuestaAfirmativa" : "respuestaPregunta";
            PACorazonSoplo.Text = objConsulta.CorazonSoplo ? "Si" : "No";
            PACorazonSoplo.CssClass = objConsulta.CorazonSoplo ? "respuestaAfirmativa" : "respuestaPregunta";

            if (objConsulta.CorazonSoplo)
                situacionSoplo.Visible = true;
            else
                situacionSoplo.Visible = false;

            // Soplo
            txtDescripcionSoplo.Text = objConsulta.DescripcionSoplo;
            txtSituacionSoplo.Text = objConsulta.SituacionSoplo;

            PASoploSuave.Text = objConsulta.SoploSuave ? "SI" : "NO";
            PASoploSuave.CssClass = objConsulta.SoploSuave ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploAcentua.Text = objConsulta.SoploAcentua ? "SI" : "NO";
            PASoploAcentua.CssClass = objConsulta.SoploAcentua ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploConstante.Text = objConsulta.SoploConstante ? "SI" : "NO";
            PASoploConstante.CssClass = objConsulta.SoploConstante ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploDiastolico.Text = objConsulta.SoploDiastolico ? "SI" : "NO";
            PASoploDiastolico.CssClass = objConsulta.SoploDiastolico ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploFuerte.Text = objConsulta.SoploFuerte ? "SI" : "NO";
            PASoploFuerte.CssClass = objConsulta.SoploFuerte ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploInconstante.Text = objConsulta.SoploInconstante ? "SI" : "NO";
            PASoploInconstante.CssClass = objConsulta.SoploInconstante ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploIrradiado.Text = objConsulta.SoploIrradiado ? "SI" : "NO";
            PASoploIrradiado.CssClass = objConsulta.SoploIrradiado ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploLocalizado.Text = objConsulta.SoploLocalizado ? "SI" : "NO";
            PASoploLocalizado.CssClass = objConsulta.SoploLocalizado ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploModerado.Text = objConsulta.SoploModerado ? "SI" : "NO";
            PASoploModerado.CssClass = objConsulta.SoploModerado ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploPresistolico.Text = objConsulta.SoploPresistolico ? "SI" : "NO";
            PASoploPresistolico.CssClass = objConsulta.SoploPresistolico ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploSeAtenua.Text = objConsulta.SoploSeAtenua ? "SI" : "NO";
            PASoploSeAtenua.CssClass = objConsulta.SoploSeAtenua ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploSinCambios.Text = objConsulta.SoploSinCambios ? "SI" : "NO";
            PASoploSinCambios.CssClass = objConsulta.SoploSinCambios ? "respuestaAfirmativa" : "respuestaPregunta";
            PASoploSistolico.Text = objConsulta.SoploSistolico ? "SI" : "NO";
            PASoploSistolico.CssClass = objConsulta.SoploSistolico ? "respuestaAfirmativa" : "respuestaPregunta";

            PAOrina.Text = objConsulta.AnalisisOrina;
            PADensidad.Text = objConsulta.Densidad;
            PAAlbumina.Text = objConsulta.Albumina;
            PAGlucosa.Text = objConsulta.Glucosa;

            txtComentariosConfidenciales.Text = objConsulta.Comentarios;
        }

        CargarDatosCitaCovid(objCita.CitaDesgravamenId, objCita.PropuestoAseguradoId);
    }

    private void CargarDatosCitaCovid(int citaDesgravamenId, int propuestoAseguradoId)
    {
        using (var context = new Negocio())
        {
            var consultaCovid = context.tbl_DESG_ConsultaCitaCovid
                .Where(x => x.CitaDesgravamenId == citaDesgravamenId)
                .FirstOrDefault();
            if (consultaCovid == null) return;

            divSeccionCovid.Visible = true;

            var clienteId = context.tbl_DESG_PropuestoAsegurado
                .Where(x => x.propuestoAseguradoId == propuestoAseguradoId).First().clienteId;

            var preguntas = context.tbl_DESG_ConsultaClienteCovidPregunta
                .Where(x => x.ClienteId == clienteId)
                .ToList();

            //Preguntas
            lblPreguntaSeccionCovid1.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "0").FirstOrDefault().Pregunta.Replace("27", "18");
            lblPreguntaSeccionCovid1a.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "a").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid1b.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "b").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid1c.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "c").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid1d.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "d").FirstOrDefault().Pregunta;

            lblPreguntaSeccionCovid2.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "0").FirstOrDefault().Pregunta.Replace("28", "19");
            lblPreguntaSeccionCovid2a.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "a").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid2a2.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "a2").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid2b.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "b").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid2c.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "c").FirstOrDefault().Pregunta;
            lblPreguntaSeccionCovid2d.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "d").FirstOrDefault().Pregunta;

            //Respuestas
            PAPreguntaSeccionCovid1.Text = consultaCovid.TieneVacuna ? "SI" : "NO";
            PAPreguntaSeccionCovid1a.Text = consultaCovid.NombreVacunas;
            PAPreguntaSeccionCovid1b.Text = consultaCovid.PrimeraDosisFecha;
            PAPreguntaSeccionCovid1c.Text = consultaCovid.SegundaDosisFecha;
            PAPreguntaSeccionCovid1d.Text = consultaCovid.OtrasDosisFecha;

            PAPreguntaSeccionCovid2.Text = consultaCovid.TuvoCovid ? "SI" : "NO";
            PAPreguntaSeccionCovid2a.Text = consultaCovid.FechaDiagnostico;
            PAPreguntaSeccionCovid2a2.Text = consultaCovid.FechaNegativo;
            PAPreguntaSeccionCovid2b.Text = consultaCovid.DetalleTratamiento;
            PAPreguntaSeccionCovid2c.Text = consultaCovid.TiempoHospitalizacion;
            PAPreguntaSeccionCovid2d.Text = consultaCovid.SecuelasPostCovid;
        }
    }

    private ProveedorDesgravamen CargarProveedor(MedicoDesgravamen objMedico)
    {
        ProveedorDesgravamen result = ProveedorMedicoBLL.GetProveedorMedicoId(objMedico.ProveedorMedicoId);
        //NombreProveedor.Text = result.Nombre;
        NombreCiudad.Text = result.CiudadNombre;

        return result;
    }

    private PropuestoAsegurado CargarPA(CitaMedica objCita)
    {
        PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCita.PropuestoAseguradoId);
        PACI.Text = objPA.CarnetIdentidad;
        PAFechaNacimiento.Text = objPA.FechaNacimientoForDisplay;
        PANombre.Text = objPA.NombreCompleto;
        PANombreFirma.Text = objPA.NombreCompleto;

        if (objPA.FotoId == 0)
            FotoPAUrl.Visible = false;
        else
            FotoPAUrl.ImageUrl = objPA.FotoUrl;

        return objPA;
    }

    private MedicoDesgravamen CargarME(CitaMedica objCita)
    {
        MedicoDesgravamen objMedico = MedicoDesgravamenBLL.GetMedicoDesgravamenById(objCita.MedicoDesgravamenId);
        MENombre.Text = objMedico.Nombre;
        MENombreFirma.Text = objMedico.Nombre;
        
        return objMedico;
    }

    private void ProcessSessionParameters(ref int fullReport)
    {

        int cdid = 0;
        try
        {
            if (Request["cdid"] != null)
            {
                cdid = Convert.ToInt32(Request["cdid"]);
            }
            else
            {
                cdid = Convert.ToInt32(Session["CITADESGRAVAMENID"]);
            }
            
            if (cdid <= 0)
                throw new ArgumentException("No puede ser el id menor o igual a 0");

            CitaDesgravamenId = cdid;
            Session["CITADESGRAVAMENID"] = null;

            if (Session["PAGINABACK"] != null && !string.IsNullOrWhiteSpace(Session["PAGINABACK"].ToString()))
                PaginaBack = Session["PAGINABACK"].ToString();
            else
                PaginaBack = "ExamenMedico.aspx";

            Session["PAGINABACK"] = null;

            fullReport = 0;
            if (Request["f"] != null)
            {
                fullReport = Convert.ToInt32(Request["f"]);
            }
            else
            {
                if (Session["FULLREPORT"] != null && !string.IsNullOrWhiteSpace(Session["FULLREPORT"].ToString()))
                    fullReport = Convert.ToInt32(Session["FULLREPORT"]);
            }
            if (fullReport == 0)
            {
                pnlPreguntasConfidenciales.Visible = false;
            }
        }
        catch (Exception q)
        {
            log.Warn("IDentificador de la cita falta", q);
            SystemMessages.DisplaySystemWarningMessage("Se llamó la página sin el identificador de la cita");
            Response.Redirect("~/MainPage.aspx");
            return;
        }

    }
    protected void cmdVolverACita_Click(object sender, EventArgs e)
    {
        Session["CITADESGRAVAMENID"] = CitaDesgravamenId;
        Response.Redirect(PaginaBack);
    }

    protected void rptSeccion2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion2");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion3456_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion7_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion7");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        } 
    }

    protected void rptSeccion8910_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion12_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion13_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion13");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion14151617_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion22_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion22");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void rptSeccion2324_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }
    protected void rptSeccion26_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            Label objRespuestaPregunta = (Label)e.Item.FindControl("PAPreguntaSeccion");
            Panel pnlObservaciones = (Panel)e.Item.FindControl("pnlObservaciones");

            if (!obj.RespuestaNotSet)
            {
                objRespuestaPregunta.Text = obj.Respuesta ? "Si" : "No";
                objRespuestaPregunta.CssClass = obj.Respuesta ? "respuestaAfirmativa" : "respuestaPregunta";
                pnlObservaciones.Visible = obj.Respuesta;
            }
        }
    }

    protected void dsPreguntaSeccion12_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 12", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 12. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }


    protected void dsPreguntaSeccion2_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 2", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 2. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion7_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 7", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 7. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion13_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 13", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 13. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion8910_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 8 9 10", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 8 9 10. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }
    protected void dsPreguntaSeccion14151617_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 14 15 16 17", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 14 15 16 17. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion22_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 22", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 22. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion2324_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 23 24", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 23 24. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }

    protected void dsPreguntaSeccion26_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            log.Error("Error al obtener los datos de la sección 26", e.Exception);
            SystemMessages.DisplaySystemErrorMessage("Error al obtener información sección 26. Volver a la agenda por favor.");
            e.ExceptionHandled = true;
            return;
        }
    }
}