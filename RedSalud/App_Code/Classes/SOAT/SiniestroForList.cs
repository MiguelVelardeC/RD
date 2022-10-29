using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchComponent;

namespace Artexacta.App.Siniestro
{
    public class SiniestroForList
    {
        private int _SiniestroId;
        private DateTime _FechaSiniestro;
        private DateTime _FechaDenuncia;
        private int _TotalOcupantes;
        private int _CantidadHeridos;
        private int _CantidadFallecidos;
        private string _Lugar;
        private string _numeroRoseta;
        private string _numeroPoliza;
        private string _operacionesId;
        private string _placa;
        private string _tipo;
        private string _sector;
        private string _carnetIdentidad;
        private string _nombreTitular;
        private bool _licenciaConducir;
        private decimal _siniestrosPreliquidacion;
        private decimal _siniestrosPagados;
        private string _Zona;
        private string _Sindicato;
        private string _estadoSeguimiento;
        private string _NombreAuditor;
        private int _fileCount;
        private int _rowNumber;

        public int SiniestroId
        {
            get { return this._SiniestroId; }
            set { this._SiniestroId = value; }
        }
        public DateTime FechaSiniestro
        {
            get { return this._FechaSiniestro; }
            set { this._FechaSiniestro = value; }
        }
        public string FechaSiniestroForDisplay
        {
            get { return this._FechaSiniestro.ToString("dd/MMM/yyyy"); }
        }
        public DateTime FechaDenuncia
        {
            get { return this._FechaDenuncia; }
            set { this._FechaDenuncia = value; }
        }
        public string FechaDenunciaForDisplay
        {
            get { return this._FechaDenuncia.ToString("dd/MMM/yyyy"); }
        }
        public int TotalOcupantes
        {
            get { return this._TotalOcupantes; }
            set { this._TotalOcupantes = value; }
        }
        public int CantidadHeridos
        {
            get { return this._CantidadHeridos; }
            set { this._CantidadHeridos = value; }
        }
        public int CantidadFallecidos
        {
            get { return this._CantidadFallecidos; }
            set { this._CantidadFallecidos = value; }
        }
        public string Lugar
        {
            get { return this._Lugar; }
            set { this._Lugar = value; }
        }

        public string Zona
        {
            get { return this._Zona; }
            set { this._Zona = value; }
        }
        public string Sindicato
        {
            get { return this._Sindicato; }
            set { this._Sindicato = value; }
        }
        public bool HasSindicato
        {
            get { return !string.IsNullOrWhiteSpace(this._Sindicato); }
        }

        public string NumeroRoseta
        {
            get { return _numeroRoseta; }
            set { _numeroRoseta = value; }
        }

        public string NumeroPoliza
        {
            get { return _numeroPoliza; }
            set { _numeroPoliza = value; }
        }

        public string IdentificadorOperaciones
        {
            set { _operacionesId = value; }
            get { return _operacionesId; }
        }

        public string Placa
        {
            get { return _placa; }
            set { _placa = value; }
        }

        public string Tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string Sector
        {
            get { return _sector; }
            set { _sector = value; }
        }

        public string CarnetIdentidad
        {
            get { return _carnetIdentidad; }
            set { _carnetIdentidad = value; }
        }

        public string NombreTitular
        {
            get { return _nombreTitular; }
            set { _nombreTitular = value; }
        }

        public bool LicenciaConducir
        {
            get { return _licenciaConducir; }
            set { _licenciaConducir = value; }
        }

        public decimal SiniestrosPreliquidacion
        {
            get { return _siniestrosPreliquidacion; }
            set { _siniestrosPreliquidacion = value; }
        }

        public string SiniestrosPreliquidacionForDisplay
        {
            get { return _siniestrosPreliquidacion.ToString("#,##0.00"); }
        }

        public decimal SiniestrosPagados
        {
            get { return _siniestrosPagados; }
            set { _siniestrosPagados = value;}
        }

        public string SiniestrosPagadosForDisplay
        {
            get { return _siniestrosPagados.ToString("#,##0.00"); }
        }

        public string EstadoSeguimiento
        {
            get { return _estadoSeguimiento; }
            set { _estadoSeguimiento = value; }
        }

        public string NombreAuditor
        {
            get { return _NombreAuditor; }
            set { _NombreAuditor = value; }
        }
        public int FileCount
        {
            set { _fileCount = value; }
            get { return _fileCount; }
        }
        public string FileCountForDisplay
        {
            get { return _fileCount.ToString("00"); }
        }

        public int RowNumber
        {
            get { return this._rowNumber; }
            set { this._rowNumber = value; }
        }

        public SiniestroForList() {}

        public SiniestroForList ( int siniestroId, DateTime fechaSiniestro, DateTime fechaDenuncia, 
            int totalOcupantes, int cantidadHeridos, int cantidadFallecidos, string lugar, string zona, 
            string sindicato, string numeroRoseta, string numeroPoliza, string operacionesId, string placa,
            string tipo, string sector, string carnetIdentidad, string nombreTitular, decimal siniestrosPreliquidacion, 
            decimal siniestrosPagados, string estadoSeguimiento, string nombreAuditor, int fileCount, int rowNumber )
        {
            this._SiniestroId = siniestroId;
            this._FechaSiniestro = fechaSiniestro;
            this._FechaDenuncia = fechaDenuncia;
            this._TotalOcupantes = totalOcupantes;
            this._CantidadHeridos = cantidadHeridos;
            this._CantidadFallecidos = cantidadFallecidos;
            this._Lugar = lugar;
            this._numeroRoseta = numeroRoseta;
            this._numeroPoliza = numeroPoliza;
            this._operacionesId = operacionesId;
            this._placa = placa;
            this._tipo = tipo;
            this._sector = sector;
            this._carnetIdentidad = carnetIdentidad;
            this._nombreTitular = nombreTitular;
            this._siniestrosPreliquidacion = siniestrosPreliquidacion;
            this._siniestrosPagados = siniestrosPagados;
            this._Zona = zona;
            this._Sindicato = sindicato;
            this._estadoSeguimiento = estadoSeguimiento;
            this._NombreAuditor = nombreAuditor;
            this._fileCount = fileCount;
            this._rowNumber = rowNumber;
        }
    }

    public class SiniestroSearch : ConfigColumns
    {
        public SiniestroSearch ()
        {
            Column col = new Column("NombreTitular", "NombreTitular", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("CITitular", "CITitular", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("Placa", "Placa", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("OperacionId", "NumeroSiniestro", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("NumeroRoseta", "NumeroRoseta", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("NumeroPoliza", "NumeroPoliza", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("LugarDpto", "Departamento", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("Sindicato", "Sindicato", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[SA].Nombre", "NombreAccidentado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("[SA].CarnetIdentidad", "CIAccidentado", Column.ColumnType.String);
            col.AppearInStandardSearch = true;
            col.DisplayHelp = true;
            this.Cols.Add(col);

            if (LoginSecurity.LoginSecurity.IsUserAuthorizedPermission("SOAT_ALLDPTOS"))
            {
                col = new Column("[U].[fullname]", "NombreAuditor", Column.ColumnType.String);
                col.AppearInStandardSearch = true;
                col.DisplayHelp = true;
                this.Cols.Add(col);
            }

            col = new Column("FechaSiniestro", "FechaSiniestro", Column.ColumnType.Date);
            col.DisplayHelp = true;
            this.Cols.Add(col);

            col = new Column("FechaDenuncia", "FechaDenuncia", Column.ColumnType.Date);
            col.DisplayHelp = true;
            this.Cols.Add(col);
        }
    }
}