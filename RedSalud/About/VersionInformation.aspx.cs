using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artexacta.App.ChangesLog;
using log4net;

public partial class About_VersionInformation : System.Web.UI.Page
{
    private static readonly ILog log = LogManager.GetLogger("Standard");

    protected void Page_Load(object sender, EventArgs e)
    {
        List<ChangesLog> theList = new List<ChangesLog>();
        try
        {
            DataSet ds = new DataSet();
            ds.ReadXml(Server.MapPath("~/DataFiles/Changes.xml"));
            foreach (DataTable tbl in ds.Tables)
            {
                DataRow[] rows = tbl.Select("1=1", "version desc");
                foreach (DataRow dr in rows)
                {
                    string version = (string)dr["version"];
                    string date = (string)dr["date"];
                    string content = (string)dr[2];
                    theList.Add(new ChangesLog(version, date, content));
                }
            }
        }
        catch (Exception q)
        {
            log.Error("Failed to get the change log data", q);
        }

        ChangesDataList.DataSource = theList;
        ChangesDataList.DataBind();
    }
}