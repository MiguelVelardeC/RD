using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Caso.CasoForAprobation
{
    /// <summary>
    /// Summary description for CasoForAprobation
    /// </summary>
    public class CasoForAprobation
    {
        private int _CasoId;
        private int _Id;
        private string _TipoEstudio;
        private string _NombreProveedor;
        private string _Observaciones;
        private DateTime _FechaCreacion;
        private string _Table;
        private int _fileCount;

        public int CasoId
        {
            get { return this._CasoId; }
            set { this._CasoId = value; }
        }
        public int Id
        {
            get { return this._Id; }
            set { this._Id = value; }
        }

        public string TipoEstudio
        {
            get { return this._TipoEstudio; }
        }

        public string NombreProveedor
        {
            get { return this._NombreProveedor; }
        }
        public string Observaciones
        {
            get { return this._Observaciones; }
        }
        public DateTime FechaCreacion
        {
            get { return _FechaCreacion; }
        }
        public string Table
        {
            get { return this._Table; }
            set { this._Table = value; }
        }
        public int FileCount
        {
            set { _fileCount = value; }
            get { return _fileCount; }
        }
        public string FileCountForDisplay
        {
            get { return _fileCount.ToString("00"); }
        }

        public string NombreAnalisisRealizado {
            get
            {
                switch (_Table)
                {
                    case "tbl_Derivacion":
                        return "Derivación a Especialista";
                    case "tbl_Estudio":
                        return "Ex. Complementario";
                    case"tbl_Internacion":
                        return "Internación";
                    case "tbl_Cirugia":
                        return "Cirugía";
                    case "tbl_Emergencia":
                        return "Emergencia";
                    case "tbl_Odontologia":
                        return "PRESTACIÓN ODONTOLÓGICA";
                    default:
                        return "";
                }
            }
        }

        public string TipoFileManager
        {
            get
            {
                switch (_Table)
                {
                    case "tbl_Derivacion":
                        return "DERIVACIONES";
                    case "tbl_Estudio":
                        return "ESTUDIO";
                    case "tbl_Internacion":
                        return "INTERNACION";
                    case "tbl_Emergencia":
                        return "EMERGENCIA";
                    case "tbl_Odontologia":
                        return "ODONTOLOGIA";
                    default:
                        return "";
                }
            }
        }

        public CasoForAprobation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public CasoForAprobation(int CasoId, int Id, string TipoEstudio
                , string NombreProveedor, string Observaciones, DateTime FechaCreacion, string Table, int fileCount )
        {
            this._CasoId = CasoId;
            this._Id = Id;
            this._TipoEstudio = TipoEstudio;
            this._NombreProveedor = NombreProveedor;
            this._Observaciones = Observaciones;
            this._FechaCreacion = FechaCreacion;
            this._Table = Table;
            this._fileCount = fileCount;
        }
    }
}