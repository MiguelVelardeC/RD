namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_SEG_Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int permissionID { get; set; }

        [Required]
        [StringLength(100)]
        public string mnemonic { get; set; }

        [StringLength(250)]
        public string description { get; set; }
    }
}
