using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SiniestralidadSearchPolizaDetail
/// </summary>
/// 
namespace Artexacta.App.Siniestralidad
{
    public class SiniestralidadSearchPolizaDetail
    {
        #region "Atributos"
        private string _NombrePaciente;
        private string _CedulaIdentidad;
        private string _NumeroPoliza;
        private string _NombrePoliza;

        #endregion
        #region "Metodos"

        public string NombrePaciente
        {
            get { return this._NombrePaciente; }
            set { this._NombrePaciente= value.ToUpper(); }

        }
        public string CedulaIdentidad
        {
            get { return this._CedulaIdentidad; }
            set { this._CedulaIdentidad = value.ToUpper(); }

        }
        public string NumeroPoliza
        {
            get { return this._NumeroPoliza; }
            set { this._NumeroPoliza = value.ToUpper(); }

        }
        public string NombrePoliza
        {
            get { return this._NombrePoliza; }
            set { this._NombrePoliza = value.ToUpper(); }

        }

        #endregion

        public SiniestralidadSearchPolizaDetail()
        {
            //
            // TODO: Add constructor logic here
            //
            
        }
        public SiniestralidadSearchPolizaDetail(string NombrePaciente,string CedulaIdentidad,string NumeroPoliza,string NombrePoliza )
        {
            this._NombrePaciente = NombrePaciente;
            this._CedulaIdentidad = CedulaIdentidad;
            this._NumeroPoliza = NumeroPoliza;
            this._NombrePoliza = NombrePoliza;

        }
    }
}