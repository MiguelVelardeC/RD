using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for TipoProductoDesgravamenBLL
    /// </summary>
    public class TipoProductoDesgravamenBLL
    {
        public TipoProductoDesgravamenBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<TipoProductoDesgravamen> GetTipoProductosByCliente(int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new ArgumentException("Cliente cannot be equals or less than zero");
            }

            TipoProductoDesgravamenDSTableAdapters.TipoProductoDesgravamenTableAdapter adapter = new TipoProductoDesgravamenDSTableAdapters.TipoProductoDesgravamenTableAdapter();
            //CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter = new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            TipoProductoDesgravamenDS.TipoProductoDesgravamenDataTable table = adapter.GetTipoProductoByCliente(clienteId);
            List<TipoProductoDesgravamen> list = new List<TipoProductoDesgravamen>();

            if (table == null || table.Rows.Count <= 0)
            {
                throw new KeyNotFoundException("There are no Products Defined for this Client");
            }

            foreach (TipoProductoDesgravamenDS.TipoProductoDesgravamenRow row in table)
            {
                TipoProductoDesgravamen tipo = new TipoProductoDesgravamen()
                {
                    Codigo = row.Codigo,
                    ClienteId = row.clienteId,
                    Descripcion = row.Descripcion
                };

                list.Add(tipo);
            }



            return list;
        }

        public static TipoProductoDesgravamen GetTipoProductoByCodigo(string codigo, int clienteId)
        {
            if (clienteId <= 0)
            {
                throw new ArgumentException("Cliente cannot be equals or less than zero");
            }

            if (string.IsNullOrEmpty(codigo))
            {
                throw new ArgumentException("Codigo cannot be equals or less than zero");
            }

            TipoProductoDesgravamenDSTableAdapters.TipoProductoDesgravamenTableAdapter adapter = new TipoProductoDesgravamenDSTableAdapters.TipoProductoDesgravamenTableAdapter();
            //CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter = new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            TipoProductoDesgravamenDS.TipoProductoDesgravamenDataTable table = adapter.GetTipoProductoByCodigo(codigo, clienteId);
            List<TipoProductoDesgravamen> list = new List<TipoProductoDesgravamen>();

            if (table == null || table.Rows.Count <= 0)
            {
                throw new KeyNotFoundException("There are no Products Defined for this Client");
            }
            TipoProductoDesgravamenDS.TipoProductoDesgravamenRow row  = table[0];
                TipoProductoDesgravamen tipo = new TipoProductoDesgravamen()
                {
                    Codigo = row.Codigo,
                    ClienteId = row.clienteId,
                    Descripcion = row.Descripcion
                };
            return tipo;
        }

        public static List<ComboContainer> GetTipoProductosCombo()
        {
            TipoProductoDesgravamenDSTableAdapters.TipoProductoAllComboTableAdapter adapter = new TipoProductoDesgravamenDSTableAdapters.TipoProductoAllComboTableAdapter();
            //CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter adapter = new CitaDesgravamenDSTableAdapters.CitaDesgravamenTableAdapter();
            TipoProductoDesgravamenDS.TipoProductoAllComboDataTable table = adapter.GetTipoProductoCombo();
            List<ComboContainer> list = new List<ComboContainer>();

            if (table == null || table.Rows.Count <= 0)
            {
                throw new KeyNotFoundException("There are no Products Defined");
            }

            foreach (TipoProductoDesgravamenDS.TipoProductoAllComboRow row in table)
            {
                ComboContainer tipo = new ComboContainer()
                {
                    ContainerId = row.tipoProducto,
                    ContainerName = row.tipoProducto
                };

                list.Add(tipo);
            }



            return list;
        }
    }
}