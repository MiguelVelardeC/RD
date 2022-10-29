using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;

namespace Artexacta.App.Proveedor
{
    /// <summary>
    /// Summary description for Proveedor
    /// </summary>
    public class Proveedor
    {
        //Cliente
        private int _RedMedicaId;
        private string _RedMedicaNombre;

        //especialista
        private int _EspecialistaId;
        private string _Sedes;
        private string _ColegioMedico;
        private decimal _costoConsulta;
        private decimal _porcentageDescuento;

        //Cla Especiliadad
        private int _EspecialidadId;
        private string _NombreEspecialidad;

        //Proveedor
        private int _ProveedorId;
        private string _Nombres;
        private string _Apellidos;
        private string _NombreJuridico;
        private string _Nit;

        private string _Direccion;
        private string _TelefonoCasa;
        private string _TelefonoOficina;
        private string _Celular;
        private string _Estado;

        private string _Observaciones;
        private DateTime _FechaActualizacion;
        private string _TipoProveedorId;
        private string _TipoProveedorNombre;

        //Proveedor Ciudad
        private string _CiudadID;
        private string _DireccionPCiudad;
        private string _TelefonoPCiudad;
        private string _CelularPCiudad;


        //Proveedor Medico
        private int _MedicoId;
        private string _NombreMedico;
        private string _NombreEspecialidadMedico;
        private string _CiudadMedico;
         
        //usuario 
        private int _UserId;
        private string _NombreUsuario; 
        #region "Proveedor Medico y User"
        public int MedicoId
        {

            get { return this._MedicoId; }
            set { this._MedicoId = value; }
        }
        public string NombreUsuario
        {
            get { return this._NombreUsuario; }
            set { this._NombreUsuario = value; }
        }
    

        public int UserId
        {
            get { return this._UserId; }
            set { this._UserId = value; }
        }
    

        #endregion
        #region RedMedica
        public int RedMedicaId
        {
            get { return this._RedMedicaId; }
            set { this._RedMedicaId = value; }
        }
        public string RedMedicaNombre
        {
            get { return this._RedMedicaNombre; }
        }
        #endregion

        #region Especialista
        public int EspecialistaId
        {
            get { return this._EspecialistaId; }
            set { this._EspecialistaId = value; }
        }
        public int EspeciliadadId
        {
            get { return this._EspecialidadId; }
            set { this._EspecialidadId = value; }
        }
        public string Sedes
        {
            get { return this._Sedes; }
            set { this._Sedes = value; }
        }
        public string ColegioMedico
        {
            get { return this._ColegioMedico; }
            set { this._ColegioMedico = value; }
        }
        public decimal CostoConsulta
        {
            get { return Math.Round(_costoConsulta, 2); }
            set { _costoConsulta = value; }
        }
        public double dCostoConsulta
        {
            get { return (double)Math.Round(_costoConsulta, 2); }
            set
            {
                _costoConsulta = Convert.ToDecimal(value);
            }
        }
        public string CostoConsultaForTextbox
        {
            get { return Math.Round(_costoConsulta, 2).ToString().Replace(".", ","); }
        }
        public decimal PorcentageDescuento
        {
            get { return Math.Round(_porcentageDescuento, 2); }
            set { _porcentageDescuento = value; }
        }
        public double dPorcentageDescuento
        {
            get { return (double)Math.Round(_porcentageDescuento, 2); }
            set { _porcentageDescuento = Convert.ToDecimal(value); }
        }
        public decimal CostoConsultaConDescuento
        {
            get
            {
                decimal _costoConsultaConDescuento = _costoConsulta - (_costoConsulta * (_porcentageDescuento / 100));
                return Math.Round(_costoConsultaConDescuento, 2);
            }
            set { _costoConsulta = value; }
        }

        #endregion

        #region Especialidad
        public int EspecialidadID
        {
            get { return this._EspecialidadId; }
            set { this._EspecialidadId = value; }
        }
        public string NombreEspecialidad
        {
            get { return this._NombreEspecialidad; }
            set { this._NombreEspecialidad = value; }
        }
        #endregion

        #region Ciudad
        public string CiudadID
        {
            get { return this._CiudadID; }
        }
        public string DireccionPCiudad
        {
            get { return this._DireccionPCiudad; }
        }
        public string TelefonoPCiudad
        {
            get { return this._TelefonoPCiudad; }
        }
        public string CelularPCiudad
        {
            get { return this._CelularPCiudad; }
        }
        #endregion


