using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Caso
{
    public class CasoMedicoSearch : ConfigColumns
    {
        public CasoMedicoSearch ()
        {
            Column col = new Column("[CASO].[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[P].[Nombre]", "NombrePaciente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[POLIZA].[NumeroPoliza]", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[A].[Codigo]", "CodigoAsegurado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[FechaCreacion]", "FechaCreacion", Column.ColumnType.Date);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[MotivoConsultaId]", "TipoConsulta", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[CD].[CiudadId]", "Ciudad", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[P].[CarnetIdentidad]", "CarnetIdentidad", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[EN].[Nombre]", "Diagnostico", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[E2].[Nombre]", "Enfermedad2", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[E3].[Nombre]", "Enfermedad3", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[H].[DiagnosticoPresuntivo]", "DiagnosticoPresuntivo", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[U].fullname", "MedicoName", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[MC].MedicoId", "MedicoId", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);

            col = new Column("[U].UserId", "UserId", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = false;
            this.Cols.Add(col);
        }
    }
}