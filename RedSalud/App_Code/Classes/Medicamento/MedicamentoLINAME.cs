using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.MedicamentoLINAME
{
    public class MedicamentoLINAME 
    {
        private int _medicamentoLINAMEId;
		private int _medicamentoGrupoId;
		private int _medicamentoSubgrupoId;
		private string _nombre;
		private string _Grupo;
		private string _Subgrupo;

        public int MedicamentoLINAMEId {
			get { return _medicamentoLINAMEId; }
			set { _medicamentoLINAMEId = value; }
		}

		public int MedicamentoGrupoId {
			get { return _medicamentoGrupoId; }
			set { _medicamentoGrupoId = value; }
		}

		public int MedicamentoSubgrupoId {
			get { return _medicamentoSubgrupoId; }
			set { _medicamentoSubgrupoId = value; }
		}

		public string Nombre {
			get { return _nombre; }
			set { _nombre = value; }
		}

		public string Grupo {
			get { return _Grupo; }
			set { _Grupo = value; }
		}

		public string Subgrupo {
			get { return _Subgrupo; }
			set { _Subgrupo = value; }
		}

        public MedicamentoLINAME () { }

        public MedicamentoLINAME ( int medicamentoLINAMEId, int medicamentoGrupoId, int medicamentoSubgrupoId, 
            string nombre, string grupo, string subgrupo )
        {
            this._medicamentoLINAMEId = medicamentoLINAMEId;
            this._medicamentoGrupoId = medicamentoGrupoId;
            this._medicamentoSubgrupoId = medicamentoSubgrupoId;
            this._nombre = nombre;
            this._Grupo = grupo;
            this._Subgrupo = subgrupo;
        }
    }
}