using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Artexacta.App.Desgravamen.BLL
{
    /// <summary>
    /// Summary description for CiudadesDesgravamenBLL
    /// </summary>
    public class CiudadesDesgravamenBLL
    {
        public CiudadesDesgravamenBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<ComboContainer> GetCiudadesDesgravamenCombo()
        {
            CiudadesDesgravamenDSTableAdapters.CiudadesDesgravamenTableAdapter adapter =
                new CiudadesDesgravamenDSTableAdapters.CiudadesDesgravamenTableAdapter();
            CiudadesDesgravamenDS.CiudadesDesgravamenDataTable table = adapter.GetCiudadesDesgravamenByProveedorCombo();

            List<ComboContainer> list = new List<ComboContainer>();

            foreach (CiudadesDesgravamenDS.CiudadesDesgravamenRow row in table)
            {
                ComboContainer obj = new ComboContainer()
                {
                    ContainerId = row.ciudadId,
                    ContainerName = row.nombre
                };
                list.Add(obj);
            }
            return list;
        }
    }
}