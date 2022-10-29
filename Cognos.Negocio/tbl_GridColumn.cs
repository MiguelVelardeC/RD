namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GridColumn
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string gridId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string column { get; set; }

        public bool visible { get; set; }
    }
}
