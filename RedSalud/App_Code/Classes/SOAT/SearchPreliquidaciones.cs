using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.SearchPreliquidaciones
{
    public class SearchPreliquidaciones : ConfigColumns
    {
        public SearchPreliquidaciones ()
        {
            Column col = new Column("[PRE].[NumeroReciboFactura]", "NumeroFactura", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PRE].[Proveedor]", "Proveedor", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PRE].[Monto]", "Monto", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PRE].[Tipo]", "TipoGasto", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[A].[Nombre]", "Paciente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PRE].[Fecha]", "FechaRegistro", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[PRE].[FechaReciboFactura]", "FechaRecepcion", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            this.Cols.Add(col);
        }
    }
}