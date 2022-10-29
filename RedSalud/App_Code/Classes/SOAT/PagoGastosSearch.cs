using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.PagoGastos
{
    public class PagoGastosSearch : ConfigColumns
    {
        public PagoGastosSearch ( bool ADMIN_SOAT_PAGOS )
        {
            Column col = new Column("[NumeroReciboFactura]", "NumeroFactura", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GED].[Proveedor]", "Proveedor", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[GED].[Monto]", "Monto", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            if (ADMIN_SOAT_PAGOS)
            {
                col = new Column("[GED].[Tipo]", "TipoGasto", Column.ColumnType.String);
                col.AppearInStandardSearch = true;
                col.DisplayHelp = ADMIN_SOAT_PAGOS;
                this.Cols.Add(col);

                col = new Column("[A].[Nombre]", "Paciente", Column.ColumnType.String);
                col.AppearInStandardSearch = true;
                col.DisplayHelp = ADMIN_SOAT_PAGOS;
                this.Cols.Add(col);

                col = new Column("[GED].[FechaReciboFactura]", "FechaEmision", Column.ColumnType.Date);
                col.AppearInStandardSearch = false;
                col.DisplayHelp = ADMIN_SOAT_PAGOS;
                this.Cols.Add(col);

                col = new Column("[fullname]", "Usuario", Column.ColumnType.String);
                col.AppearInStandardSearch = false;
                col.DisplayHelp = ADMIN_SOAT_PAGOS;
                this.Cols.Add(col);

                col = new Column("[NroCheque]", "NroCheque", Column.ColumnType.String);
                col.AppearInStandardSearch = true;
                col.DisplayHelp = ADMIN_SOAT_PAGOS;
                this.Cols.Add(col);
            }
        }
    }
}