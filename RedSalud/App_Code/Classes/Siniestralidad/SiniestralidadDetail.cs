using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SiniestralidadDetail
/// </summary>
/// 
namespace Artexacta.App.Siniestralidad
{
    public class SiniestralidadDetail
    {
        #region "ATRIBUTOS"
        private string _NombrePrestacion;
        private decimal _MontoTope;
        private int _ConsultasPorAnos;
        private string _ValorCoPago;
        private decimal _MontoAcumulado;
        private decimal _CoPagoAcumulado;
        private int _ConsultasAcumuladas;
        #endregion

        #region "METODOS"
        public string NombrePrestacion
        {
            get { return this._NombrePrestacion; }
            set { this._NombrePrestacion = value; }
        }
        public decimal MontoTope
        {
            get { return this._MontoTope; }
            set { this._MontoTope = value; }
        }
        public int ConsultasPorAnos
        {
            get { return this._ConsultasPorAnos; }
            set { this._ConsultasPorAnos = value; }

        }
        public decimal MontoAcumulado
        {
            get { return this._MontoAcumulado; }
            set { this._MontoAcumulado = value; }

        }
        public decimal CoPagoAcumulado
        {
            get { return this._CoPagoAcumulado; }
            set { this._CoPagoAcumulado = value; }

        }
        public int ConsultasAcumuladas
        {
            get { return this._ConsultasAcumuladas; }
            set { this._ConsultasAcumuladas = value; }

        }
        public string ValorCoPago
        {
            get { return this._ValorCoPago; }
            set { this._ValorCoPago = value; }
        }
        #endregion

        public SiniestralidadDetail()
        {
        }

        public SiniestralidadDetail(string NombrePrestacion,decimal MontoTope, string ValorCoPago
                                    ,int ConsultasPorAnos,decimal MontoAcumulado,
                                    decimal CoPagoAcumulado,int ConsultasAcumuladas)
        {
            this._NombrePrestacion = NombrePrestacion;
            this._MontoTope = MontoTope;
            this._ValorCoPago = ValorCoPago;
            this._ConsultasPorAnos = ConsultasPorAnos;
            this._MontoAcumulado = MontoAcumulado;
            this._CoPagoAcumulado = CoPagoAcumulado;
            this._ConsultasAcumuladas = ConsultasAcumuladas;
        }
        public SiniestralidadDetail(string NombrePrestacion, decimal MontoTope
                                   , int ConsultasPorAnos, decimal MontoAcumulado,
                                   decimal CoPagoAcumulado, int ConsultasAcumuladas)
        {
            this._NombrePrestacion = NombrePrestacion;
            this._MontoTope = MontoTope;
            this._ConsultasPorAnos = ConsultasPorAnos;
            this._MontoAcumulado = MontoAcumulado;
            this._CoPagoAcumulado = CoPagoAcumulado;
            this._ConsultasAcumuladas = ConsultasAcumuladas;
        }
    }
}