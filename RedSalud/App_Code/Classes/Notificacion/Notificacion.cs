using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

/// <summary>
/// Descripción breve de Notificacion
/// </summary>
/// 
namespace Artexacta.App.Notificacion
{
	public class Notificacion
    {

		private int _Notificacionid;
		private string _Descripcion;
		private int _Tiponotificacionid;
		private string _GrupoNotificacion;
		private DateTime _FechaCreacion;
		private string _Fecha_Creacion;
		private DateTime _FechaActualizacion;
		private DateTime _FechaStart;
		private DateTime _FechaEnd;
		private string _Estado;
		private int _PrioridadElemento;
		private string _Titulo;
		private int _fileid;

		private string _Tiponotificacion;

        public Notificacion()
        {
            // TODO: Agregar aquí la lógica del constructor
        }

		public int Notificacionid
		{
			get { return this._Notificacionid; }
			set { this._Notificacionid = value; }
		}
		public string Descripcion 
		{
			get { return this._Descripcion;}
			set { this._Descripcion = value;}
		}
		public int Tiponotificacionid
		{
			get { return this._Tiponotificacionid; }
			set { this._Tiponotificacionid = value; }
		}
		public string GrupoNotificacion
        {
			get { return this._GrupoNotificacion; }
			set { this._GrupoNotificacion = value; }
		}

		public DateTime FechaCreacion
		{
			get { return this._FechaCreacion; }
			set { this._FechaCreacion = value; }
		}

		public string Fecha_Creacion
		{
			get { return this._Fecha_Creacion; }
			set { this._Fecha_Creacion = value; }
		}
		public DateTime FechaActualizacion
        {
			get { return this._FechaActualizacion; }
			set { this._FechaActualizacion = value; }
		}
		public DateTime FechaStart
        {
			get { return this._FechaStart; }
			set { this._FechaStart = value;}
        }

		public DateTime FechaEnd
        {
			get { return this._FechaEnd; }
			set { this._FechaEnd = value;}
        }

		public string Estado
        {
			get { return this._Estado; }
			set { this._Estado = value; }
			
        }

		public int PrioridadElemento 
		{
			get { return this._PrioridadElemento; }
			set { this._PrioridadElemento = value; }
		}

		public string Titulo
        {
			get { return this._Titulo; }
			set { this._Titulo = value;}
        }
		public int fileid
        {
			get { return this._fileid; }
			set { this._fileid = value; }
        }

		public string Tiponotificacion 
		{
			get { return this._Tiponotificacion; }
			set { this._Tiponotificacion = value; }
		}

		// Se utiliza para listar las notificaciones
		public Notificacion(int Notificacionid, string Tiponotificacion, string Descripcion,
			string GrupoNotificacion,string Fecha_Creacion, DateTime FechaStart,
			DateTime FechaEnd,string Estado, int PrioridadElemento)
		{	
			this._Notificacionid = Notificacionid;
			this._Tiponotificacion = Tiponotificacion;
			this._Descripcion= Descripcion;
			this._GrupoNotificacion= GrupoNotificacion;
			this._Fecha_Creacion= Fecha_Creacion;
			this._FechaStart=FechaStart;
			this._FechaEnd=FechaEnd;
			this._Estado=Estado;
			this._PrioridadElemento= PrioridadElemento;
		}





	}
}