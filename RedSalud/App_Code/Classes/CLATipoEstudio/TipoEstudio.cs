using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.TipoEstudio
{
    /// <summary>
    /// Summary description for TipoEstudio
    /// </summary>
    public class TipoEstudio
    {
        private int _TipoEstudioId;
        private string _Nombre;
        private int _PadreId;
        private List<TipoEstudio> _Childrens;

        public int TipoEstudioId
        {
            get { return this._TipoEstudioId; }
            set { this._TipoEstudioId = value; }
        }
        public int PadreId
        {
            get { return this._PadreId; }
            set { this._PadreId = value; }
        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }

        private int _cantHijos;

        public int CantHijos
        {
            get { return _cantHijos; }
            set { _cantHijos = value; }
        }

        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_TipoEstudioId, this._Nombre); }
        }

        public TipoEstudio()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public TipoEstudio(int TipoEstudioId, string Nombre, int PadreId, int cantHijos)
        {
            this._TipoEstudioId = TipoEstudioId;
            this._Nombre = Nombre;
            this._PadreId = PadreId;
            _cantHijos = cantHijos;
        }

        public List<TipoEstudio> Childrens
        {
            get
            {
                if (_Childrens == null)
                {
                    _Childrens = new List<TipoEstudio>();
                }
                try
                {
                    _Childrens = Artexacta.App.TipoEstudio.BLL.TipoEstudioBLL.GetAllTipoEstudioChildrensList(this._TipoEstudioId);
                }
                catch { }
                return _Childrens;
            }
        }
        public List<TipoEstudio> ChildrensNew(int Proveedor,int TipoEstudioHijo)
        {
           
            {
                if (_Childrens == null)
                {
                    _Childrens = new List<TipoEstudio>();
                }
                try
                {
                    _Childrens = Artexacta.App.ProveedorPrestaciones.BLL.TiposEstudiosProvPrestacionesBLL.GetAllTipoEstudioChildrensList(Proveedor,TipoEstudioHijo);
                }
                catch { }
                return _Childrens;
            }
        }

        public TipoEstudio getChildren(int TipoEstudioId)
        {
            if (_Childrens == null)
            {
                return null;
            }
            return _Childrens[TipoEstudioId];
        }
    }
}