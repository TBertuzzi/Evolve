using System;
using System.Collections.Generic;
using System.Text;

namespace Evolve.Helpers.Security
{
    public class TokenConfigurations
    {
        #region Propriedades Publicas

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }

        #endregion
    }
}
