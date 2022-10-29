using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Reportes
{
    public class EmergenciaSearch : ConfigColumns
    {
        public EmergenciaSearch ()
        {
            Column col = new Column("[C].[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GD].[FechaCreacion]", "FechaCreacion", Column.ColumnType.Date);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[P].[NumeroPoliza]", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[A].[Codigo]", "CodigoAsegurado", Column.ColumnType.String);
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