using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.CasoForAprobation
{
    public class CasoAprobationSearch : ConfigColumns
    {
        public CasoAprobationSearch ()
        {
            Column col = new Column("[CASO].[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[P].[Nombre]", "NombrePaciente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[A].[Codigo]", "CodigoAsegurado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[NumeroPoliza]", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);
        }
    }
}