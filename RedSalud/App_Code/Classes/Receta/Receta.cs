using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Receta
{
    /// <summary>
    /// Summary description for Receta
    /// </summary>
    public class Receta : IFechaCreacion
    {
        private int _DetalleId;
        private int _CasoId;
        private string _Medicamento;
        private int _MedicamentoClaId;
        private string _Grupo;
        private string _Subgrupo;
        private string _TipoMedicamentoId;
        private string _TipoMedicamentoNombre;
        private int _TipoConcentracionId;
        private string _TipoConcentracionNombre;
        private string _Indicaciones;
        private DateTime _FechaCreacion;
        private int _GastoId;
        private bool _UnPrincipioActivo;

        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;
        public int RowNumber;

        public int DetalleId
        {
            get { return this._DetalleId; }
            set { this._DetalleId = value; }
        }
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public string Medicamento
        {
            get { return this._Medicamento; }
            set { this._Medicamento = value; }
        }
        public int MedicamentoClaId
        {
            get { return this._MedicamentoClaId; }
            set { this._MedicamentoClaId = value; }
        }
        public string Grupo
        {
            get { return this._Grupo; }
        }
        public string Subgrupo
        {
            get { return this._Subgrupo; }
        }
        public string TipoMedicamentoId
        {
            get { return this._TipoMedicamentoId; }
            set { this._TipoMedicamentoId = value; }
        }
        public string TipoMedicamentoNombre
        {
            set { this._TipoMedicamentoNombre = value; }
            get { return this._TipoMedicamentoNombre; }
        }
        public int TipoConcentracionId
        {
            get { return this._TipoConcentracionId; }
            set { this._TipoConcentracionId = value; }
        }
        public string TipoConcentracionNombre
        {
            set { this._TipoConcentracionNombre = value; }
            get { return this._TipoConcentracionNombre; }
        }
        public string Indicaciones
        {
            get { return this._Indicaciones; }
            set { this._Indicaciones = value; }
        }
        DateTime IFechaCreacion.FechaCreacion
        {
            get { return this._FechaCreacion; }
            set { this._FechaCreacion = value; }
        }
        public string FechaCreacionString
        {
            get { return this._FechaCreacion.ToString(); }
        }
        public int GastoId
        {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }
        public bool UnPrincipioActivo
        {
            get { return _UnPrincipioActivo; }
        }
        private int _proveedorId;

        public int ProveedorId
        {
            get { return _proveedorId; }
            set { _proveedorId = value; }
        }
        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
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

        public Receta()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Receta(int DetalleId, int CasoId, string Medicamento, int MedicamentoClaId
            , string grupo, string subgrupo, string TipoMedicamentoId, string TipoMedicamento
            , int TipoConcentracionId, string TipoConcentracion, string Indicaciones
            , DateTime FechaCreacion
            , bool UnPrincipioActivo, int GastoId
            , decimal MontoConFactura, decimal MontoSinFactura
            , decimal RetencioImpuesto, decimal Total, int proveedorId, int cantidad)
        {
            this._DetalleId = DetalleId;
            this._CasoId = CasoId;
            this._Medicamento = Medicamento.ToUpper();
            this._MedicamentoClaId = MedicamentoClaId;
            this._TipoMedicamentoId = TipoMedicamentoId;
            this._TipoMedicamentoNombre = TipoMedicamento.ToUpper();
            this._TipoConcentracionId = TipoConcentracionId;
            this._TipoConcentracionNombre = TipoConcentracion.ToUpper();
            this._Indicaciones = Indicaciones.ToUpper();
            this._FechaCreacion = FechaCreacion;

            this._UnPrincipioActivo = UnPrincipioActivo;
            this._GastoId = GastoId;

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencioImpuesto;
            this._Total = Total;
            _proveedorId = proveedorId;
            _cantidad = cantidad;
        }
    }
}