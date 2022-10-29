using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Helper
{
    public static class ExcelProcessing
    {
        #region connstrings
        private static string conXls = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;'";
        private static string conXlsx = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0 Xml;HDR=Yes;IMEX=1'";
        private static string cmdSelect = "SELECT * FROM [{0}$]";
        private static string cmdInsert = @"INSERT INTO [{0}$] ({1}) VALUES ({2});";
        #endregion

        #region private

        private static DataTable ReadExcelInternal(string path, string conType, string sheet)
        {
            DataTable dt = new DataTable();
            string con = string.Format(conType, path);
            string cmd = string.Format(cmdSelect, sheet);
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(cmd, connection);
                using (OleDbDataReader dr = command.ExecuteReader())
                {
                    dt.Load(dr);
                    return dt;
                }
            }
        }

        private static void WriteExcelInternal(string path, string conType, string sheet, string columns, string values)
        {
            string con = string.Format(conType, path);
            string cmd = string.Format(cmdInsert, sheet,columns,values);
            using (OleDbConnection connection = new OleDbConnection(con))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand(cmd, connection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //exception here
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }
        #endregion

        #region public

        public static DataTable ReadXls(string path, string sheet)
        {
            return ReadExcelInternal(path,conXls,sheet);
        }
        public static DataTable ReadXlsx(string path, string sheet)
        {
            return ReadExcelInternal(path, conXlsx, sheet);
        }
        public static DataTable ReadExcel(string path, string sheet)
        {
            if (path.ToUpper().Contains(".XLSX"))
            {
                return ReadXlsx(path, sheet);
            }
            else if (path.ToUpper().Contains(".XLS"))
            {
                return ReadXls(path, sheet);
            }
            throw new NotSupportedException("Formato no soportado");
        }

        public static void WriteXls(string path, string sheet, string columns, string values)
        {
            WriteExcelInternal(path, conXls, sheet,columns, values);
        }
        public static void WriteXlsx(string path, string sheet, string columns, string values)
        {
            WriteExcelInternal(path, conXlsx, sheet, columns, values);
        }
        public static void WriteExcel(string path, string sheet, string columns, string values)
        {
            if (path.ToUpper().Contains(".XLSX"))
            {
                WriteXlsx(path, sheet, columns, values);
                return;
            }
            else if (path.ToUpper().Contains(".XLS"))
            {
                WriteXls(path, sheet, columns, values);
                return;
            }
            throw new NotSupportedException("Formato no soportado");
        }

        #endregion

        public static bool BulkCopy(string destTable, Dictionary<string,string> columMap, DataTable objDataTable, out string msg)
        {
            //msg = "";
            //var db = new Cognos.Negocio.Negocio();
            //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(db.Database.Connection.ConnectionString))
            //{
            //    bulkCopy.DestinationTableName = destTable;

            //    try
            //    {
            //        foreach (var item in columMap)
            //        {
            //            bulkCopy.ColumnMappings.Add(item.Key, item.Value);
            //        }
            //        bulkCopy.WriteToServer(objDataTable);
            //        return true;
            //    }
            //    catch (Exception ex)
            //    {
            //        return false;
            //        msg = ex.Message;
            //    }
            //}
            msg = "";
            return false;
        }


    }
}
