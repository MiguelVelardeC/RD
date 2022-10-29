using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;

namespace Artexacta.App
{
    /// <summary>
    /// System-Wide constants
    /// </summary>
    public class Constants
    {
        public const string PROVIDER_RAW = "RAW";
        public const string PROVIDER_IFILTER = "IFILTER";
        public const string PROVIDER_PDFBOX = "PDFBOX";
        public const string PROVIDER_HTML = "HTML";

        public const string SECURITY_VIEW_DOCUMENT = "VIEW_DOCUMENT";
        public const string SECURITY_INSERT_DOCUMENT = "INSERT_DOCUMENT";
        public const string SECURITY_UPDATE_DOCUMENT = "UPDATE_DOCUMENT";
        public const string SECURITY_DELETE_DOCUMENT = "DELETE_DOCUMENT";

        public const string SECURITY_VIEW_ACTIVITY = "VIEW_ACTIVITY";
        //public const string SECURITY_INSERT_ACTIVITY = "INSERT_ACTIVITY";
        public const string SECURITY_UPDATE_ACTIVITY = "UPDATE_ACTIVITY";
        public const string SECURITY_DELETE_ACTIVITY = "DELETE_ACTIVITY";

        public const string SECURITY_VIEW_USER = "VIEW_USER";
        public const string SECURITY_INSERT_USER = "INSERT_USER";
        public const string SECURITY_UPDATE_USER = "UPDATE_USER";
        public const string SECURITY_DELETE_USER = "DELETE_USER";

        public const string SECURITY_VIEW_OPERATION_DB = "VIEW";
        public const string SECURITY_INSERT_OPERATION_DB = "INSERT";
        public const string SECURITY_UPDATE_OPERATION_DB = "UPDATE";
        public const string SECURITY_DELETE_OPERATION_DB = "DELETE";

        public const string OBJECT_TYPE_USER = "USER";
        public const string OBJECT_TYPE_DOCUMENT = "DOCUMENT";
        public const string OBJECT_TYPE_ACTIVITY = "ACTIVITY";
        public const string OBJECT_TYPE_NOTEBOOK = "NOTEBOOK";

        public const string SECURITY_VIEW_PROJECT = "VIEW_PROJECT";
        public const string SECURITY_INSERT_PROJECT = "INSERT_PROJECT";
        public const string SECURITY_UPDATE_PROJECT = "UPDATE_PROJECT";
        public const string SECURITY_DELETE_PROJECT = "DELETE_PROJECT";


        public const string SECURITY_VIEW_RECURRENTACTIVITY = "VIEW_RECURRENTACTIVITY";
        //public const string SECURITY_INSERT_RECURRENTACTIVITY = "INSERT_RECURRENTACTIVITY";
        public const string SECURITY_UPDATE_RECURRENTACTIVITY = "UPDATE_RECURRENTACTIVITY";
        public const string SECURITY_DELETE_RECURRENTACTIVITY = "DELETE_RECURRENTACTIVITY";

        public const string USER_ACTIVE_STATUS = "Active";
        public const string THREAD_NAME = "REDSALUDTaskManagerThread";

        public const int FIRST_YEAR_REDSALUD = 2012;

        public const int FIRST_YEAR_SINIESTRALIDAD = 2016;

        public Constants()
        {
        }
    }
}