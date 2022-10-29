using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ProveedorEstudiosSearchConfig
    /// </summary>
    public class ProveedorEstudiosSearchConfig: ConfigColumns
    {
        public ProveedorEstudiosSearchConfig()
        {
            Column col = new Column("ep.[necesitaCita]", "NecesitaCita", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Define si un Estudio necesita cita o no";
            this.Cols.Add(col);

            col = new Column("ep.[deshabilitado]", "Estado", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Estado del estudio (habilitado/deshabilitado)";
            this.Cols.Add(col);
        }
    }
}