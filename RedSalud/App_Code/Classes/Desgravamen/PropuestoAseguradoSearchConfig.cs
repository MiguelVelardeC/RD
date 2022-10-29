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
    public class PropuestoAseguradoSearchConfig : ConfigColumns
    {
        public PropuestoAseguradoSearchConfig()
        {
            Column col = new Column("zz.[nombreCompleto]", "nombre", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre Completo del Propuesto Asegurado";
            this.Cols.Add(col);//zz.[carnetIdentidad]

            col = new Column("zz.[carnetIdentidad]", "ci", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "CI del Propuesto Asegurado";
            this.Cols.Add(col);

            col = new Column("zz.[tipoProducto]", "producto", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "El tipo de producto, puede ser Desgravamen o Individual";
            this.Cols.Add(col);

            col = new Column("zz.[financiera]", "financiera", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Nombre de la Entidad Financiera";
            this.Cols.Add(col);

            col = new Column("zz.[financieraId]", "financieraCodigo", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Codigo de la Entidad Financiera";
            this.Cols.Add(col);

            col = new Column("zz.[fechaHoraCita]", "fechaCita", Column.ColumnType.Date);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Fecha de la Cita";
            this.Cols.Add(col);

            //col = new Column("zz.[fechaCreacion]", "fechaCreacion", Column.ColumnType.Date);
            //col.AppearInStandardSearch = true;
            //col.DisplayHelp = true;
            //col.Description = "Fecha de la creación de la cita";
            //this.Cols.Add(col);

            col = new Column("zz.[usuarioRegistro]", "usuario", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            col.Description = "Usuario que realizó el registro de la cita";
            this.Cols.Add(col);


            //col = new Column("zz.[necesitaLaboratorio]", "necesitaEstudio", Column.ColumnType.Boolean);
            //col.SetBooleanValues("1", "0");
            //col.AppearInStandardSearch = false;
            //col.DisplayHelp = true;
            //col.Description = "Si el propuesto asegurado necesita estudios o no";
            //this.Cols.Add(col);

            //col = new Column("zz.[necesitaExamen]", "necesitaRevision", Column.ColumnType.Boolean);
            //col.SetBooleanValues("1", "0");
            //col.AppearInStandardSearch = false;
            //col.DisplayHelp = true;
            //col.Description = "Si el propuesto asegurado necesita revisión médica";
            //this.Cols.Add(col);

            col = new Column("zz.[aprobado]", "aprobado", Column.ColumnType.Boolean);
            col.SetBooleanValues("1", "0");
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "Si el caso para ese propuesto asegurado está aprobado";
            this.Cols.Add(col);

            col = new Column("zz.[nombre]", "medico", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "El nombre del médico que atiende la revisión médica";
            this.Cols.Add(col);

            col = new Column("zz.[citaDesgravamenId]", "id", Column.ColumnType.Numeric);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "El identificador de la cita de desgravamen";
            this.Cols.Add(col);

            col = new Column("zz.[ciudad]", "ciudad", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "La ciudad donde fue hecha la cita de desgravamen";
            this.Cols.Add(col);

            col = new Column("zz.[ciudadId]", "ciudadId", Column.ColumnType.String);
            col.AppearInStandardSearch = false;
            col.DisplayHelp = true;
            col.Description = "El id de ciudad donde fue hecha la cita de desgravamen";
            this.Cols.Add(col);

            //col = new Column("zz.[tieneExamenMedico]", "tieneExamenMedico", Column.ColumnType.Boolean);
            //col.AppearInStandardSearch = false;
            //col.SetBooleanValues("1", "0");
            //col.DisplayHelp = true;
            //col.Description = "Si la cita comprende un examen médico Atendido";
            //this.Cols.Add(col);
        }
    }
}