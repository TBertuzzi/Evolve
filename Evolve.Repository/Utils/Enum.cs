using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Repository.Utils
{
    public enum TipoDataBase
    {
        SQLServer = 0,
        Oracle = 1,
        LiteDB = 3
    }

    public enum TipoObjeto
    {
        Texto,
        Numerico,
        Booleano,
        Data
    }
}
