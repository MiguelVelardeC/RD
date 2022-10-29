using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.Accidentado;
using Artexacta.App.Accidentado.BLL;
using Artexacta.App.Configuration;
using Artexacta.App.GastosEjecutados;
using Artexacta.App.GastosEjecutados.BLL;
using Artexacta.App.GestionMedica;
using Artexacta.App.GestionMedica.BLL;
using Artexacta.App.LoginSecurity;
using Artexacta.App.Preliquidacion;
using Artexacta.App.Preliquidacion.BLL;
using Artexacta.App.Seguimiento;
using Artexacta.App.Seguimiento.BLL;
using Artexacta.App.Siniestro;
using Artexacta.App.Siniestro.BLL;
using Artexacta.App.Utilities.SystemMessages;
using log4net;
using Telerik.Web.UI;
using Artexacta.App.RedCliente;
using Artexacta.App.RedCliente.BLL;
using Artexacta.App.PolizaAlianzaWS;

public partial class SOAT_SOATWizard : SqlViewStatePage
{
    private static readonly ILog log = LogManager.GetLogger("Standard");
    private bool proccess = false;
    protected bool ShowAccidentadoList
    {
        set { AccidentadoListHiddenField.Value = value.ToString(); }
        get
        {
            bool result = false;

            bool.TryParse(AccidentadoListHiddenField.Value, out result);

            return result;
        }
    }
    protected int SiniestroID
    {
        set { SiniestroIDHF.Value = value.ToString(); }
        get
        {
            try
            {
                return int.Parse(SiniestroIDHF.Value);
            }
            catch
            {
                return 0;
            }
        }
    }
    protected int AccidentadoID
    {
        set { AccidentadoIdHF.Value = value.ToString(); }
        get
        {
            try
            {
                return int.Parse(AccidentadoIdHF.Value);
            }
            catch
            {
                return 0;
            }
        }
    }

    protected bool IsFinished
    {
        set { IsFinishedHF.Value = value ? "1" : ""; }
        get { return IsFinishedHF.Value == "1"; }
    }

    protected bool IsSaved
    {
        set { IsSavedHF.Value = value ? "1" : ""; }
        get { return IsSavedHF.Value == "1"; }
    }

    protected void Page_Load ( object sender, EventArgs e )
    {
        if (ProcessQueryString())
        {
            Response.Redirect("~/SOAT/SOATWizard.aspx");
            return;
        }
        
        FechaSiniestroRDP.MaxDate = DateTime.Now;
        FechaDenunciaRDP.MaxDate = DateTime.Now;
        FechaNacimientoRDP.MaxDate = DateTime.Now;
        FechaVisitaRDP.MaxDate = DateTime.Now;
        NGFechaRDP.MaxDate = DateTime.Now;
        if (!IsPostBack)
        {
            string step = ProcessSessionParameters();
            IsSaved = AccidentadoBLL.IsAccidentadoSave(SiniestroID, AccidentadoID);

            if (SiniestroID > 0)
            {
                DatosAdicionalesFileManager.Visible = true;
                DatosAdicionalesFileManager.ObjectId = SiniestroID;
            }

            checkPermissions();
            if (step != "")
            {
                MoveToStep(step);
                return;
            }
        }
        FileManager.OnListFileChange += new UserControls_FileManager.OnListFileChangeDelegate(FileManager_FileSave);
    }

    public void checkPermissions()
    {
        bool puedeEliminarArchivos = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_SINIESTRO_FILES");
            puedeEliminarArchivos = true;
        }
        catch (Exception)
        {
            puedeEliminarArchivos = false;
        }

