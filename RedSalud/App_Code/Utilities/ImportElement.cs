using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;
using log4net;
using OfficeOpenXml;
using Telerik.Web.UI;

/// <summary>
/// Summary description for ImportElement
/// </summary>
/// 
namespace Artexacta.App.Utilities.Import
{
    public class ImportElement
    {

        private static readonly ILog log = LogManager.GetLogger("Standard");

        // These are the colums of the Excel import
        public const string Nombre = "Nombre";
        public const string Apellido = "Apellido";
        public const string FechaNacimiento = "FechaNacimiento";
        public const string Genero = "Genero";
        public const string Relacion = "Relacion";
        public const string CarnetIdentidad = "CarnetIdentidad";
        public const string Direccion = "Direccion";
        public const string Telefono = "Telefono";
        public const string LugarTrabajo = "LugarTrabajo";
        public const string TelefonoTrabajo = "TelefonoTrabajo";
        public const string EstadoCivil = "EstadoCivil";
        public const string NroHijo = "NroHijo";
        public const string Antecedentes = "Antecedentes";
        public const string AntecedentesAlergicos = "AntecedentesAlergicos";
        public const string AntecedentesGinecoobstetricos = "AntecedentesGinecoobstetricos";
        public const string Email = "Email";
        public const string CodigoAsegurado = "CodigoAsegurado";
        public const string NumeroPoliza = "NumeroPoliza";
        public const string FechaInicioPoliza = "FechaInicio";
        public const string FechaFinPoliza = "FechaFin";
        public const string MontoTotal = "MontoTotal";
        public const string MontoFarmacia = "MontoFarmacia";
        public const string Cobertura = "Cobertura";
        public const string NombrePlan = "NombrePlan";
        public const string EstadoPoliza = "EstadoPoliza";

        public const string Ramo = "Ramo";
        public const string Producto = "Producto";
        public const string PolizaMadre = "PolizaMadre";
        public const string Certificado = "Certificado";
        public const string TipoIdentificacion = "TipoIdentificacion";
        public const string Edad = "Edad";

        public ImportElement()
        {
        }

        public class RowColumn
        {
            public RowColumn(string st)
            {
                string[] rowCol = st.Split('x');
                Row = Int32.Parse(rowCol[0]);
                Column = Int32.Parse(rowCol[1]);
            }

            public int Row { get; set; }
            public int Column { get; set; }
        }

        public static DataTable GetDataTableFromExcel ( Stream stream, ref string RowError )
        {
            return GetDataTableFromExcel(stream, "Pacientes", ref RowError);
        }

        public static DataTable GetDataTableFromExcel(Stream stream, string sheet, ref string RowError)
        {
            if (string.IsNullOrEmpty(sheet))
                throw new ArgumentException("Sheet name cannot be empty");


            using (var pck = new ExcelPackage())
            {

                pck.Load(stream);
                ExcelWorksheet ws = null;
                try
                {
                    ws = pck.Workbook.Worksheets.First(w => w.Name.Equals(sheet));
                }
                catch (Exception ex)
                {
                    log.Debug("The excel file does not have a " + sheet + " worksheet", ex);
                    throw new InvalidOperationException("The file must have a " + sheet + " worksheet with the survey");
                }
                //  var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                bool hasHeader = true; // addjust it accordingly( i've mentioned that this is a simple approach)
                //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                //{
                //    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                //}
                AddColumns(tbl);
                string[] columnNames = GetColumnNames();
                var startRow = hasHeader ? 2 : 1;
                int rowNumAnt = 0;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];

                    if (wsRow.Count() <= 0)
                        break;
                    var row = tbl.NewRow();
                    bool add = true;
                    string minError = "";
                    bool end = false;
                    foreach (var cell in wsRow)
                    {
                        string colName = columnNames[cell.Start.Column - 1];
                        if (((colName == Nombre || colName == FechaNacimiento || colName == Genero || colName == Relacion)
                            && string.IsNullOrWhiteSpace(cell.Text)))
                        {
                            add = false;
                            end = true;
                            break;
                        }
                        if (cell.Start.Column > columnNames.Length)
                        {
                            add = false;
                            break;
                        }
                        try
                        {
                            object response = ValidateField(colName, cell.Text, ref minError);
                            if (response != null)
                            {
                                row[colName] = response;
                            }
                        }
                        catch (Exception q)
                        {
                            minError += "Error desconocido" +
                                (colName == null || string.IsNullOrWhiteSpace(colName) ? ", revise la fila." : " en la columna '" + colName + "'.") + 
                                "<br />";
                            //RowError += "Error en la fila " + rowNum + " y la columna '" + colName + "'.<br /><div style='display: inline-block;width: 20px; height:20px;'></div>" + minError + "<br />";
                            colName = colName == null ? "No Column Name" : colName;
                            log.Error("Error en la fila " + rowNum + " y la columna '" + colName + "'.", q);
                        }
                    }
                    try
                    {
                        add = false;

                        if (!string.IsNullOrWhiteSpace(row.ItemArray[0].ToString()))
                        {
                            add = true;
                        }
                    }
                    catch {}
                    if (add)
                    {
                        tbl.Rows.Add(row);
                    }
                    rowNumAnt = rowNum;
                    if (!string.IsNullOrWhiteSpace(minError))
                    {
                        RowError += "Error en la fila " + rowNum + ":<div style='margin-left: 20px;'>" + minError + "</div>";
                    }
                    if (end)
                    {
                        break;
                    }
                }