        #region Proveedor
        public int ProveedorId
        {
            get { return this._ProveedorId; }
            set { this._ProveedorId = value; }
        }
        public string Nombres
        {
            get { return this._Nombres; }
            set { this._Nombres = value; }
        }
        public string Apellidos
        {
            get { return this._Apellidos; }
            set { this._Apellidos = value; }
        }
        public string NombreJuridico
        {
            get { return this._NombreJuridico; }
            set { this._NombreJuridico = value; }
        }
        public string Nit
        {
            get { return this._Nit; }
            set { this._Nit = value.ToString(); }
        }

        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value; }
        }
        public string TelefonoCasa
        {
            get { return this._TelefonoCasa; }
            set { this._TelefonoCasa = value; }
        }
        public string TelefonoOficina
        {
            get { return this._TelefonoOficina; }
            set { this._TelefonoOficina = value; }
        }
        public string Celular
        {
            get { return this._Celular; }
            set { this._Celular = value; }
        }
        public string Estado
        {
            get { return this._Estado; }
            set { this._Estado = value; }
        }

        public string Observaciones
        {
            get { return this._Observaciones; }
            set { this._Observaciones = value; }
        }
        public DateTime FechaActualizacion
        {
            get { return this._FechaActualizacion; }
            set { this._FechaActualizacion = value; }
        }
        public string TipoProveedorId
        {
            get { return this._TipoProveedorId; }
            set { this._TipoProveedorId = value; }
        }
        public string NombreTipoProveedor
        {
            get { return this._TipoProveedorNombre; }
            set { this._TipoProveedorNombre = value; }
        }
        #endregion

        public int RowNumber { get; set; }

        public string NombreCompletoOrJuridico
        {
            get
            {
                if (_TipoProveedorId != null && _TipoProveedorId.Equals("MEDICO"))
                    return TextUtilities.MakeForDisplay(ProveedorId, this._Nombres, _Apellidos);
                else
                    return TextUtilities.MakeForDisplay(ProveedorId, this._NombreJuridico);
            }
        }
        public string EspecialidadAndNombreCompleto
        {
            get { return this._NombreEspecialidad + " - " + this._Nombres + " " + this._Apellidos; }
        }

        public Proveedor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Proveedor(IOrderedDictionary Values)
        {
            this._ProveedorId = int.Parse(Values["ProveedorId"].ToString());
            this._RedMedicaId = int.Parse(Values["RedMedicaId"].ToString());
            this._Nombres = Values["Nombres"].ToString();
            this._Apellidos = Values["Apellidos"].ToString();
            this._NombreJuridico = Values["NombreJuridico"].ToString();
            this._Nit = Values["Nit"].ToString();

            this._Direccion = Values["Direccion"].ToString();
            this._TelefonoCasa = Values["TelefonoCasa"].ToString();
            this._TelefonoOficina = Values["TelefonoOficina"].ToString();
            this._Celular = Values["Celular"].ToString();
            this._Estado = Values["Estado"].ToString();

            this._Observaciones = Values["Observaciones"].ToString();
            this._TipoProveedorId = Values["TipoProveedorId"].ToString();

            this._EspecialistaId = int.Parse(Values["EspecialistaId"].ToString());
            this._EspecialidadId = int.Parse(Values["EspecialidadId"].ToString());
            this._Sedes = Values["Sedes"].ToString();
            this._ColegioMedico = Values["ColegioMedico"].ToString();
            if (Values["dCostoConsulta"] != null)
            {
                this._costoConsulta = decimal.Parse(Values["dCostoConsulta"].ToString().Replace(".", ","));
                this._porcentageDescuento = decimal.Parse(Values["dPorcentageDescuento"].ToString().Replace(".", ","));
            }
            //edwin suyo
            if (Values["userId"].ToString().Length > 0)
                this._UserId = int.Parse(Values["userId"].ToString());
            else
                this._UserId = 0;

            if (Values["MedicoId"].ToString().Length > 0)
                this._MedicoId = int.Parse(Values["MedicoId"].ToString());
            else
                this._MedicoId = 0;

            //this._CiudadID = Values["CiudadID"].ToString();
            //this._DireccionPCiudad = Values["DireccionPCiudad"].ToString();
            //this._TelefonoPCiudad = Values["TelefonoPCiudad"].ToString();
            //this._CelularPCiudad = Values["CelularPCiudad"].ToString();
        }

        public Proveedor(int ProveedorId, int RedMedicaId, string RedMedicaNombre, string Nombres, string Apellidos, string NombreJuridico
            , string Nit, string Direcccion, string TelefonoCasa, string TelefonoOficina
            , string Celular, string Estado, string Observaciones, DateTime FechaActualizacion
            , string TipoProveedorId, string NombreTipoProveedor, int EspecialistaId, int EspecialidadId
            , string NombreEspecialidad, string Sedes, string ColegioMedico, decimal costoConsulta, decimal porcentageDescuento
            , string CiudadID, string DireccionPCiudad, string TelefonoPCiudad, string CelularPCiudad)
        {
            this._RedMedicaId = RedMedicaId;
            this._RedMedicaNombre = RedMedicaNombre;
            this._ProveedorId = ProveedorId;
            this._Nombres = Nombres;
            this._Apellidos = Apellidos;
            this._NombreJuridico = NombreJuridico;
            this._Nit = Nit;

            this._Direccion = Direcccion;
            this._TelefonoCasa = TelefonoCasa;
            this._TelefonoOficina = TelefonoOficina;
            this._Celular = Celular;
            this._Estado = Estado;

            this._Observaciones = Observaciones;
            this._FechaActualizacion = FechaActualizacion;
            this._TipoProveedorId = TipoProveedorId;
            this._TipoProveedorNombre = NombreTipoProveedor;

            this._EspecialistaId = EspecialistaId;
            this._EspecialidadId = EspecialidadId;
            this._NombreEspecialidad = NombreEspecialidad;
            this._Sedes = Sedes;
            this._ColegioMedico = ColegioMedico;
            this._costoConsulta = costoConsulta;
            this._porcentageDescuento = porcentageDescuento;
            this._CiudadID = CiudadID;
            this._DireccionPCiudad = DireccionPCiudad;
            this._TelefonoPCiudad = TelefonoPCiudad;
            this._CelularPCiudad = CelularPCiudad;
        }

        //Proveedor Basic
        public Proveedor(int ProveedorId, string Nombres, string Apellidos, string NombreJuridico
            , string Nit, string Direcccion, string TelefonoCasa, string TelefonoOficina
            , string Celular, string Estado, string Observaciones, DateTime FechaActualizacion
            , string TipoProveedorId)
        {
            this._ProveedorId = ProveedorId;
            this._Nombres = Nombres;
            this._Apellidos = Apellidos;
            this._NombreJuridico = NombreJuridico;
            this._Nit = Nit;

            this._Direccion = Direcccion;
            this._TelefonoCasa = TelefonoCasa;
            this._TelefonoOficina = TelefonoOficina;
            this._Celular = Celular;
            this._Estado = Estado;

            this._Observaciones = Observaciones;
            this._FechaActualizacion = FechaActualizacion;
            this._TipoProveedorId = TipoProveedorId;
        }


        //para Mostrar la Lista de Los Proveedordes con Usuarios

        public Proveedor(int ProveedorId, int RedMedicaId, string RedMedicaNombre, string Nombres, string Apellidos, string NombreJuridico
       , string Nit, string Direcccion, string TelefonoCasa, string TelefonoOficina
       , string Celular, string Estado, string Observaciones, DateTime FechaActualizacion
       , string TipoProveedorId, string NombreTipoProveedor, int EspecialistaId, int EspecialidadId,int userId,int MedicoId,string NombreUsuario
       , string NombreEspecialidad, string Sedes, string ColegioMedico, decimal costoConsulta, decimal porcentageDescuento
       , string CiudadID, string DireccionPCiudad, string TelefonoPCiudad, string CelularPCiudad)
        {
            this._RedMedicaId = RedMedicaId;
            this._RedMedicaNombre = RedMedicaNombre;
            this._ProveedorId = ProveedorId;
            this._Nombres = Nombres;
            this._Apellidos = Apellidos;
            this._NombreJuridico = NombreJuridico;
            this._Nit = Nit;

            this._Direccion = Direcccion;
            this._TelefonoCasa = TelefonoCasa;
            this._TelefonoOficina = TelefonoOficina;
            this._Celular = Celular;
            this._Estado = Estado;

            this._Observaciones = Observaciones;
            this._FechaActualizacion = FechaActualizacion;
            this._TipoProveedorId = TipoProveedorId;
            this._TipoProveedorNombre = NombreTipoProveedor;

            this._EspecialistaId = EspecialistaId;
            this._EspecialidadId = EspecialidadId;
            this._UserId = userId;
            this._MedicoId = MedicoId;
            this._NombreUsuario = NombreUsuario;
            this._NombreEspecialidad = NombreEspecialidad;
            this._Sedes = Sedes;
            this._ColegioMedico = ColegioMedico;
            this._costoConsulta = costoConsulta;
            this._porcentageDescuento = porcentageDescuento;
            this._CiudadID = CiudadID;
            this._DireccionPCiudad = DireccionPCiudad;
            this._TelefonoPCiudad = TelefonoPCiudad;
            this._CelularPCiudad = CelularPCiudad;
        }


        public Proveedor(int ProveedorId, int MedicoId,int userId, string NombreMedico, string NombreEspecialidad, decimal CostoConsulta
            , decimal PorcentageDescuento, string CiudadId)
        {
            this._ProveedorId = ProveedorId;
            this._MedicoId = MedicoId;
            this._UserId = userId;
            this._Nombres = NombreMedico;
            this._NombreEspecialidad = NombreEspecialidad;
            this._costoConsulta = CostoConsulta;
            this._porcentageDescuento = PorcentageDescuento;
            this._CiudadID = CiudadId;
        }
    }
}