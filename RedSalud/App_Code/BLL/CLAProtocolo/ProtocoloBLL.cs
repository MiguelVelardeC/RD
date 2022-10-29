using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Protocolo.BLL
{
    /// <summary>
    /// Summary description for ProtocoloBLL
    /// </summary>
    public class ProtocoloBLL
    {
        public ProtocoloBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<Protocolo> GetProtocoloList()
        {
            ProtocoloDSTableAdapters.ProtocoloTableAdapter theAdapter = new ProtocoloDSTableAdapters.ProtocoloTableAdapter();
            ProtocoloDS.ProtocoloDataTable theTable = theAdapter.GetAllProtocolo();

            List<Protocolo> theList = new List<Protocolo>();
            foreach (ProtocoloDS.ProtocoloRow row in theTable)
            {
                Protocolo obj = GetProtocoloFromRow(row);
                theList.Add(obj);
            }
            return theList;
        }

        public static Protocolo GetProtocoloById(int protocoloId)
        {
            ProtocoloDSTableAdapters.ProtocoloTableAdapter theAdapter = new ProtocoloDSTableAdapters.ProtocoloTableAdapter();
            ProtocoloDS.ProtocoloDataTable theTable = theAdapter.GetProtocoloById(protocoloId);

            if(theTable == null || theTable.Rows.Count != 1)
            {
                throw new Exception("Ocurrio un error al obtener el registro de Protocolo con ID: " + protocoloId);
            }
            Protocolo obj = GetProtocoloFromRow(theTable[0]);
            return obj;
        }

        public static int InsertProtocolo(Protocolo obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("El objeto protocolo no puede ser nulo");
            }

            if (string.IsNullOrEmpty(obj.NombreEnfermedad))
            {
                throw new ArgumentException("NombreEnfermedad no puede ser nulo o vacio");
            }

            if (obj.TipoEnfermedadId <= 0)
            {
                throw new ArgumentException("TipoEnfermedadId no puede ser menor o igual que cero");
            }

            if (string.IsNullOrEmpty(obj.TextoProtocolo))
            {
                throw new ArgumentException("TextoProtocolo no puede ser nulo o vacio");
            }

            int? protocoloId = 0;
            ProtocoloDSTableAdapters.ProtocoloTableAdapter theAdapter = new ProtocoloDSTableAdapters.ProtocoloTableAdapter();
            theAdapter.InsertProtocolo(obj.NombreEnfermedad, obj.TipoEnfermedadId, obj.TextoProtocolo, ref protocoloId);

            if (protocoloId == null || protocoloId.Value == 0)
            {
                throw new Exception("Ocurrio un error al generar la llave primaria de la tabla");
            }

            return protocoloId.Value;
        }

        public static void UpdateProtocolo(Protocolo obj)
        {
            if (obj == null)
            {
                throw new ArgumentException("El objeto protocolo no puede ser nulo");
            }

            if (string.IsNullOrEmpty(obj.NombreEnfermedad))
            {
                throw new ArgumentException("NombreEnfermedad no puede ser nulo o vacio");
            }

            if (obj.TipoEnfermedadId <= 0)
            {
                throw new ArgumentException("TipoEnfermedadId no puede ser menor o igual que cero");
            }

            if (string.IsNullOrEmpty(obj.TextoProtocolo))
            {
                throw new ArgumentException("TextoProtocolo no puede ser nulo o vacio");
            }
            ProtocoloDSTableAdapters.ProtocoloTableAdapter theAdapter = new ProtocoloDSTableAdapters.ProtocoloTableAdapter();
            theAdapter.UpdateProtocolo(obj.NombreEnfermedad, obj.TipoEnfermedadId, obj.TextoProtocolo, obj.ProtocoloId);
        }

        public static void DeleteProtocolo(int protocoloId)
        {
            if (protocoloId <= 0)
            {
                throw new ArgumentException("ProtocoloId no puede ser menor o igual que cero");
            }
            ProtocoloDSTableAdapters.ProtocoloTableAdapter theAdapter = new ProtocoloDSTableAdapters.ProtocoloTableAdapter();
            theAdapter.DeleteProtocolo(protocoloId);
        }

        private static Protocolo GetProtocoloFromRow(ProtocoloDS.ProtocoloRow row)
        {
            Protocolo obj = new Protocolo(row.ProtocoloId, row.NombreEnfermedad, row.TipoEnfermedadId, row.TextoProtocolo);
            return obj;
        }
    }
}