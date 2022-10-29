namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TSK_Manager
    {
        [Key]
        public int ManagerId { get; set; }

        public bool Status { get; set; }

        public long SleepTimeSeconds { get; set; }

        public int NumberOfOverlapsAllowed { get; set; }
    }
}
