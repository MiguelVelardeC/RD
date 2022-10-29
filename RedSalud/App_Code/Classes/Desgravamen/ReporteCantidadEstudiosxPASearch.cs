using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for ReporteCantidadEstudiosxPASearch
    /// </summary>
    public class ReporteCantidadEstudiosxPASearch : ConfigColumns
    {
        public ReporteCantidadEstudiosxPASearch()
        {
            Column col = new Column("zz.[ID]", "citaId", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Número de cita de acuerdo a sistema";
            this.Cols.Add(col);

            col = new Column("zz.[NOMBRE PROVEEDOR]", "proveedor", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "El nombre del proveedor médico";
            this.Cols.Add(col);

            col = new Column("zz.[NOMBRE COMPLETO]", "propuestoAsegurado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "El nombre del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("zz.[CARNET]", "carnet", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "El carnet del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("zz.[FINANCIERA]", "financiera", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "La financiera a cargo o Sin Financiera";
            this.Cols.Add(col);

            col = new Column("zz.[COBRO CLIENTE]", "cobrarCliente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Busque si se le va a cobrar al cliente como texto (SI o NO)";
            this.Cols.Add(col);

            col = new Column("zz.[COBRO CLIENTE]", "cobrarCliente", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Busque si se le va a cobrar al cliente como texto (SI o NO)";
            this.Cols.Add(col);

            col = new Column("zz.[TIPO PRODUCTO]", "producto", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Solamente coloque Desgravamen o Individual";
            this.Cols.Add(col);

            col = new Column("zz.[REVISION MEDICA]", "producto", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Si se le hizo o no revision médica (1 o 0)";
            this.Cols.Add(col);
        }
    }
}