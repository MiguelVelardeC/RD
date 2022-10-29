using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProveedorDesgravamenSearchConfig
    /// </summary>
    public class ProveedorDesgravamenSearchConfig: ConfigColumns
    {
        public ProveedorDesgravamenSearchConfig()
        {
            Column col = new Column("pm.[nombre]", "Proveedor", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre del Proveedor Desgravamen";
            this.Cols.Add(col);

            col = new Column("u.[username]", "Usuario", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Usuario del Proveedor";
            this.Cols.Add(col);

            col = new Column("c.[nombre]", "Ciudad", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Plaza del Proveedor";
            this.Cols.Add(col);
        }
    }
}