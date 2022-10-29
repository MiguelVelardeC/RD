using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Medicamento
{
    public class Medicamento : IFechaCreacion
    {
        private int _MedicamentoId;
        private int _CasoId;
        private int _MedicamentoCLAId;
        private string _MedicamentoNombre;
        private string _TipoMedicamentoId;
        private string _Indicaciones;
        private DateTime _FechaCreacion;
        private int _GastoId;
        private string _TipoMedicamentoNombre;

        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;

        public int MedicamentoId
        {
            get { return this._MedicamentoId; }
            set { this._MedicamentoId = value; }
        }
        public int CasoId {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }

        public int MedicamentoCLAId
        {
            get { return this._MedicamentoCLAId; }
            set { this._MedicamentoCLAId = value; }
        }
        public string MedicamentoNombre
        {
            get { return this._MedicamentoNombre; }
        }
        public string TipoMedicamentoId {
            get { return this._TipoMedicamentoId; }
            set { this._TipoMedicamentoId = value; }
        }
        public string TipoMedicamentoNombre {
            get { return this._TipoMedicamentoNombre; }
        }
        public string Indicaciones {
            get { return this._Indicaciones; }
            set { this._Indicaciones = value; }
        }
        DateTime IFechaCreacion.FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public string FechaCreacionString {
            get { return this._FechaCreacion.ToString(); }
        }
        public int GastoId {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }

        #region Gastos
        public decimal MontoConFactura
        {
            get { return this._MontoConFactura; }
        }
        public decimal MontoSinFactura
        {
            get { return this._MontoSinFactura; }
        }
        public decimal RetencionImpuesto
        {
            get { return this._RetencionImpuesto; }
        }
        public decimal Total
        {
            get { return this._Total; }
        }
        public string MontoConFacturaForDisplay
        {
            get { return this._MontoConFactura.ToString("#,##0.00 Bs"); }
        }
        public string MontoSinFacturaForDisplay
        {
            get { return this._MontoSinFactura.ToString("#,##0.00 Bs"); }
        }
        public string RetencionImpuestoForDisplay
        {
            get { return this._RetencionImpuesto.ToString("#,##0.00 Bs"); }
        }
        public string TotalForDisplay
        {
            get { return this._Total.ToString("#,##0.00 Bs"); }
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
        #endregion

        public Medicamento() {}

        public Medicamento ( int MedicamentoId, int CasoId, int MedicamentoCLAId, string Medicamento
            , string TipoMedicamentoId, string TipoMedicamentoNombre, string Indicaciones
            , DateTime FechaCreacion, int GastoId
            
            ,decimal MontoConFactura,decimal MontoSinFactura
            ,decimal RetencioImpuesto, decimal Total, int fileCount)
        {
            this._MedicamentoId = MedicamentoId;
            this._CasoId = CasoId;
            this._MedicamentoCLAId = MedicamentoCLAId;
            this._MedicamentoNombre = Medicamento;
            this._TipoMedicamentoId = TipoMedicamentoId;
            this._Indicaciones = Indicaciones;
            this._FechaCreacion = FechaCreacion;
            this._GastoId = GastoId;

            this._TipoMedicamentoNombre = TipoMedicamentoNombre;

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencioImpuesto;
            this._Total = Total;
            this._fileCount = fileCount;
        }
    }
}