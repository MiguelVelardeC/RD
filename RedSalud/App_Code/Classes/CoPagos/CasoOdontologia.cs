using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CasoOdontologia
/// </summary>
namespace Artexacta.App.CoPagos
{
    public class CasoOdontologia
    {
        #region "Atributos"
        //para odontologia
        private int _PrestacionOdontologicaId;
        //esto es general para todos los casos
        private int _detId;
        private int _CasoId;
        private int _ProveedorId;
        private decimal _detPrecio;
        private decimal _detCoPagoMonto;
        private decimal _detCoPagoPorcentaje;
        private string _detFechaCoPagoPagado;
        private DateTime _Fecha;


        //esto es para mostrar la boleta
        private string _NombrePaciente;
        private string _NombreProveedor;
        private string _NombreMedico;
        private string _NombreEspecilidad;
        private string _CarnetIdentidad;
        private string _Solicito;
        private string _CodigoCaso;
        private string _NombrePoliza;
        private string _NumeroPoliza;
        private string _Observacion;
        private string _Diagnostico;
        private decimal _detCoPagoRefencial;
        private decimal _detMontoAPagar;
        private string _detCoPagoReferencialTipo;
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
        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
            set { this._NombreProveedor = value.ToUpper(); }
        }
        public DateTime Fecha
        {
            get { return this._Fecha; }
            set { this._Fecha = value; }
        }
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }

        }
        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }

        }
        public decimal detPrecio
        {
            get { return this._detPrecio; }
            set { this._detPrecio = value; }
        }
        public decimal detCoPagoMonto
        {
            get { return this._detCoPagoMonto; }
            set { this._detCoPagoMonto = value; }
        }
        public decimal detCoPagoPorcentaje
        {
            get { return this._detCoPagoPorcentaje; }
            set { this._detCoPagoPorcentaje = value; }
        }

        public string detFechaCoPagoPagado
        {
            get { return this._detFechaCoPagoPagado; }
            set { this._detFechaCoPagoPagado = value.ToUpper(); }

        }

        //Caso Odontologia
        public int PrestacionOdontologicaId
        {
            get { return this._PrestacionOdontologicaId; }
            set { this._PrestacionOdontologicaId = value; }

        }
        public string NombreMedico
        {
            get { return this._NombreMedico; }
            set { this._NombreMedico = value.ToUpper(); }
        }

        public string NombreEspecilidad
        {
            get { return this._NombreEspecilidad; }
            set { this._NombreEspecilidad = value.ToUpper(); }
        }
        public string CarnetIdentidad
        {
            get { return this._CarnetIdentidad; }
            set { this._CarnetIdentidad = value.ToUpper(); }
        }
        public string Solicito
        {
            get { return this._Solicito; }
            set { this._Solicito = value.ToUpper(); }
        }
        public string CodigoCaso
        {
            get { return this._CodigoCaso; }
            set { this._CodigoCaso = value.ToUpper(); }

        }
        public string NombrePoliza
        {
            get { return this._NombrePoliza; }
            set { this._NombrePoliza = value.ToUpper(); }

        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value.ToUpper(); }
        }
        public string Diagnostico
        {
            get { return this._Diagnostico; }
            set { this._Diagnostico = value.ToUpper(); }
        }
        public string Observacion
        {
            get { return this._Observacion; }
            set { this._Observacion = value.ToUpper(); }
        }
        public decimal detCoPagoReferencial
        {
            get { return this._detCoPagoRefencial; }
            set { this._detCoPagoRefencial = value; }
        }
        public decimal detMontoAPagar
        {
            get { return this._detMontoAPagar; }
            set { this._detMontoAPagar = value; }
        }
        public string detCoPagoReferencialTipo
        {
            get { return this._detCoPagoReferencialTipo; }
            set { this._detCoPagoReferencialTipo = value; }

        }
        #endregion
        #region "Insertar Caso Odontologia"

        public CasoOdontologia()
        {

        }
        public CasoOdontologia(int CasoId, int PrestacionOdontologicaId, int ProveedorId, decimal detPrecio
            , decimal detCoPagoMonto, decimal detCoPagoPorcentaje, DateTime detFecha)
        {
            this._CasoId = CasoId;
            this._PrestacionOdontologicaId = PrestacionOdontologicaId;
            this._ProveedorId = ProveedorId;
            this._detPrecio = detPrecio;
            this._detCoPagoMonto = detCoPagoMonto;
            this._detCoPagoPorcentaje = detCoPagoPorcentaje;
            this._Fecha = detFecha;
        }
        #endregion


        #region "Para Mostrar los Tipos Casos Odontologia"
        public CasoOdontologia(int detId, int CasoId, int PrestacionOdontologicaId
                            , decimal detPrecio
                            , string detFechaCoPagoPagado
                            , DateTime fecha, string NombrePaciente, string CedulaIdentidad, string NumeroPoliza
                            , string NombrePoliza, string NombreMedico, string NombreProveedor, string Solicito
                            , string NombreEspecialidad, string CodigoCaso, string Diagnostico, string Observacion
                            , decimal detMontoAPagar, decimal detCoPagoReferencial, string detCopagoReferencialTipo)
        {
            this._detId = detId;
            this._CasoId = CasoId;
            this._PrestacionOdontologicaId = PrestacionOdontologicaId;
            this._detPrecio = detPrecio;
            this._detFechaCoPagoPagado = detFechaCoPagoPagado;
            this._Fecha = fecha;
            this._NombrePaciente = NombrePaciente;
            this._CarnetIdentidad = CedulaIdentidad;
            this._NumeroPoliza = NumeroPoliza;
            this._NombrePoliza = NombrePoliza;
            this._NombreMedico = NombreMedico;
            this._NombreProveedor = NombreProveedor;
            this._Solicito = Solicito;
            this._NombreEspecilidad = NombreEspecialidad;
            this._CodigoCaso = CodigoCaso;
            this._Diagnostico = Diagnostico;
            this._Observacion = Observacion;
            this._detMontoAPagar = detMontoAPagar;
            this._detCoPagoRefencial = detCoPagoReferencial;
            this._detCoPagoReferencialTipo = detCopagoReferencialTipo;
        }
        #endregion

    }
}