using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.Medico
{
    public class MedicoCliente
    {
        private int _medicoId;
        private int _userId;
        private int _clienteId;
        private string _nombre;
        private string _nombreCliente;
        private string _nit;
        private string _codigoCliente;

        public MedicoCliente ()
        {
        }

        public MedicoCliente ( int medicoId, int clienteId, int userId, string nombre, string nombreCliente, string nit, string codigoCliente )
        {
            this._medicoId = medicoId;
            this._userId = userId;
            this._clienteId = clienteId;
            this._nombre = nombre;
            this._nombreCliente = nombreCliente;
            this._nit = nit;
            this._codigoCliente = codigoCliente;
        }

        public int MedicoId
        {
            get { return _medicoId; }
            set { _medicoId = value; }
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public int ClienteId
        {
            get { return _clienteId; }
            set { _clienteId = value; }
        }

        public string NombreCliente
        {
            get { return _nombreCliente; }
            set { _nombreCliente = value; }
        }

        public string NIT
        {
            get { return _nit; }
            set { _nit = value; }
        }

        public string CodigoCliente
        {
            get { return _codigoCliente; }
            set { _codigoCliente = value; }
        }

        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_medicoId, _nombre); }
        }
    }
}