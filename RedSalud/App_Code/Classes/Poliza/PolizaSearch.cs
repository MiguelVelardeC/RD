using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Poliza
{
    public class PolizaSearch : ConfigColumns
    {
        public PolizaSearch ()
        {
            Column col = new Column("[Codigo]", "CodigoAsegurado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[NumeroPoliza]", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[Nombre]", "NombrePaciente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[CarnetIdentidad]", "CarnetIdentidad", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[Estado]", "Estado", Column.ColumnType.Boolean);
            col.SetBooleanValues("ACTIVO", "INACTIVO");
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            this.Cols.Add(col);
        }
    }
}