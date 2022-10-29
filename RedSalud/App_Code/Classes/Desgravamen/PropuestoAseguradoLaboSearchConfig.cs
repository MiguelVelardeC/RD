using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for PropuestoAseguradoSearchConfig
    /// </summary>
    public class PropuestoAseguradoLaboSearchConfig : ConfigColumns
    {
        public PropuestoAseguradoLaboSearchConfig()
        {
            Column col = new Column("pa.[nombreCompleto]", "nombre", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre Completo del Propuesto Asegurado";
            this.Cols.Add(col);

            col = new Column("pa.[telefonoCelular]", "telefono", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Teléfono celular del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("pa.[carnetIdentidad]", "carnet", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Carnet de identidad del Propuesto Asegurado";
            this.Cols.Add(col);

            col = new Column("pa.[fechaNacimiento]", "fechaNacimiento", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Fecha de nacimiento del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("c.[cobroFinanciera]", "cobrar", Column.ColumnType.Boolean);
            col.AppearInStandardSearch = false;
            col.SetBooleanValues("0", "1");
            col.DisplayHelp = true;
            col.Description = "Indica si se le debe cobrar al propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("c.[citaDesgravamenId]", "citaId", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Número de cita de acuerdo a sistema";
            this.Cols.Add(col);

            col = new Column("pc.[fechaCita]", "fechaCita", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Fecha de la cita del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("pc.[fechaAtencion]", "fechaAtencion", Column.ColumnType.Date);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Fecha de atención efectiva del propuesto asegurado";
            this.Cols.Add(col);

            col = new Column("pm.proveedorMedicoId", "CodigoProveedor", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "codigo del proveedor";
            this.Cols.Add(col);
        }
    }
}