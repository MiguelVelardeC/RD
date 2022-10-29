namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_GridPageSize
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(250)]
        public string gridID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(250)]
        public string userID { get; set; }

        public int pagesize { get; set; }
    }
}
