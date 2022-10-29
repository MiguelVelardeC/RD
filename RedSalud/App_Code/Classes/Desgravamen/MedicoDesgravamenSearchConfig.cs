using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for MedicoDesgravamenSearchConfig
    /// </summary>
    public class MedicoDesgravamenSearchConfig : ConfigColumns
    {
        public MedicoDesgravamenSearchConfig()
        {
            Column col = new Column("m.[nombre]", "Nombre", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre Completo del Medico Desgravamen";
            this.Cols.Add(col);

            col = new Column("u.[username]", "Usuario", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Usuario del Medico";
            this.Cols.Add(col);

            col = new Column("pm.[nombre]", "Proveedor", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre del Proveedor";
            this.Cols.Add(col);
        }
    }
}