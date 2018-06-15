using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Helpers.Formatters
{
    public class Formatters
    {
        public static string ComprimirTexto(string valor, int maximo)
        {
            if (valor.Length > maximo)
            {
                return valor.Trim().Substring(0, maximo) + "...";
            }

            else return valor;
        }
    }
}
