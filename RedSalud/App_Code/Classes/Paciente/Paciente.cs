using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;
using System.Data.SqlTypes;

namespace Artexacta.App.Paciente
{
    /// <summary>
    /// Summary description for Paciente
    /// </summary>
    public class Paciente
    {
        private string _StaticEmptyAntecedentes = "";
        private string _StaticEmptyAntecedentesAlergicos = "";

        private int _AseguradoId;
        private string _codigoAsegurado;
        private int _PacienteId;
        private string _Nombre;
        //private string _Apellido;
        private DateTime _FechaNacimiento;
        private string _CarnetIdentidad;//
        private string _Direccion;
        private string _Telefono;
        private string _LugarTrabajo;//
        private string _TelefonoTrabajo;//
        private string _EstadoCivil;
        private int _NroHijos;
        private string _Antecedentes;//
        private string _AntecedentesAlergicos;//
        private string _AntecedentesGinecoobstetricos;//
        private string _Email;
        private bool _Genero;
        private int _FotoId;
        private string _Celular;
        private string _UsuarioMovil;
        private bool _UsuarioVerificado;


        public string Celular
        {
            get { return this._Celular; }
            set { this._Celular = value; }
        }
        public string UsuarioMovil
        {
            get { return this._UsuarioMovil; }
            set { this._UsuarioMovil = value; }
        }
        public bool UsuarioVerificado
        {
            get { return this._UsuarioVerificado; }
            set { this._UsuarioVerificado = value; }
        }
        public int AseguradoId
        {
            get { return this._AseguradoId; }
            set { this._AseguradoId = value; }
        }
        public string CodigoAsegurado
        {
            get { return this._codigoAsegurado; }
            set { this._codigoAsegurado = value; }
        }
        public int PacienteId
        {
            get { return this._PacienteId; }
            set { this._PacienteId = value; }
        }
        public string Nombre
        {
            get { return this._Nombre; }
            set { this._Nombre = value; }
        }
        /*public string Apellido
        {
            get { return this._Apellido; }
            set { this._Apellido = value; }
        }*/
        public DateTime FechaNacimiento
        {
            get { return this._FechaNacimiento; }
            set { this._FechaNacimiento = value; }
        }
        public string FechaNacimientoString {
            get {
                if (_FechaNacimiento == SqlDateTime.MinValue.Value)
                {
                    return "";
                }
                else
                {
                    return _FechaNacimiento.ToShortDateString();
                }
            }
        }
        public string Edad
        {
            get
            {
                if (_FechaNacimiento == SqlDateTime.MinValue.Value)
                {
                    return "";
                }
                else
                {
                    DateTime today = DateTime.Today;
                    int age = today.Year - _FechaNacimiento.Year;
                    if (_FechaNacimiento > today.AddYears(-age)) age--;
                    return age.ToString("0 AÑOS");
                }
            }
        }
        public string CarnetIdentidad
        {
            get { return this._CarnetIdentidad; }
            set { this._CarnetIdentidad = value; }
        }
        public string Direccion
        {
            get { return this._Direccion; }
            set { this._Direccion = value; }
        }
        public string Telefono
        {
            get { return this._Telefono; }
            set { this._Telefono = value; }
        }
        public string LugarTrabajo
        {
            get { return this._LugarTrabajo; }
            set { this._LugarTrabajo = value; }
        }
        public string TelefonoTrabajo
        {
            get { return this._TelefonoTrabajo; }
            set { this._TelefonoTrabajo = value; }
        }
        public string EstadoCivil
        {
            get {
                string estadoCivil = this._EstadoCivil;
                if (this._Genero && this._EstadoCivil.EndsWith("A"))
                {
                    estadoCivil = this._EstadoCivil.Remove(this._EstadoCivil.Length - 1) + "O";
                }else if (!this._Genero && this._EstadoCivil.EndsWith("O")){
                    estadoCivil = this._EstadoCivil.Remove(this._EstadoCivil.Length - 1) + "A";
                }
                return estadoCivil; 
            }
            set { this._EstadoCivil = value; }
        }
        public int NroHijos
        {
            get { return this._NroHijos; }
            set { this._NroHijos = value; }
        }

        public string Antecedentes
        {
            get{ return this._Antecedentes; }
            set { this._Antecedentes = value; }
        }
        public string AntecedentesAutoComplete
        {
            get
            {
                if (string.IsNullOrEmpty(_Antecedentes))
                    return _StaticEmptyAntecedentes;
                else
                    return this._Antecedentes;
            }
            set { this._Antecedentes = value; }
        }
        public string AntecedentesAlergicos
        {
            get{return this._AntecedentesAlergicos;}
            set { this._AntecedentesAlergicos = value; }
        }
        public string AntecedentesAlergicosAutoComplete
        {
            get
            {
                if (string.IsNullOrEmpty(_AntecedentesAlergicos))
                    return _StaticEmptyAntecedentesAlergicos;
                else
                    return this._AntecedentesAlergicos;
            }
            set { this._AntecedentesAlergicos = value; }
        }
        public string AntecedentesGinecoobstetricos
        {
            get { return this._AntecedentesGinecoobstetricos; }
            set { this._AntecedentesGinecoobstetricos = value; }
        }
        public string Email
        {
            get { return this._Email; }
            set { this._Email = value; }
        }
        public bool Genero
        {
            get { return this._Genero; }
            set { this._Genero = value; }
        }
        public string GeneroForDisplay
        {
            get { return this._Genero ? "MASCULINO" : "FEMENINO"; }
        }
        public int FotoId
        {
            get { return this._FotoId; }
            set { this._FotoId = value; }
        }


        /*public string NombreCompleto {
            get { return _Nombre + " " + _Apellido; }
        }*/
        public string NombreForDisplay
        {
            get { return TextUtilities.MakeForDisplay(_PacienteId, _Nombre, ""); }
        }
        public string FechaNacimientoShort {
            get { return _FechaNacimiento.ToShortDateString(); }
        }

        //static
        public string StaticEmptyAntecedentes
        {
            get { return this._StaticEmptyAntecedentes; }
        }
        public string StaticEmptyAntecedentesAlergicos {
            get { return this._StaticEmptyAntecedentesAlergicos; }
        }

        public Paciente()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Paciente(int PacienteId, string Nombre//, string Apellido
            , DateTime FechaNacimiento, string CarnetIdentidad, string Direccion, string Telefono
            , string LugarTrabajo, string TelefonoTrabajo, string EstadoCivil, int NroHijos
            , string Antecedentes ,string AntecedentesAlergeticos
            , string AntecedentesGinecoobstetricos, string Email, bool Genero, string Celular, string UsuarioMovil, bool UsuarioVerificado)
        {
            this._PacienteId = PacienteId;
            this._Nombre = Nombre;
            //this._Apellido = Apellido;
            this._FechaNacimiento = FechaNacimiento;
            this._CarnetIdentidad = CarnetIdentidad;
            this._Direccion = Direccion;
            this._Telefono = Telefono;
            this._LugarTrabajo = LugarTrabajo;
            this._TelefonoTrabajo = TelefonoTrabajo;
            this._EstadoCivil = EstadoCivil;
            this._NroHijos = NroHijos;

            this._Antecedentes = Antecedentes;
            this._AntecedentesAlergicos = AntecedentesAlergeticos;
            this._AntecedentesGinecoobstetricos = AntecedentesGinecoobstetricos;
            this._Email = Email;
            this._Genero = Genero;

            this._Celular = Celular;
            this._UsuarioMovil = UsuarioMovil;
            this._UsuarioVerificado = UsuarioVerificado;
        }
    }
}