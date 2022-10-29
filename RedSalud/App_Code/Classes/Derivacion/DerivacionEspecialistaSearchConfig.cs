using SearchComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DerivacionEspecialistaSearchConfig
/// </summary>
public class DerivacionEspecialistaSearchConfig: ConfigColumns
{
	public DerivacionEspecialistaSearchConfig()
	{
        /*
            tmp.DerivacionId, 
		    tmp.CasoId,
		    tmp.CodigoCaso, 
		    tmp.GastoId,
		    tmp.CiudadDerivacionId,
		    tmp.CiudadDerivacionNombre,
		    tmp.PacienteId,
		    tmp.PacienteNombre, 
		    tmp.DerivadorUserId,
		    tmp.DerivadorNombre, 
		    tmp.FechaCreacion,
		    tmp.MedicoId,
		    tmp.MedicoNombre,
		    tmp.DerivacionCasoId,
		    tmp.EspecialidadNombre,
		    tmp.CodigoCasoDerivacion
         */

        Column col = new Column("tmp.[PacienteNombre]", "Paciente", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Nombre del Paciente";
        this.Cols.Add(col);

        col = new Column("tmp.[MedicoNombre]", "MedicoDerivado", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Nombre del Medico Derivado";
        this.Cols.Add(col);

        col = new Column("tmp.[DerivacionId]", "Derivacion", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Numero de Derivacion";
        this.Cols.Add(col);

        col = new Column("tmp.[CodigoCaso]", "CodigoCaso", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Codigo de Caso Inicial";
        this.Cols.Add(col);

        col = new Column("tmp.[CiudadDerivacionNombre]", "Ciudad", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Ciudad de la Derivacion";
        this.Cols.Add(col);

        col = new Column("tmp.[EspecialidadNombre]", "EspecialidadNombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Especialidad";
        this.Cols.Add(col);

        col = new Column("tmp.[MedicoDerivadorNombre]", "MedicoDerivador", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Medico Derivador";
        this.Cols.Add(col);

        col = new Column("tmp.[MedicoDerivadorId]", "MedicoDerivadorId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Medico Derivador Id";
        this.Cols.Add(col);

        col = new Column("tmp.[MedicoId]", "MedicoDerivadoId", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Medico Derivado Id";
        this.Cols.Add(col);

        col = new Column("tmp.[CodigoCasoDerivacion]", "CodigoCasoDerivacion", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Codigo de Caso Derivacion";
        this.Cols.Add(col);

        col = new Column("tmp.[PacienteNombre]", "PacienteNombre", Column.ColumnType.String);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Nombre del Paciente";
        this.Cols.Add(col);

        col = new Column("tmp.[FechaCreacion]", "FechaCreacion", Column.ColumnType.Date);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Fecha De Creacion";
        this.Cols.Add(col);

        col = new Column("tmp.[ClienteId]", "Cliente", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Codigo Interno del Cliente";
        this.Cols.Add(col);

        col = new Column("tmp.[IsAtendido]", "IsAtendido", Column.ColumnType.Numeric);
        col.AppearInStandardSearch = true;
        col.DisplayHelp = true;
        col.Description = "Indica si la derivacion ha sido atendida";
        this.Cols.Add(col);
	}


}