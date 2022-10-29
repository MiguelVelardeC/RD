
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.CLAMedicamento
{
    public class Medicamento
    {
        private int _medicamentoId;
        private string _nombre;

        public Medicamento () { }

        public Medicamento ( int medicamentoId, string nombre )
        {
            this._medicamentoId = medicamentoId;
            this._nombre = nombre;
        }

        public int MedicamentoId
        {
            get { return _medicamentoId; }
            set { _medicamentoId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}