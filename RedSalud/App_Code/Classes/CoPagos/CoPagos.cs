using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Prestamos
/// </summary>
namespace Artexacta.App.CoPagos
{
    public class CoPagos
    {
        #region "Variables"
        //Esto Es para Mostrar en la Lista
        private int _CasoId;
        private string _NombrePaciente;
        private string _NombreCliente;
        private string _NombreProveedor;
        private string _NombreTipoProveedor;
        private DateTime _FechaCita;
        private string _TipoCaso;
        private int _detId;
        private string _Estado;
        private decimal _MontoTotal;
        private decimal _MontoAPagar;
        private string _ValorCoPago;
        private string _NumeroPoliza;
        private string _CarnetIdentidad;
        private string _CodigoCaso;
        private string _NombreCiudad;

        #endregion

        #region "Metodos"
        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
            set { this._NombrePaciente = value.ToUpper(); }
        }
        public string FechaHoraForDisplay
        {
            get
            {
                return String.Format("{0:dd/MMM/yyyy  HH:mm:ss}", FechaCita);
            }
        }
        public string NombreCliente
        {
            get { return this._NombreCliente; }
            set { this._NombreCliente = value.ToUpper(); }
        }
        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
            set { this._NombreProveedor = value.ToUpper(); }
        }
        public string NombreTipoProveedor
        {
            get { return this._NombreTipoProveedor; }
            set { this._NombreTipoProveedor = value.ToUpper(); }
        }
        public decimal MontoTotal
        {
            get { return this._MontoTotal; }
            set { this._MontoTotal = value; }
        }
        public decimal MontoAPagar
        {
            get { return this._MontoAPagar; }
            set { this._MontoAPagar = value; }
        }
        public string ValorCoPago
        {
            get { return this._ValorCoPago; }
            set { this._ValorCoPago = value; }
        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value; }
        }
        public string CarnetIdentidad
        {
            get { return this._CarnetIdentidad; }
            set { this._CarnetIdentidad = value; }
        }
        public string CodigoCaso
        {
            get { return this._CodigoCaso; }
            set { this._CodigoCaso = value; }
        }
        public string NombreCiudad
        {
            get { return this._NombreCiudad; }
            set { this._NombreCiudad = value; }
        }

        public DateTime FechaCita
        {
            get { return this._FechaCita; }
            set { this._FechaCita = value; }
        }
        public string TipoCaso
        {
            get { return this._TipoCaso; }
            set { this._TipoCaso = value.ToUpper(); }
        }
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }

        }
        public string Estado
        {
            get { return this._Estado; }
            set { this._Estado=value; }
        }
        #endregion

        public CoPagos()
        {

        }
        #region "Para Listar Todos los Tipos Casos Odontologia, Laboratorio, Especialidad"
        public CoPagos(string NombrePaciente, string NombreCliente, string NombreProveedor, string NombreTipoProveedor,DateTime FechaCita,string TipoCaso,int detId, string Estado)
        {
          
            this._NombrePaciente = NombrePaciente;
            this._NombreCliente = NombreCliente;
            this._NombreProveedor = NombreProveedor;
            this._NombreTipoProveedor = NombreTipoProveedor;
            this._FechaCita = FechaCita;
            this._TipoCaso = TipoCaso;
            this._detId = detId;
            this._Estado = Estado;
        }
        public CoPagos(string NombrePaciente, string NombreCliente, string NombreProveedor, string NombreTipoProveedor
         ,DateTime FechaCita,string TipoCaso, int detId, string Estado, decimal MontoTotal,decimal MontoAPagar, string ValorCoPago)       
        {

            this._NombrePaciente = NombrePaciente;
            this._NombreCliente = NombreCliente;
            this._NombreProveedor = NombreProveedor;
            this._NombreTipoProveedor = NombreTipoProveedor;
            this._FechaCita = FechaCita;
            this._TipoCaso = TipoCaso;
            this._detId = detId;
            this._Estado = Estado;
            this._MontoTotal = MontoTotal;
            this._MontoAPagar = MontoAPagar;
            this._ValorCoPago = ValorCoPago;       

        }

        public CoPagos(string NombrePaciente, string NombreCliente, string NombreProveedor, string NombreTipoProveedor
        , DateTime FechaCita, string TipoCaso, int detId, string Estado, decimal MontoTotal, decimal MontoAPagar, string ValorCoPago
        , string NumeroPoliza, string CarnetIdentidad,string CodigoCaso, string NombreCiudad )
        {

            this._NombrePaciente = NombrePaciente;
            this._NombreCliente = NombreCliente;
            this._NombreProveedor = NombreProveedor;
            this._NombreTipoProveedor = NombreTipoProveedor;
            this._FechaCita = FechaCita;
            this._TipoCaso = TipoCaso;
            this._detId = detId;
            this._Estado = Estado;
            this._MontoTotal = MontoTotal;
            this._MontoAPagar = MontoAPagar;
            this._ValorCoPago = ValorCoPago;
            this._NumeroPoliza = NumeroPoliza;
            this._CarnetIdentidad=CarnetIdentidad;
            this._CodigoCaso=CodigoCaso;
            this._NombreCiudad=NombreCiudad;

    }
        #endregion





    }
}