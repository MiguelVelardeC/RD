using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

/// <summary>
/// Summary description for MedicoSearch
/// </summary>
public class ProveedorSearch: ConfigColumns
{
    public ProveedorSearch () : base()
    {
        Column col = new Column("[P].[Nombres]", "Nombres", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[P].[Apellidos]", "Apellidos", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[P].[NombreJuridico]", "NombreJuridico", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[P].[Estado]", "Estado", Column.ColumnType.Boolean);
        col.SetBooleanValues("ACTIVO", "INACTIVO");
        col.AppearInStandardSearch = false;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[ClaEsp].[Nombre]", "Especialidad", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
	}
}