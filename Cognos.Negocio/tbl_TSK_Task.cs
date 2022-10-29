namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_TSK_Task
    {
        [Key]
        [StringLength(50)]
        public string TaskId { get; set; }

        [Required]
        [StringLength(50)]
        public string TaskName { get; set; }

        [Required]
        [StringLength(200)]
        public string TaskDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public long PeriodLengthSeconds { get; set; }

        public int Iterations { get; set; }

        public bool Enabled { get; set; }

        public int IterationsExecuted { get; set; }

        public DateTime? LastExecutionDate { get; set; }
    }
}
