using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Documents;
using Artexacta.App.CasoEmergecia.BLL;
using log4net;
namespace Artexacta.App.Emergencia
{
    /// <summary>
    /// Summary description for Emergencia
    /// </summary>
    public class Emergencia
    {
        private static readonly ILog log = LogManager.GetLogger("Standard");
        private int _EmergenciaId;
        private int _CasoId;
        private int _ProveedorId;
        private string _Observaciones;

        private int _GastoId;
        
        private string _NombreProveedor;

        //Gastos
        private decimal _MontoConFactura;
        private decimal _MontoSinFactura;
        private decimal _RetencionImpuesto;
        private decimal _Total;
        private int _fileCount;


        //Para saber la fecha creada de la Emergencia
        private DateTime _detFecha;
        #region Estudio

        public int EmergenciaId
        {
            get { return this._EmergenciaId; }
            set { this._EmergenciaId = value; }
        }
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }
        }
        
        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value; }
        }
        public int GastoId
        {
            get { return this._GastoId; }
            set { this._GastoId = value; }
        }
        
        #endregion

        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
        }
        public DateTime detFecha
        {
            set { this._detFecha = value; }
            get { return this._detFecha; }
        }
        public string NITProveedor
        {
            get
            {
                Proveedor.Proveedor obj = Proveedor.BLL.ProveedorBLL.getProveedorByID(_ProveedorId);
                if (obj != null)
                    return obj.Nit;
                else
                    return "-";
            }
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

        public Emergencia()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Emergencia(int EmergenciaId, int CasoId
            , int ProveedorId, string Observaciones
            , int GastoId
            ,string NombreProveedor
            
            , decimal MontoConFactura, decimal MontoSinFactura
            , decimal RetencionImpuesto, decimal Total, int fileCount )
        {
            this._EmergenciaId = EmergenciaId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._Observaciones = Observaciones;
            this._GastoId = GastoId;

            this._NombreProveedor = NombreProveedor;

            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencionImpuesto;
            this._Total = Total;
            this._fileCount = fileCount;
        }

        public Emergencia(int EmergenciaId, int CasoId
          , int ProveedorId, string Observaciones
          , int GastoId
          , string NombreProveedor
            ,DateTime detfecha
          , decimal MontoConFactura, decimal MontoSinFactura
          , decimal RetencionImpuesto, decimal Total, int fileCount
            )
        {
            this._EmergenciaId = EmergenciaId;
            this._CasoId = CasoId;
            this._ProveedorId = ProveedorId;
            this._Observaciones = Observaciones;
            this._GastoId = GastoId;

            this._NombreProveedor = NombreProveedor;
            this._detFecha = detfecha;
            this._MontoConFactura = MontoConFactura;
            this._MontoSinFactura = MontoSinFactura;
            this._RetencionImpuesto = RetencionImpuesto;
            this._Total = Total;
            this._fileCount = fileCount;
        }
        public List<DocumentFile> LaboratorioFiles
        {
            get
            {
                if (EmergenciaId <= 0)
                    return new List<DocumentFile>();
                List<DocumentFile> list = null;
                try
                {
                    list = CasoEmergenciaBLL.GetEmergenciaFiles(EmergenciaId);
                }
                catch (Exception ex)
                {
                    log.Error("Cannot get List of Files for CitaDesgravamen", ex);
                    list = new List<DocumentFile>();
                }
                return list;
            }
        }

    }
}