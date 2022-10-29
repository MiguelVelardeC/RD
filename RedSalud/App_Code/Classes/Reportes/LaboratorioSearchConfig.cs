using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Reportes
{
    /// <summary>
    /// Summary description for LaboratorioSearchConfig
    /// </summary>
    public class LaboratorioSearchConfig : ConfigColumns
    {
        public LaboratorioSearchConfig()
        {
            Column col = new Column("[C].[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[FechaCreacion]", "FechaCreacion", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[A].[Codigo]", "CodigoAsegurado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PAC].[Nombre]", "NombrePaciente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[P].[NumeroPoliza]", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[U].[fullname]", "Medico", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[CIU].[Nombre]", "Ciudad", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);
        }
    }
}