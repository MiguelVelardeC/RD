namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_Paciente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_Paciente()
        {
            rstPacienteMovilAsegurado = new HashSet<rstPacienteMovilAsegurado>();
            tbl_Asegurado = new HashSet<tbl_Asegurado>();
            tbl_Cita = new HashSet<tbl_Cita>();
            tbl_Historia = new HashSet<tbl_Historia>();
        }

        [Key]
        public int PacienteId { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [StringLength(20)]
        public string CarnetIdentidad { get; set; }

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(250)]
        public string LugarTrabajo { get; set; }

        [StringLength(20)]
        public string TelefonoTrabajo { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoCivil { get; set; }

        public int NroHijo { get; set; }

        [StringLength(4000)]
        public string Antecedentes { get; set; }

        [StringLength(4000)]
        public string AntecedentesAlergicos { get; set; }

        [StringLength(4000)]
        public string AntecedentesGinecoobstetricos { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool Genero { get; set; }

        [Required]
        [StringLength(250)]
        public string Nombre { get; set; }

        public int? FotoId { get; set; }

        [StringLength(20)]
        public string Celular { get; set; }

        [StringLength(3)]
        public string CiudadId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rstPacienteMovilAsegurado> rstPacienteMovilAsegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Asegurado> tbl_Asegurado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Cita> tbl_Cita { get; set; }

        public virtual tbl_CLA_Ciudad tbl_CLA_Ciudad { get; set; }

        public virtual tbl_File tbl_File { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_Historia> tbl_Historia { get; set; }
    }
}
