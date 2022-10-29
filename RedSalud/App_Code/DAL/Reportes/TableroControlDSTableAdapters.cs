using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TableroControlDSTableAdapters
{
    /// <summary>
    /// Summary description for TotalesConsultasTableAdapter
    /// </summary>
    public partial class TotalesConsultasTableAdapter
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
    /// Summary description for EnfermedadesXConsultasTableAdapter
    /// </summary>
    public partial class EnfermedadesXConsultasTableAdapter
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
    /// Summary description for TotalesPacientesTableAdapter
    /// </summary>
    public partial class TotalesPacientesTableAdapter
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
    /// Summary description for ConsultasXPacientesTableAdapter
    /// </summary>
    public partial class ConsultasXPacientesTableAdapter
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
    /// Summary description for ConsultasXPacientesCriticosTableAdapter
    /// </summary>
    public partial class ConsultasXPacientesCriticosTableAdapter
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
    /// Summary description for GastosEstudiosTableAdapter
    /// </summary>
    public partial class GastosEstudiosTableAdapter
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
    /// Summary description for GastosFarmaciaTableAdapter
    /// </summary>
    public partial class GastosFarmaciaTableAdapter
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
    /// Summary description for PrestacionesOdontologicasTableAdapter
    /// </summary>
    public partial class PrestacionesOdontologicasTableAdapter
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
    /// Summary description for CasosXMesTableAdapter
    /// </summary>
    public partial class CasosXMesTableAdapter
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
    /// Summary description for EspecialistasXDerivacionesTableAdapter
    /// </summary>
    public partial class EspecialistasXDerivacionesTableAdapter
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
    /// Summary description for DoctoresXCasosTableAdapter
    /// </summary>
    public partial class DoctoresXCasosTableAdapter
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
    /// Summary description for EnfermedadesCronicasTableAdapter
    /// </summary>
    public partial class EnfermedadesCronicasTableAdapter
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
    /// Summary description for ConteoConsultasTableAdapter
    /// </summary>
    public partial class ConteoConsultasTableAdapter
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