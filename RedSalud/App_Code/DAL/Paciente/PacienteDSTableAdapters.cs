using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PacienteDSTableAdapters
{
    /// <summary>
    /// Summary description for SearchSiniestroForListTableAdapter
    /// </summary>
    public partial class PacienteTableAdapter
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