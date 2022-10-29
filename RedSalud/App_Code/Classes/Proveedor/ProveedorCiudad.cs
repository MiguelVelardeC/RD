using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Proveedor
{
    public class ProveedorCiudad
    {
        private int _proveedorId;
        private string _ciudadId;
        private string _nombreCiudad;
        private string _direccion;
        private string _telefono;
        private string _celular;

        public ProveedorCiudad ()
        {
        }

        public ProveedorCiudad ( int proveedorId, string ciudadId, string nombreCiudad, string direccion, string telefono, string celular )
        {
            this._proveedorId = proveedorId;
            this._ciudadId = ciudadId;
            this._nombreCiudad = nombreCiudad;
            this._direccion = direccion;
            this._telefono = telefono;
            this._celular = celular;

        }

        public int ProveedorId
        {
            get { return _proveedorId; }
            set { _proveedorId = value; }
        }

        public string CiudadId
        {
            get { return _ciudadId; }
            set { _ciudadId = value; }
        }

        public string NombreCiudad
        {
            get { return _nombreCiudad; }
            set { _nombreCiudad = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public string Celular
        {
            get { return _celular; }
            set { _celular = value; }
        }
    }
}
