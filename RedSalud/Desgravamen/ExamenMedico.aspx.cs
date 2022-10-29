using Artexacta.App.Caso.BLL;
using Artexacta.App.Desgravamen;
using Artexacta.App.Desgravamen.BLL;
using Artexacta.App.Paciente;
using Artexacta.App.Paciente.BLL;
using Artexacta.App.User.BLL;
using Artexacta.App.Utilities.Bitacora;
using Artexacta.App.Utilities.Email;
using Artexacta.App.Utilities.SystemMessages;
using Cognos.Negocio;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Esta pagina necesita de algunos argumentos para su funcionamiento:
/// CITADESGRAVAMENID de tipo entero
/// </summary>
public partial class Desgravamen_ExamenMedico : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    public static Bitacora theBitacora = new Bitacora();

    public int FotoId
    {
        get
        {
            int fotoId = 0;
            try
            {
                fotoId = Convert.ToInt32(FotoIDHiddenField.Value);
            }
            catch (Exception ex)
            {
                log.Error("Cannot convert 'FotoIDHiddenField.Value' to int value", ex);
            }
            return fotoId;
        }
        set
        {
            if (value < 0)
                FotoIDHiddenField.Value = "0";
            else
                FotoIDHiddenField.Value = value.ToString();
        }
    }

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

    protected void Page_Load(object sender, EventArgs e)
    {
        FotoPAFileUpload.FilesLoaded += FotoPAFileUpload_FilesLoaded;
        FotoPAFileUpload.MaxFileInputCount = 1;
        
        if (!IsPostBack)
        {
            try
            {
                ProcessSessionParameters();
                bool isDirectEdit = false;
                try
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_HISTORIA_DIRECTLY");
                    isDirectEdit = true;
                }
                catch (Exception eq)
                {
                    log.Warn("User is not authorized to edit Historia Clinica directly", eq);
                    isDirectEdit = false;
                }

                CitaMedica objCita = CitaMedicaBLL.GetCitaMedicaById(CitaDesgravamenId);
                if (objCita != null && (objCita.Estado == CitaMedica.EstadoCita.Atendida || isDirectEdit))
                {
                    IsRevisionCreated.Value = "1";
                }
                MedicoDesgravamen objMedico = CargarME(objCita);
                PropuestoAsegurado objPA = CargarPA(objCita);
                CargarDatosCita(objCita);
                int casoSisaId = 0;
                try
                {
                    EnlaceDesgravamenSISA objSisa = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(objCita.CitaDesgravamenId, ref casoSisaId);
                    if (objSisa == null || objSisa.Dirty)
                        MensajeDeIncompleto.Visible = true;
                }
                catch (Exception q)
                {
                    log.Debug("no pudo obtener el caso sisa", q);
                }
                try 
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EXAMEN_MEDICO");
                    log.Debug("Utiliza el enlace de Volver a la agenda");
                } catch {
                    cmdVolver.NavigateUrl = "PropuestoAseguradoLista.aspx";
                    cmdVolver.Text = "Volver a lista";
                }

                //CitaDesgravamen citaDesgravamen = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
                //int clienteId = citaDesgravamen.ClienteId;

                //int clienteFortaleza = Artexacta.App.Configuration.Configuration.GetFortalezaDesgravamenId();

                //if (clienteId == clienteFortaleza)
                //{
                //    lblp19.Text = "19. Presión arterial (favor completar 3 tomas)";
                //}
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

    private void CargarDatosCita(CitaMedica objCita)
    {
        bool isAuthorizedSaves = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_ALLREVISIONES");
            isAuthorizedSaves = true;
        }
        catch (Exception)
        {
            isAuthorizedSaves = false;
        }
        MedicoDesgravamen objMedico = null;
        try
        {
            int userId = UserBLL.GetUserIdByUsername(Context.User.Identity.Name);
            objMedico = MedicoDesgravamenBLL.GetMedicoDesgravamenByUserId(userId);
            ProgramacionCita objCitaExamen = Artexacta.App.Desgravamen.BLL.PropuestoAseguradoBLL.GetProgramacionCita(objCita.CitaDesgravamenId);
            // Si el medico NO es el que está assignado, NO SE PUEDE MODIFICAR
            if ((objMedico != null && objMedico.MedicoDesgravamenId != objCitaExamen.MedicoId) && !isAuthorizedSaves)
                objMedico = null;
        }
        catch { }

        //////////////////////////////
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch (Exception e)
        {
            log.Warn("User is not authorized to edit Revisiones Medicas", e);
            isApproved = false;
        }

        if ((objMedico == null || objCita.Aprobado) && !isApproved)
        {
            btnSaveExamenMedico.Visible = false;
            btnSaveExamenMedico2.Visible = false;
        }

        CargarTextosCitaCovid(objCita.PropuestoAseguradoId);

        ConsultaCita objConsulta = ConsultaCitaBLL.GetConsultaCitaById(objCita.CitaDesgravamenId);
        if (objConsulta == null)
        {
            log.Info("No existe consulta todavia para citaDesgravamen id " + objCita.CitaDesgravamenId);
            return;
        }

        //////////////////////////////
        

        PAConsultaReciente.Text = objConsulta.FechaMotivoConsultaReciente;
        PAMedicoParticular.Text = objConsulta.NombreDireccionMP;
        PAProfesion.Text = objConsulta.OcupacionPA;
        PATratamiento.Text = objConsulta.TratamientoMedicacion;
        chkEstadoCivil.ClearSelection();
        chkEstadoCivil.Items.FindByValue(objConsulta.EstadoCivilPA).Selected = true;

        txtEdadMadre.Value = objConsulta.EdadMadre;
        txtEdadMuerteMadre.Value = objConsulta.EdadMuerteMadre;
        txtEdadMuertePadre.Value = objConsulta.EdadMuertePadre;
        txtEdadPadre.Value = objConsulta.EdadPadre;
        txtEstadoMadre.Text = objConsulta.EstadoSaludMadre;
        txtEstadoPadre.Text = objConsulta.EstadoSaludPadre;

        txtEdadGeneroHnos.Text = objConsulta.EdadGeneroHermanos;
        txtEstadoHermanos.Text = objConsulta.EstadoSaludHermanos;
        nroHijosVivos.Value = objConsulta.NumeroVivos;
        nroHijosMuertos.Value = objConsulta.NumeroMuertos;

        p11Estatura.Value = objConsulta.EstaturaCm <= 30 ? (double?)null : Convert.ToDouble(objConsulta.EstaturaCm) / 100.0;
        p11Peso.Value = objConsulta.PesoKg <= 1 ? (double?)null : Convert.ToDouble(objConsulta.PesoKg);

        txtConocePA.Text = objConsulta.TiempoConocePA;
        txtParientePA.Text = objConsulta.RelacionParentesco;

        txtPechoExpiracion.Value = objConsulta.PechoExpiracionCm < 0 || objConsulta.PechoExpiracionCm > 300 ? 
            (double?)null : Convert.ToDouble(objConsulta.PechoExpiracionCm);
        txtPechoInspiracion.Value = objConsulta.PechoInspiracionCm < 0 || objConsulta.PechoInspiracionCm > 300 ?
            (double?)null : Convert.ToDouble(objConsulta.PechoInspiracionCm);
        txtAbdomen.Value = objConsulta.AbdomenCm < 0 || objConsulta.AbdomenCm > 300 ?
            (double?)null : Convert.ToDouble(objConsulta.AbdomenCm);

        PAAparentaMasEdad.ClearSelection();
        PAAparentaMasEdad.SelectedIndex = objConsulta.AspectoEnfermizo ? 0 : 1;

        txtFrecuenciaDescanso.Value = objConsulta.PulsoFrecuenciaDescanso < 0 || objConsulta.PulsoFrecuenciaDescanso > 300 ?
            (double?)null : Convert.ToDouble(objConsulta.PulsoFrecuenciaDescanso);
        txtFrecuenciaEsfuerzo.Value = objConsulta.PulsoFrecuenciaEsfuerzo < 0 || objConsulta.PulsoFrecuenciaEsfuerzo > 300 ?
            (double?)null : Convert.ToDouble(objConsulta.PulsoFrecuenciaEsfuerzo);
        txtFrecuencia5Minutos.Value = objConsulta.PulsoFrecuencia5Minutos < 0 || objConsulta.PulsoFrecuencia5Minutos > 300 ?
            (double?)null : Convert.ToDouble(objConsulta.PulsoFrecuencia5Minutos);

        txtPresionArterial1.Text = objConsulta.PresionArterial1;
        txtPresionArterial2.Text = objConsulta.PresionArterial2;
        txtPresionArterial3.Text = objConsulta.PresionArterial3;

        txtIrregDescanso.Value = objConsulta.PulsoIrregularidadesDescanso;
        txtIrregEsfuerzo.Value = objConsulta.PulsoIrregularidadesEsfuerzo;
        txtIrreg5Minutos.Value = objConsulta.PulsoIrregularidades5Minutos;
        // Corazon
        PACorazonDisnea.SelectedIndex = objConsulta.CorazonDisnea ? 0 : 1;
        PACorazonEdema.SelectedIndex = objConsulta.CorazonEdema ? 0 : 1;
        PACorazonHipertrofia.SelectedIndex = objConsulta.CorazonHipertrofia ? 0 : 1;
        PACorazonSoplo.SelectedIndex = objConsulta.CorazonSoplo ? 0 : 1;

        // Soplo
        txtDescripcionSoplo.Text = objConsulta.DescripcionSoplo;
        txtSituacionSoplo.Text = objConsulta.SituacionSoplo;

        PASoploSuave.SelectedIndex = objConsulta.SoploSuave ? 0 : 1;
        PASoploAcentua.SelectedIndex = objConsulta.SoploAcentua ? 0 : 1;
        PASoploConstante.SelectedIndex = objConsulta.SoploConstante ? 0 : 1;
        PASoploDiastolico.SelectedIndex = objConsulta.SoploDiastolico ? 0 : 1;
        PASoploFuerte.SelectedIndex = objConsulta.SoploFuerte ? 0 : 1;
        PASoploInconstante.SelectedIndex = objConsulta.SoploInconstante ? 0 : 1;
        PASoploIrradiado.SelectedIndex = objConsulta.SoploIrradiado ? 0 : 1;
        PASoploLocalizado.SelectedIndex = objConsulta.SoploLocalizado ? 0 : 1;
        PASoploModerado.SelectedIndex = objConsulta.SoploModerado ? 0 : 1;
        PASoploPresistolico.SelectedIndex = objConsulta.SoploPresistolico ? 0 : 1;
        PASoploSeAtenua.SelectedIndex = objConsulta.SoploSeAtenua ? 0 : 1;
        PASoploSinCambios.SelectedIndex = objConsulta.SoploSinCambios ? 0 : 1;
        PASoploSistolico.SelectedIndex = objConsulta.SoploSistolico ? 0 : 1;

        PAOrina.Text = objConsulta.AnalisisOrina;
        PADensidad.Text = objConsulta.Densidad;
        PAAlbumina.Text = objConsulta.Albumina;
        PAGlucosa.Text = objConsulta.Glucosa;

        txtComentariosConfidenciales.Text = objConsulta.Comentarios;

        CargarDatosCitaCovid(objCita.PropuestoAseguradoId, objCita.CitaDesgravamenId);
        
        if (objConsulta != null)
        {
            blockInputs();
        }
    }

    private void CargarDatosCitaCovid(int propuestoAseguradoId, int citaDesgravamenId)
    {
        using (var context = new Negocio())
        {
            //Se verifica si tiene datos
            var consultaCovid = context.tbl_DESG_ConsultaCitaCovid
                .Where(x => x.CitaDesgravamenId == citaDesgravamenId)
                .FirstOrDefault();
            divSeccionCovid.Visible = consultaCovid != null;

            if (consultaCovid != null)
            {
                PAPreguntaSeccionCovid1.SelectedValue = consultaCovid.TieneVacuna ? "1" : "0";
                PAPreguntaSeccionCovid1a.Text = consultaCovid.NombreVacunas;
                PAPreguntaSeccionCovid1b.Text = consultaCovid.PrimeraDosisFecha;
                PAPreguntaSeccionCovid1c.Text = consultaCovid.SegundaDosisFecha;
                PAPreguntaSeccionCovid1d.Text = consultaCovid.OtrasDosisFecha;

                PAPreguntaSeccionCovid2.SelectedValue = consultaCovid.TuvoCovid ? "1" : "0";
                PAPreguntaSeccionCovid2a.Text = consultaCovid.FechaDiagnostico;
                PAPreguntaSeccionCovid2a2.Text = consultaCovid.FechaNegativo;
                PAPreguntaSeccionCovid2b.Text = consultaCovid.DetalleTratamiento;
                PAPreguntaSeccionCovid2c.Text = consultaCovid.TiempoHospitalizacion;
                PAPreguntaSeccionCovid2d.Text = consultaCovid.SecuelasPostCovid;
            }
        }
    }

    private void CargarTextosCitaCovid(int propuestoAseguradoId)
    {
        using(var context = new Negocio())
        {
            //Se verifica si el cliente tiene acceso a seccion covid
            var clienteId = context.tbl_DESG_PropuestoAsegurado.Find(propuestoAseguradoId).clienteId;
            var clienteTieneSeccionCovid = context.tbl_DESG_ConsultaClienteCovid.Where(x => x.ClienteId == clienteId).Any();
            divSeccionCovid.Visible = clienteTieneSeccionCovid;

            if (!clienteTieneSeccionCovid) return;

            var preguntas = context.tbl_DESG_ConsultaClienteCovidPregunta.Where(x => x.ClienteId == clienteId).ToList();

            lblPAPreguntaSeccionCovid1.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "0").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid1a.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "a").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid1b.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "b").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid1c.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "c").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid1d.Text = preguntas.Where(x => x.Seccion == 1 && x.Inciso == "d").FirstOrDefault().Pregunta;

            lblPAPreguntaSeccionCovid2.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "0").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid2a.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "a").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid2a2.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "a2").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid2b.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "b").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid2c.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "c").FirstOrDefault().Pregunta;
            lblPAPreguntaSeccionCovid2d.Text = preguntas.Where(x => x.Seccion == 2 && x.Inciso == "d").FirstOrDefault().Pregunta;
        }        
    }

    private void blockInputs()
    {
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }
        if (!isApproved)
        {
            PAConsultaReciente.Enabled = false;
            PAMedicoParticular.Enabled = false;
            PAProfesion.Enabled = false;
            PATratamiento.Enabled = false;
            //chkEstadoCivil.ClearSelection();
            //chkEstadoCivil.Items.FindByValue(objConsulta.EstadoCivilPA).Selected = true;
            chkEstadoCivil.Enabled = false;
            txtEdadMadre.Enabled = false;
            txtEdadMuerteMadre.Enabled = false;
            txtEdadMuertePadre.Enabled = false;
            txtEdadPadre.Enabled = false;
            txtEstadoMadre.Enabled = false;
            txtEstadoPadre.Enabled = false;

            txtEdadGeneroHnos.Enabled = false;
            txtEstadoHermanos.Enabled = false;
            nroHijosVivos.Enabled = false;
            nroHijosMuertos.Enabled = false;

            //p11Estatura.Enabled = false;
            //p11Peso.Enabled = false;

            p11Peso.ReadOnly = true;
            p11Estatura.ReadOnly = true;


            txtConocePA.Enabled = false;
            txtParientePA.Enabled = false;

            txtPechoExpiracion.Enabled = false;
            txtPechoInspiracion.Enabled = false;
            txtAbdomen.Enabled = false;

            PAAparentaMasEdad.Enabled = false;

            txtFrecuenciaDescanso.Enabled = false;
            txtFrecuenciaEsfuerzo.Enabled = false;
            txtFrecuencia5Minutos.Enabled = false;

            txtPresionArterial1.Enabled = false;
            txtPresionArterial2.Enabled = false;
            txtPresionArterial3.Enabled = false;

            txtIrregDescanso.Enabled = false;
            txtIrregEsfuerzo.Enabled = false;
            txtIrreg5Minutos.Enabled = false;
            // Corazon
            PACorazonDisnea.Enabled = false;
            PACorazonEdema.Enabled = false;
            PACorazonHipertrofia.Enabled = false;
            PACorazonSoplo.Enabled = false;

            // Soplo
            txtDescripcionSoplo.Enabled = false;
            txtSituacionSoplo.Enabled = false;

            PASoploSuave.Enabled = false;
            PASoploAcentua.Enabled = false;
            PASoploConstante.Enabled = false;
            PASoploDiastolico.Enabled = false;
            PASoploFuerte.Enabled = false;
            PASoploInconstante.Enabled = false;
            PASoploIrradiado.Enabled = false;
            PASoploLocalizado.Enabled = false;
            PASoploModerado.Enabled = false;
            PASoploPresistolico.Enabled = false;
            PASoploSeAtenua.Enabled = false;
            PASoploSinCambios.Enabled = false;
            PASoploSistolico.Enabled = false;

            PAOrina.Enabled = false;
            PADensidad.Enabled = false;
            PAAlbumina.Enabled = false;
            PAGlucosa.Enabled = false;


            ConfirmarAP.Visible = false;

            BlockInputsCovid();
        }
    }

    private void BlockInputsCovid()
    {
        PAPreguntaSeccionCovid1.Enabled = false;
        PAPreguntaSeccionCovid1a.Enabled = false;
        PAPreguntaSeccionCovid1b.Enabled = false;
        PAPreguntaSeccionCovid1c.Enabled = false;
        PAPreguntaSeccionCovid1d.Enabled = false;

        PAPreguntaSeccionCovid2.Enabled = false;
        PAPreguntaSeccionCovid2a.Enabled = false;
        PAPreguntaSeccionCovid2a2.Enabled = false;
        PAPreguntaSeccionCovid2b.Enabled = false;
        PAPreguntaSeccionCovid2c.Enabled = false;
        PAPreguntaSeccionCovid2d.Enabled = false;
    }

    private PropuestoAsegurado CargarPA(CitaMedica objCita)
    {
        PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(objCita.PropuestoAseguradoId);
        PACI.Text = objPA.CarnetIdentidad;
        PropuestoAseguradoId = objPA.PropuestoAseguradoId;
        PAFechaNacimiento.Text = objPA.FechaNacimientoForDisplay;
        PANombre.Text = objPA.NombreCompleto;

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

        return objMedico;
    }

    public void FotoPAFileUpload_FilesLoaded(object sender, Artexacta.App.Documents.FileUpload.FilesLoadedArgs e)
    {
        if (e.FilesLoaded != null && e.FilesLoaded.Count > 0)
        {
            FotoId = e.FilesLoaded[0].ID;
            FotoPAUrl.Visible = true;
            FotoPAUrl.ImageUrl = "~/ImageResize.aspx?ID=" + FotoId.ToString() + "&W=200&H=200";

            if (PropuestoAseguradoId > 0)
            {
                PropuestoAseguradoBLL.UpdateFotoId(PropuestoAseguradoId, FotoId);
            }
        }
    }

    private void ProcessSessionParameters()
    {

        int cdid = 0;
        try
        {
            if (Request.QueryString["CITADESGRAVAMENID"] != null)
                cdid = Convert.ToInt32(Request.QueryString["CITADESGRAVAMENID"]);
            else
                cdid = Convert.ToInt32(Session["CITADESGRAVAMENID"]);
            if (cdid <= 0)
                throw new ArgumentException("No puede ser el id menor o igual a 0");

            CitaDesgravamenId = cdid;
            Session["CITADESGRAVAMENID"] = null;
        }
        catch (Exception q)
        {
            log.Warn("IDentificador de la cita falta", q);
            SystemMessages.DisplaySystemWarningMessage("Se llamó la página sin el identificador de la cita");
            Response.Redirect("~/MainPage.aspx");
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

    protected void btnSaveExamenMedico_Click(object sender, EventArgs e)
    {
        ConsultaCita objCita = ConsultaCitaBLL.GetConsultaCitaById(CitaDesgravamenId);

        if (objCita == null) {
            objCita = new ConsultaCita();
            objCita.CitaDesgravamenId = CitaDesgravamenId;
        }
        p11Estatura.Enabled = true;
        p11Peso.Enabled = true;
        objCita.AbdomenCm = Convert.ToInt32(txtAbdomen.Value);
        objCita.Albumina = PAAlbumina.Text;
        objCita.AnalisisOrina = PAOrina.Text;
        objCita.AspectoEnfermizo = PAAparentaMasEdad.SelectedValue == "1";
        objCita.Comentarios = txtComentariosConfidenciales.Text;
        objCita.CorazonDisnea = PACorazonDisnea.SelectedValue == "1";
        objCita.CorazonEdema = PACorazonEdema.SelectedValue == "1";
        objCita.CorazonHipertrofia = PACorazonHipertrofia.SelectedValue == "1";
        objCita.CorazonSoplo = PACorazonSoplo.SelectedValue == "1";
        objCita.Densidad = PADensidad.Text;
        objCita.DescripcionSoplo = txtDescripcionSoplo.Text;
        objCita.EdadGeneroHermanos = txtEdadGeneroHnos.Text;
        objCita.EdadMadre = Convert.ToInt32(txtEdadMadre.Value);
        objCita.EdadMuerteMadre = Convert.ToInt32(txtEdadMuerteMadre.Value);
        objCita.EdadMuertePadre = Convert.ToInt32(txtEdadMuertePadre.Value);
        objCita.EdadPadre = Convert.ToInt32(txtEdadPadre.Value);
        objCita.EstadoCivilPA = chkEstadoCivil.SelectedValue;
        objCita.EstadoSaludHermanos = txtEstadoHermanos.Text;
        objCita.EstadoSaludMadre = txtEstadoMadre.Text.Trim();
        objCita.EstadoSaludPadre = txtEstadoPadre.Text.Trim();
        objCita.EstaturaCm = Convert.ToInt32(p11Estatura.Value * 100.0);
        objCita.FechaMotivoConsultaReciente = PAConsultaReciente.Text;
        objCita.Glucosa = PAGlucosa.Text;
        objCita.NombreDireccionMP = PAMedicoParticular.Text;
        objCita.NumeroMuertos = Convert.ToInt32(nroHijosMuertos.Value);
        objCita.NumeroVivos = Convert.ToInt32(nroHijosVivos.Value);
        objCita.OcupacionPA = PAProfesion.Text;
        objCita.PechoExpiracionCm = Convert.ToInt32(txtPechoExpiracion.Value);
        objCita.PechoInspiracionCm = Convert.ToInt32(txtPechoInspiracion.Value);
        objCita.PesoKg = Convert.ToDecimal(p11Peso.Value);
        objCita.PresionArterial1 = txtPresionArterial1.Text;
        objCita.PresionArterial2 = txtPresionArterial2.Text;
        objCita.PresionArterial3 = txtPresionArterial3.Text;
        objCita.PulsoFrecuencia5Minutos = Convert.ToInt32(txtFrecuencia5Minutos.Value);
        objCita.PulsoFrecuenciaDescanso = Convert.ToInt32(txtFrecuenciaDescanso.Value);
        objCita.PulsoFrecuenciaEsfuerzo = Convert.ToInt32(txtFrecuenciaEsfuerzo.Value);
        objCita.PulsoIrregularidades5Minutos = Convert.ToInt32(txtIrreg5Minutos.Value);
        objCita.PulsoIrregularidadesDescanso = Convert.ToInt32(txtIrregDescanso.Value);
        objCita.PulsoIrregularidadesEsfuerzo = Convert.ToInt32(txtIrregEsfuerzo.Value);
        objCita.RelacionParentesco = txtParientePA.Text.Trim();
        objCita.SituacionSoplo = txtSituacionSoplo.Text;
        objCita.SoploAcentua = PASoploAcentua.SelectedValue == "1";
        objCita.SoploConstante = PASoploConstante.SelectedValue == "1";
        objCita.SoploDiastolico = PASoploDiastolico.SelectedValue == "1";
        objCita.SoploFuerte = PASoploFuerte.SelectedValue == "1";
        objCita.SoploInconstante = PASoploInconstante.SelectedValue == "1";
        objCita.SoploIrradiado = PASoploIrradiado.SelectedValue == "1";
        objCita.SoploLocalizado = PASoploLocalizado.SelectedValue == "1";
        objCita.SoploModerado = PASoploModerado.SelectedValue == "1";
        objCita.SoploPresistolico = PASoploPresistolico.SelectedValue == "1";
        objCita.SoploSeAtenua = PASoploSeAtenua.SelectedValue == "1";
        objCita.SoploSinCambios = PASoploSinCambios.SelectedValue == "1";
        objCita.SoploSistolico = PASoploSistolico.SelectedValue == "1";
        objCita.SoploSuave = PASoploSuave.SelectedValue == "1";
        objCita.TiempoConocePA = txtConocePA.Text.Trim();
        objCita.TratamientoMedicacion = PATratamiento.Text;

        LeerPreguntasDeForm(objCita);
        bool noHayError = true;

        try
        {
            CitaDesgravamen cita = CitaDesgravamenBLL.GetCitaDesgravamenById(objCita.CitaDesgravamenId);
            ConsultaCitaBLL.UpdateConsultaCita(objCita, cita.ClienteId);
            CrearActualizarCitaCovid(CitaDesgravamenId, cita.PropuestoAseguradoId);
            SystemMessages.DisplaySystemMessage("Se actualizó la cita " + CitaDesgravamenId);
            theBitacora.RecordTrace(Bitacora.TraceType.DESGMedicoGuardaRevision, User.Identity.Name, "Revisión Médica", 
                CitaDesgravamenId.ToString(), "El médico " + User.Identity.Name + " guardó/actualizó la cita " + CitaDesgravamenId);
            IsRevisionCreated.Value = "1";
            blockInputs();
        }
        catch (Exception q)
        {
            log.Error("No se pudo actualizar la consulta " + CitaDesgravamenId, q);
            SystemMessages.DisplaySystemErrorMessage("Error al guardar la cita " + CitaDesgravamenId);
            noHayError = false;
        }

        if (noHayError)
        {
            EmailUtilities.CheckFirstAndSendEmailLaboCitaConcluidos(CitaDesgravamenId);
        }
    }

    private void CrearActualizarCitaCovid(int citaDesgravamenId, int propuestoAseguradoId)
    {
        using (var context = new Negocio())
        {
            var actualizarCita = context.tbl_DESG_ConsultaCitaCovid.Where(x => x.CitaDesgravamenId == citaDesgravamenId).FirstOrDefault();
            if (actualizarCita != null)
            {
                actualizarCita.TieneVacuna = PAPreguntaSeccionCovid1.SelectedValue == "1";
                actualizarCita.NombreVacunas = PAPreguntaSeccionCovid1a.Text;
                actualizarCita.PrimeraDosisFecha = PAPreguntaSeccionCovid1b.Text;
                actualizarCita.SegundaDosisFecha = PAPreguntaSeccionCovid1c.Text;
                actualizarCita.OtrasDosisFecha = PAPreguntaSeccionCovid1d.Text;
                
                actualizarCita.TuvoCovid = PAPreguntaSeccionCovid2.SelectedValue == "1";
                actualizarCita.FechaDiagnostico = PAPreguntaSeccionCovid2a.Text;
                actualizarCita.FechaNegativo = PAPreguntaSeccionCovid2a2.Text;
                actualizarCita.DetalleTratamiento = PAPreguntaSeccionCovid2b.Text;
                actualizarCita.TiempoHospitalizacion = PAPreguntaSeccionCovid2c.Text;
                actualizarCita.SecuelasPostCovid = PAPreguntaSeccionCovid2d.Text;

                context.Entry(actualizarCita).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                //Se verifica si el cliente tiene acceso a seccion covid
                var clienteId = context.tbl_DESG_PropuestoAsegurado.Find(propuestoAseguradoId).clienteId;
                var clienteTieneSeccionCovid = context.tbl_DESG_ConsultaClienteCovid.Where(x => x.ClienteId == clienteId).Any();

                if (!clienteTieneSeccionCovid) return;

                var cita = new tbl_DESG_ConsultaCitaCovid
                {
                    CitaDesgravamenId = citaDesgravamenId,
                    TieneVacuna = PAPreguntaSeccionCovid1.SelectedValue == "1",
                    NombreVacunas = PAPreguntaSeccionCovid1a.Text,
                    PrimeraDosisFecha = PAPreguntaSeccionCovid1b.Text,
                    SegundaDosisFecha = PAPreguntaSeccionCovid1c.Text,
                    OtrasDosisFecha = PAPreguntaSeccionCovid1d.Text,
                    TuvoCovid = PAPreguntaSeccionCovid2.SelectedValue == "1",
                    FechaDiagnostico = PAPreguntaSeccionCovid2a.Text,
                    FechaNegativo = PAPreguntaSeccionCovid2a2.Text,
                    DetalleTratamiento = PAPreguntaSeccionCovid2b.Text,
                    TiempoHospitalizacion = PAPreguntaSeccionCovid2c.Text,
                    SecuelasPostCovid = PAPreguntaSeccionCovid2d.Text
                };

                context.tbl_DESG_ConsultaCitaCovid.Add(cita);
            }

            context.SaveChanges();
        }
    }

    private void LeerPreguntasDeForm(ConsultaCita objCita)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        // Seccion 2
        int[] seccionesConIncisos = { 2, 7, 13, 22 };
        Repeater[] repeatersConIncisos = { rptSeccion2, rptSeccion7, rptSeccion13, rptSeccion22 };
        int idxRpt = 0;
        foreach (int unaSeccion in seccionesConIncisos)
        {
            foreach (RepeaterItem objItem in repeatersConIncisos[idxRpt].Items)
            {
                RadioButtonList objRespuestaPregunta = (RadioButtonList)objItem.FindControl("PAPreguntaSeccion" + unaSeccion.ToString());
                HiddenField objIdPregunta = (HiddenField)objItem.FindControl("PAPreguntaSeccion" + unaSeccion.ToString() + "Id");
                TextBox pAPreguntaAfirmativaSeccion = (TextBox)objItem.FindControl("PAPreguntaAfirmativaSeccion" + unaSeccion.ToString());

                ConsultaPreguntaCita objPreguntaCita = new ConsultaPreguntaCita();
                objPreguntaCita.Inciso = objIdPregunta.Value;
                objPreguntaCita.CitaDesgravamenId = objCita.CitaDesgravamenId;
                objPreguntaCita.Observacion = pAPreguntaAfirmativaSeccion.Text;
                objPreguntaCita.Pregunta = "";
                if (objRespuestaPregunta.SelectedIndex >= 0)
                {
                    objPreguntaCita.RespuestaNotSet = false;
                    objPreguntaCita.Respuesta = objRespuestaPregunta.SelectedValue == "1";
                    if (!isApproved)
                    {
                        objRespuestaPregunta.Enabled = false;
                    }
                }
                else
                {
                    objPreguntaCita.RespuestaNotSet = true;
                    objPreguntaCita.Respuesta = false;
                }
                objPreguntaCita.Seccion = unaSeccion;

                objCita.actualizarPregunta(objPreguntaCita);
            }
            idxRpt++;
        }

        // Seccion 3 4 5 6
        string[] secciones = { "3456", "8910", "12","14151617", "2324", "26" };
        Repeater[] repeatersSecciones = { rptSeccion3456, rptSeccion8910, rptSeccion12, rptSeccion14151617, rptSeccion2324, rptSeccion26 };
        idxRpt = 0;
        foreach (string grupoSecciones in secciones)
        {
            bool primeroSeccion8 = true;
            foreach (RepeaterItem objItem in repeatersSecciones[idxRpt].Items)
            {
                RadioButtonList objRespuestaPregunta = (RadioButtonList)objItem.FindControl("PAPreguntaSeccion");
                HiddenField objIdPregunta = (HiddenField)objItem.FindControl("PAPreguntaSeccion" + grupoSecciones + "Id");
                TextBox pAPreguntaAfirmativaSeccion = (TextBox)objItem.FindControl("PAPreguntaAfirmativaSeccion");

                ConsultaPreguntaCita objPreguntaCita = new ConsultaPreguntaCita();
                objPreguntaCita.Seccion = Convert.ToInt32(objIdPregunta.Value);
                if (objPreguntaCita.Seccion == 8)
                {
                    if (primeroSeccion8)
                    {
                        objPreguntaCita.Inciso = "0";
                        primeroSeccion8 = false;
                    }
                    else
                    {
                        objPreguntaCita.Inciso = "1";
                    }
                }
                else
                {
                    objPreguntaCita.Inciso = "0";
                }
                objPreguntaCita.CitaDesgravamenId = objCita.CitaDesgravamenId;
                objPreguntaCita.Observacion = pAPreguntaAfirmativaSeccion.Text;
                objPreguntaCita.Pregunta = "";
                if (objRespuestaPregunta.SelectedIndex >= 0)
                {
                    objPreguntaCita.RespuestaNotSet = false;
                    objPreguntaCita.Respuesta = objRespuestaPregunta.SelectedValue == "1";
                    if (!isApproved)
                    {
                        objRespuestaPregunta.Enabled = false;
                    }
                }
                else
                {
                    objPreguntaCita.RespuestaNotSet = true;
                    objPreguntaCita.Respuesta = false;
                }

                objCita.actualizarPregunta(objPreguntaCita);
            }
            idxRpt++;
        }
    }


    protected void cmdImprimir_Click(object sender, EventArgs e)
    {
        Session["FULLREPORT"] = 0;
        Session["CITADESGRAVAMENID"] = CitaDesgravamenId;
        Response.Redirect("ExamenMedicoImprimir.aspx");
    }

    protected void rptSeccion22_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita) e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion22");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion14151617_ItemDataBound(object sender, RepeaterItemEventArgs e)
    { 
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion13_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion13");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion8910_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;

            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion7_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion7");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion3456_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion2");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion2324_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion26_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }

        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void rptSeccion12_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Get Permission
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch
        {
            isApproved = false;
        }
        if (e.Item.ItemType == ListItemType.Item ||
            e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ConsultaPreguntaCita obj = (ConsultaPreguntaCita)e.Item.DataItem;
            RadioButtonList objRespuestaPregunta = (RadioButtonList)e.Item.FindControl("PAPreguntaSeccion");

            if (obj.RespuestaNotSet)
            {
                objRespuestaPregunta.ClearSelection();
                return;
            }

            objRespuestaPregunta.SelectedIndex = obj.Respuesta ? 0 : 1;
            if (!isApproved)
            {
                objRespuestaPregunta.Enabled = false;
            }
        }
    }
    protected void cmdFichaMedica_Click(object sender, EventArgs e)
    {
        int casoId = 0;
        
        EnlaceDesgravamenSISA objEnlace = CitaMedicaBLL.GetCasoIdByCitaDesgravamenId(CitaDesgravamenId, ref casoId);


        if (casoId <= 0)
        {
            log.Info("Esta cita " + CitaDesgravamenId + " no tiene historia clínica, se está creando en este momento");

            PropuestoAsegurado objPA = PropuestoAseguradoBLL.GetPropuestoAseguradoId(PropuestoAseguradoId);
            CitaDesgravamen objCita = CitaDesgravamenBLL.GetCitaDesgravamenById(CitaDesgravamenId);
            Paciente objPaciente = new Paciente();
            objPaciente.CarnetIdentidad = objPA.CarnetIdentidad;
            objPaciente.FechaNacimiento = objPA.FechaNacimiento;
            objPaciente.Nombre = objPA.NombreCompleto;
            objPaciente.Telefono = objPA.TelefonoCelular;
            if (objPA.Genero != null)
                objPaciente.Genero = (objPA.Genero == "M");

            objPaciente.PacienteId = PacienteBLL.InsertOrGetPacienteByCI(objPaciente, objCita.CiudadId);

            int userId = Artexacta.App.User.BLL.UserBLL.GetUserIdByUsername(User.Identity.Name);
            objEnlace = new EnlaceDesgravamenSISA();
            objEnlace.CarnetIdentidad = objPaciente.CarnetIdentidad;
            objEnlace.CasoId = 0;
            objEnlace.CitaDesgravamenId = CitaDesgravamenId;
            objEnlace.CodigoAsegurado = "DESG" + objPaciente.PacienteId.ToString().PadLeft(10,'0');
            objEnlace.NumeroPoliza = objEnlace.CodigoAsegurado;
            objEnlace.PolizaId = 0;
            objEnlace.PropuestoAseguradoId = objPA.PropuestoAseguradoId;
            objEnlace.PacienteId = objPaciente.PacienteId;

            CasoBLL.InsertCasoDesgravamen(objEnlace, objCita.CiudadId, userId);
                        
        }
        Session["CasoId"] = objEnlace.CasoId;
        
        bool isApproved = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("DESGRAVAMEN_EDIT_REVISIONES");
            isApproved = true;
        }
        catch (Exception eq)
        {
            log.Warn("User is not authorized to edit Revisiones Medicas", eq);
            isApproved = false;
        }

        //bool isCitaAprobado = (btnSaveExamenMedico.Visible || btnSaveExamenMedico2.Visible) ? false : true;


        if (MensajeDeIncompleto.Visible || isApproved)
        {
            Session["Mode"] = "Edit";
            Session["isApproved"] = "True";
        }

        Session["CITADESGRAVAMENID"] = CitaDesgravamenId;


        
        Session["PesoDesgravamen"] = p11Peso.Value.ToString();
        Session["EstaturaDesgravamen"] = p11Estatura.Value.ToString();                
        

        Response.Redirect("~/CasoMedico/CasoMedicoDetalle.aspx");
    }

    

    [WebMethod(EnableSession = true)]
    public static bool IsConsultaInsert(string strCitaDesgravamen)
    {
        try
        {
            int intCitaDesgravamen = Convert.ToInt32(strCitaDesgravamen);
            ConsultaCita objCita = ConsultaCitaBLL.GetConsultaCitaById(intCitaDesgravamen);

            return (objCita != null) ? true : false;
        }
        catch (Exception q)
        {
            log.Error("Could not Get Consulta row with Id: " + strCitaDesgravamen, q);
            throw q;
        }
    }
}