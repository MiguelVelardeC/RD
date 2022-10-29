using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CasoDSTableAdapters
{
    /// <summary>
    /// Summary description for SearchCasoTableAdapter
    /// </summary>
    public partial class SearchCasoTableAdapter
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
    /// Summary description for SearchCasoForAprobationTableAdapter
    /// </summary>
    public partial class SearchCasoForAprobationTableAdapter
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
    /// Summary description for HistorialTableAdapter
    /// </summary>
    public partial class HistorialTableAdapter
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