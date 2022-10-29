using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.TipoMedicamento
{
    /// <summary>
    /// Summary description for TipoMedicamento
    /// </summary>
    public class TipoMedicamento
    {
        private string _TipoMedicamentoId;
        private string _Nombre;

        public string TipoMedicamentoId {
            get { return this._TipoMedicamentoId; }
            set { this._TipoMedicamentoId = value; }
        }
        public string Nombre {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }

        public TipoMedicamento()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public TipoMedicamento(string TipoMedicamentoId, string Nombre)
        {
            this._TipoMedicamentoId = TipoMedicamentoId;
            this._Nombre = Nombre;
        }
    }
}