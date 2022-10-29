namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_DESG_CitaDesgravamen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_DESG_CitaDesgravamen()
        {
            tbl_DESG_ConsultaCitaCovid = new HashSet<tbl_DESG_ConsultaCitaCovid>();
            tbl_DESG_ConsultaPreguntaCita = new HashSet<tbl_DESG_ConsultaPreguntaCita>();
            tbl_DESG_LaboratorioEstudio = new HashSet<tbl_DESG_LaboratorioEstudio>();
            tbl_DESG_LaboratorioFile = new HashSet<tbl_DESG_LaboratorioFile>();
            tbl_DESG_ProgramacionCitaLabo = new HashSet<tbl_DESG_ProgramacionCitaLabo>();
        }

        [Key]
        public int citaDesgravamenId { get; set; }

        public int propuestoAseguradoId { get; set; }

        public int? financieraId { get; set; }

        [Required]
        [StringLength(3)]
        public string ciudadId { get; set; }

        public bool necesitaExamen { get; set; }

        public bool necesitaLaboratorio { get; set; }

        public bool cobroFinanciera { get; set; }

        [Required]
        [StringLength(1000)]
        public string referencia { get; set; }

        [Required]
        [StringLength(20)]
        public string tipoProducto { get; set; }

        public bool deleted { get; set; }

        public int? ejecutivoId { get; set; }

        public DateTime? fechaRegistro { get; set; }

        public int clienteId { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_DESG_PropuestoAsegurado tbl_DESG_PropuestoAsegurado { get; set; }

        public virtual tbl_RED_Cliente tbl_RED_Cliente { get; set; }

        public virtual tbl_SEG_User tbl_SEG_User { get; set; }

        public virtual tbl_DESG_ConsultaCita tbl_DESG_ConsultaCita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaCitaCovid> tbl_DESG_ConsultaCitaCovid { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ConsultaPreguntaCita> tbl_DESG_ConsultaPreguntaCita { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioEstudio> tbl_DESG_LaboratorioEstudio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_LaboratorioFile> tbl_DESG_LaboratorioFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_DESG_ProgramacionCitaLabo> tbl_DESG_ProgramacionCitaLabo { get; set; }
    }
}
