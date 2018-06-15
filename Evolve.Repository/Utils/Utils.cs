using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Repository.Utils
{
    internal class Utils
    {
        private object VerificaDbNull(TipoObjeto tipoObjeto, object campo)
        {
            if (campo.Equals(DBNull.Value))
            {
                switch (tipoObjeto)
                {
                    case TipoObjeto.Texto:
                        return string.Empty;
                    case TipoObjeto.Numerico:
                        return 0;
                    case TipoObjeto.Data:
                        return System.DateTime.MinValue;
                    case TipoObjeto.Booleano:
                        return false;
                    default:
                        throw new ArgumentException("O tipo de dado não foi identificado");
                }
            }
            else
            {
                return campo;
            }
        }
    }
}
