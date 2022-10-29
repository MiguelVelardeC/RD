using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace Artexacta.App.Cita
{
    /// <summary>
    /// Summary description for RedSaludSchedulerInfo
    /// </summary>
    [Serializable]
    public class RedSaludSchedulerInfo : SchedulerInfo
    {
        public int MedicoId { get; set; }
        public int ProveedorId { get; set; }

        public RedSaludSchedulerInfo()
        {
            MedicoId = 0;
            ProveedorId = 0;
        }

        public RedSaludSchedulerInfo(ISchedulerInfo baseInfo, int medicoId, int proveedorId)
            : base(baseInfo)
        {
            this.MedicoId = medicoId;
            this.ProveedorId = proveedorId;
        }
    }
}