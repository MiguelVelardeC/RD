using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

/// <summary>
/// Summary description for MedicoSearch
/// </summary>
public class MedicoSearch: ConfigColumns
{
    public MedicoSearch () : base()
    {
        Column col = new Column("[MedicoId]", "MedicoId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = false;
        col.DisplayHelp = false;
        this.Cols.Add(col);

        col = new Column("[USER].[fullname]", "Nombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[ESP].[Nombre]", "Especialidad", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[Sedes]", "Sedes", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);

        col = new Column("[ColegioMedico]", "ColegioMedico", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        this.Cols.Add(col);
	}
}