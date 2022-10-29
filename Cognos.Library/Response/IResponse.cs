using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cognos.Library.Response
{
    public interface IResponse//<T>
    {
        #region public props
        TipoMensaje TipoMensaje { get; }
        string Mensaje { get; }
        //object Result { get; }
        string Origen { get; }
        //Exception Excepcion { get; set; }
        #endregion
    }
}
