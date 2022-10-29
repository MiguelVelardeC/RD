namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_ViewState
    {
        [Key]
        public Guid ViewStateId { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] Value { get; set; }

        public DateTime LastAccessed { get; set; }

        public int Timeout { get; set; }
    }
}
