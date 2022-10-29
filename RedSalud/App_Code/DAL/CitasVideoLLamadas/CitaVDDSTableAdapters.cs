using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CitasVideoLLamadasDSTableAdapters
{
    /// <summary>
    /// Summary description for SearchSiniestroForListTableAdapter
    /// </summary>
    public partial class Poliza_GetPolizaByPolizaIdTableAdapter
    {
        public int cmdTimeout
        {
            get
            {
                return this.CommandCollection[0].CommandTimeout;
            }

            set
            {
                foreach (System.Data.SqlClient.SqlCommand sqlCommand in this.CommandCollection)
                {
                    sqlCommand.CommandTimeout = value;
                }
            }
        }
    }
}