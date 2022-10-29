namespace Cognos.Negocio
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Negocio : DbContext
    {
        public Negocio()
        : base("name=RedSaludDBConnectionString")
        {
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public virtual DbSet<C_TransactionIndex_b56a0255_cfeb_4caa_920a_6c0beca7d876> C_TransactionIndex_b56a0255_cfeb_4caa_920a_6c0beca7d876 { get; set; }
        public virtual DbSet<rstCitasDetalleTxn> rstCitasDetalleTxn { get; set; }
        public virtual DbSet<rstCitasLogTxn> rstCitasLogTxn { get; set; }
        public virtual DbSet<rstCitasParametros> rstCitasParametros { get; set; }
        public virtual DbSet<rstCitasTxn> rstCitasTxn { get; set; }
        public virtual DbSet<rstClienteParametrosAPIVL> rstClienteParametrosAPIVL { get; set; }
        public virtual DbSet<rstHorarioMedicoVideoLlamada> rstHorarioMedicoVideoLlamada { get; set; }
        public virtual DbSet<rstPacienteMovil> rstPacienteMovil { get; set; }
        public virtual DbSet<rstPacienteMovilAsegurado> rstPacienteMovilAsegurado { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tbl_Asegurado> tbl_Asegurado { get; set; }
        public virtual DbSet<tbl_Caso> tbl_Caso { get; set; }
        public virtual DbSet<tbl_Caso_Emergencia> tbl_Caso_Emergencia { get; set; }
        public virtual DbSet<tbl_Caso_Especialidad> tbl_Caso_Especialidad { get; set; }
        public virtual DbSet<tbl_Caso_LaboratoriosImagenologia> tbl_Caso_LaboratoriosImagenologia { get; set; }
        public virtual DbSet<tbl_Caso_Odontologia> tbl_Caso_Odontologia { get; set; }
        public virtual DbSet<tbl_Cita> tbl_Cita { get; set; }
        public virtual DbSet<tbl_CLA_Ciudad> tbl_CLA_Ciudad { get; set; }
        public virtual DbSet<tbl_CLA_CodigoArancelario> tbl_CLA_CodigoArancelario { get; set; }
        public virtual DbSet<tbl_CLA_Enfermedad> tbl_CLA_Enfermedad { get; set; }
        public virtual DbSet<tbl_CLA_EnfermedadCronica> tbl_CLA_EnfermedadCronica { get; set; }
        public virtual DbSet<tbl_CLA_Especialidad> tbl_CLA_Especialidad { get; set; }
        public virtual DbSet<tbl_CLA_MedicamentoEnfermeria> tbl_CLA_MedicamentoEnfermeria { get; set; }
        public virtual DbSet<tbl_CLA_MedicamentoGrupo> tbl_CLA_MedicamentoGrupo { get; set; }
        public virtual DbSet<tbl_CLA_MedicamentoLINAME> tbl_CLA_MedicamentoLINAME { get; set; }
        public virtual DbSet<tbl_CLA_MedicamentoSubgrupo> tbl_CLA_MedicamentoSubgrupo { get; set; }
        public virtual DbSet<tbl_CLA_MotivoConsulta> tbl_CLA_MotivoConsulta { get; set; }
        public virtual DbSet<tbl_CLA_PresentacionMedicamento> tbl_CLA_PresentacionMedicamento { get; set; }
        public virtual DbSet<tbl_CLA_PrestacionesOdontologicas> tbl_CLA_PrestacionesOdontologicas { get; set; }
        public virtual DbSet<tbl_CLA_Protocolo> tbl_CLA_Protocolo { get; set; }
        public virtual DbSet<tbl_CLA_TipoConcentracion> tbl_CLA_TipoConcentracion { get; set; }
        public virtual DbSet<tbl_CLA_TipoEnfermedad> tbl_CLA_TipoEnfermedad { get; set; }
        public virtual DbSet<tbl_CLA_TipoEstudio> tbl_CLA_TipoEstudio { get; set; }
        public virtual DbSet<tbl_CLA_TipoMedicamento> tbl_CLA_TipoMedicamento { get; set; }
        public virtual DbSet<tbl_CLA_TipoProveedor> tbl_CLA_TipoProveedor { get; set; }
        public virtual DbSet<tbl_Consolidacion> tbl_Consolidacion { get; set; }
        public virtual DbSet<tbl_Derivacion> tbl_Derivacion { get; set; }
        public virtual DbSet<tbl_DerivacionFile> tbl_DerivacionFile { get; set; }
        public virtual DbSet<tbl_DESG_CitaDesgravamen> tbl_DESG_CitaDesgravamen { get; set; }
        public virtual DbSet<tbl_DESG_ClienteUsuario> tbl_DESG_ClienteUsuario { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaCita> tbl_DESG_ConsultaCita { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaCitaCovid> tbl_DESG_ConsultaCitaCovid { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaClienteCovid> tbl_DESG_ConsultaClienteCovid { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaClienteCovidPregunta> tbl_DESG_ConsultaClienteCovidPregunta { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaPregunta> tbl_DESG_ConsultaPregunta { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaPreguntaCita> tbl_DESG_ConsultaPreguntaCita { get; set; }
        public virtual DbSet<tbl_DESG_Estudio> tbl_DESG_Estudio { get; set; }
        public virtual DbSet<tbl_DESG_EstudioGrupo> tbl_DESG_EstudioGrupo { get; set; }
        public virtual DbSet<tbl_DESG_EstudioProveedor> tbl_DESG_EstudioProveedor { get; set; }
        public virtual DbSet<tbl_DESG_Financiera> tbl_DESG_Financiera { get; set; }
        public virtual DbSet<tbl_DESG_LaboratorioEstudio> tbl_DESG_LaboratorioEstudio { get; set; }
        public virtual DbSet<tbl_DESG_LaboratorioFile> tbl_DESG_LaboratorioFile { get; set; }
        public virtual DbSet<tbl_DESG_Medico> tbl_DESG_Medico { get; set; }
        public virtual DbSet<tbl_DESG_MedicoHorarios> tbl_DESG_MedicoHorarios { get; set; }
        public virtual DbSet<tbl_DESG_ProgramacionCita> tbl_DESG_ProgramacionCita { get; set; }
        public virtual DbSet<tbl_DESG_ProgramacionCitaLabo> tbl_DESG_ProgramacionCitaLabo { get; set; }
        public virtual DbSet<tbl_DESG_PropuestoAsegurado> tbl_DESG_PropuestoAsegurado { get; set; }
        public virtual DbSet<tbl_DESG_ProveedorMedico> tbl_DESG_ProveedorMedico { get; set; }
        public virtual DbSet<tbl_DESG_ProveedorMedicoHorarios> tbl_DESG_ProveedorMedicoHorarios { get; set; }
        public virtual DbSet<tbl_DESG_TipoProducto> tbl_DESG_TipoProducto { get; set; }
        public virtual DbSet<tbl_Emergencia> tbl_Emergencia { get; set; }
        public virtual DbSet<tbl_EmergenciaFile> tbl_EmergenciaFile { get; set; }
        public virtual DbSet<tbl_Estudio> tbl_Estudio { get; set; }
        public virtual DbSet<tbl_EstudioFile> tbl_EstudioFile { get; set; }
        public virtual DbSet<tbl_File> tbl_File { get; set; }
        public virtual DbSet<tbl_Gasto> tbl_Gasto { get; set; }
        public virtual DbSet<tbl_GastoDetalle> tbl_GastoDetalle { get; set; }
        public virtual DbSet<tbl_GridColumn> tbl_GridColumn { get; set; }
        public virtual DbSet<tbl_GridPageSize> tbl_GridPageSize { get; set; }
        public virtual DbSet<tbl_Historia> tbl_Historia { get; set; }
        public virtual DbSet<tbl_Internacion> tbl_Internacion { get; set; }
        public virtual DbSet<tbl_Internacion_Cirugia> tbl_Internacion_Cirugia { get; set; }
        public virtual DbSet<tbl_Internacion_Internacion> tbl_Internacion_Internacion { get; set; }
        public virtual DbSet<tbl_InternacionFile> tbl_InternacionFile { get; set; }
        public virtual DbSet<tbl_Medicamento> tbl_Medicamento { get; set; }
        public virtual DbSet<tbl_MedicamentoFile> tbl_MedicamentoFile { get; set; }
        public virtual DbSet<tbl_Odontologia> tbl_Odontologia { get; set; }
        public virtual DbSet<tbl_OdontologiaFile> tbl_OdontologiaFile { get; set; }
        public virtual DbSet<tbl_Paciente> tbl_Paciente { get; set; }
        public virtual DbSet<tbl_Plan> tbl_Plan { get; set; }
        public virtual DbSet<tbl_PlanPrestacion> tbl_PlanPrestacion { get; set; }
        public virtual DbSet<tbl_Poliza> tbl_Poliza { get; set; }
        public virtual DbSet<tbl_PolizaEstado> tbl_PolizaEstado { get; set; }
        public virtual DbSet<tbl_Receta> tbl_Receta { get; set; }
        public virtual DbSet<tbl_RecetaFile> tbl_RecetaFile { get; set; }
        public virtual DbSet<tbl_RED_Cliente> tbl_RED_Cliente { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Cirugias> tbl_RED_Cliente_Cirugias { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Especialidad> tbl_RED_Cliente_Especialidad { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Imagenologia> tbl_RED_Cliente_Imagenologia { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Laboratorios> tbl_RED_Cliente_Laboratorios { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Odontologia> tbl_RED_Cliente_Odontologia { get; set; }
        public virtual DbSet<tbl_RED_Cliente_Prestaciones> tbl_RED_Cliente_Prestaciones { get; set; }
        public virtual DbSet<tbl_RED_Especialista> tbl_RED_Especialista { get; set; }
        public virtual DbSet<tbl_RED_Medico> tbl_RED_Medico { get; set; }
        public virtual DbSet<tbl_RED_Proveedor> tbl_RED_Proveedor { get; set; }
        public virtual DbSet<tbl_RED_Proveedor_LaboratoriosImagenologia> tbl_RED_Proveedor_LaboratoriosImagenologia { get; set; }
        public virtual DbSet<tbl_RED_Proveedor_Odontologia> tbl_RED_Proveedor_Odontologia { get; set; }
        public virtual DbSet<tbl_RED_ProveedorCiudad> tbl_RED_ProveedorCiudad { get; set; }
        public virtual DbSet<tbl_RED_RedMedica> tbl_RED_RedMedica { get; set; }
        public virtual DbSet<tbl_RED_RedMedicaProveedor> tbl_RED_RedMedicaProveedor { get; set; }
        public virtual DbSet<tbl_SavedSearch> tbl_SavedSearch { get; set; }
        public virtual DbSet<tbl_SEG_AccessRole> tbl_SEG_AccessRole { get; set; }
        public virtual DbSet<tbl_SEG_AccessUser> tbl_SEG_AccessUser { get; set; }
        public virtual DbSet<tbl_SEG_Permission> tbl_SEG_Permission { get; set; }
        public virtual DbSet<tbl_SEG_User> tbl_SEG_User { get; set; }
        public virtual DbSet<tbl_SOAT_Accidentado> tbl_SOAT_Accidentado { get; set; }
        public virtual DbSet<tbl_SOAT_GastosEjecutados> tbl_SOAT_GastosEjecutados { get; set; }
        public virtual DbSet<tbl_SOAT_GastosEjecutadosDetalle> tbl_SOAT_GastosEjecutadosDetalle { get; set; }
        public virtual DbSet<tbl_SOAT_GestionMedica> tbl_SOAT_GestionMedica { get; set; }
        public virtual DbSet<tbl_SOAT_PagoGastos> tbl_SOAT_PagoGastos { get; set; }
        public virtual DbSet<tbl_SOAT_PolizaVehiculo> tbl_SOAT_PolizaVehiculo { get; set; }
        public virtual DbSet<tbl_SOAT_Preliquidacion> tbl_SOAT_Preliquidacion { get; set; }
        public virtual DbSet<tbl_SOAT_PreliquidacionDetalle> tbl_SOAT_PreliquidacionDetalle { get; set; }
        public virtual DbSet<tbl_SOAT_Proveedor> tbl_SOAT_Proveedor { get; set; }
        public virtual DbSet<tbl_SOAT_Seguimiento> tbl_SOAT_Seguimiento { get; set; }
        public virtual DbSet<tbl_SOAT_Siniestro> tbl_SOAT_Siniestro { get; set; }
        public virtual DbSet<tbl_TSK_Manager> tbl_TSK_Manager { get; set; }
        public virtual DbSet<tbl_TSK_Task> tbl_TSK_Task { get; set; }
        public virtual DbSet<tbl_UsuarioServicio> tbl_UsuarioServicio { get; set; }
        public virtual DbSet<tbl_UsuarioServicioCliente> tbl_UsuarioServicioCliente { get; set; }
        public virtual DbSet<tbl_ViewState> tbl_ViewState { get; set; }
        public virtual DbSet<tbl_Configuration> tbl_Configuration { get; set; }
        public virtual DbSet<tbl_DatabaseInfo> tbl_DatabaseInfo { get; set; }
        public virtual DbSet<tbl_DESG_CitaDesgravamenBackup> tbl_DESG_CitaDesgravamenBackup { get; set; }
        public virtual DbSet<tbl_DESG_Configuracion> tbl_DESG_Configuracion { get; set; }
        public virtual DbSet<tbl_DESG_Consulta3229> tbl_DESG_Consulta3229 { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaCita3229> tbl_DESG_ConsultaCita3229 { get; set; }
        public virtual DbSet<tbl_DESG_ConsultaPreguntaCita_4955_TEMP> tbl_DESG_ConsultaPreguntaCita_4955_TEMP { get; set; }
        public virtual DbSet<tbl_DESG_EstudioProveedorBackup> tbl_DESG_EstudioProveedorBackup { get; set; }
        public virtual DbSet<tbl_DESG_LaboratorioEstudioBackup> tbl_DESG_LaboratorioEstudioBackup { get; set; }
        public virtual DbSet<tbl_DESG_ProgramacionCitaLaboBackup> tbl_DESG_ProgramacionCitaLaboBackup { get; set; }
        public virtual DbSet<tbl_DESG_ProgramacionCitaLaboTest> tbl_DESG_ProgramacionCitaLaboTest { get; set; }
        public virtual DbSet<tbl_DESG_PropuestoAseguradoBackup> tbl_DESG_PropuestoAseguradoBackup { get; set; }
        public virtual DbSet<tbl_File2> tbl_File2 { get; set; }
        public virtual DbSet<tbl_LOG_Bitacoras> tbl_LOG_Bitacoras { get; set; }
        public virtual DbSet<usv_RED_Cirugias_Prestaciones> usv_RED_Cirugias_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Cliente_Prestaciones> usv_RED_Cliente_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Especialidades_Prestaciones> usv_RED_Especialidades_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_GruposLab_Prestaciones> usv_RED_GruposLab_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Imagen_Prestaciones> usv_RED_Imagen_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Odonto_Prestaciones> usv_RED_Odonto_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Prov_LabImgCar_Prestaciones> usv_RED_Prov_LabImgCar_Prestaciones { get; set; }
        public virtual DbSet<usv_RED_Prov_Odo_Prestaciones> usv_RED_Prov_Odo_Prestaciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<rstCitasDetalleTxn>()
                .Property(e => e.Medicamento)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasDetalleTxn>()
                .Property(e => e.TipoMedicamentoId)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasDetalleTxn>()
                .Property(e => e.Indicaciones)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasLogTxn>()
                .Property(e => e.logComentario)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasLogTxn>()
                .Property(e => e.logEstado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasLogTxn>()
                .Property(e => e.UsuarioId)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasLogTxn>()
                .Property(e => e.logTipoUsuario)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasParametros>()
                .Property(e => e.parCalificacionCorreosNotificacion)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasParametros>()
                .Property(e => e.parClienteIdCorreoPersonalizadoBancoFassil)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasParametros>()
                .Property(e => e.parTelefonoCorreoPersonalizadoBancoFassil)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.citMotivo)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.citEstado)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.citRecomendaciones)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.citCalififacionComentario)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.EnfermedadId)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.Enfermedad2Id)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.Enfermedad3Id)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .Property(e => e.citObservaciones)
                .IsUnicode(false);

            modelBuilder.Entity<rstCitasTxn>()
                .HasMany(e => e.rstCitasDetalleTxn)
                .WithRequired(e => e.rstCitasTxn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<rstCitasTxn>()
                .HasMany(e => e.rstCitasLogTxn)
                .WithRequired(e => e.rstCitasTxn)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<rstClienteParametrosAPIVL>()
                .Property(e => e.parContrasena)
                .IsUnicode(false);

            modelBuilder.Entity<rstClienteParametrosAPIVL>()
                .Property(e => e.parNumeroPoliza)
                .IsUnicode(false);

            modelBuilder.Entity<rstClienteParametrosAPIVL>()
                .Property(e => e.parNombrePlan)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .Property(e => e.pacNombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .Property(e => e.pacUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .Property(e => e.pacContrasena)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .Property(e => e.pacCodigoAsegurado)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .Property(e => e.pacTokenNotificacion)
                .IsUnicode(false);

            modelBuilder.Entity<rstPacienteMovil>()
                .HasMany(e => e.rstPacienteMovilAsegurado)
                .WithRequired(e => e.rstPacienteMovil)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .Property(e => e.Relacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .HasMany(e => e.rstCitasTxn)
                .WithRequired(e => e.tbl_Asegurado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .HasMany(e => e.rstPacienteMovilAsegurado)
                .WithRequired(e => e.tbl_Asegurado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .HasMany(e => e.tbl_Poliza)
                .WithRequired(e => e.tbl_Asegurado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Asegurado>()
                .HasMany(e => e.tbl_CLA_EnfermedadCronica)
                .WithMany(e => e.tbl_Asegurado)
                .Map(m => m.ToTable("tbl_Asegurado_EnfermedadCronica").MapLeftKey("AseguradoId").MapRightKey("EnfermedadCronicaId"));

            modelBuilder.Entity<tbl_Caso>()
                .Property(e => e.CodigoCaso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Caso>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Caso>()
                .Property(e => e.MotivoConsultaId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Caso>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Derivacion)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Emergencia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Estudio)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Historia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Internacion)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Medicamento)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Odontologia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Receta)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Caso_Emergencia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Caso_Especialidad)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Caso_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso>()
                .HasMany(e => e.tbl_Caso_Odontologia)
                .WithRequired(e => e.tbl_Caso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoEmergencia)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoHonorariosMedicos)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoFarmacia)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoLaboratorios)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoEstudios)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoOtros)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoTotal)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detPorcentajeCopago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .Property(e => e.detMontoCoPago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Emergencia>()
                .HasMany(e => e.tbl_File)
                .WithMany(e => e.tbl_Caso_Emergencia)
                .Map(m => m.ToTable("tbl_Caso_EmergenciaFile").MapLeftKey("detId").MapRightKey("FileId"));

            modelBuilder.Entity<tbl_Caso_Especialidad>()
                .Property(e => e.detPrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Especialidad>()
                .Property(e => e.detCoPagoMonto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Especialidad>()
                .Property(e => e.detCoPagoPorcentaje)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_LaboratoriosImagenologia>()
                .Property(e => e.detPrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_LaboratoriosImagenologia>()
                .Property(e => e.detCoPagoMonto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_LaboratoriosImagenologia>()
                .Property(e => e.detCoPagoPorcentaje)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Odontologia>()
                .Property(e => e.detPrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Odontologia>()
                .Property(e => e.detCoPagoMonto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Caso_Odontologia>()
                .Property(e => e.detCoPagoPorcentaje)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_Caso)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_DESG_CitaDesgravamen)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_DESG_Financiera)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .HasForeignKey(e => e.centralCiudadId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_DESG_ProveedorMedico)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_RED_ProveedorCiudad)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_SEG_User)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_SOAT_Proveedor)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_Internacion_Cirugia)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Ciudad>()
                .HasMany(e => e.tbl_Internacion_Internacion)
                .WithRequired(e => e.tbl_CLA_Ciudad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_CodigoArancelario>()
                .Property(e => e.CodigoArancelarioId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_CodigoArancelario>()
                .Property(e => e.UMA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tbl_CLA_CodigoArancelario>()
                .HasMany(e => e.tbl_RED_Cliente_Cirugias)
                .WithRequired(e => e.tbl_CLA_CodigoArancelario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Enfermedad>()
                .Property(e => e.EnfermedadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_Enfermedad>()
                .HasMany(e => e.rstCitasTxn)
                .WithOptional(e => e.tbl_CLA_Enfermedad)
                .HasForeignKey(e => e.EnfermedadId);

            modelBuilder.Entity<tbl_CLA_Enfermedad>()
                .HasMany(e => e.rstCitasTxn1)
                .WithOptional(e => e.tbl_CLA_Enfermedad1)
                .HasForeignKey(e => e.Enfermedad2Id);

            modelBuilder.Entity<tbl_CLA_Enfermedad>()
                .HasMany(e => e.rstCitasTxn2)
                .WithOptional(e => e.tbl_CLA_Enfermedad2)
                .HasForeignKey(e => e.Enfermedad3Id);

            modelBuilder.Entity<tbl_CLA_Enfermedad>()
                .HasMany(e => e.tbl_Internacion_Internacion)
                .WithRequired(e => e.tbl_CLA_Enfermedad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.rstCitasTxn)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.tbl_Caso_Especialidad)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.tbl_RED_Especialista)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.tbl_RED_Medico)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.tbl_Internacion_Cirugia)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_Especialidad>()
                .HasMany(e => e.tbl_RED_Cliente_Especialidad)
                .WithRequired(e => e.tbl_CLA_Especialidad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoEnfermeria>()
                .HasMany(e => e.tbl_Medicamento)
                .WithRequired(e => e.tbl_CLA_MedicamentoEnfermeria)
                .HasForeignKey(e => e.MedicamentoClaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoGrupo>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoGrupo>()
                .HasMany(e => e.tbl_CLA_MedicamentoLINAME)
                .WithRequired(e => e.tbl_CLA_MedicamentoGrupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoLINAME>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoLINAME>()
                .Property(e => e.ClasificacionATQ)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoLINAME>()
                .HasMany(e => e.tbl_CLA_PresentacionMedicamento)
                .WithRequired(e => e.tbl_CLA_MedicamentoLINAME)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoLINAME>()
                .HasMany(e => e.tbl_CLA_TipoConcentracion)
                .WithMany(e => e.tbl_CLA_MedicamentoLINAME)
                .Map(m => m.ToTable("tbl_CLA_ConcentracionMedicamento").MapLeftKey("MedicamentoLINAMEId").MapRightKey("TipoConcentracionId"));

            modelBuilder.Entity<tbl_CLA_MedicamentoSubgrupo>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_MedicamentoSubgrupo>()
                .HasMany(e => e.tbl_CLA_MedicamentoLINAME)
                .WithRequired(e => e.tbl_CLA_MedicamentoSubgrupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_MotivoConsulta>()
                .Property(e => e.MotivoConsultaId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_MotivoConsulta>()
                .HasMany(e => e.tbl_Caso)
                .WithRequired(e => e.tbl_CLA_MotivoConsulta)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_PresentacionMedicamento>()
                .Property(e => e.TipoMedicamentoId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_PrestacionesOdontologicas>()
                .HasMany(e => e.tbl_Caso_Odontologia)
                .WithRequired(e => e.tbl_CLA_PrestacionesOdontologicas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_PrestacionesOdontologicas>()
                .HasMany(e => e.tbl_Odontologia)
                .WithRequired(e => e.tbl_CLA_PrestacionesOdontologicas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_PrestacionesOdontologicas>()
                .HasMany(e => e.tbl_RED_Cliente_Odontologia)
                .WithRequired(e => e.tbl_CLA_PrestacionesOdontologicas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_PrestacionesOdontologicas>()
                .HasMany(e => e.tbl_RED_Proveedor_Odontologia)
                .WithRequired(e => e.tbl_CLA_PrestacionesOdontologicas)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoConcentracion>()
                .HasMany(e => e.tbl_CLA_PresentacionMedicamento)
                .WithRequired(e => e.tbl_CLA_TipoConcentracion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEnfermedad>()
                .HasMany(e => e.tbl_CLA_Protocolo)
                .WithRequired(e => e.tbl_CLA_TipoEnfermedad)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_Caso_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_CLA_TipoEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_PlanPrestacion)
                .WithRequired(e => e.tbl_CLA_TipoEstudio)
                .HasForeignKey(e => e.TipoEstudioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_RED_Cliente_Imagenologia)
                .WithRequired(e => e.tbl_CLA_TipoEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_RED_Cliente_Laboratorios)
                .WithRequired(e => e.tbl_CLA_TipoEstudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_RED_Proveedor_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_CLA_TipoEstudio)
                .HasForeignKey(e => e.EstudioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_RED_Proveedor_LaboratoriosImagenologia1)
                .WithOptional(e => e.tbl_CLA_TipoEstudio1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<tbl_CLA_TipoEstudio>()
                .HasMany(e => e.tbl_Estudio)
                .WithMany(e => e.tbl_CLA_TipoEstudio)
                .Map(m => m.ToTable("tbl_Estudio_TipoEstudio").MapLeftKey("TipoEstudioId").MapRightKey("EstudioId"));

            modelBuilder.Entity<tbl_CLA_TipoMedicamento>()
                .Property(e => e.TipoMedicamentoId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_TipoMedicamento>()
                .HasMany(e => e.rstCitasDetalleTxn)
                .WithRequired(e => e.tbl_CLA_TipoMedicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoMedicamento>()
                .HasMany(e => e.tbl_CLA_PresentacionMedicamento)
                .WithRequired(e => e.tbl_CLA_TipoMedicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoMedicamento>()
                .HasMany(e => e.tbl_Medicamento)
                .WithRequired(e => e.tbl_CLA_TipoMedicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoMedicamento>()
                .HasMany(e => e.tbl_Receta)
                .WithRequired(e => e.tbl_CLA_TipoMedicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_CLA_TipoProveedor>()
                .Property(e => e.TipoProveedorId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_CLA_TipoProveedor>()
                .HasMany(e => e.tbl_RED_Proveedor)
                .WithRequired(e => e.tbl_CLA_TipoProveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Consolidacion>()
                .Property(e => e.MontoTotal)
                .HasPrecision(20, 4);

            modelBuilder.Entity<tbl_Derivacion>()
                .HasMany(e => e.tbl_DerivacionFile)
                .WithRequired(e => e.tbl_Derivacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .Property(e => e.ciudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .Property(e => e.tipoProducto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasOptional(e => e.tbl_DESG_ConsultaCita)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasMany(e => e.tbl_DESG_ConsultaCitaCovid)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasMany(e => e.tbl_DESG_ConsultaPreguntaCita)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasMany(e => e.tbl_DESG_LaboratorioEstudio)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasMany(e => e.tbl_DESG_LaboratorioFile)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamen>()
                .HasMany(e => e.tbl_DESG_ProgramacionCitaLabo)
                .WithRequired(e => e.tbl_DESG_CitaDesgravamen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.estadoCivilPA)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.edadGeneroHermanos)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.pesoKg)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.tiempoConocePA)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.relacionParentesco)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.presionArterial1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.presionArterial2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita>()
                .Property(e => e.presionArterial3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.PrimeraDosisFecha)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.SegundaDosisFecha)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.OtrasDosisFecha)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.FechaDiagnostico)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.FechaNegativo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.DetalleTratamiento)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.TiempoHospitalizacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCitaCovid>()
                .Property(e => e.SecuelasPostCovid)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaClienteCovidPregunta>()
                .Property(e => e.Inciso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaClienteCovidPregunta>()
                .Property(e => e.Pregunta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaPregunta>()
                .Property(e => e.inciso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaPreguntaCita>()
                .Property(e => e.inciso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_Estudio1)
                .WithOptional(e => e.tbl_DESG_Estudio2)
                .HasForeignKey(e => e.parentEstudioId);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_EstudioProveedor)
                .WithRequired(e => e.tbl_DESG_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_LaboratorioEstudio)
                .WithRequired(e => e.tbl_DESG_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_LaboratorioFile)
                .WithRequired(e => e.tbl_DESG_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_ProgramacionCitaLabo)
                .WithRequired(e => e.tbl_DESG_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_ProveedorMedicoHorarios)
                .WithRequired(e => e.tbl_DESG_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_Estudio>()
                .HasMany(e => e.tbl_DESG_EstudioGrupo)
                .WithMany(e => e.tbl_DESG_Estudio)
                .Map(m => m.ToTable("tbl_DESG_EstudioGrupoEstudio").MapLeftKey("estudioId").MapRightKey("estudioGrupoId"));

            modelBuilder.Entity<tbl_DESG_Financiera>()
                .Property(e => e.centralCiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Financiera>()
                .Property(e => e.nit)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Medico>()
                .HasMany(e => e.tbl_DESG_MedicoHorarios)
                .WithRequired(e => e.tbl_DESG_Medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProgramacionCita>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ProgramacionCita>()
                .HasOptional(e => e.tbl_DESG_ProgramacionCita1)
                .WithRequired(e => e.tbl_DESG_ProgramacionCita2);

            modelBuilder.Entity<tbl_DESG_ProgramacionCitaLabo>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_PropuestoAsegurado>()
                .Property(e => e.carnetIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_PropuestoAsegurado>()
                .Property(e => e.genero)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_PropuestoAsegurado>()
                .HasMany(e => e.tbl_DESG_CitaDesgravamen)
                .WithRequired(e => e.tbl_DESG_PropuestoAsegurado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .Property(e => e.ciudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_EstudioProveedor)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_LaboratorioEstudio)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_LaboratorioFile)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_Medico)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_ProgramacionCitaLabo)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_ProveedorMedico>()
                .HasMany(e => e.tbl_DESG_ProveedorMedicoHorarios)
                .WithRequired(e => e.tbl_DESG_ProveedorMedico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_DESG_TipoProducto>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Emergencia>()
                .HasMany(e => e.tbl_EmergenciaFile)
                .WithRequired(e => e.tbl_Emergencia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Estudio>()
                .HasMany(e => e.tbl_Caso_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_Estudio)
                .HasForeignKey(e => e.OrdenDeServicioId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Estudio>()
                .HasMany(e => e.tbl_EstudioFile)
                .WithRequired(e => e.tbl_Estudio)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_DerivacionFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_DESG_LaboratorioFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_DESG_PropuestoAsegurado)
                .WithOptional(e => e.tbl_File)
                .HasForeignKey(e => e.fotoId);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_EmergenciaFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_EstudioFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_InternacionFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_MedicamentoFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_OdontologiaFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_Paciente)
                .WithOptional(e => e.tbl_File)
                .HasForeignKey(e => e.FotoId);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_RecetaFile)
                .WithRequired(e => e.tbl_File)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_RED_Medico)
                .WithOptional(e => e.tbl_File)
                .HasForeignKey(e => e.FotoId);

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_SOAT_Accidentado)
                .WithMany(e => e.tbl_File)
                .Map(m => m.ToTable("tbl_SOAT_AccidentadoFile").MapLeftKey("FileId").MapRightKey("AccidentadoId"));

            modelBuilder.Entity<tbl_File>()
                .HasMany(e => e.tbl_SOAT_Siniestro)
                .WithMany(e => e.tbl_File)
                .Map(m => m.ToTable("tbl_SOAT_SiniestroFile").MapLeftKey("FileId").MapRightKey("SiniestroId"));

            modelBuilder.Entity<tbl_Gasto>()
                .Property(e => e.MontoConFactura)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Gasto>()
                .Property(e => e.MontoSinFactura)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Gasto>()
                .Property(e => e.RetencionImpuestos)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Gasto>()
                .Property(e => e.Total)
                .HasPrecision(17, 4);

            modelBuilder.Entity<tbl_Gasto>()
                .HasMany(e => e.tbl_GastoDetalle)
                .WithRequired(e => e.tbl_Gasto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_GastoDetalle>()
                .Property(e => e.Monto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_GastoDetalle>()
                .Property(e => e.TipoDocumento)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GastoDetalle>()
                .Property(e => e.RetencionImpuestoPorcentaje)
                .HasPrecision(4, 2);

            modelBuilder.Entity<tbl_GridColumn>()
                .Property(e => e.gridId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GridColumn>()
                .Property(e => e.column)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GridPageSize>()
                .Property(e => e.gridID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_GridPageSize>()
                .Property(e => e.userID)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Historia>()
                .Property(e => e.EnfermedadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Historia>()
                .Property(e => e.Enfermedad2Id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Historia>()
                .Property(e => e.Enfermedad3Id)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Historia>()
                .Property(e => e.Talla)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Historia>()
                .Property(e => e.Peso)
                .HasPrecision(8, 2);

            modelBuilder.Entity<tbl_Internacion>()
                .Property(e => e.CodigoArancelarioId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Internacion>()
                .HasMany(e => e.tbl_InternacionFile)
                .WithRequired(e => e.tbl_Internacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Internacion>()
                .HasOptional(e => e.tbl_Internacion_Cirugia)
                .WithRequired(e => e.tbl_Internacion);

            modelBuilder.Entity<tbl_Internacion>()
                .HasOptional(e => e.tbl_Internacion_Internacion)
                .WithRequired(e => e.tbl_Internacion);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detValorUma)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detPorcentajeCirujano)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoCirujano)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detPorcentajeAnestesiologo)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoAnestesiologo)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detPorcentajeAyudante)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoAyudante)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detPorcentajeInstrumentista)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoInstrumentista)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoTotal)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detPorcentajeCoPago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Cirugia>()
                .Property(e => e.detMontoCoPago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.EnfermedadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoEmergencia)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoHonorariosMedicos)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoFarmacia)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoLaboratorios)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoEstudios)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoHospitalizacion)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoOtros)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoTotal)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detPorcentajeCoPago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Internacion_Internacion>()
                .Property(e => e.detMontoCoPago)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Medicamento>()
                .Property(e => e.TipoMedicamentoId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Medicamento>()
                .HasMany(e => e.tbl_MedicamentoFile)
                .WithRequired(e => e.tbl_Medicamento)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Odontologia>()
                .Property(e => e.Pieza)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Odontologia>()
                .HasMany(e => e.tbl_OdontologiaFile)
                .WithRequired(e => e.tbl_Odontologia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.CarnetIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.TelefonoTrabajo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.EstadoCivil)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Paciente>()
                .HasMany(e => e.rstPacienteMovilAsegurado)
                .WithRequired(e => e.tbl_Paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Paciente>()
                .HasMany(e => e.tbl_Asegurado)
                .WithRequired(e => e.tbl_Paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Paciente>()
                .HasMany(e => e.tbl_Historia)
                .WithRequired(e => e.tbl_Paciente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Poliza>()
                .Property(e => e.NumeroPoliza)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Poliza>()
                .Property(e => e.MontoTotal)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Poliza>()
                .Property(e => e.Lugar)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Poliza>()
                .Property(e => e.MontoFarmacia)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_Poliza>()
                .Property(e => e.Cobertura)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Poliza>()
                .HasMany(e => e.rstCitasTxn)
                .WithRequired(e => e.tbl_Poliza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Poliza>()
                .HasMany(e => e.tbl_Caso)
                .WithRequired(e => e.tbl_Poliza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_Poliza>()
                .HasMany(e => e.tbl_PolizaEstado)
                .WithRequired(e => e.tbl_Poliza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_PolizaEstado>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Receta>()
                .Property(e => e.TipoMedicamentoId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Receta>()
                .HasMany(e => e.tbl_RecetaFile)
                .WithRequired(e => e.tbl_Receta)
                .HasForeignKey(e => e.RecetaId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .Property(e => e.CodigoCliente)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .Property(e => e.Nit)
                .HasPrecision(15, 0);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .Property(e => e.Telefono1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .Property(e => e.Telefono2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.rstCitasTxn)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.rstClienteParametrosAPIVL)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_Asegurado)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_CitaDesgravamen)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ClienteUsuario)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ConsultaClienteCovid)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ConsultaClienteCovidPregunta)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ConsultaPregunta)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ConsultaPreguntaCita)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_EstudioProveedor)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_Financiera)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_LaboratorioEstudio)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_MedicoHorarios)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_PropuestoAsegurado)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_ProveedorMedicoHorarios)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_DESG_TipoProducto)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_SOAT_Siniestro)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Cirugias)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Especialidad)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Imagenologia)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Laboratorios)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Odontologia)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Cliente_Prestaciones)
                .WithRequired(e => e.tbl_RED_Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_RedMedica)
                .WithMany(e => e.tbl_RED_Cliente)
                .Map(m => m.ToTable("tbl_RED_ClienteRedMedica").MapLeftKey("ClienteId").MapRightKey("RedMedicaId"));

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_SEG_User)
                .WithMany(e => e.tbl_RED_Cliente)
                .Map(m => m.ToTable("tbl_RED_ClienteUsuario").MapLeftKey("ClienteId").MapRightKey("UserId"));

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_RED_Medico)
                .WithMany(e => e.tbl_RED_Cliente)
                .Map(m => m.ToTable("tbl_RED_MedicoCliente").MapLeftKey("ClienteId").MapRightKey("MedicoId"));

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_SEG_User1)
                .WithMany(e => e.tbl_RED_Cliente1)
                .Map(m => m.ToTable("tbl_SEG_UserCliente").MapLeftKey("ClienteId").MapRightKey("UserId"));

            modelBuilder.Entity<tbl_RED_Cliente>()
                .HasMany(e => e.tbl_SEG_User2)
                .WithMany(e => e.tbl_RED_Cliente2)
                .Map(m => m.ToTable("tbl_SEG_UserClienteSOAT").MapLeftKey("ClienteId").MapRightKey("UserId"));

            modelBuilder.Entity<tbl_RED_Cliente_Cirugias>()
                .Property(e => e.CodigoArancelarioId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente_Cirugias>()
                .Property(e => e.detMontoTope)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Cliente_Especialidad>()
                .Property(e => e.detFrecuenciaVideoLlamada)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente_Prestaciones>()
                .Property(e => e.preTipoPrestacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Cliente_Prestaciones>()
                .Property(e => e.prePrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Cliente_Prestaciones>()
                .Property(e => e.preCoPagoMonto)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Cliente_Prestaciones>()
                .Property(e => e.preCoPagoPorcentaje)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Cliente_Prestaciones>()
                .Property(e => e.preMontoTope)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Especialista>()
                .Property(e => e.Sedes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Especialista>()
                .Property(e => e.ColegioMedico)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Especialista>()
                .Property(e => e.CostoConsulta)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_RED_Especialista>()
                .Property(e => e.PorcentageDescuento)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tbl_RED_Medico>()
                .Property(e => e.Sedes)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .Property(e => e.ColegioMedico)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .Property(e => e.TokenNotificacion)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .HasMany(e => e.rstCitasTxn)
                .WithRequired(e => e.tbl_RED_Medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .HasMany(e => e.rstHorarioMedicoVideoLlamada)
                .WithRequired(e => e.tbl_RED_Medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .HasMany(e => e.tbl_Internacion_Cirugia)
                .WithRequired(e => e.tbl_RED_Medico)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Medico>()
                .HasMany(e => e.tbl_RED_Proveedor)
                .WithOptional(e => e.tbl_RED_Medico)
                .HasForeignKey(e => e.MedicoIdProv);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.Nit)
                .HasPrecision(15, 0);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.TelefonoCasa)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.TelefonoOficina)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .Property(e => e.TipoProveedorId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Caso_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Consolidacion)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Derivacion)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Emergencia)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Estudio)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_Internacion)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_RED_Especialista)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_RED_ProveedorCiudad)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_RED_RedMedicaProveedor)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_RED_Proveedor_LaboratoriosImagenologia)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_RED_Proveedor_Odontologia)
                .WithRequired(e => e.tbl_RED_Proveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_RED_Proveedor>()
                .HasMany(e => e.tbl_SEG_User1)
                .WithMany(e => e.tbl_RED_Proveedor1)
                .Map(m => m.ToTable("tbl_RED_ProveedorUser").MapLeftKey("ProveedorId").MapRightKey("UserId"));

            modelBuilder.Entity<tbl_RED_Proveedor_LaboratoriosImagenologia>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_Proveedor_LaboratoriosImagenologia>()
                .Property(e => e.detPrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_Proveedor_Odontologia>()
                .Property(e => e.detPrecio)
                .HasPrecision(15, 4);

            modelBuilder.Entity<tbl_RED_ProveedorCiudad>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_ProveedorCiudad>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_ProveedorCiudad>()
                .Property(e => e.Celular)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_RedMedica>()
                .Property(e => e.Codigo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_RED_RedMedica>()
                .HasMany(e => e.tbl_RED_RedMedicaProveedor)
                .WithRequired(e => e.tbl_RED_RedMedica)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SavedSearch>()
                .Property(e => e.searchId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SavedSearch>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SavedSearch>()
                .Property(e => e.searchExpression)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_AccessRole>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_Permission>()
                .Property(e => e.mnemonic)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_Permission>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.fullname)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.cellphone)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .Property(e => e.ciudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_Caso)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_Consolidacion)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_Derivacion)
                .WithOptional(e => e.tbl_SEG_User)
                .HasForeignKey(e => e.AprobacionUserId);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_DESG_CitaDesgravamen)
                .WithOptional(e => e.tbl_SEG_User)
                .HasForeignKey(e => e.ejecutivoId);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_DESG_ClienteUsuario)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_DESG_Medico)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_DESG_ProveedorMedico)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_Estudio)
                .WithOptional(e => e.tbl_SEG_User)
                .HasForeignKey(e => e.AprobacionUserId);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_Internacion)
                .WithOptional(e => e.tbl_SEG_User)
                .HasForeignKey(e => e.AprobacionUserId);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_RED_Medico)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_RED_Proveedor)
                .WithOptional(e => e.tbl_SEG_User)
                .HasForeignKey(e => e.userIdProv);

            modelBuilder.Entity<tbl_SEG_User>()
                .HasMany(e => e.tbl_SOAT_PagoGastos)
                .WithRequired(e => e.tbl_SEG_User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .Property(e => e.CarnetIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .Property(e => e.EstadoCivil)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .HasMany(e => e.tbl_SOAT_GastosEjecutados)
                .WithRequired(e => e.tbl_SOAT_Accidentado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .HasMany(e => e.tbl_SOAT_GestionMedica)
                .WithRequired(e => e.tbl_SOAT_Accidentado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .HasMany(e => e.tbl_SOAT_Preliquidacion)
                .WithRequired(e => e.tbl_SOAT_Accidentado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Accidentado>()
                .HasMany(e => e.tbl_SOAT_Siniestro)
                .WithMany(e => e.tbl_SOAT_Accidentado)
                .Map(m => m.ToTable("tbl_SOAT_SiniestroAccidentado").MapLeftKey("AccidentadoId").MapRightKey("SiniestroId"));

            modelBuilder.Entity<tbl_SOAT_GastosEjecutados>()
                .Property(e => e.MontoGestion)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_SOAT_GastosEjecutados>()
                .HasMany(e => e.tbl_SOAT_GastosEjecutadosDetalle)
                .WithRequired(e => e.tbl_SOAT_GastosEjecutados)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_GastosEjecutadosDetalle>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_GastosEjecutadosDetalle>()
                .Property(e => e.Monto)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_SOAT_GastosEjecutadosDetalle>()
                .Property(e => e.NumeroReciboFactura)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_GastosEjecutadosDetalle>()
                .HasMany(e => e.tbl_SOAT_PagoGastos)
                .WithRequired(e => e.tbl_SOAT_GastosEjecutadosDetalle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_GestionMedica>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_GestionMedica>()
                .Property(e => e.DiagnosticoPreliminar)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_GestionMedica>()
                .Property(e => e.Grado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PagoGastos>()
                .Property(e => e.NroCheque)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.OperacionId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.NumeroRoseta)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.NumeroPoliza)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.NombreTitular)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.CITitular)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.Placa)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.Sector)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PolizaVehiculo>()
                .Property(e => e.Cilindrada)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Preliquidacion>()
                .Property(e => e.MontoGestion)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_SOAT_Preliquidacion>()
                .HasMany(e => e.tbl_SOAT_PreliquidacionDetalle)
                .WithRequired(e => e.tbl_SOAT_Preliquidacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_PreliquidacionDetalle>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_PreliquidacionDetalle>()
                .Property(e => e.Monto)
                .HasPrecision(18, 4);

            modelBuilder.Entity<tbl_SOAT_PreliquidacionDetalle>()
                .Property(e => e.NumeroReciboFactura)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Proveedor>()
                .Property(e => e.CiudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Seguimiento>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Seguimiento>()
                .Property(e => e.Observaciones)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .Property(e => e.LugarDpto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .Property(e => e.LugarProvincia)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .HasMany(e => e.tbl_SOAT_GastosEjecutados)
                .WithRequired(e => e.tbl_SOAT_Siniestro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .HasMany(e => e.tbl_SOAT_GestionMedica)
                .WithRequired(e => e.tbl_SOAT_Siniestro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .HasOptional(e => e.tbl_SOAT_PolizaVehiculo)
                .WithRequired(e => e.tbl_SOAT_Siniestro);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .HasMany(e => e.tbl_SOAT_Preliquidacion)
                .WithRequired(e => e.tbl_SOAT_Siniestro)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tbl_SOAT_Siniestro>()
                .HasOptional(e => e.tbl_SOAT_Seguimiento)
                .WithRequired(e => e.tbl_SOAT_Siniestro);

            modelBuilder.Entity<tbl_TSK_Task>()
                .Property(e => e.TaskId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TSK_Task>()
                .Property(e => e.TaskName)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_TSK_Task>()
                .Property(e => e.TaskDescription)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_UsuarioServicio>()
                .Property(e => e.Usuario)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_UsuarioServicio>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_UsuarioServicio>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_Configuration>()
                .Property(e => e.PorcentajeSiniestralidadAlerta)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_Configuration>()
                .Property(e => e.MontoMinimoEnPoliza)
                .HasPrecision(18, 0);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamenBackup>()
                .Property(e => e.ciudadId)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_CitaDesgravamenBackup>()
                .Property(e => e.tipoProducto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.estadoCivilPA)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.edadGeneroHermanos)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.pesoKg)
                .HasPrecision(5, 2);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.tiempoConocePA)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.relacionParentesco)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.presionArterial1)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.presionArterial2)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_Consulta3229>()
                .Property(e => e.presionArterial3)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaCita3229>()
                .Property(e => e.inciso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ConsultaPreguntaCita_4955_TEMP>()
                .Property(e => e.inciso)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ProgramacionCitaLaboBackup>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_ProgramacionCitaLaboTest>()
                .Property(e => e.estado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_PropuestoAseguradoBackup>()
                .Property(e => e.carnetIdentidad)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_DESG_PropuestoAseguradoBackup>()
                .Property(e => e.genero)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_File2>()
                .Property(e => e.extension)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_LOG_Bitacoras>()
                .Property(e => e.tipoEvento)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_LOG_Bitacoras>()
                .Property(e => e.empleado)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_LOG_Bitacoras>()
                .Property(e => e.tipoObjeto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_LOG_Bitacoras>()
                .Property(e => e.idObjeto)
                .IsUnicode(false);

            modelBuilder.Entity<tbl_LOG_Bitacoras>()
                .Property(e => e.mensaje)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Cirugias_Prestaciones>()
                .Property(e => e.CodigoArancelarioId)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Cliente_Prestaciones>()
                .Property(e => e.prestacion)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Cliente_Prestaciones>()
                .Property(e => e.preTipoPrestacion)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Especialidades_Prestaciones>()
                .Property(e => e.FrecuenciaVideoLLamadas)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_GruposLab_Prestaciones>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Imagen_Prestaciones>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Prov_LabImgCar_Prestaciones>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);

            modelBuilder.Entity<usv_RED_Prov_Odo_Prestaciones>()
                .Property(e => e.CategoriaId)
                .IsUnicode(false);
        }
    }
}
