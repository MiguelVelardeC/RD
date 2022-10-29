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
    public class SiniestralidadClienteDetail
    {
        #region "ATRIBUTOS"
        private string _Prestacion;
        private decimal _MontoTope;
        private decimal _MontoAcumulado;
        private decimal _MontoCoPago;
        private int _ConsultasAno;
        private int _ConsultasAcumuladas;                
        #endregion

        #region "METODOS"
        public string Prestacion
        {
            get { return this._Prestacion; }
            set { this._Prestacion = value; }
        }
        public decimal MontoTope
        {
            get { return this._MontoTope; }
            set { this._MontoTope = value; }
        }
        public decimal MontoAcumulado
        {
            get { return this._MontoAcumulado; }
            set { this._MontoAcumulado = value; }

        }
        public decimal MontoCoPago
        {
            get { return this._MontoCoPago; }
            set { this._MontoCoPago = value; }

        }
        public int ConsultasAno
        {
            get { return this._ConsultasAno; }
            set { this._ConsultasAno = value; }

        }
        public int ConsultasAcumuladas
        {
            get { return this._ConsultasAcumuladas; }
            set { this._ConsultasAcumuladas = value; }

        }
        #endregion

        public SiniestralidadClienteDetail()
        {
        }

        public SiniestralidadClienteDetail(string NombrePrestacion, decimal MontoTope, decimal MontoAcumulado,
                                    decimal MontoCoPago, int ConsultasPorAnos,
                                    int ConsultasAcumuladas)
        {
            this._Prestacion = NombrePrestacion;
            this._MontoTope = MontoTope;            
            this._MontoAcumulado = MontoAcumulado;
            this._MontoCoPago = MontoCoPago;
            this._ConsultasAno = ConsultasPorAnos;
            this._ConsultasAcumuladas = ConsultasAcumuladas;
        }
    }
}