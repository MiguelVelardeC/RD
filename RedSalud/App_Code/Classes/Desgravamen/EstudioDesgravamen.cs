using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Artexacta.App.Desgravamen
{
    /// <summary>
    /// Summary description for EstudioDesgravamen
    /// </summary>
    public class EstudioDesgravamen
    {
        public int EstudioId { get; set; }
        public string EstudioNombre { get; set; }
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }

        public EstudioDesgravamen()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}