                ValidateNulls(tbl, 0, ref RowError);
                return tbl;
            }

        }


        public static DataTable GetDataTableFromExcelNacionalVida(Stream stream, ref string RowError)
        {
            using (var pck = new ExcelPackage())
            {
                pck.Load(stream);
                ExcelWorksheet ws = null;
                try
                {
                    ws = pck.Workbook.Worksheets.First();
                }
                catch (Exception ex)
                {
                    log.Debug("The excel file does not have a worksheet", ex);
                    throw new InvalidOperationException("The file must have a worksheet with the survey");
                }
                //  var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                //{
                //    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                //}
                AddColumns(tbl);
                string[] columnNames = GetNacionalVidaColumnNames();
                var startRow = 19;
                int rowNumAnt = 19;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 2, rowNum, ws.Dimension.End.Column];

                    if (wsRow.Count() <= 0)
                        break;
                    var row = tbl.NewRow();
                    bool add = true;
                    string minError = "";
                    foreach (var cell in wsRow)
                    {
                        if (rowNum != rowNumAnt && string.IsNullOrWhiteSpace(cell.Text))
                        {
                            add = false;
                            break;
                        }
                        if (cell.Start.Column > columnNames.Length)
                        {
                            add = false;
                            break;
                        }

                        string colName = columnNames[cell.Start.Column - 2];
                        try
                        {
                            switch (colName)
                            {
                                case NombrePlan:
                                case Ramo:
                                case PolizaMadre:
                                case Certificado:
                                case TipoIdentificacion:
                                    break;
                                default:
                                    colName = colName == Producto ? NombrePlan : colName;
                                    object response = ValidateField(colName, cell.Text, ref minError);
                                    if (response != null)
                                    {
                                        row[colName] = response;
                                        switch (colName)
                                        {
                                            case CarnetIdentidad:
                                                row[CodigoAsegurado] = response;
                                                break;
                                            case Nombre:
                                                row[Genero] = true;
                                                break;
                                        }
                                    }
                                    break;
                            }
                            rowNumAnt = rowNum;
                        }
                        catch (Exception q)
                        {
                            minError += "Error desconocido" +
                                (colName == null || string.IsNullOrWhiteSpace(colName) ? ", revise la fila." : " en la columna '" + colName + "'.") +
                                "<br />";
                            //RowError += "Error en la fila " + rowNum + " y la columna '" + colName + "'.<br /><div style='display: inline-block;width: 20px; height:20px;'></div>" + minError + "<br />";
                            colName = colName == null ? "No Column Name" : colName;
                            log.Error("Error en la fila " + rowNum + " y la columna '" + colName + "'.", q);
                        }
                    }
                    try
                    {
                        add = false;

                        if (!string.IsNullOrWhiteSpace(row.ItemArray[0].ToString()))
                        {
                            add = true;
                        }
                    }
                    catch { }
                    if (add)
                    {
                        row[MontoTotal] = -1;
                        row[NroHijo] = 0;
                        row[MontoFarmacia] = 0;
                        row[Cobertura] = "0/0";
                        row[EstadoPoliza] = "ACTIVO";
                        tbl.Rows.Add(row);
                    }
                    if (!string.IsNullOrWhiteSpace(minError))
                    {
                        RowError += "Error en la fila " + rowNum + ":<div style='margin-left: 20px;'>" + minError + "</div>";
                    }
                }

                ValidateNulls(tbl, 19, ref RowError);
                return tbl;
            }

        }

        public static DataTable GetDataTableFromExcelGeneral ( Stream stream, ref string RowError )
        {
            using (var pck = new ExcelPackage())
            {
                pck.Load(stream);
                ExcelWorksheet ws = null;
                try
                {
                    ws = pck.Workbook.Worksheets.First();
                }
                catch (Exception ex)
                {
                    log.Debug("The excel file does not have a worksheet", ex);
                    throw new InvalidOperationException("The file must have a worksheet with the survey");
                }
                //  var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                //foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                //{
                //    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                //}
                AddColumns(tbl);
                string[] columnNames = GetGeneralColumnNames();
                var startRow = 2;
                int rowNumAnt = 2;
                for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];

                    if (wsRow.Count() <= 0)
                        break;
                    var row = tbl.NewRow();
                    bool add = true;
                    string minError = "";
                    foreach (var cell in wsRow)
                    {
                        if (rowNum != rowNumAnt && string.IsNullOrWhiteSpace(cell.Text))
                        {
                            add = false;
                            break;
                        }
                        if (cell.Start.Column > columnNames.Length)
                        {
                            add = false;
                            break;
                        }

                        string colName = columnNames[cell.Start.Column - 1];
                        try
                        {
                            switch (colName)
                            {
                                //case NombrePlan:
                                case Ramo:
                                case PolizaMadre:
                                case Certificado:
                                case TipoIdentificacion:
                                    break;
                                default:
                                    colName = colName == Producto ? NombrePlan : colName;
                                    object response = ValidateField(colName, cell.Text, ref minError);
                                    if (response != null)
                                    {
                                        row[colName] = response;
                                        switch (colName){
                                            case CarnetIdentidad:
                                                row[CodigoAsegurado] = response;
                                                break;
                                            case Nombre:
                                                row[Genero] = true;
                                                break;
                                        }
                                    }
                                    break;
                            }
                            rowNumAnt = rowNum;
                        }
                        catch (Exception q)
                        {
                            minError += "Error desconocido" +
                                (colName == null || string.IsNullOrWhiteSpace(colName) ? ", revise la fila." : " en la columna '" + colName + "'.") +
                                "<br />";
                            //RowError += "Error en la fila " + rowNum + " y la columna '" + colName + "'.<br /><div style='display: inline-block;width: 20px; height:20px;'></div>" + minError + "<br />";
                            colName = colName == null ? "No Column Name" : colName;
                            log.Error("Error en la fila " + rowNum + " y la columna '" + colName + "'.", q);
                        }
                    }
                    try
                    {
                        add = false;

                        if (!string.IsNullOrWhiteSpace(row.ItemArray[0].ToString()))
                        {
                            add = true;
                        }
                    }
                    catch { }
                    if (add)
                    {
                        row[MontoTotal] = row[MontoTotal] != DBNull.Value ? row[MontoTotal] : -1;
                        row[NroHijo] = row[NroHijo] != DBNull.Value ? row[NroHijo] : 0;
                        row[MontoFarmacia] = row[MontoFarmacia] != DBNull.Value ? row[MontoFarmacia] : 0;
                        row[Cobertura] = row[Cobertura] != DBNull.Value ? row[Cobertura] : "0/0";
                        row[EstadoPoliza] = row[EstadoPoliza] != DBNull.Value ? row[EstadoPoliza] : "ACTIVO";
                        tbl.Rows.Add(row);
                    }
                    if (!string.IsNullOrWhiteSpace(minError))
                    {
                        RowError += "Error en la fila " + rowNum + ":<div style='margin-left: 20px;'>" + minError + "</div>";
                    }
                }

                ValidateNulls(tbl, 0, ref RowError);
                return tbl;
            }

        }

        public static object ValidateField ( string colName, string cellText, ref string minError )
        {
            bool genero = true;
            //if (row[columnNames[cell.Start.Column - 1]].GetType() == typeof(int) && string.IsNullOrEmpty(cellText))
            if ((colName.ToLower().EndsWith("id") || colName.ToLower() == "nrohijo")
                    && string.IsNullOrEmpty(cellText))
            {
                return 0;
            }
            else switch (colName.ToLower())
            {
                case "fechainicio":
                case "fechafin":
                case "fechanacimiento":
                    try
                    {
                        return Convert.ToDateTime(cellText.Trim());
                    }
                    catch
                    {
                        try
                        {
                            return DateTime.ParseExact(cellText.Trim(), "yyyy-MM-dd HH:mm tt", null);
                        }
                        catch
                        {
                            minError += "Columna '" + colName + "': Formato de la Fecha no reconocido.<br />";
                            break;
                            //throw new ArgumentException("Error en el Formato de " + colName);
                        }
                    }
                case "genero":
                    if (!cellText.Trim().ToLower().StartsWith("ma") && !cellText.Trim().ToLower().StartsWith("fe"))
                    {
                        minError += "Columna '" + colName + "': Formato de Genero no reconocido (solo acepta MASCULINO/FEMENINO).<br />";
                        break;
                        //throw new ArgumentException("Error en el Formato de " + colName);
                    }
                    genero = (cellText.Trim().ToLower().StartsWith("m"));
                    return genero ? 1 : 0;
                case "cobertura":
                    if (!Regex.IsMatch(cellText, "^[0-9]{2}[/][0-9]{2}$"))
                    {
                        minError += "Columna '" + colName + "': Formato de Cobertura no reconocido (ejemplo 80/20 o 70/30).<br />";
                        break;
                        //throw new ArgumentException("Error en el Formato de " + colName);
                    }
                    return cellText;
                case "direccion":
                case "lugartrabajo":
                    if (cellText.Trim().Length > 250)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 250 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "telefono":
                case "estadocivil":
                case "telefonotrabajo":
                    if (cellText.Trim().Length > 20)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 20 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "antecedentes":
                    if (cellText.Trim().Length > 2000)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 2000 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "antecedentesalergicos":
                case "antecedentesginecoobstetricos":
                    if (cellText.Trim().Length > 4000)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 4000 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "email":
                case "nombreplan":
                case "nombre":
                case "apellido":
                    if (cellText.Trim().Length > 100)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 100 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "codigoasegurado":
                    if (cellText.Trim().Length > 50)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 50 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return cellText.Trim().ToLower().ToUpper();
                case "numeropoliza":
                    string numeroPoliza = cellText.Replace("POL-", "").Replace("POL", "").Trim().ToUpper();
                    if (numeroPoliza.ToString().Length > 20)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 20 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    return numeroPoliza;
                case "carnetidentidad":
                    if (cellText.Trim().Length > 20)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 20 caracteres.<br />";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    //else if (!Regex.IsMatch(cellText.Trim(), @"^[0-9]*\s?(LP|SC|CB|CHQ|OR|PT|TJ|BE|PA|EX|lp|sc|cb|chq|or|pt|tj|be|pa|ex)?$"))
                    //{
                    //    minError += "Columna '" + colName + "' (" + cellText.Trim() + "): El formato del Carnet de Identidad es incorrecto.<br />";
                    //    break;
                    //}
                    return cellText.Replace(" ", "").Trim().ToUpper();
                case "estadopoliza":
                    if (cellText.ToUpper().StartsWith("ACTIV"))
                    {
                        return "ACTIVO";
                    }
                    else if (cellText.ToUpper().StartsWith("INACTIV"))
                    {
                        return "INACTIVO";
                    }
                    else
                    {
                        minError += "Columna '" + colName + "': Estado de la póliza invalido, solo puede ser ACTIVO o INACTIVO.<br />";
                        break;
                        //throw new ArgumentException("El estado de la póliza solo puede ser activo o inactivo.");
                    }
                case "relacion":
                    if (cellText.Trim().Length > 50)
                    {
                        minError += "Columna '" + colName + "': Texto muy largo, solo puede contener máximo 50 caracteres<br />.";
                        break;
                        //throw new ArgumentException(colName + " muy Largo");
                    }
                    if (Regex.IsMatch(cellText.ToLower().Trim(), "(asegurado)|(asegurado\\s+principal)|(titular)"))
                    {
                        cellText = "TITULAR";
                    }
                    else
                    {
                        cellText = "DEPENDIENTE";
                    }
                    return cellText.ToUpper();
                default:
                    return cellText.Trim().ToLower().ToUpper();
            }
            return null;
        }

        public static void ValidateNulls ( DataTable tbl, int startRowNumber, ref string rowError )
        {
            ValidateNulls(ref tbl, startRowNumber, false, ref rowError);
        }

        public static void ValidateNulls ( ref DataTable tbl, int startRowNumber, bool eraseInvalid, ref string rowError )
        {
            string[] columnasObligatorias = new string[]{Nombre, FechaNacimiento, Genero, Relacion, CodigoAsegurado, 
                    NumeroPoliza, FechaInicioPoliza, FechaFinPoliza, MontoTotal, MontoFarmacia, Cobertura, NombrePlan, EstadoPoliza};
            int rowNumber = 1;
            List<DataRow> borrar = new List<DataRow>();
            foreach (DataRow row in tbl.Rows)
            {
                rowNumber++;
                bool delete = false;
                foreach (string columnName in columnasObligatorias)
                {
                    if (string.IsNullOrWhiteSpace(row[columnName].ToString()))
                    {
                        rowError += "No hay datos en la Fila " + (rowNumber + startRowNumber) + " columna '" + columnName + "'.<br />";
                        delete = true;
                    }
                }
                if (delete && eraseInvalid)
                {
                    borrar.Remove(row);
                }
            }
            foreach (DataRow row in borrar)
            {
                tbl.Rows.Remove(row);
            }
        }
        
        private static string[] GetColumnNames()
        {
            string[] names = new string[]{
                Nombre,
                FechaNacimiento,
                Genero,
                Relacion,
                CarnetIdentidad, 
                Direccion,
                Telefono,
                LugarTrabajo, 
                TelefonoTrabajo,
                EstadoCivil,
                NroHijo,
                Antecedentes, 
                AntecedentesAlergicos, 
                AntecedentesGinecoobstetricos, 
                Email, 
                CodigoAsegurado,
                NumeroPoliza,
                FechaInicioPoliza,
                FechaFinPoliza,
                MontoTotal, 
				MontoFarmacia,
				Cobertura,
                NombrePlan, 
                EstadoPoliza
            };
            return names;
        }

        private static string[] GetNacionalVidaColumnNames()
        {
            string[] names = new string[]{
                Ramo,
                Producto,
                NumeroPoliza,
                PolizaMadre,
                Certificado,
                TipoIdentificacion,
                CarnetIdentidad, //CodigoAsegurado
                Nombre,
                Relacion,
                NombrePlan,
                FechaInicioPoliza,
                FechaFinPoliza,
                Direccion,
                TelefonoTrabajo,
                Email,
                Telefono,
                FechaNacimiento,
                Edad,
                /*Campos no recibidos en el excel:
                Genero,
                LugarTrabajo, 
                EstadoCivil,
                NroHijo,
                Antecedentes, 
                AntecedentesAlergicos, 
                AntecedentesGinecoobstetricos, 
                MontoTotal, //No tienen monto fijo: 0
				MontoFarmacia, //No tienen cobertura en farmacia: 0
				Cobertura, //Siempre 0/0
                EstadoPoliza //Siempre son activas*/
            };
            return names;
        }

        private static string[] GetGeneralColumnNames ()
        {
            string[] names = new string[]{
                Nombre,
                FechaNacimiento,
                Genero,
                Relacion,
                CodigoAsegurado,
                NumeroPoliza,
                FechaInicioPoliza,
                FechaFinPoliza,
                MontoTotal,
                MontoFarmacia,
                Cobertura,
                NombrePlan,
                EstadoPoliza                       								
            };
            return names;
        }

        public static string[] GetAlianzaColumnNames ()
        {
            string[] names = new string[]{
                Ramo,
                Producto,
                NumeroPoliza,
                PolizaMadre,
                Certificado,
                FechaInicioPoliza,
                FechaFinPoliza,
                CodigoAsegurado,
                Nombre,
                MontoTotal,
                EstadoPoliza
            };
            return names;
        }

        public static void AddColumns(DataTable dt)
        {
            dt.Columns.Add(Nombre, typeof(string));
            dt.Columns.Add(FechaNacimiento, typeof(DateTime));
            dt.Columns.Add(Genero, typeof(int));
            dt.Columns.Add(Relacion, typeof(string));
            dt.Columns.Add(CarnetIdentidad, typeof(string));
            dt.Columns.Add(Direccion, typeof(string));
            dt.Columns.Add(Telefono, typeof(string));
            dt.Columns.Add(LugarTrabajo, typeof(string));
            dt.Columns.Add(TelefonoTrabajo, typeof(string));
            dt.Columns.Add(EstadoCivil, typeof(string));
            dt.Columns.Add(NroHijo, typeof(int));
            dt.Columns.Add(Antecedentes, typeof(string));
            dt.Columns.Add(AntecedentesAlergicos, typeof(string));
            dt.Columns.Add(AntecedentesGinecoobstetricos, typeof(string));
            dt.Columns.Add(Email, typeof(string));
            dt.Columns.Add(CodigoAsegurado, typeof(string));
            dt.Columns.Add(NumeroPoliza, typeof(string));
            dt.Columns.Add(FechaInicioPoliza, typeof(DateTime));
            dt.Columns.Add(FechaFinPoliza, typeof(DateTime));
            dt.Columns.Add(MontoTotal, typeof(decimal));
            dt.Columns.Add(MontoFarmacia, typeof(decimal));
            dt.Columns.Add(Cobertura, typeof(string));
            dt.Columns.Add(NombrePlan, typeof(string));
            dt.Columns.Add(EstadoPoliza, typeof(string));
        }

        public static void FillFromRow(DataRow row, ref DataRow newRow, ref string errors)
        {
            try
            {
                newRow[Nombre] = ValidateField(Nombre, Regex.Replace(row["sCliename"].ToString().Replace(",", ""), @"\s+", " ").Trim(), ref errors);
                try
                {
                    newRow[FechaNacimiento] = Convert.ToDateTime(row["dBirthdat"]).Date;
                }
                catch
                {
                    newRow[FechaNacimiento] = SqlDateTime.MinValue.Value;
                }
                try
                {
                    newRow[Genero] = row["sSexClient"] == null ? 1 : 
                        row["sSexClient"].ToString().Trim().ToLower() == "masculino" ? 1 : 0;
                }
                catch
                {
                    newRow[Genero] = 1;
                }
                newRow[Relacion] = (row["nCertif"] == null ? -1 : Convert.ToInt32(row["nCertif"])) > 0 ? "DEPENDIENTE" : "TITULAR";
                string _CarnetIdentidad = "";
                try
                {
                    if(row["nCuit"] != null){
                        _CarnetIdentidad = row["nCuit"].ToString();
                        if(row["SComplementCuit"] != null){
                            _CarnetIdentidad += (row["SComplementCuit"].ToString().Trim() == "" ? "" : "-" + row["SComplementCuit"].ToString());
                        }
                        if (row["sExt"] != null)
                        {
                            _CarnetIdentidad += (row["sExt"].ToString().Trim() == "" ? "" : " " + row["sExt"].ToString());
                        }
                    }
                }
                catch {}
                newRow[CarnetIdentidad] = _CarnetIdentidad.Trim();
                newRow[Direccion] = "";
                newRow[Telefono] = "";
                newRow[LugarTrabajo] = "";
                newRow[TelefonoTrabajo] = "";
                newRow[EstadoCivil] = "";
                newRow[NroHijo] = 0;
                newRow[Antecedentes] = "";
                newRow[AntecedentesAlergicos] = "";
                newRow[AntecedentesGinecoobstetricos] = "";
                newRow[Email] = "";
                newRow[CodigoAsegurado] = ValidateField(Nombre, row["sClient"].ToString().Trim(), ref errors);
                newRow[NumeroPoliza] = ValidateField(Nombre, row["nPolicy"].ToString().Trim(), ref errors);
                newRow[FechaInicioPoliza] = ValidateField(Nombre, row["dEffecdate"].ToString(), ref errors);
                newRow[FechaFinPoliza] = ValidateField(Nombre, row["dExpirdat"].ToString(), ref errors);
                string producto = row["nProduct"].ToString().Trim();
                newRow[MontoTotal] = producto == "555" ? 35000 : producto == "550" ? 21000 : producto == "560" ? 7000 : 0;
                newRow[MontoFarmacia] = 0;
                newRow[Cobertura] = producto == "555" ? "20/80" : producto == "550" ? "20/80" : producto == "560" ? "60/40" : "0/0";
                newRow[NombrePlan] = ValidateField(Nombre, (row["nProduct"] + "-" + row["nBranch"]).Trim(), ref errors);
                newRow[EstadoPoliza] = row["sStatus"].ToString().StartsWith("Valida") ? "ACTIVO" : "INACTIVO";
            }
            catch (Exception q)
            {
                log.Error("Error converting data from webservice", q);
                errors = q.Message;
            }
        }
    }
}