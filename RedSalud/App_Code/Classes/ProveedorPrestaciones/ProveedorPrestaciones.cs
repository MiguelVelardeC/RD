using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.ProveedorPrestaciones
{
    /// <summary>
    /// Summary description for RedClientePrestaciones
    /// </summary>

    public class TiposEstudiosProvPrestaciones
    {
        private string _id;
        private string _nombre;

        #region Propiedades
        public string id
        {
            get { return this._id; }
            set { this._id = value; }
        }
        public string nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }

        #endregion

        public TiposEstudiosProvPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public TiposEstudiosProvPrestaciones(string id, string nombre)
        {
            this._id = id;
            this._nombre = nombre;
        }
    }

    public class RedProvLabImgCarPrestaciones
    {
        private int _EstudioId;
        private string _Estudio;
        private string _CategoriaId;

        #region Propiedades
        public int EstudioId
        {
            get { return this._EstudioId; }
            set { this._EstudioId = value; }
        }
        public string Estudio
        {
            get { return this._Estudio; }
            set { this._Estudio = value; }
        }
        public string CategoriaId
        {
            get { return this._CategoriaId; }
            set { this._CategoriaId = value; }
        }

        #endregion

        public RedProvLabImgCarPrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedProvLabImgCarPrestaciones(int EstudioId, string Estudio, string CategoriaId)
        {
            this._EstudioId = EstudioId;
            this._Estudio = Estudio;
            this._CategoriaId = CategoriaId;
        }
    }

    public class RedProvLabImgCarDetallePrestaciones
    {
        private int _detId;
        private int _EstudioId;
        private string _Estudio;
        private int _ParentId;
        private string _CategoriaId;
        private decimal _detPrecio;

        #region Propiedades
        public int detId
        {
            get { return this._detId; }
            set { this._detId = value; }
        }
        public int EstudioId
        {
            get { return this._EstudioId; }
            set { this._EstudioId = value; }
        }
        public string Estudio
        {
            get { return this._Estudio; }
            set { this._Estudio = value; }
        }
        public int ParentId
        {
            get { return this._ParentId; }
            set { this._ParentId = value; }
        }
        public string CategoriaId
        {
            get { return this._CategoriaId; }
            set { this._CategoriaId = value; }
        }
        public decimal detPrecio
        {
            get { return this._detPrecio; }
            set { this._detPrecio = value; }
        }

        #endregion

        public RedProvLabImgCarDetallePrestaciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public RedProvLabImgCarDetallePrestaciones(int detId, int EstudioId, string Estudio, int ParentId, string CategoriaId, decimal detPrecio)
        {
            this._detId = detId;
            this._EstudioId = EstudioId;
            this._Estudio = Estudio;
            this._ParentId = ParentId;
            this._CategoriaId = CategoriaId;
            this._detPrecio = detPrecio;
        }

        //Para Mostrar Solo La Tabla Red PRoveedor PRestaciones
        public RedProvLabImgCarDetallePrestaciones(int detId, int EstudioId, int ParentId, string CategoriaId, decimal detPrecio)
        {
            this._detId = detId;
            this._EstudioId = EstudioId;
            this._ParentId = ParentId;
            this._CategoriaId = CategoriaId;
            this._detPrecio = detPrecio;
        }
    }
    public class TiposEstudiosProvPrestacionesCategoria
    {
        private int _ProveedorId;
        private string _nombre;
        private string _categoria;

        #region Propiedades
        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }
        }
        public string nombre
        {
            get { return this._nombre; }
            set { this._nombre = value; }
        }
        public string categoria
        {
            get { return this._categoria; }
            set { this._categoria = value; }
        }
        #endregion

        public TiposEstudiosProvPrestacionesCategoria()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public TiposEstudiosProvPrestacionesCategoria(int ProveedorId, string nombre,string categoria)
        {
            this._ProveedorId = ProveedorId;
            this._nombre = nombre;
            this._categoria = categoria;
        }
    }
}