using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Artexacta.App.Utilities.Text;
/// <summary>
/// Descripción breve de CitaEstudio
/// </summary>
/// 
namespace Artexacta.App.CitaEstudio
{
    public class CitaEstudio
    {
        private int _citaDesgravamenId;
        private int _estudioId;
        private int _proveedormedicoid;
        private string _nom_estudio;
        private DateTime _fechaRealizado;
        private string _nom_proveedor;
        private string _realizado;
        private string _necesitoCita;

        public CitaEstudio()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public int citaDesgravamenId
        {
            get { return this._citaDesgravamenId; }
            set { this._citaDesgravamenId = value; }
        }

        public int estudioId
        {
            get { return this._estudioId; }
            set { this._estudioId = value; }
        }

        public int proveedormedicoid
        {
            get { return this._proveedormedicoid; }
            set { this._proveedormedicoid = value; }
        }


        public string nom_estudio
        {
            get { return this._nom_estudio; }
            set { this._nom_estudio = value; }
        }

        public DateTime fechaRealizado
        {
            get { return this._fechaRealizado; }
            set { this._fechaRealizado = value; }

        }

        public string nom_proveedor
        {
            get { return this._nom_proveedor; }
            set { this._nom_proveedor = value; }

        }

        public string realizado
        {
            get { return this._realizado; }
            set { this._realizado = value; }

        }

        public string  necesitoCita
        {
            get { return this._necesitoCita; }
            set { this._necesitoCita = value; }

        }

       

        // Se utiliza para listar las notificaciones
        public CitaEstudio(int citaDesgravamenId, int estudioId, int proveedormedicoid, string nom_estudio, DateTime fechaRealizado,
            string nom_proveedor, string realizado, string necesitoCita)
        {
            this._citaDesgravamenId = citaDesgravamenId;
            this._estudioId = estudioId;
            this._proveedormedicoid = proveedormedicoid;
            this._nom_estudio = nom_estudio;
            this._fechaRealizado = fechaRealizado;
            this._nom_proveedor = nom_proveedor;
            this._realizado = realizado;
            this._necesitoCita = necesitoCita;
        }

    }

    public class CitaEstudioC
    {
        private int _citaDesgravamenId;
        

        private string _estado;
        private DateTime _fechaCreacion;
        private DateTime _fechaHoraCita;
        private string _aprobado;
        private string _observacionLabo;
        private string _nom;



        public CitaEstudioC()
        {
            //
            // TODO: Agregar aquí la lógica del constructor
            //
        }

        public int citaDesgravamenId
        {
            get { return this._citaDesgravamenId; }
            set { this._citaDesgravamenId = value; }
        }

        public string estado
        {
            get { return this._estado; }
            set { this._estado = value; }

        }

        public DateTime fechaCreacion
        {
            get { return this._fechaCreacion; }
            set { this._fechaCreacion = value; }

        }

        public DateTime fechaHoraCita
        {
            get { return this._fechaHoraCita; }
            set { this._fechaHoraCita = value; }

        }
        public string aprobado
        {
            get { return this._aprobado; }
            set { this._aprobado = value; }

        }
        public string observacionLabo
        {
            get { return this._observacionLabo; }
            set { this._observacionLabo = value; }

        }

        public string nom
        {
            get { return this._nom; }
            set { this._nom = value; }
        }

        // Se utiliza para listar las notificaciones
       
        public CitaEstudioC(int citaDesgravamenId, string estado, DateTime fechaCreacion, DateTime fechaHoraCita,
            string aprobado, string observacionLabo, string nom)
        {
            this._citaDesgravamenId = citaDesgravamenId;
            this._estado = estado;
            this._fechaCreacion = fechaCreacion;
            this._fechaHoraCita = fechaHoraCita;
            this._aprobado = aprobado;
            this._observacionLabo = observacionLabo;
            this._nom = nom;
        }


    }
}