namespace Cognos.Negocio
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_File2
    {
        [Key]
        [Column(Order = 0)]
        public int fileId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(1000)]
        public string fileName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string extension { get; set; }

        [Key]
        [Column(Order = 3)]
        public string documentText { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime dateUploaded { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long fileSize { get; set; }

        [Key]
        [Column(Order = 6)]
        public string fileStoragePath { get; set; }
    }
}