        bool puedeInsertarAccidentado = false;
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_ACCIDENTADO");
            puedeInsertarAccidentado = true;
        }
        catch
        {
            puedeInsertarAccidentado = false;
        }

        NewAccidentadoLB.Visible = puedeInsertarAccidentado;




        DatosAdicionalesFileManager.CanOnlyDeleteFiles = puedeEliminarArchivos;
    }


    public void FileManager_FileSave ( string ObjectName, string type )
    {
        AccidentadoRadGrid.DataBind();
    }
    protected void Page_PreRender ( object sender, EventArgs e )
    {
        if (!proccess)
        {
            MoveToStep(StepHF.Value);
        }
    }
    protected bool ProcessQueryString ()
    {
        string strSiniestroId = Request.QueryString["SiniestroId"];

        if (!string.IsNullOrWhiteSpace(strSiniestroId))
        {
            Session["SiniestroId"] = Convert.ToInt32(Session["SiniestroId"]);
        }
        else
        {
            return false;
        }

        string strAccidentadoId = Request.QueryString["AccidentadoId"];

        if (!string.IsNullOrWhiteSpace(strSiniestroId))
        {
            Session["AccidentadoId"] = Convert.ToInt32(Session["AccidentadoId"]);
        }

        return true;
    }
    protected string ProcessSessionParameters ()
    {
        string moveTo = "";
        if (Session["SiniestroId"] != null && !string.IsNullOrEmpty(Session["SiniestroId"].ToString()))
        {
            try
            {
                SiniestroID = Convert.ToInt32(Session["SiniestroId"]);
                moveTo = "2";
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session SiniestroId:" + Session["SiniestroId"]);
            }
        }

        if (SiniestroID <= 0)
        {
            Session["AccidentadoId"] = null;
            return "";
        }

        IsFinishedHF.Value = "";
        if (Session["Teminado"] != null && !string.IsNullOrEmpty(Session["Teminado"].ToString()))
        {
            IsFinishedHF.Value = Session["Teminado"].ToString();
            moveTo = "1";
        }
        Session["Teminado"] = null;

        if (Session["AccidentadoId"] != null && !string.IsNullOrEmpty(Session["AccidentadoId"].ToString()))
        {
            try
            {
                AccidentadoID = Convert.ToInt32(Session["AccidentadoId"]);
                moveTo = "2";
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session AccidentadoId:" + Session["AccidentadoId"]);
            }
        }

        if (Session["SiniestroId"] != null && 
            !string.IsNullOrEmpty(Session["SiniestroId"].ToString()) &&
            Session["AccidentadoId"] != null &&
            !string.IsNullOrEmpty(Session["AccidentadoId"].ToString()) &&
            Session["GestionMedica"] != null &&
            !string.IsNullOrEmpty(Session["GestionMedica"].ToString()) &&
            Session["GestionMedica"].ToString() == "3"
            )
        {
            try
            {
                SiniestroID = Convert.ToInt32(Session["SiniestroId"]);
                AccidentadoID = Convert.ToInt32(Session["AccidentadoId"]);
                List <GestionMedica> list = GestionMedicaBLL.GetAllGestionMedicaBySiniestroID(SiniestroID, AccidentadoID);

                if (list.Count > 0)
                {
                    GestionMedicaIdHF.Value = "NEW";
                }
                moveTo = "3";
            }
            catch
            {
                log.Error("no se pudo realizar la conversion de la session SiniestroId:" + Session["SiniestroId"]);
            }
        }
        string s = (Session["AccidentadoList"] != null) ? Session["AccidentadoList"].ToString() : "";

        if (Session["AccidentadoList"] != null &&
            !string.IsNullOrEmpty(Session["AccidentadoList"].ToString()) &&
            AccidentadoID <= 0 && 
            SiniestroID > 0 && 
            moveTo == "2")
        {
            ShowAccidentadoList = true;
        }
        Session["AccidentadoId"] = null;
        Session["SiniestroId"] = null;
        Session["GestionMedica"] = null;
        Session["AccidentadoList"] = null;
        return moveTo;
    }
    private void setStep ()
    {
        PreevWizardLB.Enabled = !(StepHF.Value == "1");
        PreevWizardLB.ValidationGroup = "step" + StepHF.Value;
        PreevWizardLB.OnClientClick = "validatePreev('step" + StepHF.Value + "');";
        NextWizardLB.ValidationGroup = "step" + StepHF.Value;

        BackToList.OnClientClick = IsSaved ? "" : "return confirm('¿Está seguro de Salir?');";

        Step1Panel.Visible = StepHF.Value == "1";
        Step2Panel.Visible = StepHF.Value == "2";
        Step3Panel.Visible = StepHF.Value == "3";

        EstadoCasoDDL.Enabled = StepHF.Value == "2";

        proccess = true;
    }
    protected void NextWizardLB_Click ( object sender, EventArgs e )
    {
        bool moveTo = (sender as LinkButton).CommandName == "next";
        bool save = moveTo ? true : (SaveHF.Value == "1");
        switch (StepHF.Value)
        {
            case "1":
                finishStep1("2", save);
                break;
            case "2":
                Control control = (sender as LinkButton).Controls[0];
                Label label = new Label();
                if (control != null)
                    label = (control as Label);
                if ((sender as LinkButton).Text.Contains("Salir") || label.Text.Contains("Salir"))
                {
                    Response.Redirect("~/SOAT/SOATList.aspx", true);
                }
                string move = moveTo ? "2" : "1";
                finishStep2(move, save);
                break;
            case "3":
                finishStep3("2", save);
                break;
        }
    }
    [WebMethod]
    public static InformacionPolizaAlianza CargarDeAlianza(string nroRoseta, string placa)
    {
        bool isRosetaSearch = false;
        //Total, Lugar Venta, Razón social, CI, placa, Tipo, Sector y Cilindrada.
        InformacionPolizaAlianza resultado = new InformacionPolizaAlianza();
        List<InformacionPolizaAlianza> polizas = new List<InformacionPolizaAlianza>();

        if (nroRoseta.Length > 0)
            isRosetaSearch = true;


        

        return resultado;
    }

    protected void finishStep1 ( string movetoStep, bool save )
    {
        if (save)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(ClienteDDL.SelectedValue))
                    ClienteDDL.DataBind();

                Siniestro siniestro = new Siniestro();
                siniestro.SiniestroId = SiniestroID;
                siniestro.ClienteId = Convert.ToInt32(ClienteDDL.SelectedValue);
                siniestro.OperacionId = OperacionesIdTextBox.Text.ToUpper();
                siniestro.LugarDpto = DepartamentoDDL.SelectedValue.ToUpper();
                siniestro.LugarProvincia = ProvinciaHF.Value.ToUpper();
                siniestro.NumeroRoseta = NumeroRosetaTextBox.Text.ToUpper();
                siniestro.NumeroPoliza = NumeroRosetaTextBox.Text.ToUpper();  //PolizaTextBox.Text.ToUpper();
                siniestro.Placa = PlacaTextBox.Text.ToUpper();
                siniestro.FechaSiniestro = FechaSiniestroRDP.SelectedDate.Value;
                siniestro.FechaDenuncia = FechaDenunciaRDP.SelectedDate.Value;
                siniestro.TipoCausa = TipoCausaRBL.SelectedValue;
                siniestro.Causa = CausaTextBox.Text.ToUpper();
                siniestro.Zona = AreaRadioList.SelectedValue.ToUpper(); //ZonaTextBox.Text.ToUpper();
                siniestro.Sindicato = SindicatoTextBox.Text.ToUpper();
                siniestro.LugarVenta = LugarVentaDDL.SelectedValue.ToUpper();
                siniestro.NombreTitular = NombreTitularTextBox.Text.ToUpper();
                siniestro.NombreInspector = NombreInspectorTextBox.Text.ToUpper();
                siniestro.CITitular = CITitularTextBox.Text.ToUpper();
                siniestro.Placa = PlacaTextBox.Text.ToUpper();
                siniestro.Tipo = TipoVehiculoDDL.SelectedValue.ToUpper();
                siniestro.NroChasis = NroChasisTextBox.Text.ToUpper();
                siniestro.NroMotor = NroMotorTextBox.Text.ToUpper();                
                siniestro.Sector = SectorDDL.SelectedValue.ToUpper();                

                Seguimiento seguimiento = new Seguimiento();
                seguimiento.Acuerdo = AcuerdoTransaccionalRBL.SelectedValue == "1";
                seguimiento.Rechazado = RechazadoRBL.SelectedValue == "1";
                seguimiento.Observaciones = ObservacionesTextBox.Text;

                if (!IsFinished)
                    if (siniestro.SiniestroId <= 0)
                    {
                        if (!SiniestroBLL.ValidateSiniestro(siniestro.FechaSiniestro, siniestro.OperacionId, siniestro.LugarDpto))
                        {
                            OperacionesIdTextBox.Focus();
                            SystemMessages.DisplaySystemErrorMessage("El siniestro '" + siniestro.OperacionId + "' ya existe.");
                            return;
                        }
                        SiniestroBLL.InsertSiniestro(ref siniestro, seguimiento);
                    }
                    else
                    {
                        if (siniestro.OperacionId != OperacionesIdHF.Value)
                        {
                            if (!SiniestroBLL.ValidateSiniestro(siniestro.FechaSiniestro, siniestro.OperacionId, siniestro.LugarDpto))
                            {
                                OperacionesIdTextBox.Focus();
                                SystemMessages.DisplaySystemErrorMessage("El siniestro '" + siniestro.OperacionId + "' ya existe.");
                                return;
                            }
                        }
                        SiniestroBLL.UpdateSiniestro(siniestro, seguimiento);
                    }
                SiniestroID = siniestro.SiniestroId;
            }
            catch (Exception q)
            {
                string siniestro = "\n\tSiniestroId = " + SiniestroID;
                try { siniestro += "\n\tClienteId = " + ClienteDDL.SelectedValue; }
                catch (Exception x) { siniestro += "\n\tClienteId = [" + x.Message + "]"; }
                try { siniestro += "\n\tFechaSiniestro = " + FechaSiniestroRDP.SelectedDate.Value; } 
                catch (Exception x) { siniestro += "\n\tFechaSiniestro = [" + x.Message + "]"; }
                try { siniestro += "\n\tFechaDenuncia = " + FechaDenunciaRDP.SelectedDate.Value;}
                catch (Exception x) { siniestro += "\n\tFechaDenuncia = [" + x.Message + "]"; }
                try { siniestro += "\n\tTipoCausa = " + TipoCausaRBL.SelectedValue; }
                catch (Exception x) { siniestro += "\n\tTipoCausa = [" + x.Message + "]"; }
                try { siniestro += "\n\tCausa = " + CausaTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tCausa = [" + x.Message + "]"; }
                try { siniestro += "\n\tLugarDpto = " + DepartamentoDDL.SelectedValue.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tLugarDpto = [" + x.Message + "]"; }
                try { siniestro += "\n\tLugarProvincia = " + ProvinciaHF.Value.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tLugarProvincia = [" + x.Message + "]"; }
                try { siniestro += "\n\tZona = " + AreaRadioList.SelectedValue.ToUpper(); /*ZonaTextBox.Text.ToUpper();*/ }
                catch (Exception x) { siniestro += "\n\tZona = [" + x.Message + "]"; }
                try { siniestro += "\n\tSindicato = " + SindicatoTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tSindicato = [" + x.Message + "]"; }
                try { siniestro += "\n\tOperacionId = " + OperacionesIdTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tOperacionId = [" + x.Message + "]"; }
                try { siniestro += "\n\tNumeroRoseta = " + NumeroRosetaTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tNumeroRoseta = [" + x.Message + "]"; }
                try { siniestro += "\n\tNumeroPoliza = " + NumeroRosetaTextBox.Text.ToUpper();/*PolizaTextBox.Text.ToUpper();*/ }
                catch (Exception x) { siniestro += "\n\tNumeroPoliza = [" + x.Message + "]"; }
                try { siniestro += "\n\tLugarVenta = " + LugarVentaDDL.SelectedValue.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tLugarVenta = [" + x.Message + "]"; }
                try { siniestro += "\n\tNombreTitular = " + NombreTitularTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tNombreTitular = [" + x.Message + "]"; }
                try { siniestro += "\n\tCITitular = " + CITitularTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tCITitular = [" + x.Message + "]"; }
                try { siniestro += "\n\tPlaca = " + PlacaTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tPlaca = [" + x.Message + "]"; }
                try { siniestro += "\n\tTipo = " + TipoVehiculoDDL.SelectedValue.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tTipo = [" + x.Message + "]"; }
                try { siniestro += "\n\tNroChasis = " + NroChasisTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tNroChasis = [" + x.Message + "]"; }
                try { siniestro += "\n\tNroMotor = " + NroMotorTextBox.Text.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tNroMotor = [" + x.Message + "]"; }
                try { siniestro += "\n\tSector = " + SectorDDL.SelectedValue.ToUpper(); }
                catch (Exception x) { siniestro += "\n\tSector = [" + x.Message + "]"; }
                log.Error("Error al " + (SiniestroID > 0 ? "actualizar" : "insertar") + " siniestro: " + siniestro, q);
                SystemMessages.DisplaySystemErrorMessage("Error al guardar el siniestro.");
                return;
            }
        }
        if (DatosAdicionalesFileManager.Visible)
        {
            MoveToStep(movetoStep);
            
        }
        else
        {
            DatosAdicionalesFileManager.Visible = true;
            DatosAdicionalesFileManager.ObjectId = SiniestroID;
        }
    }
    protected void finishStep2 ( string movetoStep, bool save )
    {
        try
        {
            bool newAccidentado = AccidentadoID <= 0;
            if (save && ((Step2Panel.GroupingText == "Datos del Accidentado")
                     || (Step2Panel.GroupingText == "Datos del Conductor")))
            {
                Accidentado accidentado = new Accidentado();
                accidentado.AccidentadoId = AccidentadoID;
                accidentado.Nombre = NombresTextBox.Text.ToUpper().Trim();
                accidentado.CarnetIdentidad = CITextBox.Text.ToUpper().Trim();
                accidentado.Genero = SexoRBL.SelectedValue == "1";
                accidentado.FechaNacimiento = FechaNacimientoRDP.IsEmpty ? SqlDateTime.MinValue.Value : FechaNacimientoRDP.SelectedDate.Value;
                accidentado.EstadoCivil = EstadoCivilDDL.SelectedValue.ToUpper();
                accidentado.LicenciaConducir = LicenciaDDL.SelectedValue;
                accidentado.Tipo = (EstadoRBL.SelectedValue == "-") ? "-" : TipoAccidentadoRBL.SelectedValue.ToUpper();
                accidentado.Estado = (EstadoRBL.SelectedValue.ToUpper() != "F");
                accidentado.IsIncapacidadTotal = (EstadoRBL.SelectedValue.ToUpper() == "I");
                //Gets Accidentado and checks its previous Estado
                Accidentado accidentado_old = null;
                
                if(AccidentadoID > 0 && SiniestroID > 0)
                    accidentado_old = AccidentadoBLL.GetAccidentadoByID(AccidentadoID, SiniestroID);

                if (accidentado_old != null && accidentado.Estado != accidentado_old.Estado)
                {
                    decimal montoGestion = 0;
                    if (accidentado.Estado && accidentado.Tipo == "A" && !accidentado.IsIncapacidadTotal)//si el nuevo estado es herido
                    {
                        montoGestion = Configuration.GetMontoGestion();
                        if (montoGestion > 0)
                            GastosEjecutadosBLL.UpdateGastosEjecutados(SiniestroID, AccidentadoID, montoGestion);

                    }
                    else
                    {
                        montoGestion = Configuration.GetMontoGestionFallecido();
                        if (montoGestion > 0)
                            GastosEjecutadosBLL.UpdateGastosEjecutados(SiniestroID, AccidentadoID, montoGestion);
                    }
                }
                if (!IsFinished)
                {
                    AccidentadoBLL.SaveAccidentado(ref accidentado, SiniestroID);
                    AccidentadoID = accidentado.AccidentadoId;
                }
            }
            switch (Step2Panel.GroupingText)
            {
                case "Datos del Conductor":
                    if (AccidentadoID > 0)
                        if (EstadoRBL.SelectedValue == "-"
                            || movetoStep == "1")
                        {
                            movetoStep = "2";
                            AccidentadoID = 0;
                        }
                        else if (newAccidentado)
                        {
                            movetoStep = "3";
                        }
                        else
                        {
                            AccidentadoID = 0;
                        }
                    break;
                case "Datos del Accidentado":
                    if (movetoStep == "1")
                    {
                        movetoStep = "2";
                        AccidentadoID = 0;
                    }
                    else if (newAccidentado)
                    {
                        movetoStep = "3";
                    }
                    else
                    {
                        AccidentadoID = 0;
                    }
                    break;
                default:
                    movetoStep = movetoStep == "1" ? "1" : "2.1";
                    break;
            }
            MoveToStep(movetoStep);

        }
        catch (Exception q)
        {
            string accidentado = "\n\tSiniestroId = " + SiniestroID;
            try { accidentado += "\n\tAccidentadoId = " + AccidentadoID; } catch { }
            try { accidentado += "\n\tCarnetIdentidad = " + CITextBox.Text.ToUpper(); } catch { }
            try { accidentado += "\n\tNombre = " + NombresTextBox.Text.ToUpper(); } catch { }
            try { accidentado += "\n\tGenero = " + SexoRBL.SelectedValue; } catch { }
            try { accidentado += "\n\tFechaNacimiento = " + (FechaNacimientoRDP.IsEmpty ? SqlDateTime.MinValue.Value : FechaNacimientoRDP.SelectedDate.Value); } catch { }
            try { accidentado += "\n\tEstadoCivil = " + EstadoCivilDDL.SelectedValue.ToUpper(); } catch { }
            try { accidentado += "\n\tLicenciaConducir = " + LicenciaDDL.SelectedValue; } catch { }
            try { accidentado += "\n\tTipo = " + TipoAccidentadoRBL.SelectedValue.ToUpper(); } catch { }
            try { accidentado += "\n\tEstado = " + EstadoRBL.SelectedValue.ToUpper(); } catch { }
            log.Error("Error al Salvar Accidentado:" + accidentado, q);
            if (Step3Panel.GroupingText == "Datos del Conductor")
            {
                SystemMessages.DisplaySystemErrorMessage("Error al guardar los datos del Conductor.");
            }
            else
            {
                SystemMessages.DisplaySystemErrorMessage("Error al guardar los datos del Accidentado.");
            }
        }
    }
    protected void finishStep3 ( string movetoStep, bool save )
    {
        try
        {
            bool isUpdate = GestionMedicaIdHF.Value.EndsWith("UPDATE");
            GestionMedicaIdHF.Value = GestionMedicaIdHF.Value.Replace("UPDATE", "");
            if (save)
            {
                GestionMedica gestionMedica = new GestionMedica();
                if (GestionMedicaIdHF.Value != "NEW" && GestionMedicaIdHF.Value != "FIRST")
                    gestionMedica.GestionMedicaId = Convert.ToInt32("0" + GestionMedicaIdHF.Value);

                gestionMedica.Nombre = NombreTextBox.Text;
                /*try { gestionMedica.Nombre = NombreTextBox.SelectedTags.Split(new char[] { ',' })[0].ToUpper(); }
                catch { gestionMedica.Nombre = NombreTextBox.SelectedTags.ToUpper(); }*/
                if (string.IsNullOrWhiteSpace(gestionMedica.Nombre))
                {
                    NombreRFV.Style.Remove("display");
                    return;
                }
                int proveedorId = 0;
                int.TryParse(ProveedorGestionDDL.SelectedValue, out proveedorId);

                gestionMedica.ProveedorId = proveedorId;
                gestionMedica.FechaVisita = FechaVisitaRDP.IsEmpty ? SqlDateTime.MinValue.Value : FechaVisitaRDP.SelectedDate.Value;
                gestionMedica.Grado = GradoDDL.SelectedValue.ToUpper();
                gestionMedica.DiagnosticoPreliminar = DiagnosticoPreliminarTextBox.Text.ToUpper();
                gestionMedica.SiniestroId = SiniestroID;
                gestionMedica.AccidentadoId = AccidentadoID;

                if (GestionMedicaIdHF.Value == "FIRST")
                {
                    decimal reservas = convertToDecimal(ReservaGMTextBox.Text);
                    GestionMedicaBLL.InsertGestionMedica(ref gestionMedica, reservas);
                    AccidentadoID = 0;
                }
                else if (!IsFinished)
                    if (gestionMedica.GestionMedicaId <= 0)
                    {
                        GestionMedicaBLL.InsertGestionMedica(ref gestionMedica);
                    }
                    else
                    {
                        GestionMedicaBLL.UpdateGestionMedica(gestionMedica);
                    }
            }

            GestionMedicaIdHF.Value = "";
            MoveToStep("2");
        }
        catch (Exception q)
        {
            string gestionMedica = "\n\tSiniestroId = " + SiniestroID;
            try { gestionMedica += "\n\tAccidentadoId = " + AccidentadoID; } catch { }
            try { gestionMedica += "\n\tGestionMedicaId = " + Convert.ToInt32("0" + GestionMedicaIdHF.Value); } catch { }
            try { gestionMedica += "\n\tNombre = " + ((ProveedorGestionDDL.SelectedItem != null)? ProveedorGestionDDL.SelectedItem.Text: ""); } catch { }
            try { gestionMedica += "\n\tFechaVisita = " + (FechaVisitaRDP.IsEmpty ? SqlDateTime.MinValue.Value : FechaVisitaRDP.SelectedDate.Value); } catch { }
            try { gestionMedica += "\n\tGrado = " + GradoDDL.SelectedValue.ToUpper(); } catch { }
            try { gestionMedica += "\n\tDiagnosticoPreliminar = " + DiagnosticoPreliminarTextBox.Text.ToUpper(); } catch { }
            log.Error("Error al insertar gestionMedica: " + gestionMedica, q);
            SystemMessages.DisplaySystemErrorMessage("Error al guardar los datos de la gestion médica.");
        }
    }
    private decimal convertToDecimal ( string textToConvert )
    {
        try
        {
            string coma = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
            textToConvert = textToConvert.Replace(",", coma).Replace(".", coma);
            return decimal.Parse(textToConvert);
        }
        catch
        {
            return 0;
        }
    }
    protected void MoveToStep ( string moveToStep )
    {
        AccidentadoDataPanel.Visible = false;
        SiniestroDataPanel.Visible = true;
        ConductorDataPanel.Visible = false;
        NewAccidentadoLB.Visible = false;
        PreevWizardLB.Visible = true;
        NextWizardLB.Visible = true;
        NextWizardLabel.Text = "Siguiente";
        PreevWizardLabel.Text = "Anterior";
        switch (moveToStep)
        {
            case "1":
                loadStep1();
                SiniestroDataPanel.Visible = false;
                break;
            case "2":
                bool puedeInsertarAccidentado = false;
                try
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_ACCIDENTADO");
                    puedeInsertarAccidentado = true;
                }
                catch
                {
                    puedeInsertarAccidentado = false;
                }
                NewAccidentadoLB.Visible = puedeInsertarAccidentado;
                loadStep2();
                break;
            case "2.1":
                moveToStep = "2";
                NewAccidentado();
                NextWizardLabel.Text = "Siguiente";
                break;
            case "3":
                loadStep3();
                NextWizardLabel.Text = "Guardar";
                PreevWizardLabel.Text = "Volver al Accidentado";
                break;
        }
        StepHF.Value = moveToStep;
        setStep();
    }
    protected void loadStep1 ()
    {
        try
        {
            Siniestro siniestro = SiniestroBLL.GetSiniestroByID(SiniestroID);

            if (siniestro != null)
            {
                try { ClienteDDL.SelectedValue = siniestro.ClienteId.ToString(); }
                catch { }
                FechaSiniestroRDP.SelectedDate = siniestro.FechaSiniestro;
                FechaDenunciaRDP.SelectedDate = siniestro.FechaDenuncia;
                try { TipoCausaRBL.SelectedValue = siniestro.TipoCausa; } catch { }
                CausaTextBox.Text = siniestro.Causa;
                try { DepartamentoDDL.SelectedValue = siniestro.LugarDpto; } catch { }
                ProvinciaHF.Value = siniestro.LugarProvincia;
                //AreaRadioList.SelectedValue = 

                if (AreaRadioList.Items.FindByValue(siniestro.Zona) != null)
                {
                    AreaRadioList.SelectedValue = siniestro.Zona;
                }
                else
                {
                    AreaRadioList.Items.Insert(0, siniestro.Zona);
                }
                
                //ZonaTextBox.Text = siniestro.Zona;
                TieneSindicatoRBL.SelectedValue = siniestro.HasSindicato ? "S" : "N";
                SindicatoTextBox.Text = siniestro.Sindicato;

                OperacionesIdTextBox.Text = siniestro.OperacionId;
                OperacionesIdHF.Value = siniestro.OperacionId;
                NumeroRosetaTextBox.Text = siniestro.NumeroRoseta;
                NombreInspectorTextBox.Text = siniestro.NombreInspector;
                /*PolizaTextBox.Text = siniestro.NumeroPoliza;*/
                ListItem lugarVenta = LugarVentaDDL.Items.FindByText(siniestro.LugarVenta);
                if (lugarVenta != null)
                {
                    LugarVentaDDL.ClearSelection();
                    lugarVenta.Selected = true;
                }
                NombreTitularTextBox.Text = siniestro.NombreTitular;
                CITitularTextBox.Text = siniestro.CITitular;
                PlacaTextBox.Text = siniestro.Placa;
                NroChasisTextBox.Text = siniestro.NroChasis;
                NroMotorTextBox.Text = siniestro.NroMotor;
                try { TipoVehiculoDDL.SelectedValue = siniestro.Tipo; } catch { }
                //CilindradaTextBox.Text = siniestro.Cilindrada;
                try { SectorDDL.SelectedValue = siniestro.Sector; }
                catch { }
                try
                {
                    Seguimiento seguimiento = SeguimientoBLL.GetSeguimientoByID(SiniestroID);
                    if (seguimiento != null)
                    {
                        AcuerdoTransaccionalRBL.SelectedValue = seguimiento.Acuerdo ? "1" : "0";
                        RechazadoRBL.SelectedValue = seguimiento.Rechazado ? "1" : "0";
                        ObservacionesTextBox.Text = seguimiento.Observaciones;
                    }
                }
                catch (Exception q)
                {
                    log.Error(q);
                    SystemMessages.DisplaySystemErrorMessage("Error al Cargar los datos de Seguimiento.");
                }

                loadSiniestroData(siniestro);
            }
            else
            {
                SiniestroID = 0;
                FechaSiniestroRDP.Clear();
                FechaDenunciaRDP.Clear();
                TipoCausaRBL.SelectedValue = "";
                CausaTextBox.Text = "";
                DepartamentoDDL.ClearSelection();
                ProvinciaHF.Value = "";
                //ZonaTextBox.Text = "";
                TieneSindicatoRBL.SelectedValue = "";
                SindicatoTextBox.Text = "";

                OperacionesIdTextBox.Text = "";
                NumeroRosetaTextBox.Text = "";
                //PolizaTextBox.Text = "";
                LugarVentaDDL.ClearSelection();
                NombreTitularTextBox.Text = "";
                CITitularTextBox.Text = "";
                PlacaTextBox.Text = "";
                TipoVehiculoDDL.ClearSelection();
                NroMotorTextBox.Text = "";
                NroChasisTextBox.Text = "";
                SectorDDL.ClearSelection();
            }
        }
        catch { }

        FechaSiniestroRDP.CssClass += IsFinished ? "disable" : "";
        FechaDenunciaRDP.CssClass += IsFinished ? "disable" : "";
        TipoCausaRBL.Enabled = !IsFinished;
        CausaTextBox.CssClass += IsFinished ? "disable" : "";
        DepartamentoDDL.CssClass += IsFinished ? "disable" : "";
        ProvinciaDDL.CssClass += IsFinished ? "disable" : "";
        /*ZonaTextBox.CssClass*/ AreaRadioList.CssClass += IsFinished ? "disable" : "";
        TieneSindicatoRBL.Enabled = !IsFinished;
        SindicatoTextBox.CssClass += IsFinished ? "disable" : "";

        OperacionesIdTextBox.CssClass += IsFinished ? "disable" : "";
        NumeroRosetaTextBox.CssClass += IsFinished ? "disable" : "";
        //PolizaTextBox.CssClass += IsFinished ? "disable" : "";
        LugarVentaDDL.CssClass += IsFinished ? "disable" : "";
        NombreTitularTextBox.CssClass += IsFinished ? "disable" : "";
        CITitularTextBox.CssClass += IsFinished ? "disable" : "";
        PlacaTextBox.CssClass += IsFinished ? "disable" : "";
        TipoVehiculoDDL.CssClass += IsFinished ? "disable" : "";
        NroChasisTextBox.CssClass += IsFinished ? "disable" : "";
        NroMotorTextBox.CssClass += IsFinished ? "disable" : "";
        SectorDDL.CssClass += IsFinished ? "disable" : "";

        setPermissionsUpdateSiniestro();        
    }

    private void setPermissionsUpdateSiniestro()
    {

        //check update permissions for Siniestro
        bool puedeModificarSiniestro = false;
        if (SiniestroID <= 0)
            return;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_UPDATE_SINIESTRO");
            puedeModificarSiniestro = true;
        }
        catch (Exception)
        {
            puedeModificarSiniestro = false;
        }
        OperacionesIdTextBox.Enabled = puedeModificarSiniestro;
        TipoCausaRBL.Enabled = puedeModificarSiniestro;
        CausaTextBox.Enabled = puedeModificarSiniestro;
        DepartamentoDDL.Enabled = puedeModificarSiniestro;
        AreaRadioList.Enabled = puedeModificarSiniestro;
        ProvinciaDDL.Enabled = puedeModificarSiniestro;
        TieneSindicatoRBL.Enabled = puedeModificarSiniestro;
        SindicatoTextBox.Enabled = puedeModificarSiniestro;

        NumeroRosetaTextBox.Enabled = puedeModificarSiniestro;
        PlacaTextBox.Enabled = puedeModificarSiniestro;
        LugarVentaDDL.Enabled = puedeModificarSiniestro;
        NombreTitularTextBox.Enabled = puedeModificarSiniestro;
        CITitularTextBox.Enabled = puedeModificarSiniestro;
        TipoVehiculoDDL.Enabled = puedeModificarSiniestro;
        NroMotorTextBox.Enabled = puedeModificarSiniestro;
        NroChasisTextBox.Enabled = puedeModificarSiniestro;
        SectorDDL.Enabled = puedeModificarSiniestro;
        ObservacionesTextBox.Enabled = puedeModificarSiniestro;
        NombreInspectorTextBox.Enabled = puedeModificarSiniestro;

    }

    private void prepareConductorForm()
    {
        AccidentadosListPanel.Visible = false;
        AccidentadoModifyPanel.Visible = true;
        TipoAccidentadoRBL.Visible = false;
        TipoAccidentadoLabel.Visible = false;
        if (EstadoRBL.Items.FindByValue("-") == null)
        {
            EstadoRBL.Items.Add(new ListItem("ILESO", "-"));
        }
        if (EstadoRBL.Items.FindByValue("-") == null)
        {
            EstadoRBL.Items.Add(new ListItem("ILESO", "-"));
        }
        LicenciaDDL.SelectedValue = LicenciaDDL.Items[0].Value;
        Step2Panel.GroupingText = "Datos del Conductor";
        NombresTextBox.Text = NombreTitularTextBox.Text;
        CITextBox.Text = CITitularTextBox.Text;
        NewAccidentadoLB.Visible = false;
    }

    protected void loadStep2 ()
    {
        try
        {
            loadSiniestroData();
            GastosEjecutadosListPanel.Visible = false;
            PreliquidacionListPanel.Visible = false;
            GestionMedicaListPanel.Visible = false;
            NGPreliquidacionDDL.Items.Clear();
            PreevWizardLabel.Text = "Ver Datos del Siniestro";

            // by default this activates when you insert Siniestro right after you save it
            // changed to check if user has permissions to insert
            //MarkerInsertCheck
            //ShowAccidentadoList

            bool puedeInsertarAccidentado = false;
            try
            {
                Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_ACCIDENTADO");
                puedeInsertarAccidentado = true;
            }
            catch
            {
                puedeInsertarAccidentado = false;
            }


            if (!SiniestroBLL.HaveConductor(SiniestroID) && puedeInsertarAccidentado && !ShowAccidentadoList)
            {
                prepareConductorForm();
                return;
            }

            
            /*
            NombresTextBox.CssClass += IsFinished ? "disable" : "";
            CITextBox.CssClass += IsFinished ? "disable" : "";
            SexoRBL.Enabled = !IsFinished;
            FechaNacimientoRDP.CssClass += IsFinished ? "disable" : "";
            EdadTextBox.CssClass += IsFinished ? "disable" : "";
            EstadoCivilDDL.CssClass += IsFinished ? "disable" : "";
            TipoAccidentadoRBL.Enabled = !IsFinished;
            LicenciaDDL.CssClass += IsFinished ? "disable" : "";
            TipoAccidentadoRBL.Enabled = !IsFinished;
            EstadoRBL.Enabled = !IsFinished;
             */

            if (AccidentadoID > 0)
            {
                Accidentado accidentado = AccidentadoBLL.GetAccidentadoByID(AccidentadoID, SiniestroID);

                //set permissions here SOAT_MODIFY_ACCIDENTADO
                bool puedeModificar = false;

                try
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_MODIFY_ACCIDENTADO");
                    puedeModificar = true;
                }
                catch (Exception)
                {
                    puedeModificar = false;
                }

                NombresTextBox.Enabled = puedeModificar;
                CITextBox.Enabled = puedeModificar;
                SexoRBL.Enabled = puedeModificar;
                FechaNacimientoRDP.Enabled = puedeModificar;
                EdadTextBox.Enabled = puedeModificar;
                EstadoCivilDDL.Enabled = puedeModificar;
                TipoAccidentadoRBL.Enabled = puedeModificar;
                LicenciaDDL.Enabled = puedeModificar;
                TipoAccidentadoRBL.Enabled = puedeModificar;
                EstadoRBL.Enabled = puedeModificar;
                ReservaAccidentadoTextBox.Enabled = puedeModificar;
                ReservaAccidentadoTextBox.Visible = true;
                ReservaImageButton.Visible = true;
                //ReservaAccidentadoTextBox.Visible = puedeModificar;
                ReservaImageButton.Enabled = puedeModificar;
                //ReservaImageButton.Visible = puedeModificar;




                if (NGPreliquidacionDDL.Items.Count <= 0)
                {
                    List<PreliquidacionDetalle> lista = PreliquidacionBLL.GetPreliquidacionDetalleForCombo(SiniestroID, AccidentadoID);
                    foreach (PreliquidacionDetalle pre in lista)
                    {
                        string text = (pre.IsFactura ? "Factura: " : "Reserva: ") + pre.NumeroReciboFactura
                            + " (" + pre.MontoForDisplay + ")";
                        string proveedor = pre.Proveedor;
                        string value = pre.PreliquidacionDetalleId.ToString();
                        ListItem li = new ListItem(text, value);
                        li.Attributes.Add("proveedor", proveedor);
                        NGPreliquidacionDDL.Items.Add(li);
                    }
                    if (NGPreliquidacionDDL.Items.Count > 0)
                        NGPreliquidacionDDL.SelectedIndex = 0;
                }

                if (accidentado != null)
                {
                    GastosEjecutadosListPanel.Visible = true;
                    //PreliquidacionListPanel.Visible = true;
                    GestionMedicaListPanel.Visible = true;

                    bool puedeInsertarGestionMedica = false;
                    //check for permission for insert
                    //NewGestionMedicaLB
                    try
                    {
                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_VISITA_MEDICA");
                        puedeInsertarGestionMedica = true;
                    }
                    catch (Exception)
                    {
                        puedeInsertarGestionMedica = false;
                    }

                    NewGestionMedicaLB.Visible = puedeInsertarGestionMedica;

                    bool puedeInsertarGastosEjecutados = false;
                    //check for permission for insert
                    //NewGestionMedicaLB
                    try
                    {
                        Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_GASTO_MEDICO");
                        puedeInsertarGastosEjecutados = true;
                    }
                    catch (Exception)
                    {
                        puedeInsertarGastosEjecutados = false;
                    }

                    NewPreliquidacionLB.Visible = puedeInsertarGastosEjecutados;


                    NombresTextBox.Text = accidentado.Nombre;
                    CITextBox.Text = accidentado.CarnetIdentidad;
                    SexoRBL.SelectedValue = accidentado.Genero ? "1" : "0";
                    if (accidentado.FechaNacimiento == SqlDateTime.MinValue)
                    {
                        FechaNacimientoRDP.SelectedDate = FechaNacimientoRDP.MinDate;
                        EdadTextBox.Text = "";
                    }
                    else
                    {
                        FechaNacimientoRDP.SelectedDate = accidentado.FechaNacimiento;
                        int age = DateTime.Now.Year - accidentado.FechaNacimiento.Year;
                        if (accidentado.FechaNacimiento > DateTime.Now.AddYears(-age)) age--;
                        EdadTextBox.Text = age + " años";
                    }
                    EstadoCivilDDL.SelectedValue = accidentado.EstadoCivil.ToUpper();
                    if (accidentado.Conductor)
                    {
                        Step2Panel.GroupingText = "Datos del Conductor";
                        TipoAccidentadoRBL.Visible = false;
                        TipoAccidentadoLabel.Visible = false;
                        if (EstadoRBL.Items.FindByValue("-") == null)
                        {
                            EstadoRBL.Items.Add(new ListItem("ILESO", "-"));
                        }
                        if (LicenciaDDL.Items.FindByValue("-") != null)
                        {
                            LicenciaDDL.Items.RemoveAt(2);
                        }
                    }
                    else
                    {
                        Step2Panel.GroupingText = "Datos del Accidentado";
                        TipoAccidentadoRBL.Visible = true;
                        TipoAccidentadoLabel.Visible = true;
                        ListItem ninguno = EstadoRBL.Items.FindByValue("-");
                        if (EstadoRBL.Items.FindByValue("-") == null)
                        {
                            EstadoRBL.Items.Remove(ninguno);
                        }
                        if (LicenciaDDL.Items.FindByValue("-") == null)
                        {
                            LicenciaDDL.Items.Add(new ListItem("INDIFERENTE", "-"));
                        }
                        FechaNacimientoRFV.Visible = false;
                        CIRFV.Visible = false;
                    }
                    if (accidentado.Tipo != "-")
                        TipoAccidentadoRBL.SelectedValue = accidentado.Tipo.Trim();
                    EstadoRBL.SelectedValue = accidentado.Tipo == "-" ? "-" : (accidentado.Estado ? "A" : "F");
                    ListItem licenciaItem = LicenciaDDL.Items.FindByValue(accidentado.LicenciaConducir);
                    if (licenciaItem != null)
                    {
                        licenciaItem.Selected = true;
                    }
                    else
                    {
                        LicenciaDDL.SelectedIndex = 1;
                    }

                    NewPreliquidacionLB.OnClientClick = "return OpenNGDialog(0, null, 0, 'G');";
                    //NewGastosEjecutadosLB.OnClientClick = "return OpenNGDialog(0, null, 0, 'R');";
                    Step2Panel.Visible = true;
                    AccidentadosListPanel.Visible = false;
                    AccidentadoModifyPanel.Visible = true;
                    if (IsSaved && ((accidentado.Conductor && accidentado.Tipo.Trim() != "-") || !accidentado.Conductor))
                    {
                        GastosEjecutadosListPanel.Visible = true;
                        //PreliquidacionListPanel.Visible = true;
                        GestionMedicaListPanel.Visible = true;
                        GestionMedicaRadGrid.Columns.FindByUniqueName("DeleteGestionMedica").Visible = LoginSecurity.IsUserAuthorizedPermission("DELETE_SOAT");
                        GastosEjecutadosRadGrid.MasterTableView.DetailTables[0].Columns.FindByUniqueName("DeleteGastosEjecutados").Visible = LoginSecurity.IsUserAuthorizedPermission("DELETE_SOAT");
                        PreliquidacionRadGrid.MasterTableView.DetailTables[0].Columns.FindByUniqueName("DeletePreliquidacion").Visible = LoginSecurity.IsUserAuthorizedPermission("DELETE_SOAT");
                    }
                    NextWizardLB.Visible = true;
                    NextWizardLabel.Text = "Guardar";
                    PreevWizardLabel.Text = "Volver a Lista de Accidentados";
                    return;
                }
            }

            NGFechaRDP.SelectedDate = DateTime.Now;
            loadConductorData();
            AccidentadoRadGrid.Columns.FindByUniqueName("DeleteAccidentado").Visible = LoginSecurity.IsUserAuthorizedPermission("DELETE_SOAT");
            AccidentadosListPanel.Visible = true;
            AccidentadoModifyPanel.Visible = false;
            AccidentadoID = 0;
            ReservaDetallePNL.Visible = false;
            Step2Panel.GroupingText = "Lista de Accidentados";
            AccidentadoRadGrid.DataBind();
            BackToList.OnClientClick = "";
            FechaNacimientoRFV.Visible = false;
            CIRFV.Visible = false;
            NextWizardLB.Visible = false;
            ListItem item = LicenciaDDL.Items.FindByValue("-");
            if (item == null)
            {
                item = new ListItem("INDIFERENTE", "-");
                LicenciaDDL.Items.Add(item);
            }
            item.Selected = true;
        }
        catch (Exception q){
            //SystemMessages.DisplaySystemErrorMessage("Error al Cargar Accidentado.");
            log.Warn(q);
        }
        finally
        {
            GestionMedicaRadGrid.DataBind();
            GastosEjecutadosRadGrid.DataBind();
            PreliquidacionRadGrid.DataBind();
        }
    }
    protected void loadStep3 ()
    {
        try
        {
            loadSiniestroData();
            loadAccidentadoData();

            ReservaDiv.Visible = false;
            //NombreTextBox.Enabled = !IsFinished;
            ProveedorGestionDDL.Enabled = !IsFinished;
            //NombreTextBox.Attributes.("readonly", "readonly");
            FechaVisitaRDP.CssClass += IsFinished ? "disable" : "";
            GradoDDL.CssClass += IsFinished ? "disable" : "";
            FechaNacimientoRDP.CssClass += IsFinished ? "disable" : "";
            DiagnosticoPreliminarTextBox.CssClass += IsFinished ? "disable" : "";

            int gestionMedicaId = 0;
            if (GestionMedicaIdHF.Value == "NEW")
            {
                FechaVisitaRDP.SelectedDate = DateTime.UtcNow.AddHours(-4.00);
            } 
            else try
            {
                gestionMedicaId = int.Parse(GestionMedicaIdHF.Value);
            }catch{}
            if (gestionMedicaId > 0)
            {
                GestionMedica gestionMedica = GestionMedicaBLL.GetGestionMedicaByID(gestionMedicaId);

                GestionMedicaIdHF.Value = gestionMedica.GestionMedicaId.ToString();
                NombreTextBox.Text = gestionMedica.Nombre;

                Artexacta.App.SOAT.Proveedor.Proveedor proveedor = null;
                try 
	            {
                    proveedor = (gestionMedica.ProveedorId > 0) ? Artexacta.App.SOAT.Proveedor.BLL.ProveedorBLL.GetSOATProveedorById(gestionMedica.ProveedorId) : null;
	            }
                catch (Exception)
	            {
		            
	            }
                if (gestionMedica.ProveedorId > 0 && proveedor != null)
                {
                    //ProveedorGestionDDL.SelectedValue = proveedor.ProveedorId.ToString();
                    //ProveedorGestionDDL.SelectedItem.Text = proveedor.Nombre;
                    ProveedorGestionDDL.Items.Add(new RadComboBoxItem(proveedor.Nombre, proveedor.ProveedorId.ToString()));
                    ProveedorGestionDDL.SelectedValue = proveedor.ProveedorId.ToString();
                    ProveedorGestionDDL.DataBind();
                    CiudadDerivacionComboBox.SelectedValue = proveedor.CiudadId;
                }
                else
                {
                    ProveedorGestionDDL.ClearSelection();
                    ProveedorGestionDDL.Items.Clear();
                    CiudadDerivacionComboBox.ClearSelection();
                }

                if (gestionMedica.FechaVisita <= SqlDateTime.MinValue.Value || gestionMedica.FechaVisita <= FechaVisitaRDP.MinDate)
                {
                    FechaVisitaRDP.Clear();
                }
                else
                {
                    FechaVisitaRDP.SelectedDate = gestionMedica.FechaVisita;
                }
                GradoDDL.SelectedValue = gestionMedica.Grado;
                DiagnosticoPreliminarTextBox.Text = gestionMedica.DiagnosticoPreliminar;
            }
            else
            {
                NombreTextBox.Text = "";
                GradoDDL.ClearSelection();
                ReservaGMTextBox.Text = "0";
                DiagnosticoPreliminarTextBox.Text = "";
                //ReservaGMTextBox.Enabled = false;
                Accidentado accidentadoTemp = null;
                try
                {
                    accidentadoTemp = AccidentadoBLL.GetAccidentadoByID(AccidentadoID, SiniestroID);
                }
                catch (Exception)
                {
                    
                }


                ProveedorGestionDDL.ClearSelection();
                ProveedorGestionDDL.Items.Clear();
                CiudadDerivacionComboBox.ClearSelection();
                //A - Accidentado(Herido), everything else is 
                

                if (GestionMedicaIdHF.Value == "")
                {
                    ReservaDiv.Visible = true;
                    string coma = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;
                    string miles = NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator;
                    string format = "0.00";
                    string coberturaRAW = Configuration.GetMontoGestion().ToString();
                    string cobertura = Configuration.GetMontoGestion().ToString(format); //; //.Replace(miles, "")
                    CoberturaGMTextBox.Text = cobertura;
                    
                    if (accidentadoTemp != null && (!accidentadoTemp.Estado || accidentadoTemp.IsIncapacidadTotal))
                    {
                        ReservaGMTextBox.Enabled = false;
                        ReservaGMTextBox.Text = Configuration.GetMontoGestionFallecido().ToString(format);
                        CoberturaGMTextBox.Text = Configuration.GetMontoGestionFallecido().ToString(format);
                        //if(accidentadoTemp.Estado)
                        GradoDDL.SelectedValue = "FATAL";
                    }
                    GestionMedicaIdHF.Value = "FIRST";
                }
                else
                {
                    GestionMedicaIdHF.Value = "NEW";
                }
            }
        }
        catch (Exception q)
        {
            if (GestionMedicaIdHF.Value == "FIRST" || GestionMedicaIdHF.Value == "NEW")
            {
                SystemMessages.DisplaySystemErrorMessage("Error desconocido en Gestion Medica");
            }
            else
            {
                SystemMessages.DisplaySystemErrorMessage("Error al cargar Gestion Medica");
            }
            log.Error("Error en loadStep3 () en la Pagina SOATwizard.aspx", q);
        }
    }
    private void loadConductorData ()
    {
        if (SiniestroBLL.HaveConductor(SiniestroID))
        {
            Accidentado conductor = AccidentadoBLL.GetConductor(SiniestroID);
            if (conductor.Tipo == "-")
            {
                ConductorDataPanel.Visible = true;
                ConductorIdHF.Value = conductor.AccidentadoId.ToString();
                NombreConductorLabel.Text = @"NOMBRE: <span class=""NoStrong"">" + conductor.Nombre + "</span>";
                CIConductorLabel.Text = @"CI: <span class=""NoStrong"">" + conductor.CarnetIdentidad + "</span>";
                TieneLicenciaLabel.Text = @"TIENE LIC. DE CONDUCIR: <span class=""NoStrong"">" + conductor.LicenciaConducirForDisplay + "</span>";

                bool puedeInsertarAccidentado = false;
                try
                {
                    Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_MODIFY_ACCIDENTADO");
                    puedeInsertarAccidentado = true;
                }
                catch (Exception)
                {
                    puedeInsertarAccidentado = false;
                }
                ModifyDriverLB.Visible = puedeInsertarAccidentado;



            }
        }
        else
        {
            ConductorDataPanel.Visible = false;
            NombreConductorLabel.Text = @"NOMBRE: <span class=""NoStrong"">" + "" + "</span>";
            CIConductorLabel.Text = @"CI: <span class=""NoStrong"">" + "" + "</span>";
            TieneLicenciaLabel.Text = @"TIENE LIC. DE CONDUCIR: <span class=""NoStrong"">" + "" + "</span>";

        }

    }

    private void loadAccidentadoData ()
    {
        if (string.IsNullOrWhiteSpace(NombreAccidentadoTextBox.Text))
        {
            loadAccidentadoData(AccidentadoBLL.GetAccidentadoByID(AccidentadoID, SiniestroID));
        }
        AccidentadoDataPanel.Visible = true;
    }
    private void loadAccidentadoData ( Accidentado accidentado )
    {
        if (accidentado != null)
        {
            NombreAccidentadoTextBox.Text = (accidentado.Estado ? "ACCIDENTADO: " : "FALLECIDO: ") + @"<span class=""NoStrong"">" + accidentado.Nombre + "</span>";
            CIAccidentadoTextBox.Text = @"CARNET DE IDENTIDAD: <span class=""NoStrong"">" + accidentado.CarnetIdentidad + "</span>";
            /*EstadoSeguimientoLabel.Text = @"ESTADO SEGUIMIENTO: <span class=""NoStrong"">" + accidentado.EstadoSeguimiento + "</span>";*/
        }
    }
    private void loadSiniestroData ()
    {
        Siniestro siniestro = null;
        if (string.IsNullOrWhiteSpace(FechaSiniestro.Text))
        {
            siniestro = SiniestroBLL.GetSiniestroByID(SiniestroID);
        }
        loadSiniestroData(siniestro);
    }
    private void loadSiniestroData ( Siniestro siniestro )
    {
        if (siniestro != null)
        {
            RedCliente cliente = RedClienteBLL.GetRedClienteByClienteId(siniestro.ClienteId);
            NombreCliente.Text = @"CLIENTE: <span class=""NoStrong"">" + cliente.NombreJuridico + "</span>";
            FechaSiniestro.Text = @"FECHA DE SINIESTRO: <span class=""NoStrong"">" + siniestro.FechaSiniestro.ToShortDateString() + "</span>";
            FechaDenuncia.Text = @"FECHA DE DENUNCIA: <span class=""NoStrong"">" + siniestro.FechaDenuncia.ToShortDateString() + "</span>";
            FechaVisitaRDP.SelectedDate = siniestro.FechaDenuncia;
            NroPoliza.Text = @"NUMERO DE POLIZA: <span class=""NoStrong"">" + siniestro.NumeroPoliza + "</span>";
            OperacionId.Text = @"IDENTIFICADOR DE OPERACION: <span class=""NoStrong"">" + siniestro.OperacionId + "</span>";

            LugarSiniestro.Text = @"LUGAR DEL SINIESTRO: <span class=""NoStrong"">" + siniestro.LugarDpto + "</span>";

            

            

            EstadoCasoDDL.SelectedValue = SeguimientoBLL.GetEstado(SiniestroID);
            //EstadoCasoDDL.Visible = true;
            //EstadoCasoLabel.Visible = true;
        }
        if (SiniestroID <= 0)
        {
            //EstadoCasoDDL.Visible = false;
            //EstadoCasoLabel.Visible = false;
        }
    }
    protected void BackToList_Click ( object sender, EventArgs e )
    {
        //try
        //{
        //    if (SiniestroID > 0 && !IsSaved)
        //    {
        //        SiniestroBLL.DeleteSiniestro(SiniestroID);
        //    }
        //}
        //catch { }
        Response.Redirect("~/SOAT/SOATList.aspx");
    }
    protected void AccidentadoButton_Command ( object sender, CommandEventArgs e )
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                string[] strArgument = e.CommandArgument.ToString().Split(new char[] { ',' });
                int AccidentadoId = Convert.ToInt32(strArgument[0]);
                Session["SiniestroId"] = SiniestroID.ToString();
                Session["AccidentadoId"] = AccidentadoId;
                Session["Teminado"] = strArgument[1] == "TERMINADO" ? "1" : null;
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir AccidentadoID in AccidentadoButton_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar el Accidentado");
            }
            Response.Redirect("~/SOAT/SOATWizard.aspx");
        }
        else if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                AccidentadoID = 0;
                int AccidentadoId = Convert.ToInt32(e.CommandArgument);

                AccidentadoBLL.DeleteAccidentado(AccidentadoId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el Accidentado seleccionado");
                AccidentadoRadGrid.DataBind();
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir AccidentadoID in AccidentadoButton_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el Accidentado.");
            }
            NombresTextBox.Text = "";
            CITextBox.Text = "";
            SexoRBL.SelectedValue = "1";
            FechaNacimientoRDP.SelectedDate = DateTime.Today.AddYears(-80);
            EdadTextBox.Text = "";
            EstadoCivilDDL.SelectedValue = EstadoCivilDDL.Items[0].Value;
            LicenciaDDL.SelectedValue = LicenciaDDL.Items[0].Value; ;
            TipoAccidentadoRBL.SelectedValue = "A";
        }
        else if (e.CommandName.Equals("AddGestion"))
        {
            if (AccidentadoID <= 0)
                AccidentadoID = Convert.ToInt32(e.CommandArgument);
            GestionMedicaIdHF.Value = "NEW";
            MoveToStep("3");
        }
    }
    protected void SearchAccidentadoLB_Click ( object sender, EventArgs e )
    {
        AccidentadoID = 0;
        StepHF.Value = "2";
    }
    protected void NewAccidentadoLB_Click ( object sender, EventArgs e )
    {
        ShowAccidentadoList = false;
        NewAccidentado();
    }
    protected void NewAccidentado ()
    {        
        PreevWizardLabel.Text = "Volver a Lista de Accidentados";

        if (!SiniestroBLL.HaveConductor(SiniestroID))
        {
            prepareConductorForm();
        }
        else
        {

            Step2Panel.GroupingText = "Datos del Accidentado";
            AccidentadosListPanel.Visible = false;
            NewAccidentadoLB.Visible = false;
            NextWizardLB.Visible = true;
            AccidentadoModifyPanel.Visible = true;
            GastosEjecutadosListPanel.Visible = false;
            PreliquidacionListPanel.Visible = false;
            GestionMedicaListPanel.Visible = false;
            TipoAccidentadoRBL.Visible = true;
            TipoAccidentadoLabel.Visible = true;

            NombresTextBox.Text = "";
            CITextBox.Text = "";
            SexoRBL.SelectedValue = "1";
            FechaNacimientoRDP.Clear();
            EdadTextBox.Text = "";
            EstadoCivilDDL.SelectedValue = EstadoCivilDDL.Items[0].Value;
            LicenciaDDL.SelectedValue = LicenciaDDL.Items[0].Value;
            ListItem ninguno = EstadoRBL.Items.FindByValue("-");
            if (ninguno != null)
            {
                EstadoRBL.Items.Remove(ninguno);
            }
            EstadoRBL.SelectedValue = "A";
            FechaNacimientoRFV.Visible = false;
            CIRFV.Visible = false;
            ListItem item = LicenciaDDL.Items.FindByValue("-");
            if (item == null)
            {
                item = new ListItem("INDIFERENTE", "-");
                LicenciaDDL.Items.Add(item);
            }
            item.Selected = true;
            AcuerdoTransaccionalRBL.SelectedIndex = 1;
            RechazadoRBL.SelectedIndex = 1;
            ObservacionesTextBox.Text = "";
            proccess = true;
            IsSaved = false;
            AccidentadoID = 0;
            ReservaDetallePNL.Visible = false;

            /* enables control */
            NombresTextBox.Enabled = true;
            CITextBox.Enabled = true;
            SexoRBL.Enabled = true;
            FechaNacimientoRDP.Enabled = true;
            EdadTextBox.Enabled = true;
            EstadoCivilDDL.Enabled = true;
            TipoAccidentadoRBL.Enabled = true;
            LicenciaDDL.Enabled = true;
            TipoAccidentadoRBL.Enabled = true;
            EstadoRBL.Enabled = true;
            //ReservaAccidentadoTextBox.Enabled = true;
            


        }
        NextWizardLabel.Text= "SIGUIENTE";
    }
    protected void ModifyDriverLB_Click ( object sender, EventArgs e )
    {
        AccidentadoID = int.Parse("0" + ConductorIdHF.Value);
        MoveToStep("2");
    }
    protected void NGSaveLB_Click ( object sender, EventArgs e )
    {
        try
        {
            if (TypeHF.Value == "R")
            {
                GastosEjecutadosDetalle gastosEjecutadosDetalle = new GastosEjecutadosDetalle();
                gastosEjecutadosDetalle.SiniestroId = SiniestroID;
                gastosEjecutadosDetalle.AccidentadoId = AccidentadoID;
                gastosEjecutadosDetalle.PreliquidacionDetalleId = int.Parse("0" + PreliquidacionDetalleIdHF.Value);
                gastosEjecutadosDetalle.Tipo = NGTipoGastoDDL.SelectedValue;
                gastosEjecutadosDetalle.Fecha = FechaForDB(NGFechaRDP.SelectedDate);
                gastosEjecutadosDetalle.Proveedor = ProveedorHF.Value;
                gastosEjecutadosDetalle.FechaReciboFactura = FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                gastosEjecutadosDetalle.NumeroReciboFactura = NumeroReciboFacturaTextBox.Text;
                gastosEjecutadosDetalle.Monto = convertToDecimal(NGMontoTextBox.Text);
                gastosEjecutadosDetalle.GastosEjecutadosDetalleId = int.Parse("0" + saveIdHF.Value);
                if (gastosEjecutadosDetalle.GastosEjecutadosDetalleId <= 0)
                {
                    GastosEjecutadosBLL.InsertGastosEjecutadosDetalle(gastosEjecutadosDetalle);
                }
                else
                {
                    GastosEjecutadosBLL.UpdateGastosEjecutadosDetalle(gastosEjecutadosDetalle);
                }
            }
            else if (TypeHF.Value == "RES")
            {
                decimal montoDecimal = convertToDecimal(NGMontoTextBox.Text);
                decimal cobertura = GastosEjecutadosBLL.GetReservaBySiniestroIdAndAccidentadoId(SiniestroID, AccidentadoID);

                if (cobertura < montoDecimal)
                {
                    //coberturaaaa 
                    SystemMessages.DisplaySystemErrorMessage("No se puede insertar una reserva mayor a la cobertura");
                    return;
                }

                GastosEjecutadosBLL.UpdateReserva(SiniestroID, AccidentadoID, convertToDecimal(NGMontoTextBox.Text));
                GastosEjecutadosRadGrid.DataBind();
            }
            else
            {
                PreliquidacionDetalle PreliquidacionDetalle = new PreliquidacionDetalle();
                PreliquidacionDetalle.SiniestroId = SiniestroID;
                PreliquidacionDetalle.AccidentadoId = AccidentadoID;
                PreliquidacionDetalle.Tipo = NGTipoGastoDDL.SelectedValue;
                PreliquidacionDetalle.Fecha = FechaForDB(NGFechaRDP.SelectedDate);
                try { 
                    PreliquidacionDetalle.Proveedor = 
                        ProveedorTags.SelectedTags.Split(new char[] { ',' })[0].ToUpper(); 
                }
                catch { 
                    PreliquidacionDetalle.Proveedor = ProveedorTags.SelectedTags.ToUpper(); }
                PreliquidacionDetalle.FechaReciboFactura = FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                PreliquidacionDetalle.NumeroReciboFactura = NumeroReciboFacturaTextBox.Text;
                PreliquidacionDetalle.IsFactura = IsFacturaCheckBox.Checked;
                PreliquidacionDetalle.Monto = convertToDecimal(NGMontoTextBox.Text);
                PreliquidacionDetalle.PreliquidacionDetalleId = int.Parse("0" + saveIdHF.Value);
                PreliquidacionDetalle.Estado = EstadoDDL.SelectedValue == "1";
                if (PreliquidacionDetalle.PreliquidacionDetalleId <= 0)
                {
                    //en el insert el saveIDHF.value es el id de preliquidacionDetalleId

                    int idPreliquidacion = PreliquidacionBLL.InsertPreliquidacionDetalle(PreliquidacionDetalle);

                    if (idPreliquidacion <= 0)
                    {
                        SystemMessages.DisplaySystemErrorMessage("Hubo un error insertando el gastro, no se puedo procesar la preliquidacion automatica");
                        return;
                    }


                    GastosEjecutadosDetalle gastosEjecutadosDetalle = new GastosEjecutadosDetalle();
                    gastosEjecutadosDetalle.SiniestroId = PreliquidacionDetalle.SiniestroId;
                    gastosEjecutadosDetalle.AccidentadoId = PreliquidacionDetalle.AccidentadoId;
                    gastosEjecutadosDetalle.PreliquidacionDetalleId = idPreliquidacion;  //int.Parse("0" + PreliquidacionDetalleIdHF.Value);
                    gastosEjecutadosDetalle.Tipo = PreliquidacionDetalle.Tipo; //NGTipoGastoDDL.SelectedValue;
                    gastosEjecutadosDetalle.Fecha = PreliquidacionDetalle.Fecha;  //FechaForDB(NGFechaRDP.SelectedDate);
                    gastosEjecutadosDetalle.Proveedor = PreliquidacionDetalle.Proveedor; //ProveedorHF.Value;
                    gastosEjecutadosDetalle.FechaReciboFactura = PreliquidacionDetalle.FechaReciboFactura; //FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                    gastosEjecutadosDetalle.NumeroReciboFactura = PreliquidacionDetalle.NumeroReciboFactura; //NumeroReciboFacturaTextBox.Text;
                    gastosEjecutadosDetalle.Monto = PreliquidacionDetalle.Monto;  //convertToDecimal(NGMontoTextBox.Text);
                    gastosEjecutadosDetalle.GastosEjecutadosDetalleId = int.Parse("0" + saveIdHF.Value);


                    GastosEjecutadosBLL.InsertGastosEjecutadosDetalle(gastosEjecutadosDetalle);
                }
                else
                {
                    //en el update el saveIDHF.value es el id de preliquidacionDetalleId asi que 
                    //lo cambiamos y obtenemos el id de preliquidacion en base al gastosEjecutadosDetalleId
                    int gastosEjecutadosId = PreliquidacionDetalle.PreliquidacionDetalleId;
                    int preliquidacionDetalleId = PreliquidacionBLL.GetPreliquidacionDetalleIdByGastoesEjecutadosDetalleId(gastosEjecutadosId);
                    
                    GastosEjecutadosDetalle gastosEjecutadosDetalle = new GastosEjecutadosDetalle();
                    gastosEjecutadosDetalle.SiniestroId = PreliquidacionDetalle.SiniestroId;
                    gastosEjecutadosDetalle.AccidentadoId = PreliquidacionDetalle.AccidentadoId;
                    gastosEjecutadosDetalle.PreliquidacionDetalleId = preliquidacionDetalleId;  //int.Parse("0" + PreliquidacionDetalleIdHF.Value);
                    gastosEjecutadosDetalle.Tipo = PreliquidacionDetalle.Tipo; //NGTipoGastoDDL.SelectedValue;
                    gastosEjecutadosDetalle.Fecha = PreliquidacionDetalle.Fecha;  //FechaForDB(NGFechaRDP.SelectedDate);
                    gastosEjecutadosDetalle.Proveedor = PreliquidacionDetalle.Proveedor; //ProveedorHF.Value;
                    gastosEjecutadosDetalle.FechaReciboFactura = PreliquidacionDetalle.FechaReciboFactura; //FechaForDB(FechaReciboFacturaRDP.SelectedDate);
                    gastosEjecutadosDetalle.NumeroReciboFactura = PreliquidacionDetalle.NumeroReciboFactura; //NumeroReciboFacturaTextBox.Text;
                    gastosEjecutadosDetalle.Monto = PreliquidacionDetalle.Monto;  //convertToDecimal(NGMontoTextBox.Text);
                    gastosEjecutadosDetalle.GastosEjecutadosDetalleId = gastosEjecutadosId;
                    GastosEjecutadosBLL.UpdateGastosEjecutadosDetalle(gastosEjecutadosDetalle);

                    PreliquidacionDetalle.PreliquidacionDetalleId = preliquidacionDetalleId;

                    PreliquidacionBLL.UpdatePreliquidacionDetalle(PreliquidacionDetalle);
                }
            }
            NGTipoGastoDDL.ClearSelection();
            NGFechaRDP.SelectedDate = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
            ProveedorTags.SelectedTags = "";
            FechaReciboFacturaRDP.SelectedDate = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
            NumeroReciboFacturaTextBox.Text = "";
            IsFacturaCheckBox.Checked = false;
            NGMontoTextBox.Text = "";
            EstadoDDL.SelectedValue = "1";
            MoveToStep("2");
        }
        catch (Exception q)
        {
            log.Error("Error inserting new Gasto Ejecutado Detalle", q);
            if (TypeHF.Value == "RES")
            {
                SystemMessages.DisplaySystemErrorMessage("Error al guardar la Reserva");
            }
            else
            {
                SystemMessages.DisplaySystemErrorMessage((TypeHF.Value == "R") ? "Error al guardar el gasto." : "Error al guardar la Pre-liquidación.");
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "NGOpenPOPUP", "ShowNGDialog();", true);
        }
        PreliquidacionRadGrid.DataBind();
        GastosEjecutadosRadGrid.DataBind();
    }
    public DateTime FechaForDB(DateTime? fecha){
        if (fecha == null)
        {
            fecha = Configuration.ConvertToClientTimeZone(DateTime.UtcNow);
        }
        return Configuration.ConvertToUTCFromClientTimeZone((DateTime)fecha);
    }
    protected void GestionMedicaRadGrid_Command ( object sender, CommandEventArgs e )
    {
        if (e.CommandName.Equals("Select"))
        {
            try
            {
                GestionMedicaIdHF.Value = e.CommandArgument.ToString();
                MoveToStep("3");
                GestionMedicaIdHF.Value += "UPDATE";
                NextWizardLabel.Text = "Guardar";
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener GestionMedicaId in GestionMedicaRadGrid_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("Error al seleccionar la gestion médica");
            }
        }
        else if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                int GestionMedicaId = Convert.ToInt32(e.CommandArgument);

                GestionMedicaBLL.Delete(GestionMedicaId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente la gestion médica.");
                GestionMedicaRadGrid.DataBind();
                MoveToStep("2");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir GestionMedicaId in GestionMedicaRadGrid_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la gestion médica.");
            }
        }
    }
    protected void GastosEjecutadosRadGrid_Command ( object sender, CommandEventArgs e )
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                int GastosEjecutadosDetalleId = Convert.ToInt32(e.CommandArgument);

                GastosEjecutadosBLL.DeleteGastosEjecutadosDetalle(GastosEjecutadosDetalleId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente la Pre-liquidación.");
                MoveToStep("2");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir GastosEjecutadosDetalleId in GastosEjecutadosRadGrid_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar la Pre-liquidación.");
            }
        }
    }
    protected void PreliquidacionRadGrid_Command ( object sender, CommandEventArgs e )
    {
        if (e.CommandName.Equals("Eliminar"))
        {
            try
            {
                int PreliquidacionDetalleId = Convert.ToInt32(e.CommandArgument);

                PreliquidacionBLL.DeletePreliquidacionDetalle(PreliquidacionDetalleId);
                SystemMessages.DisplaySystemMessage("Se elimino correctamente el Gasto.");
                MoveToStep("2");
            }
            catch (Exception ex)
            {
                log.Error("Error al obtener/Convertir PreliquidacionDetalleId in PreliquidacionRadGrid_Command on page SOATWizard.aspx", ex);
                SystemMessages.DisplaySystemErrorMessage("No se pudo eliminar el Gasto.");
            }
        }
    }
    protected void AccidentadoODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Accidentados.");
            log.Error("Function AccidentadoODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void GestionMedicaODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Gestión Médica.");
            log.Error("Function GestionMedicaODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void PreliquidacionODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Pre-liquidación.");
            log.Error("Function PreliquidacionODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void GastosEjecutadosODS_Selected ( object sender, ObjectDataSourceStatusEventArgs e )
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Gastos Médicos.");
            log.Error("Function GastosEjecutadosODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
    protected void FileManager_Command ( object sender, CommandEventArgs e )
    {
        FileManager.OpenFileManager(e.CommandName, Convert.ToInt32(e.CommandArgument));
    }
    protected void PreliquidacionRadGrid_ItemCreated ( object sender, Telerik.Web.UI.GridItemEventArgs e )
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            proccessGastos(e.Item);
        }
    }
    protected void GastosEjecutadosRadGrid_ItemCreated ( object sender, Telerik.Web.UI.GridItemEventArgs e )
    {
        if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
        {
            proccessGastos(e.Item);
        }
    }
    private void proccessGastos ( GridItem gridItem )
    {
        try
        {
            GridDataItem item = (GridDataItem)gridItem;
            string Tipo = item.GetDataKeyValue("Tipo").ToString();
            if (Tipo == "RESERVAS" || Tipo == "TOTALES" || Tipo == "SALDO" || Tipo == "AHORRO")
                gridItem.Cells[0].Text = "";
            if (Tipo == "TOTALES")
            {
                foreach (TableCell cell in gridItem.Cells)
                {
                    if (gridItem.Cells.GetCellIndex(cell) == 0)
                    {
                        cell.Style.Add("border-color", "#FFF");
                    }
                    cell.Style.Add("border-top", "1px dashed #000");
                    cell.Style.Add("background-color", "#E8F1FF");
                }
            }
            else if (Tipo == "RESERVAS")
            {
                gridItem.Display = false;
                ReservaAccidentadoTextBox.Text = DataBinder.Eval(item.DataItem, "MontoForDisplay").ToString();
            }
            else if (Tipo == "SALDO")
            {
                foreach (TableCell cell in gridItem.Cells)
                {
                    cell.Style.Add("background-color", "#C9DFFC");
                }
            }
            else if (Tipo == "AHORRO")
            {
                gridItem.Display = false;
                AhorroTexBox.Text = DataBinder.Eval(item.DataItem, "MontoForDisplay").ToString();
            }
        }
        catch { }
    }
    protected void AccidentadoRadGrid_ItemDataBound ( object sender, GridItemEventArgs e )
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = e.Item as GridDataItem;

            int _AccidentadoId = (int)item.GetDataKeyValue("AccidentadoId");

            List<PreliquidacionDetalle> lista = PreliquidacionBLL.GetPreliquidacionDetalleForCombo(SiniestroID, _AccidentadoId);
            foreach (PreliquidacionDetalle pre in lista)
            {
                string text = (pre.IsFactura ? "Factura: " : "Reserva: ") + pre.NumeroReciboFactura 
                    + " (" + pre.MontoForDisplay + ")";
                string proveedor = pre.Proveedor;
                string value = pre.PreliquidacionDetalleId.ToString();
                HiddenField ctl = (HiddenField)item.FindControl("PreliquidaciondetalleidHF");
                ctl.Value += value + "###" + proveedor + "###" + text + "#;#";
            }
        }
    }
    protected void EstadoCasoDDL_SelectedIndexChanged ( object sender, EventArgs e )
    {
        try
        {
            SeguimientoBLL.UpdateEstado(SiniestroID,EstadoCasoDDL.SelectedValue);
        }
        catch (Exception q)
        {
            log.Error(q);
            SystemMessages.DisplaySystemErrorMessage("Error al cambiar el estado.");
        }
        MoveToStep("2");
    }
    protected void ClienteODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al obtener la lista de Clientes.");
            log.Error("Function ClienteODS_Selected on page SOATWizard.aspx", e.Exception);
            e.ExceptionHandled = true;
            Response.Redirect("SOATList.aspx");
        }
        else
        {
            if (((List<Artexacta.App.RedCliente.RedCliente>)e.ReturnValue).Count <= 0)
            {
                SystemMessages.DisplaySystemWarningMessage("No tiene Clientes Asignados.");
                Response.Redirect("~/SOAT/SOATList.aspx", true);
            }
        }
    }
    protected void GestionMedicaRadGrid_PreRender(object sender, EventArgs e)
    {
        bool puedeModificarGestionMedicas = false;
        bool puedeEliminarGestionMedicas = false;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_UPDATE_VISITA_MEDICA");
            puedeModificarGestionMedicas = true;
        }
        catch (Exception)
        {
            puedeModificarGestionMedicas = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_VISITA_MEDICA");
            puedeEliminarGestionMedicas = true;
        }
        catch (Exception)
        {
            puedeEliminarGestionMedicas = false;
        }


        GestionMedicaRadGrid.MasterTableView.GetColumn("EditGestionMedica").Visible = puedeModificarGestionMedicas;
        GestionMedicaRadGrid.MasterTableView.GetColumn("DeleteGestionMedica").Visible = puedeEliminarGestionMedicas;
    }
    protected void GastosEjecutadosRadGrid_PreRender(object sender, EventArgs e)
    {
        bool puedeModificarGastosEjecutados = false;
        bool puedeEliminarGastosEjecutados = false;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_UPDATE_GASTOS_EJECUTADOS");
            puedeModificarGastosEjecutados = true;
        }
        catch (Exception)
        {
            puedeModificarGastosEjecutados = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_GASTOS_EJECUTADOS");
            puedeEliminarGastosEjecutados = true;
        }
        catch (Exception)
        {
            puedeEliminarGastosEjecutados = false;
        }
        //EditGastosEjecutados
        GridTableView ch = GastosEjecutadosRadGrid.MasterTableView.DetailTables[0];
        if (ch != null)
        {
            ch.GetColumn("EditGastosEjecutados").Visible = puedeModificarGastosEjecutados;
            ch.GetColumn("DeleteGastosEjecutados").Visible = puedeEliminarGastosEjecutados;
        }

        GastosEjecutadosRadGrid.MasterTableView.Rebind();
        //GastosEjecutadosRadGrid.MasterTableView.GetColumn("EditGastosEjecutados").Visible = puedeModificarGastosEjecutados;
        //GastosEjecutadosRadGrid.MasterTableView.GetColumn("DeleteGastosEjecutados").Visible = puedeEliminarGastosEjecutados;
    }
    protected void AccidentadoRadGrid_PreRender(object sender, EventArgs e)
    {
        //bool puedeModificarAccidentado = false;
        bool puedeEliminarAccidentado = false;
        bool puedeInsertarGestionMedica = false;
        bool puedeInsertarGastosEjecutados = false;

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_ACCIDENTADOS");
            puedeEliminarAccidentado = true;
        }
        catch (Exception)
        {
            puedeEliminarAccidentado = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_VISITA_MEDICA");
            puedeInsertarGestionMedica = true;
        }
        catch (Exception)
        {
            puedeInsertarGestionMedica = false;
        }

        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_INSERT_GASTO_MEDICO");
            puedeInsertarGastosEjecutados = true;
        }
        catch (Exception)
        {
            puedeInsertarGastosEjecutados = false;
        }

        /*
        try
        {
            Artexacta.App.Security.BLL.SecurityBLL.IsUserAuthorizedPerformOperation("SOAT_DELETE_GASTOS_EJECUTADOS");
            puedeEliminarGastosEjecutados = true;
        }
        catch (Exception)
        {
            puedeEliminarGastosEjecutados = false;
        }
        */
        AccidentadoRadGrid.MasterTableView.GetColumn("DeleteAccidentado").Visible = puedeEliminarAccidentado;
        AccidentadoRadGrid.MasterTableView.GetColumn("GestionMedicaAccidentado").Visible = puedeInsertarGestionMedica;
        AccidentadoRadGrid.MasterTableView.GetColumn("PreliquidacionDetalleInsert").Visible = puedeInsertarGastosEjecutados;
        //PreliquidacionDetalleInsert
        
    }

    protected void CiudadExComplementarioODS_Selected(object sender, ObjectDataSourceStatusEventArgs e)
    {
        if (e.Exception != null)
        {
            SystemMessages.DisplaySystemErrorMessage("Error al Obtener los datos de las ciudades para examenes complementarios.");
            log.Error("Function CiudadExComplementarioODS_Selected on page CasoMedicoDetalle.aspx", e.Exception);
            e.ExceptionHandled = true;
        }
    }
}