namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_RED_Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_RED_Cliente()
        {
            rstCitasTxn = new HashSet<rstCitasTxn>();
            rstClienteParametrosAPIVL = new HashSet<rstClienteParametrosAPIVL>();
            tbl_Asegurado = new HashSet<tbl_Asegurado>();
            tbl_DESG_CitaDesgravamen = new HashSet<tbl_DESG_CitaDesgravamen>();
            tbl_DESG_ClienteUsuario = new HashSet<tbl_DESG_ClienteUsuario>();
            tbl_DESG_ConsultaClienteCovid = new HashSet<tbl_DESG_ConsultaClienteCovid>();
            tbl_DESG_ConsultaClienteCovidPregunta = new HashSet<tbl_DESG_ConsultaClienteCovidPregunta>();
            tbl_DESG_ConsultaPregunta = new HashSet<tbl_DESG_ConsultaPregunta>();
            tbl_DESG_ConsultaPreguntaCita = new HashSet<tbl_DESG_ConsultaPreguntaCita>();
            tbl_DESG_EstudioProveedor = new HashSet<tbl_DESG_EstudioProveedor>();
            tbl_DESG_Financiera = new HashSet<tbl_DESG_Financiera>();
            tbl_DESG_LaboratorioEstudio = new HashSet<tbl_DESG_LaboratorioEstudio>();
            tbl_DESG_MedicoHorarios = new HashSet<tbl_DESG_MedicoHorarios>();
            tbl_DESG_PropuestoAsegurado = new HashSet<tbl_DESG_PropuestoAsegurado>();
            tbl_DESG_ProveedorMedicoHorarios = new HashSet<tbl_DESG_ProveedorMedicoHorarios>();
            tbl_DESG_TipoProducto = new HashSet<tbl_DESG_TipoProducto>();
            tbl_UsuarioServicioCliente = new HashSet<tbl_UsuarioServicioCliente>();
            tbl_SOAT_Siniestro = new HashSet<tbl_SOAT_Siniestro>();
            tbl_RED_Cliente_Cirugias = new HashSet<tbl_RED_Cliente_Cirugias>();
            tbl_RED_Cliente_Especialidad = new HashSet<tbl_RED_Cliente_Especialidad>();
            tbl_RED_Cliente_Imagenologia = new HashSet<tbl_RED_Cliente_Imagenologia>();
            tbl_RED_Cliente_Laboratorios = new HashSet<tbl_RED_Cliente_Laboratorios>();
            tbl_RED_Cliente_Odontologia = new HashSet<tbl_RED_Cliente_Odontologia>();
            tbl_RED_Cliente_Prestaciones = new HashSet<tbl_RED_Cliente_Prestaciones>();
            tbl_RED_RedMedica = new HashSet<tbl_RED_RedMedica>();
            tbl_SEG_User = new HashSet<tbl_SEG_User>();
            tbl_RED_Medico = new HashSet<tbl_RED_Medico>();
            tbl_SEG_User1 = new HashSet<tbl_SEG_User>();
            tbl_SEG_User2 = new HashSet<tbl_SEG_User>();
        }

        [Key]
        public int ClienteId { get; set; }

        [Required]
        [StringLength(20)]
        public string CodigoCliente { get; set; }

        [Required]
        [StringLength(250)]
        public string NombreJuridico { get; set; }

        public decimal Nit { get; set; }

        [StringLength(250)]
        public string Direccion { get; set; }

        [StringLength(20)]
        public string Telefono1 { get; set; }

        [StringLength(20)]
        public string Telefono2 { get; set; }

        [StringLength(250)]
        public string NombreContacto { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public int CostoConsultaInternista { get; set; }

        public int NumeroDiasReconsulta { get; set; }

        public bool SoloLiname { get; set; }

        public bool IsSOAT { get; set; }

        public bool IsGestionMedica { get; set; }

        public bool IsDesgravamen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstCitasTxn> rstCitasTxn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstClienteParametrosAPIVL> rstClienteParametrosAPIVL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Asegurado> tbl_Asegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_CitaDesgravamen> tbl_DESG_CitaDesgravamen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ClienteUsuario> tbl_DESG_ClienteUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaClienteCovid> tbl_DESG_ConsultaClienteCovid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaClienteCovidPregunta> tbl_DESG_ConsultaClienteCovidPregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaPregunta> tbl_DESG_ConsultaPregunta { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaPreguntaCita> tbl_DESG_ConsultaPreguntaCita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_EstudioProveedor> tbl_DESG_EstudioProveedor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_Financiera> tbl_DESG_Financiera { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioEstudio> tbl_DESG_LaboratorioEstudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_MedicoHorarios> tbl_DESG_MedicoHorarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_PropuestoAsegurado> tbl_DESG_PropuestoAsegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProveedorMedicoHorarios> tbl_DESG_ProveedorMedicoHorarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_TipoProducto> tbl_DESG_TipoProducto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_UsuarioServicioCliente> tbl_UsuarioServicioCliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SOAT_Siniestro> tbl_SOAT_Siniestro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Cirugias> tbl_RED_Cliente_Cirugias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Especialidad> tbl_RED_Cliente_Especialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Imagenologia> tbl_RED_Cliente_Imagenologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Laboratorios> tbl_RED_Cliente_Laboratorios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Odontologia> tbl_RED_Cliente_Odontologia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Cliente_Prestaciones> tbl_RED_Cliente_Prestaciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_RedMedica> tbl_RED_RedMedica { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SEG_User> tbl_SEG_User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_RED_Medico> tbl_RED_Medico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SEG_User> tbl_SEG_User1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_SEG_User> tbl_SEG_User2 { get; set; }
    }
}
