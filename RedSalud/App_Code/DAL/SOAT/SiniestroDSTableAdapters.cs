using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiniestroDSTableAdapters
{
    /// <summary>
    /// Summary description for SearchSiniestroForListTableAdapter
    /// </summary>
    public partial class SearchSiniestroForListTableAdapter
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
    /// <summary>
    /// Summary description for ExportToExcelTableAdapter
    /// </summary>
    public partial class ExportToExcelTableAdapter
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