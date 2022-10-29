using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasoMedicoDSTableAdapters
{
    /// <summary>
    /// Summary description for CasoMedicoDSTableAdapter
    /// </summary>
    public partial class CasoMedicoTableAdapter
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