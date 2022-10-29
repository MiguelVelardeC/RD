using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Reportes
{
    public class EnfermeriaSearch : ConfigColumns
    {
        public EnfermeriaSearch ()
        {
            Column col = new Column("[C].[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[R].[Medicamento]", "Medicamento", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[R].[Presentacion]", "Presentacion", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GD].[TipoDocumento]", "TipoDocumento", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GD].[FechaCreacion]", "FechaCreacion", Column.ColumnType.Date);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GD].[FechaGasto]", "FechaGasto", Column.ColumnType.Date);
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